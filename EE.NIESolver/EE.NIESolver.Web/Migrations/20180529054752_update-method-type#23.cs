using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EE.NIESolver.Web.Migrations
{
    public partial class updatemethodtype23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MathodParameter_Method_MethodId",
                schema: "solver",
                table: "MathodParameter");

            migrationBuilder.DropForeignKey(
                name: "FK_MathodParameter_Constant_ParameterTypeId",
                schema: "solver",
                table: "MathodParameter");

            migrationBuilder.DropForeignKey(
                name: "FK_MethodParameterValue_MathodParameter_ParameterId",
                schema: "solver",
                table: "MethodParameterValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MathodParameter",
                schema: "solver",
                table: "MathodParameter");

            migrationBuilder.RenameTable(
                name: "MathodParameter",
                schema: "solver",
                newName: "MethodParameter");

            migrationBuilder.RenameIndex(
                name: "IX_MathodParameter_ParameterTypeId",
                schema: "solver",
                table: "MethodParameter",
                newName: "IX_MethodParameter_ParameterTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_MathodParameter_MethodId",
                schema: "solver",
                table: "MethodParameter",
                newName: "IX_MethodParameter_MethodId");

            migrationBuilder.AddColumn<string>(
                name: "MethodExpression",
                schema: "solver",
                table: "Method",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MethodParameter",
                schema: "solver",
                table: "MethodParameter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MethodParameter_Method_MethodId",
                schema: "solver",
                table: "MethodParameter",
                column: "MethodId",
                principalSchema: "solver",
                principalTable: "Method",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MethodParameter_Constant_ParameterTypeId",
                schema: "solver",
                table: "MethodParameter",
                column: "ParameterTypeId",
                principalSchema: "commmon",
                principalTable: "Constant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MethodParameterValue_MethodParameter_ParameterId",
                schema: "solver",
                table: "MethodParameterValue",
                column: "ParameterId",
                principalSchema: "solver",
                principalTable: "MethodParameter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MethodParameter_Method_MethodId",
                schema: "solver",
                table: "MethodParameter");

            migrationBuilder.DropForeignKey(
                name: "FK_MethodParameter_Constant_ParameterTypeId",
                schema: "solver",
                table: "MethodParameter");

            migrationBuilder.DropForeignKey(
                name: "FK_MethodParameterValue_MethodParameter_ParameterId",
                schema: "solver",
                table: "MethodParameterValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MethodParameter",
                schema: "solver",
                table: "MethodParameter");

            migrationBuilder.DropColumn(
                name: "MethodExpression",
                schema: "solver",
                table: "Method");

            migrationBuilder.RenameTable(
                name: "MethodParameter",
                schema: "solver",
                newName: "MathodParameter");

            migrationBuilder.RenameIndex(
                name: "IX_MethodParameter_ParameterTypeId",
                schema: "solver",
                table: "MathodParameter",
                newName: "IX_MathodParameter_ParameterTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_MethodParameter_MethodId",
                schema: "solver",
                table: "MathodParameter",
                newName: "IX_MathodParameter_MethodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MathodParameter",
                schema: "solver",
                table: "MathodParameter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MathodParameter_Method_MethodId",
                schema: "solver",
                table: "MathodParameter",
                column: "MethodId",
                principalSchema: "solver",
                principalTable: "Method",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MathodParameter_Constant_ParameterTypeId",
                schema: "solver",
                table: "MathodParameter",
                column: "ParameterTypeId",
                principalSchema: "commmon",
                principalTable: "Constant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MethodParameterValue_MathodParameter_ParameterId",
                schema: "solver",
                table: "MethodParameterValue",
                column: "ParameterId",
                principalSchema: "solver",
                principalTable: "MathodParameter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
