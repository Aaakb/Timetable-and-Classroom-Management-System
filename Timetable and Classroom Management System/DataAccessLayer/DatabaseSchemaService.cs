using Microsoft.EntityFrameworkCore;

namespace Timetable_and_Classroom_Management_System.DataAccessLayer
{
    public static class DatabaseSchemaService
    {
        public static void EnsureCompatibleSchema()
        {
            using AppDbContext context = new AppDbContext();

            context.Database.ExecuteSqlRaw(@"
IF OBJECT_ID(N'dbo.Schedules', N'U') IS NOT NULL
BEGIN
    IF EXISTS (
        SELECT 1
        FROM sys.key_constraints
        WHERE parent_object_id = OBJECT_ID(N'dbo.Schedules')
            AND name = N'UQ_Year_Branch_Time'
    )
    BEGIN
        ALTER TABLE dbo.Schedules DROP CONSTRAINT UQ_Year_Branch_Time;
    END;
    ELSE IF EXISTS (
        SELECT 1
        FROM sys.indexes
        WHERE object_id = OBJECT_ID(N'dbo.Schedules')
            AND name = N'UQ_Year_Branch_Time'
    )
    BEGIN
        DROP INDEX UQ_Year_Branch_Time ON dbo.Schedules;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM sys.indexes
        WHERE object_id = OBJECT_ID(N'dbo.Schedules')
            AND name = N'UQ_Section_Time'
    )
    BEGIN
        CREATE UNIQUE INDEX UQ_Section_Time
        ON dbo.Schedules(SectionID, DayOfWeek, TimeSlotID)
        WHERE SectionID IS NOT NULL;
    END;
END;
");
        }
    }
}
