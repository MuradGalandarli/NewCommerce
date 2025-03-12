using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewCommerce.Persistence.Migrations
{
    public partial class mig11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Sütunları sil
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Sütunları geri əlavə et
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId1",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid());
        }
    }
}
