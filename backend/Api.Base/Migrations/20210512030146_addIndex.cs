using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Base.Migrations
{
    public partial class addIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Controller",
                table: "ActionAudit",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "ActionAudit",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActionAudit_Action",
                table: "ActionAudit",
                column: "Action");

            migrationBuilder.CreateIndex(
                name: "IX_ActionAudit_Controller",
                table: "ActionAudit",
                column: "Controller");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ActionAudit_Action",
                table: "ActionAudit");

            migrationBuilder.DropIndex(
                name: "IX_ActionAudit_Controller",
                table: "ActionAudit");

            migrationBuilder.AlterColumn<string>(
                name: "Controller",
                table: "ActionAudit",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "ActionAudit",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);
        }
    }
}
