using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.Data.Migrations;

/// <inheritdoc />
public partial class delMy2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "CanUpdate",
            table: "UserRoles",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CanUpdate",
            table: "UserRoles");
    }
}
