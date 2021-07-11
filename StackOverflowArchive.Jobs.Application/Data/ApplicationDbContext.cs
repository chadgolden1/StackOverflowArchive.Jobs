using Microsoft.EntityFrameworkCore;
using StackOverflowArchive.Jobs.Application.Models;

#nullable disable

namespace StackOverflowArchive.Jobs.Application.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasIndex(e => e.PostTypeId, "IX_PostsMissingIndex");

                entity.HasIndex(e => e.Score, "NonClusteredIndex-20210609-213145");

                entity.HasIndex(e => e.Title, "NonClusteredIndex-20210609-222321_Title");

                entity.HasIndex(e => e.ViewCount, "NonClusteredIndex-20210609-222808_ViewCount");

                entity.HasIndex(e => e.OwnerUserId, "NonClusteredIndex-20210609-224015_OwnerUserId");

                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CommunityOwnedDate).HasColumnType("datetime");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");

                entity.Property(e => e.LastEditDate).HasColumnType("datetime");

                entity.Property(e => e.LastEditorDisplayName).HasMaxLength(40);

                entity.Property(e => e.Tags).HasMaxLength(150);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
