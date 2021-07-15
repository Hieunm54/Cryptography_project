using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Infrastructure.Data.Migrations.Application
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    BlockID = table.Column<Guid>(type: "char(36)", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Hash = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PreviousHash = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Nonce = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.BlockID);
                });

            migrationBuilder.CreateTable(
                name: "BlockData",
                columns: table => new
                {
                    BlockDataID = table.Column<Guid>(type: "char(36)", nullable: false),
                    BlockID = table.Column<Guid>(type: "char(36)", nullable: false),
                    DataUserID = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockData", x => x.BlockDataID);
                });

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataUser",
                columns: table => new
                {
                    DataUserID = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataUser", x => x.DataUserID);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Phone = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exchange",
                columns: table => new
                {
                    ExchangeID = table.Column<Guid>(type: "char(36)", nullable: false),
                    AddressTo = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    AddressFrom = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false),
                    BlockID = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchange", x => x.ExchangeID);
                    table.ForeignKey(
                        name: "FK_Exchange_Block_BlockID",
                        column: x => x.BlockID,
                        principalTable: "Block",
                        principalColumn: "BlockID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exchange_BlockID",
                table: "Exchange",
                column: "BlockID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockData");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "DataUser");

            migrationBuilder.DropTable(
                name: "Exchange");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "Block");
        }
    }
}
