﻿// <auto-generated />
using DemoMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DemoMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250716083548_Create_Table")]
    partial class Create_Table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.6");

            modelBuilder.Entity("DemoMVC.Models.Entity.DaiLy", b =>
                {
                    b.Property<string>("MaDaiLy")
                        .HasColumnType("TEXT");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DienThoai")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MaHTPP")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NguoiDaiDien")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TenDaiLy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MaDaiLy");

                    b.HasIndex("MaHTPP");

                    b.ToTable("DaiLys");
                });

            modelBuilder.Entity("DemoMVC.Models.Entity.HeThongPhanPhoi", b =>
                {
                    b.Property<string>("MaHTPP")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenHTPP")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MaHTPP");

                    b.ToTable("HeThongPhanPhois");
                });

            modelBuilder.Entity("DemoMVC.Models.Entity.Person", b =>
                {
                    b.Property<string>("PersonID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PersonID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("DemoMVC.Models.Entity.Student", b =>
                {
                    b.Property<string>("StudentID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StudentID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DemoMVC.Models.Entity.DaiLy", b =>
                {
                    b.HasOne("DemoMVC.Models.Entity.HeThongPhanPhoi", "HeThongPhanPhoi")
                        .WithMany("DaiLys")
                        .HasForeignKey("MaHTPP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HeThongPhanPhoi");
                });

            modelBuilder.Entity("DemoMVC.Models.Entity.HeThongPhanPhoi", b =>
                {
                    b.Navigation("DaiLys");
                });
#pragma warning restore 612, 618
        }
    }
}
