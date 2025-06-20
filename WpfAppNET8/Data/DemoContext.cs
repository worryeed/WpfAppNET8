using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WpfAppNET8.Models;

namespace WpfAppNET8.Data;

public partial class DemoContext : DbContext
{
    public DemoContext()
    {
    }

    public DemoContext(DbContextOptions<DemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=;Database=DEMO;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.НаименованиеМатериала);

            entity.Property(e => e.НаименованиеМатериала)
                .HasMaxLength(100)
                .HasColumnName("Наименование материала");
            entity.Property(e => e.ЕдиницаИзмерения)
                .HasMaxLength(255)
                .HasColumnName("Единица измерения");
            entity.Property(e => e.КоличествоВУпаковке).HasColumnName("Количество в упаковке");
            entity.Property(e => e.КоличествоНаСкладе).HasColumnName("Количество на складе");
            entity.Property(e => e.МинимальноеКоличество).HasColumnName("Минимальное количество");
            entity.Property(e => e.ТипМатериала)
                .HasMaxLength(255)
                .HasColumnName("Тип материала");
            entity.Property(e => e.ЦенаЕдиницыМатериала)
                .HasColumnType("money")
                .HasColumnName("Цена единицы материала");

            entity.HasOne(d => d.ТипМатериалаNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.ТипМатериала)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Materials_MaterialType");

            entity.HasMany(d => d.Поставщикs).WithMany(p => p.НаименованиеМатериалаs)
                .UsingEntity<Dictionary<string, object>>(
                    "MaterialSupplier",
                    r => r.HasOne<Supplier>().WithMany()
                        .HasForeignKey("Поставщик")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MaterialSuppliers_Suppliers"),
                    l => l.HasOne<Material>().WithMany()
                        .HasForeignKey("НаименованиеМатериала")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MaterialSuppliers_Materials"),
                    j =>
                    {
                        j.HasKey("НаименованиеМатериала", "Поставщик");
                        j.ToTable("MaterialSuppliers");
                        j.IndexerProperty<string>("НаименованиеМатериала")
                            .HasMaxLength(100)
                            .HasColumnName("Наименование материала");
                        j.IndexerProperty<string>("Поставщик").HasMaxLength(100);
                    });
        });

        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.ТипМатериала);

            entity.ToTable("MaterialType");

            entity.Property(e => e.ТипМатериала)
                .HasMaxLength(255)
                .HasColumnName("Тип материала");
            entity.Property(e => e.ПроцентПотериСырья).HasColumnName("Процент потери сырья  ");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ТипПродукции);

            entity.ToTable("ProductType");

            entity.Property(e => e.ТипПродукции)
                .HasMaxLength(255)
                .HasColumnName("Тип продукции");
            entity.Property(e => e.КоэффициентТипаПродукции).HasColumnName("Коэффициент типа продукции");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.НаименованиеПоставщика);

            entity.Property(e => e.НаименованиеПоставщика)
                .HasMaxLength(100)
                .HasColumnName("Наименование поставщика");
            entity.Property(e => e.ДатаНачалаРаботыСПоставщиком)
                .HasColumnType("datetime")
                .HasColumnName("Дата начала работы с поставщиком");
            entity.Property(e => e.Инн).HasColumnName("ИНН");
            entity.Property(e => e.ТипПоставщика)
                .HasMaxLength(255)
                .HasColumnName("Тип поставщика");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
