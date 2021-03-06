﻿// <auto-generated />
using EE.NIESolver.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EE.NIESolver.Web.Migrations
{
    [DbContext(typeof(SolverContext))]
    [Migration("20180419180945_SolverInit")]
    partial class SolverInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Account.RoleEntity", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("Role","account");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Account.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<string>("PassHash");

                    b.HasKey("Id");

                    b.ToTable("User","account");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Common.ConstantEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Constant","commmon");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Account.RoleEntity", b =>
                {
                    b.HasOne("EE.NIESolver.DataLayer.Entities.Common.ConstantEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EE.NIESolver.DataLayer.Entities.Account.UserEntity", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
