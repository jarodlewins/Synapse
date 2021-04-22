using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Synapse.Migrations
{
    public partial class addInstructor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instructor",
                table: "Class_Event");

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InstructorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    middleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.InstructorID);
                });

            migrationBuilder.CreateTable(
                name: "Class_EventInstructor",
                columns: table => new
                {
                    Class_EventsID = table.Column<int>(type: "int", nullable: false),
                    InstructorsInstructorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class_EventInstructor", x => new { x.Class_EventsID, x.InstructorsInstructorID });
                    table.ForeignKey(
                        name: "FK_Class_EventInstructor_Class_Event_Class_EventsID",
                        column: x => x.Class_EventsID,
                        principalTable: "Class_Event",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Class_EventInstructor_Instructor_InstructorsInstructorID",
                        column: x => x.InstructorsInstructorID,
                        principalTable: "Instructor",
                        principalColumn: "InstructorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Class_EventInstructor_InstructorsInstructorID",
                table: "Class_EventInstructor",
                column: "InstructorsInstructorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Class_EventInstructor");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.AddColumn<string>(
                name: "Instructor",
                table: "Class_Event",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
