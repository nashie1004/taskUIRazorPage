using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using taskui.Models;

namespace taskui.Contexts
{
    public partial class ReleaseNoteContext : DbContext
    {
        public virtual DbSet<Detail> Detail { get; set; } = null!;
        public virtual DbSet<Header> Header { get; set; } = null!;

        public ReleaseNoteContext()
        {
        }

        public ReleaseNoteContext(DbContextOptions<ReleaseNoteContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Detail>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Header)
                    .WithMany(p => p.Detail)
                    .HasForeignKey(d => d.HeaderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Detail_Header");
            });

            modelBuilder.Entity<Header>(entity =>
            {
                entity.Property(e => e.LongDescription)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.ReleaseName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
