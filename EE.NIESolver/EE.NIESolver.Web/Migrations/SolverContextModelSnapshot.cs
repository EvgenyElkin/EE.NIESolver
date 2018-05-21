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
    partial class SolverContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Account.DbRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("Role","account");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Account.DbUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<string>("PassHash");

                    b.HasKey("Id");

                    b.ToTable("User","account");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Common.DbConstant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Constant","commmon");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Solver.DbExperiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MethodId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MethodId");

                    b.ToTable("Experiment","solver");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Solver.DbExperimentResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("Duration");

                    b.Property<int>("ExperimentId");

                    b.Property<string>("Result");

                    b.Property<int>("RunnerTypeId");

                    b.HasKey("Id");

                    b.HasIndex("ExperimentId");

                    b.HasIndex("RunnerTypeId");

                    b.ToTable("ExperimentResult","solver");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Solver.DbMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("MethodTypeId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MethodTypeId");

                    b.ToTable("Method","solver");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Solver.DbMethodParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<bool>("IsSystem");

                    b.Property<int>("MethodId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("ParameterTypeId");

                    b.HasKey("Id");

                    b.HasIndex("MethodId");

                    b.HasIndex("ParameterTypeId");

                    b.ToTable("MathodParameter","solver");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Solver.DbMethodParameterValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExperimentId");

                    b.Property<int>("ParameterId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ExperimentId");

                    b.HasIndex("ParameterId");

                    b.ToTable("MethodParameterValue","solver");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Solver.DbRunnerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("MethodTypeId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MethodTypeId");

                    b.ToTable("Runner","solver");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Account.DbRole", b =>
                {
                    b.HasOne("EE.NIESolver.DataLayer.Entities.Common.DbConstant", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EE.NIESolver.DataLayer.Entities.Account.DbUser", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Solver.DbExperiment", b =>
                {
                    b.HasOne("EE.NIESolver.DataLayer.Entities.Solver.DbMethod", "Method")
                        .WithMany()
                        .HasForeignKey("MethodId");
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Solver.DbExperimentResult", b =>
                {
                    b.HasOne("EE.NIESolver.DataLayer.Entities.Solver.DbExperiment", "Experiment")
                        .WithMany("Results")
                        .HasForeignKey("ExperimentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EE.NIESolver.DataLayer.Entities.Solver.DbRunnerType", "RunnerType")
                        .WithMany()
                        .HasForeignKey("RunnerTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Solver.DbMethod", b =>
                {
                    b.HasOne("EE.NIESolver.DataLayer.Entities.Common.DbConstant", "MethodType")
                        .WithMany()
                        .HasForeignKey("MethodTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Solver.DbMethodParameter", b =>
                {
                    b.HasOne("EE.NIESolver.DataLayer.Entities.Solver.DbMethod", "Method")
                        .WithMany("Parameteres")
                        .HasForeignKey("MethodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EE.NIESolver.DataLayer.Entities.Common.DbConstant", "ParameterType")
                        .WithMany()
                        .HasForeignKey("ParameterTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Solver.DbMethodParameterValue", b =>
                {
                    b.HasOne("EE.NIESolver.DataLayer.Entities.Solver.DbExperiment", "Experiment")
                        .WithMany("Values")
                        .HasForeignKey("ExperimentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EE.NIESolver.DataLayer.Entities.Solver.DbMethodParameter", "Parameter")
                        .WithMany()
                        .HasForeignKey("ParameterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EE.NIESolver.DataLayer.Entities.Solver.DbRunnerType", b =>
                {
                    b.HasOne("EE.NIESolver.DataLayer.Entities.Common.DbConstant", "MethodType")
                        .WithMany()
                        .HasForeignKey("MethodTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
