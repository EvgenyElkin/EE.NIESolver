using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EE.NIESolver.Web.Migrations
{
    public partial class initial : Migration
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
                name: "Method",
                schema: "solver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: true),
                    MethodTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Method", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Method_Constant_MethodTypeId",
                        column: x => x.MethodTypeId,
                        principalSchema: "commmon",
                        principalTable: "Constant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Runner",
                schema: "solver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: true),
                    MethodTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Runner_Constant_MethodTypeId",
                        column: x => x.MethodTypeId,
                        principalSchema: "commmon",
                        principalTable: "Constant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiment",
                schema: "solver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MethodId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiment_Method_MethodId",
                        column: x => x.MethodId,
                        principalSchema: "solver",
                        principalTable: "Method",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsSystem = table.Column<bool>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ExperimentResult",
                schema: "solver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    ExperimentId = table.Column<int>(nullable: false),
                    Result = table.Column<string>(nullable: true),
                    RunnerTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperimentResult_Experiment_ExperimentId",
                        column: x => x.ExperimentId,
                        principalSchema: "solver",
                        principalTable: "Experiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperimentResult_Runner_RunnerTypeId",
                        column: x => x.RunnerTypeId,
                        principalSchema: "solver",
                        principalTable: "Runner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MethodParameterValue",
                schema: "solver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ExperimentId = table.Column<int>(nullable: false),
                    ParameterId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodParameterValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MethodParameterValue_Experiment_ExperimentId",
                        column: x => x.ExperimentId,
                        principalSchema: "solver",
                        principalTable: "Experiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MethodParameterValue_MathodParameter_ParameterId",
                        column: x => x.ParameterId,
                        principalSchema: "solver",
                        principalTable: "MathodParameter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleId",
                schema: "account",
                table: "Role",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiment_MethodId",
                schema: "solver",
                table: "Experiment",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentResult_ExperimentId",
                schema: "solver",
                table: "ExperimentResult",
                column: "ExperimentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentResult_RunnerTypeId",
                schema: "solver",
                table: "ExperimentResult",
                column: "RunnerTypeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Method_MethodTypeId",
                schema: "solver",
                table: "Method",
                column: "MethodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodParameterValue_ExperimentId",
                schema: "solver",
                table: "MethodParameterValue",
                column: "ExperimentId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodParameterValue_ParameterId",
                schema: "solver",
                table: "MethodParameterValue",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_Runner_MethodTypeId",
                schema: "solver",
                table: "Runner",
                column: "MethodTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role",
                schema: "account");

            migrationBuilder.DropTable(
                name: "ExperimentResult",
                schema: "solver");

            migrationBuilder.DropTable(
                name: "MethodParameterValue",
                schema: "solver");

            migrationBuilder.DropTable(
                name: "User",
                schema: "account");

            migrationBuilder.DropTable(
                name: "Runner",
                schema: "solver");

            migrationBuilder.DropTable(
                name: "Experiment",
                schema: "solver");

            migrationBuilder.DropTable(
                name: "MathodParameter",
                schema: "solver");

            migrationBuilder.DropTable(
                name: "Method",
                schema: "solver");

            migrationBuilder.DropTable(
                name: "Constant",
                schema: "commmon");
        }
    }
}
