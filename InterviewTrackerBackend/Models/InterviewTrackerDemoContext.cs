using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrackerBackend.Models;

public partial class InterviewTrackerDemoContext : DbContext
{
    public InterviewTrackerDemoContext()
    {
    }

    public InterviewTrackerDemoContext(DbContextOptions<InterviewTrackerDemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ManagerTbl> ManagerTbls { get; set; }

    public virtual DbSet<PanelTbl> PanelTbls { get; set; }
    public virtual DbSet<GetPanelByDate> GetPanelByDates { get; set; }

    public virtual DbSet<GetPanelsByMgr> GetPanelByMgr { get; set; }

    public virtual DbSet<GetPanelsByManagers> GetPanelsByManagers{get;set;}
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured){

        }
    }
        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ManagerTbl>(entity =>
        {
            entity.HasKey(e => e.ManagerId);

            entity.ToTable("Tbl_Manager");

            entity.Property(e => e.ManagerId)
                .ValueGeneratedNever()
                .HasColumnName("Manager_Id");
            entity.Property(e => e.ManagerName)
                .HasMaxLength(50)
                .HasColumnName("Manager_Name");
        });


        modelBuilder.Entity<TempManagerTbl>(entity =>
        {
            entity.HasKey(e => e.ManagerId);

            entity.ToTable("Tbl_TempManager");

            entity.Property(e => e.ManagerId)
                .ValueGeneratedNever()
                .HasColumnName("Manager_Id");

            entity.Property(e => e.ManagerName)
                .HasMaxLength(50)
                .HasColumnName("Manager_Name");    
        });

        modelBuilder.Entity<PanelTbl>(entity =>
        {
            entity.HasKey(e => e.PanelId);

            entity.ToTable("Tbl_Panel");

            entity.Property(e => e.PanelId)
                .ValueGeneratedNever()
                .HasColumnName("Panel_Id");
            entity.Property(e => e.AvailableFrom)
                .HasColumnType("date")
                .HasColumnName("Available_From");
            entity.Property(e => e.AvailableTo)
                .HasColumnType("date")
                .HasColumnName("Available_To");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .HasColumnName("Email_Id");
            entity.Property(e => e.ManagerId).HasColumnName("Manager_Id");
            entity.Property(e => e.PanelName)
                .HasMaxLength(50)
                .HasColumnName("Panel_Name");
            entity.Property(e => e.Skills).HasMaxLength(50);

            entity.HasOne(d => d.Manager).WithMany(p => p.PanelTbls)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Panel_Tbl_Manager_Tbl");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
