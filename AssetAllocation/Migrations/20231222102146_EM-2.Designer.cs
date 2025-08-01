﻿// <auto-generated />
using System;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRM.Migrations
{
    [DbContext(typeof(AssetDbContext))]
    [Migration("20231222102146_EM-2")]
    partial class EM2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CRM.Models.AssetAllocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("AllocatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeAllocatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("AssetAllocation");
                });

            modelBuilder.Entity("CRM.Models.AssetMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AssetTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Condition")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LastUpdatedBy")
                        .HasColumnType("int");

                    b.Property<string>("ManufacturerNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PurchasedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("TeamverseAssetNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssetTypeId");

                    b.HasIndex("LastUpdatedBy");

                    b.ToTable("AssetMaster");
                });

            modelBuilder.Entity("CRM.Models.AssetTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AssetTypes");
                });

            modelBuilder.Entity("CRM.Models.EmployeeMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("Acc_No")
                        .HasColumnType("bigint");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DateOfJoining")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("LastUpdatedBy")
                        .HasColumnType("int");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("PrimarySkills")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UAN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.Property<string>("pan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("EmployeeMaster");
                });

            modelBuilder.Entity("CRM.Models.Leave", b =>
                {
                    b.Property<int>("LeaveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeaveId"), 1L, 1);

                    b.Property<int>("AdditionalLeaves")
                        .HasColumnType("int");

                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<int>("LeavesTaken")
                        .HasColumnType("int");

                    b.Property<int?>("Manager")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("RemainingLeaves")
                        .HasColumnType("int");

                    b.Property<int>("TotalLeaves")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("LeaveId");

                    b.HasIndex("EmpId");

                    b.ToTable("Leave");
                });

            modelBuilder.Entity("CRM.Models.LeaveRecord", b =>
                {
                    b.Property<int>("LeaveRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeaveRecordId"), 1L, 1);

                    b.Property<int>("ApprovedBy")
                        .HasColumnType("int");

                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LeaveReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeaveStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalLeaves")
                        .HasColumnType("int");

                    b.HasKey("LeaveRecordId");

                    b.HasIndex("ApprovedBy");

                    b.HasIndex("EmpId");

                    b.ToTable("LeaveRecord");
                });

            modelBuilder.Entity("CRM.Models.SalaryMaster", b =>
                {
                    b.Property<int>("Struct_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Struct_ID"), 1L, 1);

                    b.Property<DateTime>("ApplicableFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ApplicableTo")
                        .HasColumnType("datetime2");

                    b.Property<long>("AssuredPayout")
                        .HasColumnType("bigint");

                    b.Property<long>("BaseSalary_M")
                        .HasColumnType("bigint");

                    b.Property<long>("BaseSalary_Y")
                        .HasColumnType("bigint");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("EPF_M")
                        .HasColumnType("bigint");

                    b.Property<long>("EPF_Y")
                        .HasColumnType("bigint");

                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<long>("HRA_M")
                        .HasColumnType("bigint");

                    b.Property<long>("HRA_Y")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEnableEPF")
                        .HasColumnType("bit");

                    b.Property<long>("SpecialAllowance_M")
                        .HasColumnType("bigint");

                    b.Property<long>("SpecialAllowance_Y")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalCTC_M")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalCTC_Y")
                        .HasColumnType("bigint");

                    b.HasKey("Struct_ID");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("EmpId");

                    b.ToTable("SalaryMaster");
                });

            modelBuilder.Entity("CRM.Models.SalarySheet", b =>
                {
                    b.Property<int>("SID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SID"), 1L, 1);

                    b.Property<long>("BaseSalary_M")
                        .HasColumnType("bigint");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("EPF_M")
                        .HasColumnType("bigint");

                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<long>("GrossSalary_M")
                        .HasColumnType("bigint");

                    b.Property<long>("HRA_M")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsEnablePT")
                        .HasColumnType("bit");

                    b.Property<int>("LOPDays")
                        .HasColumnType("int");

                    b.Property<long>("NetSal")
                        .HasColumnType("bigint");

                    b.Property<int>("PaidDays")
                        .HasColumnType("int");

                    b.Property<long>("Part_B_ProfessionalTax_M")
                        .HasColumnType("bigint");

                    b.Property<long>("Part_B_ProfessionalTax_Y")
                        .HasColumnType("bigint");

                    b.Property<string>("SalMonth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SalYear")
                        .HasColumnType("int");

                    b.Property<long>("SpecialAllowances_M")
                        .HasColumnType("bigint");

                    b.Property<long>("TDS_M")
                        .HasColumnType("bigint");

                    b.Property<long>("TaxableIncome_M")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalDeduction_M")
                        .HasColumnType("bigint");

                    b.Property<int>("assuredPayoutPaid")
                        .HasColumnType("int");

                    b.Property<bool>("isAssuredPayoutPaid")
                        .HasColumnType("bit");

                    b.Property<int>("struct_Id")
                        .HasColumnType("int");

                    b.Property<int>("workingDays")
                        .HasColumnType("int");

                    b.HasKey("SID");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("EmpId");

                    b.HasIndex("struct_Id");

                    b.ToTable("SalarySheet");
                });

            modelBuilder.Entity("CRM.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CRM.Models.AssetAllocation", b =>
                {
                    b.HasOne("CRM.Models.AssetMaster", "AssetMaster")
                        .WithMany()
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Models.EmployeeMaster", "EmployeeMaster")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetMaster");

                    b.Navigation("EmployeeMaster");
                });

            modelBuilder.Entity("CRM.Models.AssetMaster", b =>
                {
                    b.HasOne("CRM.Models.AssetTypes", "AssetTypes")
                        .WithMany()
                        .HasForeignKey("AssetTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("LastUpdatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetTypes");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CRM.Models.EmployeeMaster", b =>
                {
                    b.HasOne("CRM.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("UsersId");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CRM.Models.Leave", b =>
                {
                    b.HasOne("CRM.Models.EmployeeMaster", "EmployeeMaster")
                        .WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeMaster");
                });

            modelBuilder.Entity("CRM.Models.LeaveRecord", b =>
                {
                    b.HasOne("CRM.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("ApprovedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Models.EmployeeMaster", "EmployeeMaster")
                        .WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeMaster");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CRM.Models.SalaryMaster", b =>
                {
                    b.HasOne("CRM.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Models.EmployeeMaster", "EmployeeMaster")
                        .WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeMaster");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CRM.Models.SalarySheet", b =>
                {
                    b.HasOne("CRM.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Models.EmployeeMaster", "EmployeeMaster")
                        .WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Models.SalaryMaster", "SalaryMaster")
                        .WithMany()
                        .HasForeignKey("struct_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeMaster");

                    b.Navigation("SalaryMaster");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
