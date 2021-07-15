using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Base.Migrations
{
    public partial class InitLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionAudit",
                columns: table => new
                {
                    ActionAuditId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Controller = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Action = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Parameter = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    BeginAuditTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EndAuditTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Ip = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Referer = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UserAgent = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ObjectId = table.Column<int>(type: "int", nullable: true),
                    OldObjectValue = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    NewObjectValue = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    TargetObject = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ErrorMessage = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    LogLevel = table.Column<int>(type: "int", nullable: false),
                    LogType = table.Column<int>(type: "int", nullable: false),
                    LogSource = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    TraceId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionAudit", x => x.ActionAuditId);
                });

            migrationBuilder.CreateTable(
                name: "MapLogging",
                columns: table => new
                {
                    MapLoggingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapLogging", x => x.MapLoggingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionAudit");

            migrationBuilder.DropTable(
                name: "MapLogging");
        }
    }
}
