using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITDeskServer.Migrations
{
    /// <inheritdoc />
    public partial class mg14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketDetais_Users_AppUserId",
                table: "TicketDetais");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TicketDetais");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "TicketDetais",
                newName: "AppuserId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketDetais_AppUserId",
                table: "TicketDetais",
                newName: "IX_TicketDetais_AppuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDetais_Users_AppuserId",
                table: "TicketDetais",
                column: "AppuserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketDetais_Users_AppuserId",
                table: "TicketDetais");

            migrationBuilder.RenameColumn(
                name: "AppuserId",
                table: "TicketDetais",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketDetais_AppuserId",
                table: "TicketDetais",
                newName: "IX_TicketDetais_AppUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TicketDetais",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDetais_Users_AppUserId",
                table: "TicketDetais",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
