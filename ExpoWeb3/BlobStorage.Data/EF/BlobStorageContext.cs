using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlobStorage.Data.EF;

public partial class BlobStorageContext : DbContext
{
    public BlobStorageContext()
    {
    }

    public BlobStorageContext(DbContextOptions<BlobStorageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LogAlbum> LogAlbums { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:storageserverblob.database.windows.net;Database=BlobStorage;Trusted_Connection=False;Encrypt=True;User ID=BlobStorageUser;Password=Azure123.;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogAlbum>(entity =>
        {
            entity.ToTable("logAlbum");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.NombreImagen)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreUser)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
