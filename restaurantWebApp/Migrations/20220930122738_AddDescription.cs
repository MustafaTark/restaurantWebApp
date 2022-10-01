using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurantWebApp.Migrations
{
    public partial class AddDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a809784-c2a3-4f27-890f-62be3a38b065");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd166f38-bb23-40cc-968e-fc702a0c187b");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealDto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealDto", x => x.id);
                    table.ForeignKey(
                        name: "FK_MealDto_CategoryDto_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8fa1c9e4-132e-492e-ac43-5b8a6ba38a72", "354d9519-f175-47f3-aa47-e84b3e7c5305", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e4119e7b-b123-481b-bca3-068f50efa048", "3565af89-dbef-47b0-ad17-1f318bfa16d6", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_MealDto_CategoryId",
                table: "MealDto",
                column: "CategoryId");
        }
    }
}
