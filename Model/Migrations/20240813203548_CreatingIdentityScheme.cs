using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Model.Migrations;

/// <inheritdoc />
public partial class CreatingIdentityScheme : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "4894e3d8-004a-479d-aae1-b8a4cf7509ea");

        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "e6a71e82-ccdc-49c2-aa28-c07329a149a3");

        migrationBuilder.AddColumn<string>(
            name: "FirstName",
            table: "AspNetUsers",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "LastName",
            table: "AspNetUsers",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            values: new object[,]
            {
                { "9de88add-92ed-463e-b99a-26e98b975ac7", null, "Librarian", "LIBRARIAN" },
                { "bd54854b-7d38-4c3c-84c1-faa5dd832d47", null, "Customer", "CUSTOMER" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "9de88add-92ed-463e-b99a-26e98b975ac7");

        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "bd54854b-7d38-4c3c-84c1-faa5dd832d47");

        migrationBuilder.DropColumn(
            name: "FirstName",
            table: "AspNetUsers");

        migrationBuilder.DropColumn(
            name: "LastName",
            table: "AspNetUsers");

        migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            values: new object[,]
            {
                { "4894e3d8-004a-479d-aae1-b8a4cf7509ea", null, "Librarian", "LIBRARIAN" },
                { "e6a71e82-ccdc-49c2-aa28-c07329a149a3", null, "Customer", "CUSTOMER" }
            });
    }
}
