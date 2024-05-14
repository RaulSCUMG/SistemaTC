using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemaTC.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Permission",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Permission",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "PermissionId", "Code", "Created", "CreatedBy", "Name", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("002622c7-4619-4679-b084-fb16f892f557"), "VIEW_REQUEST", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "View Request", null, null },
                    { new Guid("16d64a71-9d53-4672-bd88-c9dbfd237172"), "VIEW_CREDIT_CARD_INFO_FROM_USER", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "View Credit Card Info from other user", null, null },
                    { new Guid("33f7e6e5-458c-4f20-84ad-8407570fd568"), "CREATE_CC_TRANSACTION", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Create Credit/Debit Credit Card Transaction", null, null },
                    { new Guid("3d1ea76a-6607-456b-a584-142b32200f74"), "CREATE_CREDIT_CARD", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Create Credit Card", null, null },
                    { new Guid("4dadda0a-30a8-4fe7-83a2-936d9bf236b2"), "INACTIVATE_CREDIT_CARD", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Inactivate/Activate Credit Card", null, null },
                    { new Guid("54978218-8f88-49e3-93fa-f7b4c2dd2c96"), "VIEW_CREDIT_CARD_PAYMENT", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "View Credit Card Payment", null, null },
                    { new Guid("56c83f0f-53e0-4328-9ea7-9787bbcbfd02"), "VIEW_USER", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "View User", null, null },
                    { new Guid("7280baca-a1b4-42c9-9de2-b837849606a1"), "INACTIVATE_USER", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Inactivate/Activate User", null, null },
                    { new Guid("a38e0d18-6814-4e71-aa55-f66307bcd93b"), "VIEW_CREDIT_CARD_CUTOFF", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "View Credit Card Cutoff", null, null },
                    { new Guid("abbfb829-aebf-4548-a89d-97570d11c04c"), "CREATE_REQUEST", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Create Request", null, null },
                    { new Guid("b578352c-43d2-49ee-bbd0-c03d6f57e666"), "UPDATE_CREDIT_CARD", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Update Credit Card", null, null },
                    { new Guid("b8c1ee6e-393d-47e1-a58d-5bdb35e69b13"), "CREATE_CREDIT_CARD_CUTOFF", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Create Credit Card Cutoff", null, null },
                    { new Guid("c3dd326f-3d61-4a36-934b-2dba1e256b8c"), "VIEW_CREDIT_CARD", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "View Credit Card", null, null },
                    { new Guid("c89fe7b5-bb41-44fd-a726-377095491585"), "CREATE_CREDIT_CARD_PAYMENT", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Create Credit Card Payment", null, null },
                    { new Guid("d21347ca-1c8b-49b6-8694-829801012975"), "UPDATE_CREDIT_CARD_CUTOFF", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Update Credit Card Cutoff", null, null },
                    { new Guid("d72db557-1203-48c2-866b-ba41d2f50447"), "UPDATE_REQUEST", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Update Request", null, null },
                    { new Guid("ea36832a-eaca-40e6-afbd-dbc9c965a54f"), "UPDATE_PIN_CREDIT_CARD", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Update PIN of Credit Card", null, null },
                    { new Guid("eda6a773-8c6b-48ec-aa9c-f81be8b46ea2"), "PROCESS_REQUEST", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Process Request", null, null },
                    { new Guid("ef127738-9162-4654-89fc-d201386397f0"), "CREATE_USER", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Create User", null, null },
                    { new Guid("f8072b82-b1b1-4f5c-9eae-f2e7d1bd2634"), "UPDATE_USER", new DateTime(2024, 5, 14, 1, 24, 0, 0, DateTimeKind.Unspecified), "System", "Update User", null, null }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("002622c7-4619-4679-b084-fb16f892f557"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("16d64a71-9d53-4672-bd88-c9dbfd237172"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("33f7e6e5-458c-4f20-84ad-8407570fd568"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("3d1ea76a-6607-456b-a584-142b32200f74"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("4dadda0a-30a8-4fe7-83a2-936d9bf236b2"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("54978218-8f88-49e3-93fa-f7b4c2dd2c96"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("56c83f0f-53e0-4328-9ea7-9787bbcbfd02"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("7280baca-a1b4-42c9-9de2-b837849606a1"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("a38e0d18-6814-4e71-aa55-f66307bcd93b"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("abbfb829-aebf-4548-a89d-97570d11c04c"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("b578352c-43d2-49ee-bbd0-c03d6f57e666"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("b8c1ee6e-393d-47e1-a58d-5bdb35e69b13"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("c3dd326f-3d61-4a36-934b-2dba1e256b8c"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("c89fe7b5-bb41-44fd-a726-377095491585"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("d21347ca-1c8b-49b6-8694-829801012975"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("d72db557-1203-48c2-866b-ba41d2f50447"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("ea36832a-eaca-40e6-afbd-dbc9c965a54f"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("eda6a773-8c6b-48ec-aa9c-f81be8b46ea2"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("ef127738-9162-4654-89fc-d201386397f0"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
                    { new Guid("f8072b82-b1b1-4f5c-9eae-f2e7d1bd2634"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission");

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("002622c7-4619-4679-b084-fb16f892f557"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("16d64a71-9d53-4672-bd88-c9dbfd237172"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("33f7e6e5-458c-4f20-84ad-8407570fd568"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("3d1ea76a-6607-456b-a584-142b32200f74"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4dadda0a-30a8-4fe7-83a2-936d9bf236b2"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("54978218-8f88-49e3-93fa-f7b4c2dd2c96"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("56c83f0f-53e0-4328-9ea7-9787bbcbfd02"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("7280baca-a1b4-42c9-9de2-b837849606a1"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a38e0d18-6814-4e71-aa55-f66307bcd93b"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("abbfb829-aebf-4548-a89d-97570d11c04c"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b578352c-43d2-49ee-bbd0-c03d6f57e666"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b8c1ee6e-393d-47e1-a58d-5bdb35e69b13"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c3dd326f-3d61-4a36-934b-2dba1e256b8c"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c89fe7b5-bb41-44fd-a726-377095491585"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d21347ca-1c8b-49b6-8694-829801012975"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d72db557-1203-48c2-866b-ba41d2f50447"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ea36832a-eaca-40e6-afbd-dbc9c965a54f"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("eda6a773-8c6b-48ec-aa9c-f81be8b46ea2"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ef127738-9162-4654-89fc-d201386397f0"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f8072b82-b1b1-4f5c-9eae-f2e7d1bd2634"), new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a") });

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("002622c7-4619-4679-b084-fb16f892f557"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("16d64a71-9d53-4672-bd88-c9dbfd237172"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("33f7e6e5-458c-4f20-84ad-8407570fd568"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("3d1ea76a-6607-456b-a584-142b32200f74"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("4dadda0a-30a8-4fe7-83a2-936d9bf236b2"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("54978218-8f88-49e3-93fa-f7b4c2dd2c96"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("56c83f0f-53e0-4328-9ea7-9787bbcbfd02"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("7280baca-a1b4-42c9-9de2-b837849606a1"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("a38e0d18-6814-4e71-aa55-f66307bcd93b"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("abbfb829-aebf-4548-a89d-97570d11c04c"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("b578352c-43d2-49ee-bbd0-c03d6f57e666"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("b8c1ee6e-393d-47e1-a58d-5bdb35e69b13"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("c3dd326f-3d61-4a36-934b-2dba1e256b8c"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("c89fe7b5-bb41-44fd-a726-377095491585"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("d21347ca-1c8b-49b6-8694-829801012975"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("d72db557-1203-48c2-866b-ba41d2f50447"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("ea36832a-eaca-40e6-afbd-dbc9c965a54f"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("eda6a773-8c6b-48ec-aa9c-f81be8b46ea2"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("ef127738-9162-4654-89fc-d201386397f0"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: new Guid("f8072b82-b1b1-4f5c-9eae-f2e7d1bd2634"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Permission",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Permission",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);
        }
    }
}
