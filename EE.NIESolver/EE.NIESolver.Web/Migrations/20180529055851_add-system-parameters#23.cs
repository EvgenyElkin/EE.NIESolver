using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EE.NIESolver.Web.Migrations
{
    public partial class addsystemparameters23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemMethodParameter",
                schema: "solver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MethodTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ParameterTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemMethodParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemMethodParameter_Constant_MethodTypeId",
                        column: x => x.MethodTypeId,
                        principalSchema: "commmon",
                        principalTable: "Constant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemMethodParameter_Constant_ParameterTypeId",
                        column: x => x.ParameterTypeId,
                        principalSchema: "commmon",
                        principalTable: "Constant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemMethodParameter_MethodTypeId",
                schema: "solver",
                table: "SystemMethodParameter",
                column: "MethodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemMethodParameter_ParameterTypeId",
                schema: "solver",
                table: "SystemMethodParameter",
                column: "ParameterTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemMethodParameter",
                schema: "solver");
        }
    }
}
