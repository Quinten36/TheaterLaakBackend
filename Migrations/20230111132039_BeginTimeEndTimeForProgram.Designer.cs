﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheaterLaakBackend.Controllers;

#nullable disable

namespace TheaterLaakBackend.Migrations
{
    [DbContext(typeof(TheaterDbContext))]
    [Migration("20230111132039_BeginTimeEndTimeForProgram")]
    partial class BeginTimeEndTimeForProgram
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("AccountGenre", b =>
                {
                    b.Property<int>("AccountsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IntrestsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AccountsId", "IntrestsId");

                    b.HasIndex("IntrestsId");

                    b.ToTable("AccountGenre");
                });

            modelBuilder.Entity("ArtistGroup", b =>
                {
                    b.Property<int>("ArtistsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GroupsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArtistsId", "GroupsId");

                    b.HasIndex("GroupsId");

                    b.ToTable("ArtistGroup");
                });

            modelBuilder.Entity("GenreProgram", b =>
                {
                    b.Property<int>("GenresId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProgramsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GenresId", "ProgramsId");

                    b.HasIndex("ProgramsId");

                    b.ToTable("GenreProgram", (string)null);
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDonator")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSubscribed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Website")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Hall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasPaid")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Program", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BeginExclusiveSale")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BeginSale")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsExclusive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<int>("HallId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasPaid")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("HallId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ForDisabled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HallId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Row")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SeatClass")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<double>("FirstClassPrice")
                        .HasColumnType("REAL");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HallId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProgramId")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("SecondClassPrice")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<double?>("ThirdClassPrice")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("HallId");

                    b.HasIndex("ProgramId");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SeatId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShowId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("OrderId");

                    b.HasIndex("SeatId");

                    b.HasIndex("ShowId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("AccountGenre", b =>
                {
                    b.HasOne("TheaterLaakBackend.Models.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheaterLaakBackend.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("IntrestsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArtistGroup", b =>
                {
                    b.HasOne("TheaterLaakBackend.Models.Artist", null)
                        .WithMany()
                        .HasForeignKey("ArtistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheaterLaakBackend.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreProgram", b =>
                {
                    b.HasOne("TheaterLaakBackend.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheaterLaakBackend.Models.Program", null)
                        .WithMany()
                        .HasForeignKey("ProgramsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Order", b =>
                {
                    b.HasOne("TheaterLaakBackend.Models.Account", "Account")
                        .WithMany("Orders")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Program", b =>
                {
                    b.HasOne("TheaterLaakBackend.Models.Group", "Group")
                        .WithMany("Programs")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Reservation", b =>
                {
                    b.HasOne("TheaterLaakBackend.Models.Account", "Account")
                        .WithMany("Reservations")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheaterLaakBackend.Models.Hall", "Hall")
                        .WithMany("Reservations")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Hall");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Seat", b =>
                {
                    b.HasOne("TheaterLaakBackend.Models.Hall", "Hall")
                        .WithMany("Seats")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Show", b =>
                {
                    b.HasOne("TheaterLaakBackend.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("TheaterLaakBackend.Models.Hall", "Hall")
                        .WithMany("Shows")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheaterLaakBackend.Models.Program", "Program")
                        .WithMany("Shows")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Hall");

                    b.Navigation("Program");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Ticket", b =>
                {
                    b.HasOne("TheaterLaakBackend.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheaterLaakBackend.Models.Order", "Order")
                        .WithMany("Tickets")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheaterLaakBackend.Models.Seat", "Seat")
                        .WithMany("Tickets")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheaterLaakBackend.Models.Show", "Show")
                        .WithMany()
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Order");

                    b.Navigation("Seat");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Account", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Group", b =>
                {
                    b.Navigation("Programs");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Hall", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("Seats");

                    b.Navigation("Shows");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Order", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Program", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("TheaterLaakBackend.Models.Seat", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
