using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EE.NIESolver.Web.Migrations
{
    public partial class updateexperimentresult26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                schema: "solver",
                table: "ExperimentResult",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DbExperimentRunParameter",
                schema: "solver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(nullable: true),
                    ExperimentReslutId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbExperimentRunParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbExperimentRunParameter_ExperimentResult_ExperimentReslutId",
                        column: x => x.ExperimentReslutId,
                        principalSchema: "solver",
                        principalTable: "ExperimentResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentResult_StatusId",
                schema: "solver",
                table: "ExperimentResult",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DbExperimentRunParameter_ExperimentReslutId",
                schema: "solver",
                table: "DbExperimentRunParameter",
                column: "ExperimentReslutId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentResult_Constant_StatusId",
                schema: "solver",
                table: "ExperimentResult",
                column: "StatusId",
                principalSchema: "commmon",
                principalTable: "Constant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentResult_Constant_StatusId",
                schema: "solver",
                table: "ExperimentResult");

            migrationBuilder.DropTable(
                name: "DbExperimentRunParameter",
                schema: "solver");

            migrationBuilder.DropIndex(
                name: "IX_ExperimentResult_StatusId",
                schema: "solver",
                table: "ExperimentResult");

            migrationBuilder.DropColumn(
                name: "StatusId",
                schema: "solver",
                table: "ExperimentResult");
        }
    }
}
