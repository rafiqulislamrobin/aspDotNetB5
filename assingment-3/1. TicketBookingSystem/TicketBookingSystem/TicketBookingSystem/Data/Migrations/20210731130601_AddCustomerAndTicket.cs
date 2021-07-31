using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketBookingSystem.Data.Migrations
{
    public partial class AddCustomerAndTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    age = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fees = table.Column<int>(type: "int", nullable: false),
                    customerIdId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tickets_customers_customerIdId",
                        column: x => x.customerIdId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tickets_customerIdId",
                table: "tickets",
                column: "customerIdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
