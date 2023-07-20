using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class Edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_educations_tb_m_employee_guid",
                table: "tb_m_educations");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_employee_tb_m_accounts_guid",
                table: "tb_m_employee");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employee_employee_guid",
                table: "tb_tr_bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_employee",
                table: "tb_m_employee");

            migrationBuilder.RenameTable(
                name: "tb_m_employee",
                newName: "tb_m_employees");

            migrationBuilder.RenameColumn(
                name: "expired_date",
                table: "tb_m_accounts",
                newName: "expired_time");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_employee_nik_email_phone_number",
                table: "tb_m_employees",
                newName: "IX_tb_m_employees_nik_email_phone_number");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_employees",
                table: "tb_m_employees",
                column: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_educations_tb_m_employees_guid",
                table: "tb_m_educations",
                column: "guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_employees_tb_m_accounts_guid",
                table: "tb_m_employees",
                column: "guid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employees_employee_guid",
                table: "tb_tr_bookings",
                column: "employee_guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_educations_tb_m_employees_guid",
                table: "tb_m_educations");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_employees_tb_m_accounts_guid",
                table: "tb_m_employees");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employees_employee_guid",
                table: "tb_tr_bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_employees",
                table: "tb_m_employees");

            migrationBuilder.RenameTable(
                name: "tb_m_employees",
                newName: "tb_m_employee");

            migrationBuilder.RenameColumn(
                name: "expired_time",
                table: "tb_m_accounts",
                newName: "expired_date");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_employees_nik_email_phone_number",
                table: "tb_m_employee",
                newName: "IX_tb_m_employee_nik_email_phone_number");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_employee",
                table: "tb_m_employee",
                column: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_educations_tb_m_employee_guid",
                table: "tb_m_educations",
                column: "guid",
                principalTable: "tb_m_employee",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_employee_tb_m_accounts_guid",
                table: "tb_m_employee",
                column: "guid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employee_employee_guid",
                table: "tb_tr_bookings",
                column: "employee_guid",
                principalTable: "tb_m_employee",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
