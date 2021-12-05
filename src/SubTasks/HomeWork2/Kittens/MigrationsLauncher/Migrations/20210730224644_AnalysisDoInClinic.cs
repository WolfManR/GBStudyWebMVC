using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationsLauncher.Migrations
{
    public partial class AnalysisDoInClinic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClinicId",
                table: "Analysis",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Analysis_ClinicId",
                table: "Analysis",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analysis_Clinics_ClinicId",
                table: "Analysis",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analysis_Clinics_ClinicId",
                table: "Analysis");

            migrationBuilder.DropIndex(
                name: "IX_Analysis_ClinicId",
                table: "Analysis");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "Analysis");
        }
    }
}
