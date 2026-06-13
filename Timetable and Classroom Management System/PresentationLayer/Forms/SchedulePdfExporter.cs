using System.Globalization;
using System.Text;

namespace Timetable_and_Classroom_Management_System.PresentationLayer.Forms
{
    internal static class SchedulePdfExporter
    {
        private static readonly string[] Headers =
        {
            "Day",
            "Time",
            "Subject",
            "Faculty",
            "Room",
            "Study Year",
            "Branch",
            "Section"
        };

        private static readonly float[] ColumnWidths =
        {
            70,
            88,
            150,
            118,
            58,
            90,
            110,
            66
        };

        public static void Export(string filePath, string title, IReadOnlyList<string[]> rows)
        {
            const float pageWidth = 842;
            const float pageHeight = 595;
            const float margin = 32;
            const float titleY = 560;
            const float tableTop = 524;
            const float headerHeight = 22;
            const float rowHeight = 20;
            const int rowsPerPage = 23;

            var pdf = new SimplePdfDocument(pageWidth, pageHeight);

            for (int pageStart = 0; pageStart < rows.Count; pageStart += rowsPerPage)
            {
                IReadOnlyList<string[]> pageRows = rows
                    .Skip(pageStart)
                    .Take(rowsPerPage)
                    .ToList();

                var content = new StringBuilder();
                AddText(content, margin, titleY, 15, title, bold: true);
                AddText(
                    content,
                    margin,
                    titleY - 18,
                    8,
                    $"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}    Page {(pageStart / rowsPerPage) + 1} of {Math.Ceiling(rows.Count / (double)rowsPerPage).ToString(CultureInfo.InvariantCulture)}",
                    bold: false);

                DrawTable(content, margin, tableTop, headerHeight, rowHeight, pageRows);
                pdf.AddPage(content.ToString());
            }

            pdf.Save(filePath);
        }

        private static void DrawTable(StringBuilder content, float x, float y, float headerHeight, float rowHeight, IReadOnlyList<string[]> rows)
        {
            float tableWidth = ColumnWidths.Sum();

            AddFill(content, 0.09f, 0.16f, 0.29f);
            AddRectangle(content, x, y - headerHeight, tableWidth, headerHeight, fill: true);
            AddFill(content, 1f, 1f, 1f);

            float currentX = x;
            for (int i = 0; i < Headers.Length; i++)
            {
                AddText(content, currentX + 4, y - 15, 8, Headers[i], bold: true, white: true);
                currentX += ColumnWidths[i];
            }

            float rowY = y - headerHeight;
            for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                rowY -= rowHeight;

                if (rowIndex % 2 == 0)
                {
                    AddFill(content, 0.96f, 0.98f, 1f);
                    AddRectangle(content, x, rowY, tableWidth, rowHeight, fill: true);
                }

                AddStroke(content, 0.86f, 0.89f, 0.94f);
                AddLine(content, x, rowY, x + tableWidth, rowY);

                currentX = x;
                string[] row = rows[rowIndex];

                for (int columnIndex = 0; columnIndex < Headers.Length; columnIndex++)
                {
                    string value = columnIndex < row.Length ? row[columnIndex] : string.Empty;
                    int maxCharacters = Math.Max(6, (int)(ColumnWidths[columnIndex] / 5.1f));
                    AddText(content, currentX + 4, rowY + 6, 7.5f, Truncate(value, maxCharacters), bold: false);
                    currentX += ColumnWidths[columnIndex];
                }
            }

            AddStroke(content, 0.09f, 0.16f, 0.29f);
            AddRectangle(content, x, y - headerHeight - (rows.Count * rowHeight), tableWidth, headerHeight + (rows.Count * rowHeight), fill: false);
        }

        private static void AddText(StringBuilder content, float x, float y, float size, string text, bool bold, bool white = false)
        {
            content
                .Append("BT ")
                .Append(white ? "1 1 1 rg " : "0.06 0.09 0.16 rg ")
                .Append(bold ? "/F2 " : "/F1 ")
                .Append(FormatNumber(size))
                .Append(" Tf ")
                .Append(FormatNumber(x))
                .Append(' ')
                .Append(FormatNumber(y))
                .Append(" Td (")
                .Append(EscapeText(text))
                .AppendLine(") Tj ET");
        }

