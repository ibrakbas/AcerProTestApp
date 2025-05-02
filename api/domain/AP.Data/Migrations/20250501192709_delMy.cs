using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.Data.Migrations;

/// <inheritdoc />
public partial class delMy : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "MyProperty",
            table: "Users");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "MyProperty",
            table: "Users",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }
}
