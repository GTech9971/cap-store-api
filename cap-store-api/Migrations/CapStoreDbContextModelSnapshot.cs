﻿// <auto-generated />
using CapStore.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace cap_store_api.Migrations
{
    [DbContext(typeof(CapStoreDbContext))]
    partial class CapStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CapStore.Infrastructure.Ef.Categories.Data.CategoryData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("CapStore.Infrastructure.Ef.Components.Data.ComponentData", b =>
                {
                    b.Property<int>("ComponentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("component_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ComponentId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("MakerId")
                        .HasColumnType("integer")
                        .HasColumnName("maker_id");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("model_name");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ComponentId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MakerId");

                    b.ToTable("components");
                });

            modelBuilder.Entity("CapStore.Infrastructure.Ef.Components.Data.ComponentImageData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ComponentId")
                        .HasColumnType("integer")
                        .HasColumnName("component_id_fk");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("component_images");
                });

            modelBuilder.Entity("CapStore.Infrastructure.Ef.Makers.Data.MakerData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("makers");
                });

            modelBuilder.Entity("CapStore.Infrastructure.Ef.Components.Data.ComponentData", b =>
                {
                    b.HasOne("CapStore.Infrastructure.Ef.Categories.Data.CategoryData", "CategoryData")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CapStore.Infrastructure.Ef.Makers.Data.MakerData", "MakerData")
                        .WithMany()
                        .HasForeignKey("MakerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryData");

                    b.Navigation("MakerData");
                });

            modelBuilder.Entity("CapStore.Infrastructure.Ef.Components.Data.ComponentImageData", b =>
                {
                    b.HasOne("CapStore.Infrastructure.Ef.Components.Data.ComponentData", "Component")
                        .WithMany("ComponentImageDatas")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("CapStore.Infrastructure.Ef.Components.Data.ComponentData", b =>
                {
                    b.Navigation("ComponentImageDatas");
                });
#pragma warning restore 612, 618
        }
    }
}
