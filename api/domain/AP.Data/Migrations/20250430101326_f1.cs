using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.Data.Migrations;

/// <inheritdoc />
public partial class f1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "UserRoles",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                CanWrite = table.Column<bool>(type: "bit", nullable: false),
                CanDelete = table.Column<bool>(type: "bit", nullable: false),
                CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserRoles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AP_Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                MyProperty = table.Column<int>(type: "int", nullable: false),
                AP_UserRoleId = table.Column<int>(type: "int", nullable: false),
                CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AP_Users", x => x.Id);
                table.ForeignKey(
                    name: "FK_AP_Users_UserRoles_AP_UserRoleId",
                    column: x => x.AP_UserRoleId,
                    principalTable: "UserRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AP_Users_AP_UserRoleId",
            table: "AP_Users",
            column: "AP_UserRoleId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AP_Users");

        migrationBuilder.DropTable(
            name: "UserRoles");
    }
}
