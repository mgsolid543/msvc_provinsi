using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using msvc_provinsi.Models.Skeleton;

namespace msvc_provinsi.Schema;

public partial class appContext : DbContext
{
    public appContext()
    {
    }

    public appContext(DbContextOptions<appContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Provinsi> Provinsis { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
  //      => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=Kolorijo123;database=latihandotnet", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

                var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                // Prepare configuration builder
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile($"appsettings.{envName}.json", optional: false)
                    .Build();
                optionsBuilder.UseMySql(configuration["ConnectionStrings:Auth"], Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Provinsi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("provinsi");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ibukota)
                .HasMaxLength(150)
                .HasColumnName("ibukota");
            entity.Property(e => e.Nama)
                .HasMaxLength(100)
                .HasColumnName("nama");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
