using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurantWebApp.Migrations
{
    public partial class AddCategoryImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

           

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealDto");

            migrationBuilder.DropTable(
                name: "CategoryDto");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fa1c9e4-132e-492e-ac43-5b8a6ba38a72");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4119e7b-b123-481b-bca3-068f50efa048");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "055174da-581a-4ff1-9798-c75b51b7408e", "8a4ae6c7-c6a0-4a98-b183-80ff509c2c97", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0b47f649-1f9b-4501-ba0a-a414303107a4", "8b495496-e65b-47d4-918a-217eca066c12", "Customer", "CUSTOMER" });
        }
    }
}
