using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Blog.Core.Models;

namespace Blog.Core.Migrations
{
    [DbContext(typeof(BlogContext))]
    partial class BlogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Blog.Core.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Blog.Core.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<DateTime>("ModifyDate");

                    b.Property<string>("ModifyUser");

                    b.Property<bool>("Published");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Blog.Core.Models.PostCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CategoryId");

                    b.Property<Guid?>("PostId");

                    b.Property<Guid?>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PostId");

                    b.HasIndex("TagId");

                    b.ToTable("PostCategories");
                });

            modelBuilder.Entity("Blog.Core.Models.PostTag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CategoryId");

                    b.Property<Guid?>("PostId");

                    b.Property<Guid?>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PostId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("Blog.Core.Models.Reply", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<DateTime>("ModifyDate");

                    b.Property<string>("ModifyUser");

                    b.Property<Guid?>("PostId");

                    b.Property<bool>("Published");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("Blog.Core.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Blog.Core.Models.PostCategory", b =>
                {
                    b.HasOne("Blog.Core.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("Blog.Core.Models.Post", "Post")
                        .WithMany("PostCategories")
                        .HasForeignKey("PostId");

                    b.HasOne("Blog.Core.Models.Tag")
                        .WithMany("PostCategories")
                        .HasForeignKey("TagId");
                });

            modelBuilder.Entity("Blog.Core.Models.PostTag", b =>
                {
                    b.HasOne("Blog.Core.Models.Category")
                        .WithMany("PostTags")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Blog.Core.Models.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId");

                    b.HasOne("Blog.Core.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId");
                });

            modelBuilder.Entity("Blog.Core.Models.Reply", b =>
                {
                    b.HasOne("Blog.Core.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId");
                });
        }
    }
}
