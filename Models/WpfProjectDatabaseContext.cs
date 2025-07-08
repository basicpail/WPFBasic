using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WPFBasic.Models;

public partial class WpfProjectDatabaseContext : DbContext
{
    public WpfProjectDatabaseContext()
    {
    }

    public WpfProjectDatabaseContext(DbContextOptions<WpfProjectDatabaseContext> options)
        : base(options)
    {
    }

    //각각의 테이블이 DbSet로 클래스화가 되어서 이걸 가져다 쓸 수 있다.
    public virtual DbSet<GangnamguPopulation> GangnamguPopulations { get; set; }

    public virtual DbSet<TestTable> TestTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=WpfProjectDatabase;Username=postgres;Password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<GangnamguPopulation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gangnamgu_population_pkey");

            entity.ToTable("gangnamgu_population");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdministrativeAgency)
                .HasColumnType("character varying")
                .HasColumnName("administrative_agency");
            entity.Property(e => e.FemalePopulation).HasColumnName("female_population");
            entity.Property(e => e.MalePopulation).HasColumnName("male_population");
            entity.Property(e => e.NumberOfHouseholds).HasColumnName("number_of_households");
            entity.Property(e => e.NumberOfPeoplePerHousehold).HasColumnName("number_of_people_per_household");
            entity.Property(e => e.SexRatio).HasColumnName("sex_ratio");
            entity.Property(e => e.TotalPopulation).HasColumnName("total_population");
        });

        modelBuilder.Entity<TestTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TestTable_pkey");

            entity.ToTable("TestTable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
