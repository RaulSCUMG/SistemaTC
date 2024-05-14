﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaTC.Data;

#nullable disable

namespace SistemaTC.Data.Migrations
{
    [DbContext(typeof(TCContext))]
    partial class TCContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RolePermission", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("char(36)");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.CreditCard", b =>
                {
                    b.Property<Guid>("CreditCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("ActivationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<int>("BalanceCutOffDay")
                        .HasColumnType("int");

                    b.Property<string>("Ccv")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<decimal>("ChargeRate")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("CreditAvailable")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CreditLimit")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("ExpirationDate")
                        .HasColumnType("date");

                    b.Property<bool>("Expired")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Locked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LockedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateOnly>("NextBalanceCutOffDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("NextPaymentDate")
                        .HasColumnType("date");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(19)
                        .IsUnicode(true)
                        .HasColumnType("varchar(19)");

                    b.Property<int>("PaymentDay")
                        .HasColumnType("int");

                    b.Property<string>("Pin")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("CreditCardId");

                    b.HasIndex("Active");

                    b.HasIndex("Number");

                    b.HasIndex("UserId");

                    b.ToTable("CreditCard", (string)null);
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.CreditCardTransaction", b =>
                {
                    b.Property<Guid>("CreditCardTransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("CreditCardId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CreditCutOffId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("CreditCardTransactionId");

                    b.HasIndex("CreditCardId");

                    b.HasIndex("CreditCutOffId");

                    b.HasIndex("Type");

                    b.HasIndex("UserId");

                    b.ToTable("CreditCardTransaction", (string)null);
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.CreditCutOff", b =>
                {
                    b.Property<Guid>("CreditCutOffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Closed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("CreditCardId")
                        .HasColumnType("char(36)");

                    b.Property<ushort>("Month")
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Payed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<decimal>("PayedAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalBalance")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalCredit")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalDebit")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<ushort>("Year")
                        .HasColumnType("smallint unsigned");

                    b.HasKey("CreditCutOffId");

                    b.HasIndex("CreditCardId");

                    b.HasIndex("UserId");

                    b.ToTable("CreditCutOff", (string)null);
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.Payment", b =>
                {
                    b.Property<Guid>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("CreditCardId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreditCutOffId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("PaymentId");

                    b.HasIndex("CreditCardId");

                    b.HasIndex("CreditCutOffId");

                    b.HasIndex("Type");

                    b.HasIndex("UserId");

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.Permission", b =>
                {
                    b.Property<Guid>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(true)
                        .HasColumnType("varchar(25)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("PermissionId");

                    b.HasIndex("Active");

                    b.HasIndex("Code");

                    b.ToTable("Permission", (string)null);
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.Request", b =>
                {
                    b.Property<Guid>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Approved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<Guid?>("AssignedToUserId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("InternalNote")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("longtext");

                    b.Property<string>("Note")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("longtext");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<Guid>("RequestedByUserId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("RequestId");

                    b.HasIndex("AssignedToUserId");

                    b.HasIndex("Number");

                    b.HasIndex("RequestedByUserId");

                    b.HasIndex("Type");

                    b.ToTable("Request", (string)null);
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(true)
                        .HasColumnType("varchar(25)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("RoleId");

                    b.HasIndex("Active");

                    b.HasIndex("Code");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("e9f77193-3d01-4a99-aa01-6fc777d5a87a"),
                            Active = false,
                            Code = "admin",
                            Created = new DateTime(2024, 5, 11, 0, 36, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "System",
                            Name = "Administrador"
                        });
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(true)
                        .HasColumnType("varchar(15)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("varchar(100)");

                    b.HasKey("UserId");

                    b.HasIndex("Active");

                    b.HasIndex("Email");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserName");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.UserToken", b =>
                {
                    b.Property<Guid>("UserTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserTokenId");

                    b.HasIndex("Active");

                    b.HasIndex("UserId");

                    b.ToTable("UserToken", (string)null);
                });

            modelBuilder.Entity("RolePermission", b =>
                {
                    b.HasOne("SistemaTC.Data.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaTC.Data.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.CreditCard", b =>
                {
                    b.HasOne("SistemaTC.Data.Entities.User", "Owner")
                        .WithMany("CreditCards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.CreditCardTransaction", b =>
                {
                    b.HasOne("SistemaTC.Data.Entities.CreditCard", "CreditCard")
                        .WithMany("CreditCardTransactions")
                        .HasForeignKey("CreditCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaTC.Data.Entities.CreditCutOff", "CreditCutOff")
                        .WithMany("CreditCardTransactions")
                        .HasForeignKey("CreditCutOffId");

                    b.HasOne("SistemaTC.Data.Entities.User", "Owner")
                        .WithMany("CreditCardTransactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreditCard");

                    b.Navigation("CreditCutOff");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.CreditCutOff", b =>
                {
                    b.HasOne("SistemaTC.Data.Entities.CreditCard", "CreditCard")
                        .WithMany("CreditCutOffs")
                        .HasForeignKey("CreditCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaTC.Data.Entities.User", "Owner")
                        .WithMany("CreditCutOffs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreditCard");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.Payment", b =>
                {
                    b.HasOne("SistemaTC.Data.Entities.CreditCard", "CreditCard")
                        .WithMany("Payments")
                        .HasForeignKey("CreditCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaTC.Data.Entities.CreditCutOff", "CreditCutOff")
                        .WithMany("Payments")
                        .HasForeignKey("CreditCutOffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaTC.Data.Entities.User", "Owner")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreditCard");

                    b.Navigation("CreditCutOff");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.Request", b =>
                {
                    b.HasOne("SistemaTC.Data.Entities.User", "AssignedTo")
                        .WithMany("RequestsAssigned")
                        .HasForeignKey("AssignedToUserId");

                    b.HasOne("SistemaTC.Data.Entities.User", "RequestedBy")
                        .WithMany("Requests")
                        .HasForeignKey("RequestedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedTo");

                    b.Navigation("RequestedBy");
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.User", b =>
                {
                    b.HasOne("SistemaTC.Data.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.UserToken", b =>
                {
                    b.HasOne("SistemaTC.Data.Entities.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.CreditCard", b =>
                {
                    b.Navigation("CreditCardTransactions");

                    b.Navigation("CreditCutOffs");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.CreditCutOff", b =>
                {
                    b.Navigation("CreditCardTransactions");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SistemaTC.Data.Entities.User", b =>
                {
                    b.Navigation("CreditCardTransactions");

                    b.Navigation("CreditCards");

                    b.Navigation("CreditCutOffs");

                    b.Navigation("Payments");

                    b.Navigation("Requests");

                    b.Navigation("RequestsAssigned");

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
