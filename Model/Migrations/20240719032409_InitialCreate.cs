using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                BookId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "NVARCHAR(255)", nullable: false),
                Author = table.Column<string>(type: "NVARCHAR(255)", nullable: false),
                Description = table.Column<string>(type: "NVARCHAR(255)", nullable: false),
                CoverImage = table.Column<string>(type: "NVARCHAR(255)", nullable: false),
                Publisher = table.Column<string>(type: "NVARCHAR(255)", nullable: false),
                PublicationDate = table.Column<DateOnly>(type: "DATE", nullable: true),
                Category = table.Column<string>(type: "NVARCHAR(255)", nullable: false),
                Isbn = table.Column<string>(type: "NVARCHAR(255)", nullable: false),
                PageCount = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Books", x => x.BookId);
            });

        migrationBuilder.CreateTable(
            name: "InventoryLogs",
            columns: table => new
            {
                InventoryLogId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                BookId = table.Column<int>(type: "int", nullable: false),
                User = table.Column<string>(type: "NVARCHAR(255)", nullable: false),
                CheckoutDate = table.Column<DateTime>(type: "DATE", nullable: true),
                CheckinDate = table.Column<DateTime>(type: "DATE", nullable: true),
                DueDate = table.Column<DateTime>(type: "DATE", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_InventoryLogs", x => x.InventoryLogId);
                table.ForeignKey(
                    name: "FK_InventoryLogs_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "BookId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Reviews",
            columns: table => new
            {
                ReviewId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                BookId = table.Column<int>(type: "int", nullable: false),
                Rating = table.Column<int>(type: "int", nullable: false),
                Description = table.Column<string>(type: "NVARCHAR(255)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                table.ForeignKey(
                    name: "FK_Reviews_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "BookId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Book_Isbn",
            table: "Books",
            column: "Isbn",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Book_TitleAuthor",
            table: "Books",
            columns: new[] { "Title", "Author" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_InventoryLogs_BookId",
            table: "InventoryLogs",
            column: "BookId");

        migrationBuilder.CreateIndex(
            name: "IX_Reviews_BookId",
            table: "Reviews",
            column: "BookId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "InventoryLogs");

        migrationBuilder.DropTable(
            name: "Reviews");

        migrationBuilder.DropTable(
            name: "Books");
    }
}
