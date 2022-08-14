using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace yplBackend.Model
{
    public partial class ypldbContext : DbContext
    {
        public ypldbContext()
        {
        }

        public ypldbContext(DbContextOptions<ypldbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appuser> Appusers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Combination> Combinations { get; set; }
        public virtual DbSet<Element> Elements { get; set; }
        public virtual DbSet<ElementInCombination> ElementInCombinations { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Occasion> Occasions { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-311346U;Database=ypldb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Appuser>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__Appuser__D2D146378CB73B25");

                entity.ToTable("Appuser");

                entity.Property(e => e.IdUser)
                    .HasMaxLength(100)
                    .HasColumnName("id_user");

                entity.Property(e => e.Chest).HasColumnName("chest");

                entity.Property(e => e.Collar).HasColumnName("collar");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Hips).HasColumnName("hips");

                entity.Property(e => e.InsideLeg).HasColumnName("inside_leg");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Psw)
                    .HasMaxLength(100)
                    .HasColumnName("psw");

                entity.Property(e => e.Sleeve).HasColumnName("sleeve");

                entity.Property(e => e.Waist).HasColumnName("waist");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PK__Category__E548B673B3D7D1CF");

                entity.ToTable("Category");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.NameCategory)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name_category");
            });

            modelBuilder.Entity<Combination>(entity =>
            {
                entity.HasKey(e => e.IdCombination)
                    .HasName("PK__Combinat__93C1475B04ECFE46");

                entity.ToTable("Combination");

                entity.Property(e => e.IdCombination).HasColumnName("id_combination");

                entity.Property(e => e.IdOccasion).HasColumnName("id_occasion");

                entity.Property(e => e.IdUser)
                    .HasMaxLength(100)
                    .HasColumnName("id_user");

                entity.Property(e => e.IsPosted).HasColumnName("is_posted");

                entity.Property(e => e.NameCombination)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name_combination");

                entity.Property(e => e.Note)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.HasOne(d => d.IdOccasionNavigation)
                    .WithMany(p => p.Combinations)
                    .HasForeignKey(d => d.IdOccasion)
                    .HasConstraintName("FK__Combinati__id_oc__382F5661");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Combinations)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__Combinati__id_us__39237A9A");
            });

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.IdElement)
                    .HasName("PK__Element__A5D5DC34022AC82E");

                entity.ToTable("Element");

                entity.Property(e => e.IdElement).HasColumnName("id_element");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.IdUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("id_user");

                entity.Property(e => e.NameElement)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name_element");

                entity.Property(e => e.Note)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.Pic)
                    .IsUnicode(false)
                    .HasColumnName("pic");

                entity.Property(e => e.PicPath)
                    .IsUnicode(false)
                    .HasColumnName("pic_path");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Elements)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Element__id_cate__345EC57D");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Elements)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Element__id_user__3552E9B6");
            });

            modelBuilder.Entity<ElementInCombination>(entity =>
            {
                entity.ToTable("ElementInCombination");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCombination).HasColumnName("id_combination");

                entity.Property(e => e.IdElement).HasColumnName("id_element");

                entity.HasOne(d => d.IdCombinationNavigation)
                    .WithMany(p => p.ElementInCombinations)
                    .HasForeignKey(d => d.IdCombination)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ElementIn__id_co__3CF40B7E");

                entity.HasOne(d => d.IdElementNavigation)
                    .WithMany(p => p.ElementInCombinations)
                    .HasForeignKey(d => d.IdElement)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__ElementIn__id_el__3BFFE745");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdFriend)
                    .HasMaxLength(100)
                    .HasColumnName("id_friend");

                entity.Property(e => e.IdUser)
                    .HasMaxLength(100)
                    .HasColumnName("id_user");

                entity.Property(e => e.IsAccepted).HasColumnName("is_accepted");

                entity.HasOne(d => d.IdFriendNavigation)
                    .WithMany(p => p.FriendIdFriendNavigations)
                    .HasForeignKey(d => d.IdFriend)
                    .HasConstraintName("FK__Friends__id_frie__5006DFF2");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.FriendIdUserNavigations)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__Friends__id_user__4F12BBB9");
            });

            modelBuilder.Entity<Occasion>(entity =>
            {
                entity.HasKey(e => e.IdOccasion)
                    .HasName("PK__Occasion__B377412EFD3AE2DF");

                entity.ToTable("Occasion");

                entity.Property(e => e.IdOccasion).HasColumnName("id_occasion");

                entity.Property(e => e.NameOccasion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name_occasion");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.IdRating)
                    .HasName("PK__Rating__12074E478C48B474");

                entity.ToTable("Rating");

                entity.Property(e => e.IdRating).HasColumnName("id_rating");

                entity.Property(e => e.IdCombination).HasColumnName("id_combination");

                entity.Property(e => e.IdUser)
                    .HasMaxLength(100)
                    .HasColumnName("id_user");

                entity.Property(e => e.Rating1).HasColumnName("rating");

                entity.HasOne(d => d.IdCombinationNavigation)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.IdCombination)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Rating__id_combi__3FD07829");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Rating__id_user__40C49C62");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
