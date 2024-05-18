using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projekt_Avancerad.NET.Migrations
{
    /// <inheritdoc />
    public partial class Updateddatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 201);

            migrationBuilder.CreateTable(
                name: "AppointmentHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentHistories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "Address", "CompanyID", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, null, null, "jonas@gmail.se", "Jonas", "Svensson", "0712345432" },
                    { 2, null, null, "lovisa@gmail.se", "Lovisa", "Johansson", "0712345444" },
                    { 3, null, null, "Göran@gmail.se", "Göran", "Karlsson", "0712345666" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentID", "CustomerID", "Description", "EndDate", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Conference booked for 10+ people", new DateTime(2024, 5, 20, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 20, 13, 0, 0, 0, DateTimeKind.Unspecified), "Meeting" },
                    { 2, 2, "Conference booked for 10+ people", new DateTime(2024, 5, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), "Meeting" },
                    { 3, 3, "Conference booked for 10+ people", new DateTime(2024, 5, 25, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 25, 13, 0, 0, 0, DateTimeKind.Unspecified), "Meeting" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentHistories");

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyID", "CompanyName" },
                values: new object[] { 2, "Kusthotellet" });

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
        }
    }
}
