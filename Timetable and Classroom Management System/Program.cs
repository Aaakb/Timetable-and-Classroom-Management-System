using Timetable_and_Classroom_Management_System.DataAccessLayer;
using Timetable_and_Classroom_Management_System.PresentationLayer.Forms;
namespace Timetable_and_Classroom_Management_System
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            try
            {
                DatabaseSchemaService.EnsureCompatibleSchema();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not verify the database schema." + Environment.NewLine + Environment.NewLine + ex.Message,
                    "Database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            Application.Run(new MainForm());
        }
    }
}
