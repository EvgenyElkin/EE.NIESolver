using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EE.NIESolver.Web.Migrations
{
    public partial class SOLVER6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.EnsureSchema(
                name: "commmon");

            migrationBuilder.EnsureSchema(
                name: "solver");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Login = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PassHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Constant",
                schema: "commmon",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Method",
                schema: "solver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Method", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "account",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Role_Constant_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "commmon",
                        principalTable: "Constant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "account",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MathodParameter",
                schema: "solver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MethodId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ParameterTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MathodParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MathodParameter_Method_MethodId",
                        column: x => x.MethodId,
                        principalSchema: "solver",
                        principalTable: "Method",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MathodParameter_Constant_ParameterTypeId",
                        column: x => x.ParameterTypeId,
                        principalSchema: "commmon",
                        principalTable: "Constant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleId",
                schema: "account",
                table: "Role",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MathodParameter_MethodId",
                schema: "solver",
                table: "MathodParameter",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MathodParameter_ParameterTypeId",
                schema: "solver",
                table: "MathodParameter",
                column: "ParameterTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role",
                schema: "account");

            migrationBuilder.DropTable(
                name: "MathodParameter",
                schema: "solver");

            migrationBuilder.DropTable(
                name: "User",
                schema: "account");

            migrationBuilder.DropTable(
                name: "Method",
                schema: "solver");

            migrationBuilder.DropTable(
                name: "Constant",
                schema: "commmon");
        }
    }
}
