using Microsoft.EntityFrameworkCore;

namespace AllPractice.Models;

public partial class W1Context : DbContext
{
    public W1Context()
    {
    }

    public W1Context(DbContextOptions<W1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Work> Works { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Work>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__work__3214EC070E0B8B20");

            entity.ToTable("work");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
