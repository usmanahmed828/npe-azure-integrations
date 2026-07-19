using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NPE.Infrastructure.Modules.Users.Entities;

namespace NPE.Infrastructure.Modules.iLock.Entities;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ILockApplication> ILockApplications { get; set; }

    public virtual DbSet<ILockCompany> ILockCompanies { get; set; }

    public virtual DbSet<ILockGlobalDef> ILockGlobalDefs { get; set; }

    public virtual DbSet<ILockGroup> ILockGroups { get; set; }

    //public virtual DbSet<ILockGroupRight> ILockGroupRights { get; set; }

    public virtual DbSet<ILockGroupUIObject> ILockGroupUiobjects { get; set; }

    public virtual DbSet<ILockGroupUser> ILockGroupUsers { get; set; }

    public virtual DbSet<ILockIdentity> ILockIdentities { get; set; }

    public virtual DbSet<ILockLocation> ILockLocations { get; set; }

    //public virtual DbSet<ILockRight> ILockRights { get; set; }

    //public virtual DbSet<ILockRightUIObject> ILockRightUiobjects { get; set; }

    public virtual DbSet<ILockUIContainer> ILockUicontainers { get; set; }

    public virtual DbSet<ILockUIObject> ILockUiobjects { get; set; }

    public virtual DbSet<ILockUser> ILockUsers { get; set; }

    public virtual DbSet<ILockUserAudit> ILockUserAudits { get; set; }

    public virtual DbSet<ILockUserHistory> ILockUserHistories { get; set; }

    public virtual DbSet<ILockUserLocation> ILockUserLocations { get; set; }

    public virtual DbSet<ILockWeb> ILockWebs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ILockApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK_Application");

            entity.Property(e => e.ApplicationId).ValueGeneratedNever();
        });

        modelBuilder.Entity<ILockCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK_iLock_Division");

            entity.ToTable("iLock_Company", tb =>
                {
                    tb.HasTrigger("trg_iLock_Delete_Company");
                    tb.HasTrigger("trg_iLock_Update_Company");
                    tb.HasTrigger("trg_iLock_insert_CompanyGroupUser");
                });

            entity.Property(e => e.CompanyId).ValueGeneratedNever();
        });

        modelBuilder.Entity<ILockGroup>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.CompanyId }).HasName("PK_Ilock_Group");

            entity.HasOne(d => d.Company).WithMany(p => p.ILockGroups).HasConstraintName("FK_iLock_Group_iLock_Company");
        });

        //modelBuilder.Entity<ILockGroupRight>(entity =>
        //{
        //    entity.HasOne(d => d.ILockGroup).WithMany(p => p.ILockGroupRights).HasConstraintName("FK_iLock_GroupRight_iLock_Group");

        //    entity.HasOne(d => d.ILockRight).WithMany(p => p.ILockGroupRights)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_iLock_GroupRight_iLock_Right");
        //});

        modelBuilder.Entity<ILockGroupUser>(entity =>
        {
            entity.ToTable("iLock_GroupUser", tb => tb.HasTrigger("InsertAdminEmployee"));

            entity.HasOne(d => d.ILockGroup).WithMany(p => p.ILockGroupUsers).HasConstraintName("FK_iLock_GroupUser_iLock_Group");
        });

        modelBuilder.Entity<ILockLocation>(entity =>
        {
            entity.HasKey(e => new { e.LocationId, e.CompanyId }).HasName("PK_Ilock_Location");
        });

        //modelBuilder.Entity<ILockRight>(entity =>
        //{
        //    entity.HasKey(e => new { e.RightId, e.CompanyId }).HasName("PK_Ilock_Right");

        //    entity.HasOne(d => d.Company).WithMany(p => p.ILockRights).HasConstraintName("FK_iLock_Right_iLock_Company");
        //});

        modelBuilder.Entity<ILockUIContainer>(entity =>
        {
            entity.HasKey(e => e.UIContainerId).HasName("PK_iLock_ParentObject");

            entity.Property(e => e.UIContainerId).ValueGeneratedNever();

            entity.HasOne(d => d.Application).WithMany(p => p.ILockUIContainers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Ilock_ParentObject_iLock_Application");
        });

        modelBuilder.Entity<ILockUIObject>(entity =>
        {
            entity.HasOne(d => d.UIContainer).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_iLock_UIObject_iLock_UIContainer");
        });

        modelBuilder.Entity<ILockUser>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CompanyId }).HasName("PK_Ilock_User");

            entity.ToTable("iLock_User", tb => tb.HasTrigger("ti_UserWebSettings"));
        });

        modelBuilder.Entity<ILockUserAudit>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("pk_iLock_User_audit");

            entity.Property(e => e.AppName).HasDefaultValueSql("(app_name())");
            entity.Property(e => e.AuditType).IsFixedLength();
            entity.Property(e => e.HostIpaddress).HasDefaultValueSql("([dbo].[GetHostIPAddress]())");
            entity.Property(e => e.HostName).HasDefaultValueSql("(host_name())");
        });

        modelBuilder.Entity<ILockUserHistory>(entity =>
        {
            entity.HasKey(e => new { e.HistroyId, e.CompanyId }).HasName("PK_Ilock_LogHistory");

            entity.HasOne(d => d.ILockUser).WithMany(p => p.ILockUserHistories).HasConstraintName("FK_Ilock_UserHistory_Ilock_User");
        });

        modelBuilder.Entity<ILockWeb>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
