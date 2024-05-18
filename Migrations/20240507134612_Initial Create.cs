using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projekt_Avancerad.NET.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customers_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentID);
                    table.ForeignKey(
                        name: "FK_Appointments_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyID", "CompanyName" },
                values: new object[,]
                {
                    { 1, "Solviken" },
                    { 2, "Kusthotellet" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "Address", "CompanyID", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 200, null, null, "jonas@gmail.se", "Jonas", "Svensson", "0712345432" },
                    { 201, null, null, "lovisa@gmail.se", "Lovisa", "Johansson", "0712345444" },
                    { 202, null, null, "Göran@gmail.se", "Göran", "Karlsson", "0712345666" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentID", "CustomerID", "Description", "EndDate", "StartDate", "Title" },
                values: new object[,]
                {
                    { 100, 200, "Conference booked for 10+ people", new DateTime(2024, 5, 20, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 20, 13, 0, 0, 0, DateTimeKind.Unspecified), "Meeting" },
                    { 101, 201, "Conference booked for 10+ people", new DateTime(2024, 5, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), "Meeting" },
                    { 102, 200, "Conference booked for 10+ people", new DateTime(2024, 5, 25, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 25, 13, 0, 0, 0, DateTimeKind.Unspecified), "Meeting" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerID",
                table: "Appointments",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CompanyID",
                table: "Customers",
                column: "CompanyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
