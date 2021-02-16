using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DBconnectShop.Migrations
{
    public partial class database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Address_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address_country = table.Column<string>(type: "nchar(25)", maxLength: 25, nullable: false),
                    Address_city = table.Column<string>(type: "nchar(50)", maxLength: 50, nullable: false),
                    Address_street = table.Column<string>(type: "nchar(50)", maxLength: 50, nullable: false),
                    Address_building_number = table.Column<string>(type: "nchar(50)", maxLength: 50, nullable: false),
                    Address_zip_code = table.Column<string>(type: "nchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Address_id);
                });

            migrationBuilder.CreateTable(
                name: "Product_categories",
                columns: table => new
                {
                    Product_category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_sub_category = table.Column<int>(type: "int", nullable: true),
                    Product_category_name = table.Column<string>(type: "nchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_categories", x => x.Product_category_id);
                    table.ForeignKey(
                        name: "FK_Product_categories_Product_categories_Product_sub_category",
                        column: x => x.Product_sub_category,
                        principalTable: "Product_categories",
                        principalColumn: "Product_category_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_groups",
                columns: table => new
                {
                    User_group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_group_name = table.Column<string>(type: "nchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_groups", x => x.User_group_id);
                });

            migrationBuilder.CreateTable(
                name: "User_order_status",
                columns: table => new
                {
                    User_order_status_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_order_status_name = table.Column<string>(type: "nchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_order_status", x => x.User_order_status_id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_category_id = table.Column<int>(type: "int", nullable: false),
                    Product_name = table.Column<string>(type: "nchar(50)", maxLength: 50, nullable: false),
                    Product_aviable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Product_id);
                    table.ForeignKey(
                        name: "FK_Products_Product_categories_Product_category_id",
                        column: x => x.Product_category_id,
                        principalTable: "Product_categories",
                        principalColumn: "Product_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_name = table.Column<string>(type: "nchar(25)", maxLength: 25, nullable: false),
                    User_password = table.Column<string>(type: "nchar(25)", maxLength: 25, nullable: false),
                    User_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    User_group_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_id);
                    table.ForeignKey(
                        name: "FK_Users_User_groups_User_group_id",
                        column: x => x.User_group_id,
                        principalTable: "User_groups",
                        principalColumn: "User_group_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_images",
                columns: table => new
                {
                    Product_image_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    Product_Image = table.Column<byte[]>(type: "varBinary(max)", nullable: false),
                    Product_image_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_images", x => x.Product_image_id);
                    table.ForeignKey(
                        name: "FK_Product_images_Products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "Products",
                        principalColumn: "Product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_specifications",
                columns: table => new
                {
                    Product_specification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    Product_specification_name = table.Column<string>(type: "nchar(30)", maxLength: 30, nullable: false),
                    Product_specification_value = table.Column<string>(type: "nchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_specifications", x => x.Product_specification_id);
                    table.ForeignKey(
                        name: "FK_Product_specifications_Products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "Products",
                        principalColumn: "Product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products_price",
                columns: table => new
                {
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    Product_price_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Product_price = table.Column<decimal>(type: "smallmoney", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products_price", x => new { x.Product_id, x.Product_price_date });
                    table.ForeignKey(
                        name: "FK_Products_price_Products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "Products",
                        principalColumn: "Product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_opinions",
                columns: table => new
                {
                    Product_opinion_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Product_Opinion = table.Column<string>(type: "nchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_opinions", x => x.Product_opinion_id);
                    table.ForeignKey(
                        name: "FK_Product_opinions_Products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "Products",
                        principalColumn: "Product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_opinions_Users_User_id",
                        column: x => x.User_id,
                        principalTable: "Users",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_ratings",
                columns: table => new
                {
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Product_Rating = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_ratings", x => new { x.Product_id, x.User_id });
                    table.ForeignKey(
                        name: "FK_Product_ratings_Products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "Products",
                        principalColumn: "Product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_ratings_Users_Product_id",
                        column: x => x.Product_id,
                        principalTable: "Users",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Addresses",
                columns: table => new
                {
                    User_Address_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address_id = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Addresses", x => x.User_Address_id);
                    table.ForeignKey(
                        name: "FK_User_Addresses_Addresses_Address_id",
                        column: x => x.Address_id,
                        principalTable: "Addresses",
                        principalColumn: "Address_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Addresses_Users_User_id",
                        column: x => x.User_id,
                        principalTable: "Users",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users_data",
                columns: table => new
                {
                    User_id = table.Column<int>(type: "int", nullable: false),
                    User_first_name = table.Column<string>(type: "nchar(25)", maxLength: 25, nullable: false),
                    User_second_name = table.Column<string>(type: "nchar(25)", maxLength: 25, nullable: true),
                    User_family_name = table.Column<string>(type: "nchar(25)", maxLength: 25, nullable: false),
                    User_avatar = table.Column<byte[]>(type: "varBinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_data", x => x.User_id);
                    table.ForeignKey(
                        name: "FK_Users_data_Users_User_id",
                        column: x => x.User_id,
                        principalTable: "Users",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_orders",
                columns: table => new
                {
                    User_order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_order_status_id = table.Column<int>(type: "int", nullable: false),
                    User_Address_id = table.Column<int>(type: "int", nullable: false),
                    User_order_date = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_orders", x => x.User_order_id);
                    table.ForeignKey(
                        name: "FK_User_orders_User_Addresses_User_Address_id",
                        column: x => x.User_Address_id,
                        principalTable: "User_Addresses",
                        principalColumn: "User_Address_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_orders_User_order_status_User_order_status_id",
                        column: x => x.User_order_status_id,
                        principalTable: "User_order_status",
                        principalColumn: "User_order_status_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_order_Products",
                columns: table => new
                {
                    User_order_Products_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_order_id = table.Column<int>(type: "int", nullable: false),
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    User_order_Product_price = table.Column<decimal>(type: "smallmoney", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_order_Products", x => x.User_order_Products_id);
                    table.ForeignKey(
                        name: "FK_User_order_Products_Products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "Products",
                        principalColumn: "Product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_order_Products_User_orders_User_order_id",
                        column: x => x.User_order_id,
                        principalTable: "User_orders",
                        principalColumn: "User_order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Address_id", "Address_building_number", "Address_city", "Address_country", "Address_street", "Address_zip_code" },
                values: new object[] { 1, "54", "Krakow", "Polska", "Komandora Wrońskiego Bohdana", "30-852" });

            migrationBuilder.InsertData(
                table: "Product_categories",
                columns: new[] { "Product_category_id", "Product_category_name", "Product_sub_category" },
                values: new object[] { 1, "Przykład", null });

            migrationBuilder.InsertData(
                table: "User_groups",
                columns: new[] { "User_group_id", "User_group_name" },
                values: new object[,]
                {
                    { 1, "Klient" },
                    { 2, "Pracownik" },
                    { 3, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "User_order_status",
                columns: new[] { "User_order_status_id", "User_order_status_name" },
                values: new object[,]
                {
                    { 1, "Złożone" },
                    { 2, "Wykonane" },
                    { 3, "Zrealizowane" },
                    { 4, "Zwrucone" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Product_id", "Product_aviable", "Product_category_id", "Product_name" },
                values: new object[] { 1, false, 1, "Przykład" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "User_active", "User_group_id", "User_name", "User_password" },
                values: new object[] { 1, true, 3, "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Products_price",
                columns: new[] { "Product_id", "Product_price_date", "Product_price" },
                values: new object[] { 1, new DateTime(2021, 2, 16, 12, 34, 2, 656, DateTimeKind.Local).AddTicks(6064), 100m });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Address_city_Address_country_Address_street_Address_building_number_Address_zip_code",
                table: "Addresses",
                columns: new[] { "Address_city", "Address_country", "Address_street", "Address_building_number", "Address_zip_code" },
                unique: true,
                filter: "[Address_zip_code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Product_categories_Product_sub_category",
                table: "Product_categories",
                column: "Product_sub_category");

            migrationBuilder.CreateIndex(
                name: "IX_Product_images_Product_id",
                table: "Product_images",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_opinions_Product_id",
                table: "Product_opinions",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_opinions_User_id",
                table: "Product_opinions",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_specifications_Product_id",
                table: "Product_specifications",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Product_category_id",
                table: "Products",
                column: "Product_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Addresses_Address_id_User_id",
                table: "User_Addresses",
                columns: new[] { "Address_id", "User_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Addresses_User_id",
                table: "User_Addresses",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_order_Products_Product_id",
                table: "User_order_Products",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_order_Products_User_order_id",
                table: "User_order_Products",
                column: "User_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_orders_User_Address_id",
                table: "User_orders",
                column: "User_Address_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_orders_User_order_status_id",
                table: "User_orders",
                column: "User_order_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_User_group_id",
                table: "Users",
                column: "User_group_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product_images");

            migrationBuilder.DropTable(
                name: "Product_opinions");

            migrationBuilder.DropTable(
                name: "Product_ratings");

            migrationBuilder.DropTable(
                name: "Product_specifications");

            migrationBuilder.DropTable(
                name: "Products_price");

            migrationBuilder.DropTable(
                name: "User_order_Products");

            migrationBuilder.DropTable(
                name: "Users_data");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "User_orders");

            migrationBuilder.DropTable(
                name: "Product_categories");

            migrationBuilder.DropTable(
                name: "User_Addresses");

            migrationBuilder.DropTable(
                name: "User_order_status");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "User_groups");
        }
    }
}
