using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // 1. All your DbSets stay here
    public virtual DbSet<Test> Tests { get; set; }
    public virtual DbSet<TestDepartment> TestDepartments { get; set; }
    public virtual DbSet<TestDetail> TestDetails { get; set; }
    public virtual DbSet<TestGroup> TestGroups { get; set; }
    public virtual DbSet<TestInstrumentSetting> TestInstrumentSettings { get; set; }
    public virtual DbSet<TestNormalValue> TestNormalValues { get; set; }
    public virtual DbSet<TestNormalValueGraph> TestNormalValueGraphs { get; set; }
    public virtual DbSet<TestParameter> TestParameters { get; set; }
    public virtual DbSet<TestParameterNormalValue> TestParameterNormalValues { get; set; }
    public virtual DbSet<TestProfile> TestProfiles { get; set; }
    public virtual DbSet<TestSetting> TestSettings { get; set; }
    public virtual DbSet<TestTemplate> TestTemplates { get; set; }
    public virtual DbSet<LISSpecimenSetting> LISSpecimenSettings { get; set; }
    public virtual DbSet<LISSpecimenType> LISSpecimenTypes { get; set; }

    // 2. The Magic Method
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // THIS ONE LINE replaces hundreds of lines of code.
        // It automatically finds every configuration file we just built
        // (TestConfiguration, TestGroupConfiguration, etc.) and applies them!
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
