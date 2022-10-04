using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurantWebApp.Migrations
{
    public partial class AddQuntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<int>(
                name: "Quntity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0eaf263a-d921-414f-a758-329f8eca41f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a6f62c0-e624-45ce-8a16-934e9f037fe3");

            migrationBuilder.DropColumn(
                name: "Quntity",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId1",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6e9bba81-9893-4395-8a68-fb6c2c6ac7dc", "a86ecf8b-36c9-4133-96eb-726aa3fb5044", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eb2eca72-e39b-4b2e-916f-747f3521928b", "5ef6c323-d87a-4c18-9bf7-6dbdb19d8ee4", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId1",
                table: "Orders",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId1",
                table: "Orders",
                column: "CustomerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
