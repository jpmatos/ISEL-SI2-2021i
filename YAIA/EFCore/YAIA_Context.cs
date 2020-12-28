using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EFCore
{
    public partial class YAIA_Context : DbContext
    {
        public YAIA_Context()
        {
        }

        public YAIA_Context(DbContextOptions<YAIA_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Contributor> Contributors { get; set; }
        public virtual DbSet<CreditNote> CreditNotes { get; set; }
        public virtual DbSet<CreditNoteState> CreditNoteStates { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceContributor> InvoiceContributors { get; set; }
        public virtual DbSet<InvoiceHistory> InvoiceHistories { get; set; }
        public virtual DbSet<InvoiceState> InvoiceStates { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemCredit> ItemCredits { get; set; }
        public virtual DbSet<ItemHistory> ItemHistories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        
        public static string UserId { get; set; }
        public static string Password { get; set; }
        public const string DataSource = "localhost";
        public const string InitialCatalog = "SI2_Grupo02_2021i";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($"Data Source={DataSource};Initial Catalog={InitialCatalog};User id={UserId}; Password={Password}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Contributor>(entity =>
            {
                entity.HasKey(e => e.Nif)
                    .HasName("PK__Contribu__C7DEC3316C1C76D9");

                entity.ToTable("Contributor");

                entity.Property(e => e.Nif)
                    .ValueGeneratedNever()
                    .HasColumnName("NIF");

                entity.Property(e => e.Address)
                    .HasMaxLength(128)
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CreditNote>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__CreditNo__357D4CF88379936A");

                entity.ToTable("CreditNote");

                entity.Property(e => e.Code)
                    .HasMaxLength(12)
                    .HasColumnName("code");

                entity.Property(e => e.CodeInvoice)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("codeInvoice");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.EmissionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("emission_date");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("state");

                entity.Property(e => e.TotalIva)
                    .HasColumnType("money")
                    .HasColumnName("total_IVA");

                entity.Property(e => e.TotalValue)
                    .HasColumnType("money")
                    .HasColumnName("total_value");

                entity.HasOne(d => d.CodeInvoiceNavigation)
                    .WithMany(p => p.CreditNotes)
                    .HasForeignKey(d => d.CodeInvoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CreditNot__codeI__5F7E2DAC");

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.CreditNotes)
                    .HasForeignKey(d => d.State)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CreditNot__state__607251E5");
            });

            modelBuilder.Entity<CreditNoteState>(entity =>
            {
                entity.HasKey(e => e.State)
                    .HasName("PK__CreditNo__A9360BC2C9955AE5");

                entity.ToTable("CreditNoteState");

                entity.Property(e => e.State)
                    .HasMaxLength(16)
                    .HasColumnName("state");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Invoice__357D4CF8CB130522");

                entity.ToTable("Invoice");

                entity.Property(e => e.Code)
                    .HasMaxLength(12)
                    .HasColumnName("code");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.EmissionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("emission_date");

                entity.Property(e => e.Nif).HasColumnName("NIF");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("state");

                entity.Property(e => e.TotalIva)
                    .HasColumnType("money")
                    .HasColumnName("total_IVA");

                entity.Property(e => e.TotalValue)
                    .HasColumnType("money")
                    .HasColumnName("total_value");

                entity.HasOne(d => d.NifNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.Nif)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invoice__NIF__55009F39");

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.State)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invoice__state__55F4C372");
            });

            modelBuilder.Entity<InvoiceContributor>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("InvoiceContributor");

                entity.Property(e => e.Address)
                    .HasMaxLength(128)
                    .HasColumnName("address");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("code");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.EmissionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("emission_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.Nif).HasColumnName("NIF");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("state");

                entity.Property(e => e.TotalIva)
                    .HasColumnType("money")
                    .HasColumnName("total_IVA");

                entity.Property(e => e.TotalValue)
                    .HasColumnType("money")
                    .HasColumnName("total_value");
            });

            modelBuilder.Entity<InvoiceHistory>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.AlterationDate })
                    .HasName("invoice_history_id");

                entity.ToTable("InvoiceHistory");

                entity.Property(e => e.Code)
                    .HasMaxLength(12)
                    .HasColumnName("code");

                entity.Property(e => e.AlterationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("alteration_date");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.EmissionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("emission_date");

                entity.Property(e => e.Nif).HasColumnName("NIF");

                entity.Property(e => e.State)
                    .HasMaxLength(16)
                    .HasColumnName("state");

                entity.Property(e => e.TotalIva)
                    .HasColumnType("money")
                    .HasColumnName("total_IVA");

                entity.Property(e => e.TotalValue)
                    .HasColumnType("money")
                    .HasColumnName("total_value");

                entity.HasOne(d => d.CodeNavigation)
                    .WithMany(p => p.InvoiceHistories)
                    .HasForeignKey(d => d.Code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InvoiceHis__code__671F4F74");
            });

            modelBuilder.Entity<InvoiceState>(entity =>
            {
                entity.HasKey(e => e.State)
                    .HasName("PK__InvoiceS__A9360BC2477E9F04");

                entity.ToTable("InvoiceState");

                entity.Property(e => e.State)
                    .HasMaxLength(16)
                    .HasColumnName("state");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.Sku })
                    .HasName("number");

                entity.ToTable("Item");

                entity.Property(e => e.Code)
                    .HasMaxLength(12)
                    .HasColumnName("code");

                entity.Property(e => e.Sku)
                    .HasMaxLength(10)
                    .HasColumnName("SKU");

                entity.Property(e => e.Description)
                    .HasMaxLength(128)
                    .HasColumnName("description");

                entity.Property(e => e.Discount)
                    .HasColumnType("money")
                    .HasColumnName("discount");

                entity.Property(e => e.Iva)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("IVA");

                entity.Property(e => e.SalePrice)
                    .HasColumnType("money")
                    .HasColumnName("sale_price");

                entity.Property(e => e.Units).HasColumnName("units");

                entity.HasOne(d => d.CodeNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.Code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Item__code__5BAD9CC8");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.Sku)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Item__SKU__5CA1C101");
            });

            modelBuilder.Entity<ItemCredit>(entity =>
            {
                entity.HasKey(e => new { e.CreditCode, e.InvoiceCode, e.Sku })
                    .HasName("item_credit_id");

                entity.ToTable("ItemCredit");

                entity.Property(e => e.CreditCode)
                    .HasMaxLength(12)
                    .HasColumnName("credit_code");

                entity.Property(e => e.InvoiceCode)
                    .HasMaxLength(12)
                    .HasColumnName("invoice_code");

                entity.Property(e => e.Sku)
                    .HasMaxLength(10)
                    .HasColumnName("SKU");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.CreditCodeNavigation)
                    .WithMany(p => p.ItemCredits)
                    .HasForeignKey(d => d.CreditCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ItemCredi__credi__634EBE90");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemCredits)
                    .HasForeignKey(d => new { d.InvoiceCode, d.Sku })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ItemCredit__6442E2C9");
            });

            modelBuilder.Entity<ItemHistory>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.AlterationDate, e.Sku })
                    .HasName("item_history_id");

                entity.ToTable("ItemHistory");

                entity.Property(e => e.Code)
                    .HasMaxLength(12)
                    .HasColumnName("code");

                entity.Property(e => e.AlterationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("alteration_date");

                entity.Property(e => e.Sku)
                    .HasMaxLength(10)
                    .HasColumnName("SKU");

                entity.Property(e => e.Description)
                    .HasMaxLength(128)
                    .HasColumnName("description");

                entity.Property(e => e.Discount)
                    .HasColumnType("money")
                    .HasColumnName("discount");

                entity.Property(e => e.Iva)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("IVA");

                entity.Property(e => e.SalePrice)
                    .HasColumnType("money")
                    .HasColumnName("sale_price");

                entity.Property(e => e.Units).HasColumnName("units");

                entity.HasOne(d => d.InvoiceHistory)
                    .WithMany(p => p.ItemHistories)
                    .HasForeignKey(d => new { d.Code, d.AlterationDate })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ItemHistory__69FBBC1F");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Sku)
                    .HasName("PK__Product__CA1ECF0C2CC9A3FD");

                entity.ToTable("Product");

                entity.Property(e => e.Sku)
                    .HasMaxLength(10)
                    .HasColumnName("SKU");

                entity.Property(e => e.Description)
                    .HasMaxLength(128)
                    .HasColumnName("description");

                entity.Property(e => e.Iva)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("IVA");

                entity.Property(e => e.SalePrice)
                    .HasColumnType("money")
                    .HasColumnName("sale_price");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
