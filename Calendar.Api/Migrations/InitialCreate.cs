using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calendar.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GregorianDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JulianDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MayanLongCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tzolkin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Haab = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HebrewDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntervalCalculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntervalType = table.Column<int>(type: "int", nullable: false),
                    IntervalValue = table.Column<int>(type: "int", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntervalCalculations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LottoEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LottoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrawDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number1 = table.Column<int>(type: "int", nullable: false),
                    Number2 = table.Column<int>(type: "int", nullable: false),
                    Number3 = table.Column<int>(type: "int", nullable: false),
                    Number4 = table.Column<int>(type: "int", nullable: false),
                    Number5 = table.Column<int>(type: "int", nullable: false),
                    Number6 = table.Column<int>(type: "int", nullable: false),
                    Number7 = table.Column<int>(type: "int", nullable: false),
                    Powerball = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LottoEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntervalCalculationResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntervalCalculationId = table.Column<int>(type: "int", nullable: false),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    GregorianDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JulianDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MayanLongCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tzolkin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Haab = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HebrewDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntervalCalculationResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntervalCalculationResults_IntervalCalculations_IntervalCalculationId",
                        column: x => x.IntervalCalculationId,
                        principalTable: "IntervalCalculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntervalCalculationResults_IntervalCalculationId",
                table: "IntervalCalculationResults",
                column: "IntervalCalculationId");

            migrationBuilder.CreateTable(
                name: "LottoMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LottoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrawDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Matched = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LottoMatches", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "CalendarDates");
            migrationBuilder.DropTable(name: "IntervalCalculationResults");
            migrationBuilder.DropTable(name: "LottoEntries");
            migrationBuilder.DropTable(name: "LottoMatches");
            migrationBuilder.DropTable(name: "IntervalCalculations");
        }
    }
}
