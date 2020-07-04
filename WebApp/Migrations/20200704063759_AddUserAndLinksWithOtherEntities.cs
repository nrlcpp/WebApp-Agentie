using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AddUserAndLinksWithOtherEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AddedById",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AddedById",
                table: "Remarks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AddedById",
                table: "Reservations",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_AddedById",
                table: "Remarks",
                column: "AddedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Remarks_User_AddedById",
                table: "Remarks",
                column: "AddedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_User_AddedById",
                table: "Reservations",
                column: "AddedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Remarks_User_AddedById",
                table: "Remarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_User_AddedById",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_AddedById",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Remarks_AddedById",
                table: "Remarks");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Remarks");
        }
    }
}
