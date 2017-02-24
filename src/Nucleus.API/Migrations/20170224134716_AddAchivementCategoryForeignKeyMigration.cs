using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nucleus.API.Migrations
{
    public partial class AddAchivementCategoryForeignKeyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AchievementCategoryId",
                table: "Achievements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_AchievementCategoryId",
                table: "Achievements",
                column: "AchievementCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_AchievementsCategories_AchievementCategoryId",
                table: "Achievements",
                column: "AchievementCategoryId",
                principalTable: "AchievementsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_AchievementsCategories_AchievementCategoryId",
                table: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_AchievementCategoryId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "AchievementCategoryId",
                table: "Achievements");
        }
    }
}
