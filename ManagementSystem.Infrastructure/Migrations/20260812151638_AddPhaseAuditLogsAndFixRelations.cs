using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhaseAuditLogsAndFixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPhaseHistories_Phase_FromPhaseId",
                table: "ProjectPhaseHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPhaseHistories_Phase_ToPhaseId",
                table: "ProjectPhaseHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Phase_CurrentPhaseId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phase",
                table: "Phase");

            migrationBuilder.RenameTable(
                name: "Phase",
                newName: "Phases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phases",
                table: "Phases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectPhaseHistories_Phases_FromPhaseId",
                table: "ProjectPhaseHistories",
                column: "FromPhaseId",
                principalTable: "Phases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectPhaseHistories_Phases_ToPhaseId",
                table: "ProjectPhaseHistories",
                column: "ToPhaseId",
                principalTable: "Phases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Phases_CurrentPhaseId",
                table: "Projects",
                column: "CurrentPhaseId",
                principalTable: "Phases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPhaseHistories_Phases_FromPhaseId",
                table: "ProjectPhaseHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPhaseHistories_Phases_ToPhaseId",
                table: "ProjectPhaseHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Phases_CurrentPhaseId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phases",
                table: "Phases");

            migrationBuilder.RenameTable(
                name: "Phases",
                newName: "Phase");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phase",
                table: "Phase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectPhaseHistories_Phase_FromPhaseId",
                table: "ProjectPhaseHistories",
                column: "FromPhaseId",
                principalTable: "Phase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectPhaseHistories_Phase_ToPhaseId",
                table: "ProjectPhaseHistories",
                column: "ToPhaseId",
                principalTable: "Phase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Phase_CurrentPhaseId",
                table: "Projects",
                column: "CurrentPhaseId",
                principalTable: "Phase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
