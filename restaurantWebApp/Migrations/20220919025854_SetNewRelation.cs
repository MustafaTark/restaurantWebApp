using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurantWebApp.Migrations
{
    public partial class SetNewRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Carts_CartId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Orders_OrderId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_CartId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_OrderId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Meals");

            migrationBuilder.CreateTable(
                name: "CartMeal",
                columns: table => new
                {
                    CartsId = table.Column<int>(type: "int", nullable: false),
                    MealsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartMeal", x => new { x.CartsId, x.MealsId });
                    table.ForeignKey(
                        name: "FK_CartMeal_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartMeal_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealOrder",
                columns: table => new
                {
                    MealsId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealOrder", x => new { x.MealsId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_MealOrder_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartMeal_MealsId",
                table: "CartMeal",
                column: "MealsId");

            migrationBuilder.CreateIndex(
                name: "IX_MealOrder_OrdersId",
                table: "MealOrder",
                column: "OrdersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartMeal");

            migrationBuilder.DropTable(
                name: "MealOrder");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Meals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Meals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_CartId",
                table: "Meals",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_OrderId",
                table: "Meals",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Carts_CartId",
                table: "Meals",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Orders_OrderId",
                table: "Meals",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