        private static void AddFill(StringBuilder content, float red, float green, float blue)
        {
            content
                .Append(FormatNumber(red))
                .Append(' ')
                .Append(FormatNumber(green))
                .Append(' ')
                .Append(FormatNumber(blue))
                .AppendLine(" rg");
        }

        private static void AddStroke(StringBuilder content, float red, float green, float blue)
        {
            content
                .Append(FormatNumber(red))
                .Append(' ')
                .Append(FormatNumber(green))
                .Append(' ')
                .Append(FormatNumber(blue))
                .AppendLine(" RG");
        }

        private static void AddRectangle(StringBuilder content, float x, float y, float width, float height, bool fill)
        {
            content
                .Append(FormatNumber(x))
                .Append(' ')
                .Append(FormatNumber(y))
                .Append(' ')
                .Append(FormatNumber(width))
                .Append(' ')
                .Append(FormatNumber(height))
                .Append(fill ? " re f" : " re S")
                .AppendLine();
        }

        private static void AddLine(StringBuilder content, float x1, float y1, float x2, float y2)
        {
            content
                .Append(FormatNumber(x1))
                .Append(' ')
                .Append(FormatNumber(y1))
                .Append(" m ")
                .Append(FormatNumber(x2))
                .Append(' ')
                .Append(FormatNumber(y2))
                .AppendLine(" l S");
        }

        private static string Truncate(string value, int maxCharacters)
        {
            value = Sanitize(value);

            if (value.Length <= maxCharacters)
            {
                return value;
            }

            return maxCharacters <= 3 ? value[..maxCharacters] : value[..(maxCharacters - 3)] + "...";
        }

        private static string EscapeText(string value)
        {
            return Sanitize(value)
                .Replace("\\", "\\\\")
                .Replace("(", "\\(")
                .Replace(")", "\\)");
        }

        private static string Sanitize(string value)
        {
            return new string(value
                .Where(ch => ch is >= ' ' and <= '~')
                .ToArray());
        }

        private static string FormatNumber(float value)
        {
            return value.ToString("0.###", CultureInfo.InvariantCulture);
        }

        private sealed class SimplePdfDocument
        {
            private readonly float pageWidth;
            private readonly float pageHeight;
            private readonly List<string> pageContents = new List<string>();

            public SimplePdfDocument(float pageWidth, float pageHeight)
            {
                this.pageWidth = pageWidth;
                this.pageHeight = pageHeight;
            }

            public void AddPage(string content)
            {
                pageContents.Add(content);
            }

            public void Save(string filePath)
            {
                var objects = new List<string?>
                {
                    null,
                    "<< /Type /Catalog /Pages 2 0 R >>",
                    null,
                    "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>",
                    "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Bold >>"
                };

                var pageObjectIds = new List<int>();

                foreach (string pageContent in pageContents)
                {
                    int pageObjectId = objects.Count;
                    int contentObjectId = objects.Count + 1;
                    pageObjectIds.Add(pageObjectId);

                    objects.Add($"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 {FormatNumber(pageWidth)} {FormatNumber(pageHeight)}] /Resources << /Font << /F1 3 0 R /F2 4 0 R >> >> /Contents {contentObjectId} 0 R >>");
                    objects.Add($"<< /Length {Encoding.ASCII.GetByteCount(pageContent)} >>\nstream\n{pageContent}endstream");
                }

                objects[2] = $"<< /Type /Pages /Kids [{string.Join(" ", pageObjectIds.Select(id => $"{id} 0 R"))}] /Count {pageObjectIds.Count} >>";

                using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                WriteAscii(stream, "%PDF-1.4\n");

                var offsets = new List<long> { 0 };

                for (int i = 1; i < objects.Count; i++)
                {
                    offsets.Add(stream.Position);
                    WriteAscii(stream, $"{i} 0 obj\n{objects[i]}\nendobj\n");
                }

                long xrefOffset = stream.Position;
                WriteAscii(stream, $"xref\n0 {objects.Count}\n");
                WriteAscii(stream, "0000000000 65535 f \n");

                for (int i = 1; i < offsets.Count; i++)
                {
                    WriteAscii(stream, $"{offsets[i]:0000000000} 00000 n \n");
                }

                WriteAscii(stream, $"trailer\n<< /Size {objects.Count} /Root 1 0 R >>\nstartxref\n{xrefOffset}\n%%EOF");
            }

            private static void WriteAscii(Stream stream, string text)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(text);
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
