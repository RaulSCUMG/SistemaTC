using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemaTC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClientRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "PermissionId", "Code", "Created", "CreatedBy", "Name", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("389df558-751c-40dc-9cbf-9e847eac886d"), "VIEW_ROLE", new DateTime(2024, 5, 17, 3, 23, 0, 0, DateTimeKind.Unspecified), "System", "View Role", null, null },
                    { new Guid("cf608b86-f8eb-4e75-b7c7-ecd6bde0da2b"), "SEND_NOTIFICATION", new DateTime(2024, 5, 17, 3, 23, 0, 0, DateTimeKind.Unspecified), "System", "Send Notification", null, null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Code", "Created", "CreatedBy", "Name", "Updated", "UpdatedBy" },
                values: new object[] { new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562"), "client", new DateTime(2024, 5, 17, 3, 23, 0, 0, DateTimeKind.Unspecified), "System", "Cliente", null, null });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("002622c7-4619-4679-b084-fb16f892f557"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") },
                    { new Guid("4dadda0a-30a8-4fe7-83a2-936d9bf236b2"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") },
                    { new Guid("54978218-8f88-49e3-93fa-f7b4c2dd2c96"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") },
                    { new Guid("56c83f0f-53e0-4328-9ea7-9787bbcbfd02"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") },
                    { new Guid("a38e0d18-6814-4e71-aa55-f66307bcd93b"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") },
                    { new Guid("abbfb829-aebf-4548-a89d-97570d11c04c"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") },
                    { new Guid("c3dd326f-3d61-4a36-934b-2dba1e256b8c"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") },
                    { new Guid("cf608b86-f8eb-4e75-b7c7-ecd6bde0da2b"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") },
                    { new Guid("ea36832a-eaca-40e6-afbd-dbc9c965a54f"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") },
                    { new Guid("f8072b82-b1b1-4f5c-9eae-f2e7d1bd2634"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") },
                    { new Guid("389df558-751c-40dc-9cbf-9e847eac886d"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("cf608b86-f8eb-4e75-b7c7-ecd6bde0da2b"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("002622c7-4619-4679-b084-fb16f892f557"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4dadda0a-30a8-4fe7-83a2-936d9bf236b2"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("54978218-8f88-49e3-93fa-f7b4c2dd2c96"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("56c83f0f-53e0-4328-9ea7-9787bbcbfd02"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a38e0d18-6814-4e71-aa55-f66307bcd93b"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("abbfb829-aebf-4548-a89d-97570d11c04c"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c3dd326f-3d61-4a36-934b-2dba1e256b8c"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cf608b86-f8eb-4e75-b7c7-ecd6bde0da2b"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ea36832a-eaca-40e6-afbd-dbc9c965a54f"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f8072b82-b1b1-4f5c-9eae-f2e7d1bd2634"), new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("389df558-751c-40dc-9cbf-9e847eac886d"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cf608b86-f8eb-4e75-b7c7-ecd6bde0da2b"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("389df558-751c-40dc-9cbf-9e847eac886d"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("cf608b86-f8eb-4e75-b7c7-ecd6bde0da2b"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("47fc7698-01d7-463a-a3c7-5c405fc4b562"));
        }
    }
}
