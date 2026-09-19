using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FTS.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NutritionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CarbohydratesTotal",
                table: "Recipes",
                type: "decimal(10,3)",
                precision: 10,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FatTotal",
                table: "Recipes",
                type: "decimal(10,3)",
                precision: 10,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "KcalPerServing",
                table: "Recipes",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "KcalTotal",
                table: "Recipes",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProteinsTotal",
                table: "Recipes",
                type: "decimal(10,3)",
                precision: 10,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Servings",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "RecipeIngredients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "RecipeIngredients",
                type: "decimal(10,3)",
                precision: 10,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountInGrams",
                table: "RecipeIngredients",
                type: "decimal(10,3)",
                precision: 10,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Basis",
                table: "Ingredients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DensityGPerMl",
                table: "Ingredients",
                type: "decimal(6,4)",
                precision: 6,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GramsPerPiece",
                table: "Ingredients",
                type: "decimal(8,3)",
                precision: 8,
                scale: 3,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarbohydratesTotal",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "FatTotal",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "KcalPerServing",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "KcalTotal",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ProteinsTotal",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Servings",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "AmountInGrams",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "Basis",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "DensityGPerMl",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "GramsPerPiece",
                table: "Ingredients");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "RecipeIngredients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "RecipeIngredients",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,3)",
                oldPrecision: 10,
                oldScale: 3);
        }
    }
}
