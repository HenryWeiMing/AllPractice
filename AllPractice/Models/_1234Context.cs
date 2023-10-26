using Microsoft.EntityFrameworkCore;

namespace AllPractice.Models;

public partial class _1234Context : DbContext
{
    public _1234Context()
    {
    }

    public _1234Context(DbContextOptions<_1234Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("W4_pkey");

            entity.ToTable("COMPANY");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"W4_Id_seq\"'::regclass)")
                .HasColumnName("ID");
            entity.Property(e => e.Info).HasColumnName("INFO");
            entity.Property(e => e.Name).HasColumnName("NAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
