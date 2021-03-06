// <auto-generated />
using System;
using EITShippingPlanner.Core.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EITShippingPlanner.Core.Infrastructure.Database.Migrations
{
    [DbContext(typeof(EITShippingPlannerDbContext))]
    [Migration("20220113061122_InitializeDataTable")]
    partial class InitializeDataTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EITShippingPlanner.Core.Model.CargoCenterLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Code");

                    b.HasIndex("Id");

                    b.ToTable("CargoCenterLocations");
                });

            modelBuilder.Entity("EITShippingPlanner.Core.Model.ExtraCharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsSupported")
                        .HasColumnType("bit");

                    b.Property<int>("ParcelType")
                        .HasColumnType("int");

                    b.Property<float>("Percentage")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ParcelType");

                    b.ToTable("ExtraCharges");
                });

            modelBuilder.Entity("EITShippingPlanner.Core.Model.ParcelPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("LowerWeight")
                        .HasColumnType("real");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("UpperWeight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("ParcelPrices");
                });

            modelBuilder.Entity("EITShippingPlanner.Core.Model.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FirstLocationId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSegment")
                        .HasColumnType("int");

                    b.Property<int?>("SecondLocationId")
                        .HasColumnType("int");

                    b.Property<int>("TransportationType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FirstLocationId");

                    b.HasIndex("Id");

                    b.HasIndex("SecondLocationId");

                    b.HasIndex("TransportationType");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("EITShippingPlanner.Core.Model.Route", b =>
                {
                    b.HasOne("EITShippingPlanner.Core.Model.CargoCenterLocation", "FirstLocation")
                        .WithMany("RouteFrom")
                        .HasForeignKey("FirstLocationId");

                    b.HasOne("EITShippingPlanner.Core.Model.CargoCenterLocation", "SecondLocation")
                        .WithMany("RouteTo")
                        .HasForeignKey("SecondLocationId");
                });
#pragma warning restore 612, 618
        }
    }
}
