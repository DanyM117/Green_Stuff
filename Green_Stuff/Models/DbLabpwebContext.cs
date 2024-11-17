using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Green_Stuff.Models;

public partial class DbLabpwebContext : DbContext
{
    public DbLabpwebContext()
    {
    }

    public DbLabpwebContext(DbContextOptions<DbLabpwebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Advertisement> Advertisements { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ExpositionToSun> ExpositionToSuns { get; set; }

    public virtual DbSet<PaymentCard> PaymentCards { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<WaterDemmand> WaterDemmands { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.IdAdd).HasName("PK__Addresse__2A7DD232B3627316");

            entity.Property(e => e.IdAdd).HasColumnName("ID_Add");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(true)
                .HasColumnName("Enabled_");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_");

            entity.HasOne(d => d.oUsers).WithMany(p => p.oAddresses)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Addresses__IDUse__4336F4B9");
        });

        modelBuilder.Entity<Advertisement>(entity =>
        {
            entity.HasKey(e => e.IdAdvertisement).HasName("PK__Advertis__512B1D2D1E61517E");

            entity.Property(e => e.IdAdvertisement).HasColumnName("ID_Advertisement");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Description_");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(260)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.oUsers).WithMany(p => p.oAdvertisements)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Advertise__IDUse__61BB7BD9");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__Categori__6DB3A68A361F1C96");

            entity.Property(e => e.IdCategory).HasColumnName("ID_Category");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Description_");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(true)
                .HasColumnName("Enabled_");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_");
        });

        modelBuilder.Entity<ExpositionToSun>(entity =>
        {
            entity.HasKey(e => e.IdSun).HasName("PK__Expositi__27F89E94ABB0A5CB");

            entity.ToTable("ExpositionToSun");

            entity.Property(e => e.IdSun).HasColumnName("ID_Sun");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Description_");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(true)
                .HasColumnName("Enabled_");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_");
        });

        modelBuilder.Entity<PaymentCard>(entity =>
        {
            entity.HasKey(e => e.IdCard).HasName("PK__Payment___72140EDFEB2EEBF8");

            entity.ToTable("Payment_Cards");

            entity.Property(e => e.IdCard).HasColumnName("ID_Card");
            entity.Property(e => e.Cvv).HasColumnName("CVV");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.NameOnCard)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.oUsers).WithMany(p => p.oPaymentCards)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment_C__IDUse__405A880E");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.IdSale).HasName("PK__Sales__2071DEA3219218B3");

            entity.Property(e => e.IdSale).HasColumnName("ID_Sale");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Idcard).HasColumnName("IDCard");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.TotalAmmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalQuantity).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdcardNavigation).WithMany(p => p.oSales)
                .HasForeignKey(d => d.Idcard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__IDCard__47FBA9D6");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.oSales)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__IDUser__4707859D");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(e => new { e.Idsale, e.Iditem });

            entity.ToTable("Sale_Detail");

            entity.Property(e => e.Idsale).HasColumnName("IDSale");
            entity.Property(e => e.Iditem).HasColumnName("IDItem");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.oWarehouses).WithMany(p => p.oSaleDetails)
                .HasForeignKey(d => d.Iditem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sale_Deta__IDIte__4BCC3ABA");

            entity.HasOne(d => d.oSales).WithMany(p => p.oSaleDetails)
                .HasForeignKey(d => d.Idsale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sale_Deta__IDSal__4AD81681");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.IdSize).HasName("PK__Sizes__EB77DAF052DC534D");

            entity.Property(e => e.IdSize).HasColumnName("ID_Size");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Description_");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(true)
                .HasColumnName("Enabled_");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__ED4DE442E8499BBB");

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.IduserType).HasColumnName("IDUserType");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(260)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password_");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.oUserTypes).WithMany(p => p.oUsers)
                .HasForeignKey(d => d.IduserType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__IDUserTyp__3D7E1B63");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.IdUserType).HasName("PK__UserType__486BFCDEC6364CE4");

            entity.ToTable("UserType");

            entity.Property(e => e.IdUserType).HasColumnName("ID_UserType");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(true)
                .HasColumnName("Enabled_");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.IdItem).HasName("PK__Warehous__87415D07D6DBF739");

            entity.ToTable("Warehouse");

            entity.Property(e => e.IdItem).HasColumnName("ID_Item");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Description_");
            entity.Property(e => e.Idcategory).HasColumnName("IDCategory");
            entity.Property(e => e.Idsize).HasColumnName("IDSize");
            entity.Property(e => e.Idsun).HasColumnName("IDSun");
            entity.Property(e => e.Idwater).HasColumnName("IDWater");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(260)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.oCategories).WithMany(p => p.oWarehouses)
                .HasForeignKey(d => d.Idcategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Warehouse__IDCat__37C5420D");

            entity.HasOne(d => d.oSizes).WithMany(p => p.oWarehouses)
                .HasForeignKey(d => d.Idsize)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Warehouse__IDSiz__34E8D562");

            entity.HasOne(d => d.oExpositionToSun).WithMany(p => p.oWarehouses)
                .HasForeignKey(d => d.Idsun)
                .HasConstraintName("FK__Warehouse__IDSun__35DCF99B");

            entity.HasOne(d => d.oWaterDemmand).WithMany(p => p.oWarehouses)
                .HasForeignKey(d => d.Idwater)
                .HasConstraintName("FK__Warehouse__IDWat__36D11DD4");
        });

        modelBuilder.Entity<WaterDemmand>(entity =>
        {
            entity.HasKey(e => e.IdWater).HasName("PK__WaterDem__03D348F0F04A0D40");

            entity.ToTable("WaterDemmand");

            entity.Property(e => e.IdWater).HasColumnName("ID_Water");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Description_");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(true)
                .HasColumnName("Enabled_");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
