using Microsoft.EntityFrameworkCore.Migrations;

namespace JuanJacoboSummerGames.Data
{
    public static class ExtraMigration
    {
        public static void Steps(MigrationBuilder migrationBuilder)
        {
            //Athlete trigger tables for concurrency
            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetAthleteTimestampOnUpdate
                AFTER UPDATE ON Athletes
                BEGIN
                    UPDATE Athletes
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");
            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetAthleteTimestampOnInsert
                AFTER INSERT ON Athletes
                BEGIN
                    UPDATE Athletes
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");
            //Sport trigger tables for concurrency
            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetSportTimestampOnUpdate
                AFTER UPDATE ON Sports
                BEGIN
                    UPDATE Sports
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");
            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetSportTimestampOnInsert
                AFTER INSERT ON Sports
                BEGIN
                    UPDATE Sports
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");
        }
    }
}
