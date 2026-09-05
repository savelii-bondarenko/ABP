using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameRoomServiceToAdditionalService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingSelectedServices_RoomServices_SelectedServicesId",
                table: "BookingSelectedServices");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAvailableServices_RoomServices_AvailableServicesId",
                table: "RoomAvailableServices");

            migrationBuilder.DropTable(
                name: "RoomServices");

            migrationBuilder.CreateTable(
                name: "AdditionalServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServices", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingSelectedServices_AdditionalServices_SelectedServices~",
                table: "BookingSelectedServices",
                column: "SelectedServicesId",
                principalTable: "AdditionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAvailableServices_AdditionalServices_AvailableServicesId",
                table: "RoomAvailableServices",
                column: "AvailableServicesId",
                principalTable: "AdditionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingSelectedServices_AdditionalServices_SelectedServices~",
                table: "BookingSelectedServices");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAvailableServices_AdditionalServices_AvailableServicesId",
                table: "RoomAvailableServices");

            migrationBuilder.DropTable(
                name: "AdditionalServices");

            migrationBuilder.CreateTable(
                name: "RoomServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomServices", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingSelectedServices_RoomServices_SelectedServicesId",
                table: "BookingSelectedServices",
                column: "SelectedServicesId",
                principalTable: "RoomServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAvailableServices_RoomServices_AvailableServicesId",
                table: "RoomAvailableServices",
                column: "AvailableServicesId",
                principalTable: "RoomServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
