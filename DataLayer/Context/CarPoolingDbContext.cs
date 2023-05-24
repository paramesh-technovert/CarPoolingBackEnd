using System;
using System.Collections.Generic;
using CarPoolingWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPoolingWebAPI.Context;

public partial class CarPoolingDbContext : DbContext
{
    public CarPoolingDbContext()
    {
    }

    public CarPoolingDbContext(DbContextOptions<CarPoolingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookedRide> BookedRides { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<LoginDetail> LoginDetails { get; set; }

    public virtual DbSet<OfferRide> OfferRides { get; set; }

    public virtual DbSet<Stop> Stops { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookedRide>(entity =>
        {
            entity.HasKey(e => e.BookingId);

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.BoardingStopNavigation).WithMany(p => p.BookedRideBoardingStopNavigations)
                .HasForeignKey(d => d.BoardingStop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookedRides_City");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookedRides)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookedRides_UserDetails");

            entity.HasOne(d => d.DestinationStopNavigation).WithMany(p => p.BookedRideDestinationStopNavigations)
                .HasForeignKey(d => d.DestinationStop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookedRides_City1");

            entity.HasOne(d => d.Ride).WithMany(p => p.BookedRides)
                .HasForeignKey(d => d.RideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookedRides_OfferRides");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<LoginDetail>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_LoginDetails").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<OfferRide>(entity =>
        {
            entity.HasKey(e => e.RideId);

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DestinationId).HasColumnName("DestinationID");
            entity.Property(e => e.RideProviderId).HasColumnName("RideProviderID");
            entity.Property(e => e.SourceId).HasColumnName("SourceID");

            entity.HasOne(d => d.Destination).WithMany(p => p.OfferRideDestinations)
                .HasForeignKey(d => d.DestinationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OfferRides_City1");

            entity.HasOne(d => d.RideProvider).WithMany(p => p.OfferRides)
                .HasForeignKey(d => d.RideProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OfferRides_UserDetails");

            entity.HasOne(d => d.Source).WithMany(p => p.OfferRideSources)
                .HasForeignKey(d => d.SourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OfferRides_City");
        });

        modelBuilder.Entity<Stop>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Ride).WithMany(p => p.Stops)
                .HasForeignKey(d => d.RideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stops_OfferRides");

            entity.HasOne(d => d.StopNavigation).WithMany(p => p.Stops)
                .HasForeignKey(d => d.StopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stops_City");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsFixedLength();
            entity.Property(e => e.PhoneNumber).HasColumnType("numeric(10, 0)");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.UserDetail)
                .HasForeignKey<UserDetail>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDetails_LoginDetails");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
