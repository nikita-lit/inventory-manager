using IM.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IM.Data;

public class InventoryDbContext( DbContextOptions<InventoryDbContext> options ) : DbContext( options )
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderLine> OrderLines => Set<OrderLine>();

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Category>( entity =>
        {
            entity.Property( x => x.Name ).HasMaxLength( 100 ).IsRequired();
            entity.HasIndex( x => x.Name ).IsUnique();
        } );

        modelBuilder.Entity<Supplier>( entity =>
        {
            entity.Property( x => x.Name ).HasMaxLength( 200 ).IsRequired();
            entity.Property( x => x.Email ).HasMaxLength( 254 );
            entity.Property( x => x.Phone ).HasMaxLength( 50 );
        } );

        modelBuilder.Entity<Product>( entity =>
        {
            entity.Property( x => x.Sku ).HasMaxLength( 50 ).IsRequired();
            entity.HasIndex( x => x.Sku ).IsUnique();
            entity.Property( x => x.Name ).HasMaxLength( 200 ).IsRequired();
            entity.Property( x => x.Price ).HasPrecision( 18, 2 );
            entity.Property( x => x.Version ).IsRowVersion();
            entity.HasOne( x => x.Category ).WithMany( x => x.Products )
                .HasForeignKey( x => x.CategoryId ).OnDelete( DeleteBehavior.Restrict );
            entity.HasOne( x => x.Supplier ).WithMany( x => x.Products )
                .HasForeignKey( x => x.SupplierId ).OnDelete( DeleteBehavior.Restrict );
            entity.ToTable( table =>
            {
                table.HasCheckConstraint( "CK_Products_Price", "\"Price\" >= 0" );
                table.HasCheckConstraint( "CK_Products_MinimumStock", "\"MinimumStock\" >= 0" );
            } );
        } );

        modelBuilder.Entity<StockMovement>( entity =>
        {
            entity.Property( x => x.Reason ).HasMaxLength( 500 );
            entity.HasIndex( x => new { x.ProductId, x.OccurredAt } );
            entity.HasOne( x => x.Product ).WithMany( x => x.StockMovements )
                .HasForeignKey( x => x.ProductId ).OnDelete( DeleteBehavior.Restrict );
            entity.HasOne( x => x.OrderLine ).WithOne( x => x.StockMovement )
                .HasForeignKey<StockMovement>( x => x.OrderLineId ).OnDelete( DeleteBehavior.Restrict );
            entity.ToTable( table =>
            {
                table.HasCheckConstraint( "CK_StockMovements_Quantity", "\"Quantity\" > 0" );
                table.HasCheckConstraint( "CK_StockMovements_Direction", "\"Direction\" IN (1, 2)" );
                table.HasCheckConstraint( "CK_StockMovements_OrderDirection",
                    "\"OrderLineId\" IS NULL OR \"Direction\" = 2" );
            } );
        } );

        modelBuilder.Entity<Order>().HasIndex( x => x.CreatedAt );

        modelBuilder.Entity<OrderLine>( entity =>
        {
            entity.Property( x => x.UnitPrice ).HasPrecision( 18, 2 );
            entity.HasIndex( x => new { x.OrderId, x.ProductId } ).IsUnique();
            entity.HasOne( x => x.Order ).WithMany( x => x.Lines )
                .HasForeignKey( x => x.OrderId ).OnDelete( DeleteBehavior.Restrict );
            entity.HasOne( x => x.Product ).WithMany()
                .HasForeignKey( x => x.ProductId ).OnDelete( DeleteBehavior.Restrict );
            entity.HasOne( x => x.Category ).WithMany()
                .HasForeignKey( x => x.CategoryId ).OnDelete( DeleteBehavior.Restrict );
            entity.ToTable( table =>
            {
                table.HasCheckConstraint( "CK_OrderLines_Quantity", "\"Quantity\" > 0" );
                table.HasCheckConstraint( "CK_OrderLines_UnitPrice", "\"UnitPrice\" >= 0" );
            } );
        } );
    }
}
