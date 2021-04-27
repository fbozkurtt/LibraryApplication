using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApplication.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreateSQLite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookMetas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: false),
                    ISBN = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookMetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCopies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<Guid>(nullable: true),
                    QRCode = table.Column<string>(nullable: false),
                    Reserved = table.Column<bool>(nullable: false),
                    BookMetaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCopies_BookMetas_BookMetaId",
                        column: x => x.BookMetaId,
                        principalTable: "BookMetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookReservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Returned = table.Column<bool>(nullable: false),
                    DailyExpirationFee = table.Column<decimal>(nullable: false),
                    TotalFee = table.Column<decimal>(nullable: true),
                    ReservationEnds = table.Column<DateTime>(nullable: false),
                    BookMetaId = table.Column<Guid>(nullable: true),
                    BookCopyId = table.Column<Guid>(nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookReservations_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookReservations_BookCopies_BookCopyId",
                        column: x => x.BookCopyId,
                        principalTable: "BookCopies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookReservations_BookMetas_BookMetaId",
                        column: x => x.BookMetaId,
                        principalTable: "BookMetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BookMetas",
                columns: new[] { "Id", "Author", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "ShortDescription", "Title" },
                values: new object[] { new Guid("dda9d2d9-9275-4af6-99d8-38e10ae4b735"), "Marijn Haverbeke", new DateTime(2021, 4, 27, 15, 33, 12, 957, DateTimeKind.Local).AddTicks(7084), null, null, "9781593275846", null, null, "JavaScript lies at the heart of almost every modern web application, from social apps to the newest browser-based games. Though simple for beginners to pick up and play with, JavaScript is a flexible, complex language that you can use to build full-scale applications.", "Eloquent JavaScript, Second Edition" });

            migrationBuilder.InsertData(
                table: "BookMetas",
                columns: new[] { "Id", "Author", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "ShortDescription", "Title" },
                values: new object[] { new Guid("f99a2dac-d99c-4a51-a63c-fd6c253a612b"), "Addy Osmani", new DateTime(2021, 4, 27, 15, 33, 12, 958, DateTimeKind.Local).AddTicks(8944), null, null, "9781449331818", null, null, "With Learning JavaScript Design Patterns, you'll learn how to write beautiful, structured, and maintainable JavaScript by applying classical and modern design patterns to the language. If you want to keep your code efficient, more manageable, and up-to-date with the latest best practices, this book is for you.", "Learning JavaScript Design Patterns" });

            migrationBuilder.InsertData(
                table: "BookMetas",
                columns: new[] { "Id", "Author", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "ShortDescription", "Title" },
                values: new object[] { new Guid("45b93e4d-acc9-4b40-9448-0cf475d55dff"), "Axel Rauschmayer", new DateTime(2021, 4, 27, 15, 33, 12, 958, DateTimeKind.Local).AddTicks(9003), null, null, "9781449365035", null, null, "Like it or not, JavaScript is everywhere these days-from browser to server to mobile-and now you, too, need to learn the language or dive deeper than you have. This concise book guides you into and through JavaScript, written by a veteran programmer who once found himself in the same position.", "Speaking JavaScript" });

            migrationBuilder.InsertData(
                table: "BookMetas",
                columns: new[] { "Id", "Author", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "ShortDescription", "Title" },
                values: new object[] { new Guid("ee4b128d-fcbc-4a2c-875f-0dd5bf14e3ba"), "Eric Elliott", new DateTime(2021, 4, 27, 15, 33, 12, 958, DateTimeKind.Local).AddTicks(9008), null, null, "9781491950296", null, null, "Take advantage of JavaScript's power to build robust web-scale or enterprise applications that are easy to extend and maintain. By applying the design patterns outlined in this practical book, experienced JavaScript developers will learn how to write flexible and resilient code that's easier-yes, easier-to work with as your code base grows.", "Programming JavaScript Applications" });

            migrationBuilder.InsertData(
                table: "BookMetas",
                columns: new[] { "Id", "Author", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "ShortDescription", "Title" },
                values: new object[] { new Guid("ee02d0e8-c34b-453f-83b4-dd2a1d605d51"), "Nicholas C. Zakas", new DateTime(2021, 4, 27, 15, 33, 12, 958, DateTimeKind.Local).AddTicks(9011), null, null, "9781593277574", null, null, "ECMAScript 6 represents the biggest update to the core of JavaScript in the history of the language. In Understanding ECMAScript 6, expert developer Nicholas C. Zakas provides a complete guide to the object types, syntax, and other exciting changes that ECMAScript 6 brings to JavaScript.", "Understanding ECMAScript 6" });

            migrationBuilder.InsertData(
                table: "BookMetas",
                columns: new[] { "Id", "Author", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "ShortDescription", "Title" },
                values: new object[] { new Guid("ee60226f-3a6f-44b9-9015-066ac1fa26fb"), "Kyle Simpson", new DateTime(2021, 4, 27, 15, 33, 12, 958, DateTimeKind.Local).AddTicks(9016), null, null, "9781491904244", null, null, "No matter how much experience you have with JavaScript, odds are you don’t fully understand the language. As part of the \"You Don’t Know JS\" series, this compact guide focuses on new features available in ECMAScript 6 (ES6), the latest version of the standard upon which JavaScript is built.", "You Don't Know JS" });

            migrationBuilder.InsertData(
                table: "BookMetas",
                columns: new[] { "Id", "Author", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "ShortDescription", "Title" },
                values: new object[] { new Guid("02fa06db-3359-446a-ac73-ac1a21b8461d"), "Richard E. Silverman", new DateTime(2021, 4, 27, 15, 33, 12, 958, DateTimeKind.Local).AddTicks(9019), null, null, "9781449325862", null, null, "This pocket guide is the perfect on-the-job companion to Git, the distributed version control system. It provides a compact, readable introduction to Git for new users, as well as a reference to common commands and procedures for those of you with Git experience.", "Git Pocket Guide" });

            migrationBuilder.InsertData(
                table: "BookMetas",
                columns: new[] { "Id", "Author", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "ShortDescription", "Title" },
                values: new object[] { new Guid("138a491c-3cf0-4f87-aa68-66fa410dd518"), "Glenn Block, et al.", new DateTime(2021, 4, 27, 15, 33, 12, 958, DateTimeKind.Local).AddTicks(9021), null, null, "9781449337711", null, null, "Design and build Web APIs for a broad range of clients—including browsers and mobile devices—that can adapt to change over time. This practical, hands-on guide takes you through the theory and tools you need to build evolvable HTTP services with Microsoft’s ASP.NET Web API framework. In the process, you’ll learn how design and implement a real-world Web API.", "Designing Evolvable Web APIs with ASP.NET" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_BookMetaId",
                table: "BookCopies",
                column: "BookMetaId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_QRCode",
                table: "BookCopies",
                column: "QRCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookMetas_ISBN",
                table: "BookMetas",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookReservations_ApplicationUserId",
                table: "BookReservations",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReservations_BookCopyId",
                table: "BookReservations",
                column: "BookCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReservations_BookMetaId",
                table: "BookReservations",
                column: "BookMetaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookReservations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BookCopies");

            migrationBuilder.DropTable(
                name: "BookMetas");
        }
    }
}
