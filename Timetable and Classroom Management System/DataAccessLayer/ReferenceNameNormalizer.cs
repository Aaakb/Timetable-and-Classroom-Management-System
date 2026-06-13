using System.Text;

namespace Timetable_and_Classroom_Management_System.DataAccessLayer
{
    public static class ReferenceNameNormalizer
    {
        public const string ComputerScience = "Computer Science";
        public const string CyberSecurity = "Cyber Security";
        public const string InformationTechnology = "Information Technology";
        public const string FirstYear = "First Year";
        public const string SecondYear = "Second Year";
        public const string ThirdYear = "Third Year";
        public const string FourthYear = "Fourth Year";

        private const string ArabicComputerScience = "\u0639\u0644\u0648\u0645 \u0627\u0644\u062d\u0627\u0633\u0648\u0628";
        private const string ArabicCyberSecurity = "\u0627\u0644\u0623\u0645\u0646 \u0627\u0644\u0633\u064a\u0628\u0631\u0627\u0646\u064a";
        private const string ArabicInformationTechnology = "\u062a\u0643\u0646\u0648\u0644\u0648\u062c\u064a\u0627 \u0627\u0644\u0645\u0639\u0644\u0648\u0645\u0627\u062a";
        private const string ArabicFirstYear = "\u0627\u0644\u0645\u0631\u062d\u0644\u0629 \u0627\u0644\u0623\u0648\u0644\u0649";
        private const string ArabicSecondYear = "\u0627\u0644\u0645\u0631\u062d\u0644\u0629 \u0627\u0644\u062b\u0627\u0646\u064a\u0629";
        private const string ArabicThirdYear = "\u0627\u0644\u0645\u0631\u062d\u0644\u0629 \u0627\u0644\u062b\u0627\u0644\u062b\u0629";
        private const string ArabicFourthYear = "\u0627\u0644\u0645\u0631\u062d\u0644\u0629 \u0627\u0644\u0631\u0627\u0628\u0639\u0629";

        public static string NormalizeBranchName(string branchName)
        {
            branchName = Clean(branchName);

            if (Matches(branchName, ComputerScience, "ComputerScience", ArabicComputerScience))
            {
                return ComputerScience;
            }

            if (Matches(branchName, CyberSecurity, "CyberSecurity", ArabicCyberSecurity))
            {
                return CyberSecurity;
            }

            if (Matches(branchName, InformationTechnology, "InformationTechnology", ArabicInformationTechnology))
            {
                return InformationTechnology;
            }

            return branchName;
        }

        public static string NormalizeStudyYearName(string yearName)
        {
            yearName = Clean(yearName);

            if (Matches(yearName, FirstYear, "First", "1", "1st Year", ArabicFirstYear))
            {
                return FirstYear;
            }

            if (Matches(yearName, SecondYear, "Second", "2", "2nd Year", ArabicSecondYear))
            {
                return SecondYear;
            }

            if (Matches(yearName, ThirdYear, "Third", "3", "3rd Year", ArabicThirdYear))
            {
                return ThirdYear;
            }

            if (Matches(yearName, FourthYear, "Fourth", "4", "4th Year", ArabicFourthYear))
            {
                return FourthYear;
            }

            return yearName;
        }

        private static bool Matches(string value, params string[] aliases)
        {
            string key = NormalizeKey(value);
            string decodedKey = NormalizeKey(TryDecodeMojibake(value));

            return aliases.Any(alias =>
            {
                string aliasKey = NormalizeKey(alias);
                return key == aliasKey || decodedKey == aliasKey;
            });
        }

        private static string TryDecodeMojibake(string value)
        {
            if (!value.Any(ch => ch is '\u00d8' or '\u00d9'))
            {
                return value;
            }

            try
            {
                byte[] bytes = Encoding.Latin1.GetBytes(value);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return value;
            }
        }

        private static string NormalizeKey(string value)
        {
            value = Clean(value)
                .ToLowerInvariant()
                .Replace('\u0623', '\u0627')
                .Replace('\u0625', '\u0627')
                .Replace('\u0622', '\u0627')
                .Replace('\u0649', '\u064a');

            return new string(value.Where(char.IsLetterOrDigit).ToArray());
        }

        private static string Clean(string value)
        {
            return string.Join(" ", value.Trim().Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
