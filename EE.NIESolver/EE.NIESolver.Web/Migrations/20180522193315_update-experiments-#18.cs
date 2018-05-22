using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EE.NIESolver.Web.Migrations
{
    public partial class updateexperiments18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "solver",
                table: "Experiment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "solver",
                table: "Experiment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "solver",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "solver",
                table: "Experiment");
        }
    }
}
