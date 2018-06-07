﻿// <auto-generated />
using MediaManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;

namespace MediaManager.Migrations
{
    [DbContext(typeof(MediaContext))]
    [Migration("20180607024812_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("MediaManager.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Email")
                        .HasColumnName("Email");

                    b.Property<string>("Message")
                        .HasColumnName("Message");

                    b.HasKey("Id");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("MediaManager.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Email");

                    b.Property<string>("AuthToken")
                        .HasColumnName("AuthToken");

                    b.Property<string>("Name")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .HasColumnName("Password");

                    b.Property<string>("Pictures")
                        .HasColumnName("Pictures");

                    b.Property<string>("Role")
                        .HasColumnName("Role");

                    b.Property<string>("Songs")
                        .HasColumnName("Songs");

                    b.Property<string>("Videos")
                        .HasColumnName("Videos");

                    b.HasKey("Email");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}