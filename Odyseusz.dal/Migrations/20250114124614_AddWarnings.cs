using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Odyseusz.dal.Migrations
{
    /// <inheritdoc />
    public partial class AddWarnings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ostrzezenia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KrajNazwa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tresc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ostrzezenia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ostrzezenia_Kraje_KrajNazwa",
                        column: x => x.KrajNazwa,
                        principalTable: "Kraje",
                        principalColumn: "Nazwa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ostrzezenia_KrajNazwa",
                table: "Ostrzezenia",
                column: "KrajNazwa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ostrzezenia");
        }
    }
}
