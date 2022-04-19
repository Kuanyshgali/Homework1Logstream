using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homework1.Migrations
{
    public partial class Result : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    idTeam = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Teams_idTeam",
                        column: x => x.idTeam,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "First" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "Second" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Title" },
                values: new object[] { 3, "Third" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "Age", "Cost", "Name", "idTeam" },
                values: new object[,]
                {
                    { 1, 21, 10000.0, "Player1", 2 },
                    { 2, 22, 1000.0, "Player2", 1 },
                    { 3, 23, 100.0, "Player3", 3 },
                    { 4, 24, 10000.0, "Player4", 1 },
                    { 5, 25, 1000.0, "Player5", 2 },
                    { 6, 26, 100.0, "Player6", 3 },
                    { 7, 27, 10000.0, "Player7", 3 },
                    { 8, 28, 1000.0, "Player8", 2 },
                    { 9, 29, 100.0, "Player9", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_idTeam",
                table: "Player",
                column: "idTeam");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
