﻿// <auto-generated />
using System;
using InventoryManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryManagementAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InventoryManagementAPI.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("TenantId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("InventoryManagementAPI.Models.InventoryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InventoryItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Category A",
                            Description = "Description 1",
                            Name = "Item 1",
                            Price = 10m,
                            Quantity = 100,
                            TenantId = "tenant1"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Category B",
                            Description = "Description 2",
                            Name = "Item 2",
                            Price = 20m,
                            Quantity = 50,
                            TenantId = "tenant1"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Category A",
                            Description = "Description 3",
                            Name = "Item 3",
                            Price = 15m,
                            Quantity = 75,
                            TenantId = "tenant1"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Category C",
                            Description = "Description 4",
                            Name = "Item 4",
                            Price = 50m,
                            Quantity = 200,
                            TenantId = "tenant2"
                        },
                        new
                        {
                            Id = 5,
                            Category = "Category A",
                            Description = "Description 5",
                            Name = "Item 5",
                            Price = 68m,
                            Quantity = 80,
                            TenantId = "tenant1"
                        },
                        new
                        {
                            Id = 6,
                            Category = "Category B",
                            Description = "Description 6",
                            Name = "Item 6",
                            Price = 100m,
                            Quantity = 95,
                            TenantId = "tenant2"
                        },
                        new
                        {
                            Id = 7,
                            Category = "Category1",
                            Description = "Description1",
                            Name = "Item8",
                            Price = 100m,
                            Quantity = 10,
                            TenantId = "tenant1"
                        },
                        new
                        {
                            Id = 8,
                            Category = "Category2",
                            Description = "Description2",
                            Name = "Item9",
                            Price = 200m,
                            Quantity = 20,
                            TenantId = "tenant2"
                        },
                        new
                        {
                            Id = 9,
                            Category = "Category3",
                            Description = "Description3",
                            Name = "Item10",
                            Price = 300m,
                            Quantity = 30,
                            TenantId = "tenant1"
                        },
                        new
                        {
                            Id = 10,
                            Category = "Category4",
                            Description = "Description4",
                            Name = "Item11",
                            Price = 400m,
                            Quantity = 40,
                            TenantId = "tenant2"
                        });
                });

            modelBuilder.Entity("InventoryManagementAPI.Models.Tenant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tenants");

                    b.HasData(
                        new
                        {
                            Id = "tenant1",
                            Name = "ABC Company"
                        },
                        new
                        {
                            Id = "tenant2",
                            Name = "XYZ Company"
                        });
                });

            modelBuilder.Entity("InventoryManagementAPI.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Location = "Location A",
                            Name = "Warehouse A",
                            TenantId = "tenant1"
                        },
                        new
                        {
                            Id = 2,
                            Location = "Location B",
                            Name = "Warehouse B",
                            TenantId = "tenant2"
                        },
                        new
                        {
                            Id = 3,
                            Location = "Location C",
                            Name = "Warehouse C",
                            TenantId = "tenant2"
                        },
                        new
                        {
                            Id = 4,
                            Location = "Location A",
                            Name = "Warehouse D",
                            TenantId = "tenant1"
                        });
                });

            modelBuilder.Entity("InventoryManagementAPI.Models.WarehouseInventoryItem", b =>
                {
                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.Property<int>("InventoryItemId")
                        .HasColumnType("int");

                    b.HasKey("WarehouseId", "InventoryItemId");

                    b.HasIndex("InventoryItemId");

                    b.ToTable("WarehouseInventoryItems");

                    b.HasData(
                        new
                        {
                            WarehouseId = 1,
                            InventoryItemId = 1
                        },
                        new
                        {
                            WarehouseId = 1,
                            InventoryItemId = 2
                        },
                        new
                        {
                            WarehouseId = 2,
                            InventoryItemId = 1
                        },
                        new
                        {
                            WarehouseId = 3,
                            InventoryItemId = 3
                        },
                        new
                        {
                            WarehouseId = 4,
                            InventoryItemId = 4
                        },
                        new
                        {
                            WarehouseId = 3,
                            InventoryItemId = 6
                        },
                        new
                        {
                            WarehouseId = 1,
                            InventoryItemId = 7
                        },
                        new
                        {
                            WarehouseId = 3,
                            InventoryItemId = 8
                        },
                        new
                        {
                            WarehouseId = 4,
                            InventoryItemId = 9
                        },
                        new
                        {
                            WarehouseId = 4,
                            InventoryItemId = 10
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("InventoryManagementAPI.Models.ApplicationUser", b =>
                {
                    b.HasOne("InventoryManagementAPI.Models.Tenant", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("InventoryManagementAPI.Models.WarehouseInventoryItem", b =>
                {
                    b.HasOne("InventoryManagementAPI.Models.InventoryItem", "InventoryItem")
                        .WithMany("WarehouseInventoryItems")
                        .HasForeignKey("InventoryItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagementAPI.Models.Warehouse", "Warehouse")
                        .WithMany("WarehouseInventoryItems")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InventoryItem");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("InventoryManagementAPI.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("InventoryManagementAPI.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagementAPI.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("InventoryManagementAPI.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InventoryManagementAPI.Models.InventoryItem", b =>
                {
                    b.Navigation("WarehouseInventoryItems");
                });

            modelBuilder.Entity("InventoryManagementAPI.Models.Tenant", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("InventoryManagementAPI.Models.Warehouse", b =>
                {
                    b.Navigation("WarehouseInventoryItems");
                });
#pragma warning restore 612, 618
        }
    }
}
