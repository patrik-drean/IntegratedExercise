using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using firestorm.Models;
using System.Data.Entity;

namespace firestorm.DAL
{
    public class Thunderstorm : DbContext
    {
        public Thunderstorm() : base("Thunderstorm")
        {

        }

        public DbSet<Assay> Assays { get; set; }
        public DbSet<AssayTest> AssayTests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Compound> Compounds { get; set; }
        public DbSet<CompoundSample> CompoundSamples { get; set; }
        public DbSet<ConditionalTest> ConditionalTests { get; set; }
        public DbSet<DataReport> DataReports { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<SampleTest> SampleTests { get; set; }
        public DbSet<NewTestTube> NewTestTube { get; set; }
        public DbSet<SummaryReport> SummaryReports { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestMaterial> TestMaterials { get; set; }
        public DbSet<TestReport> TestReports { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }

    }
}