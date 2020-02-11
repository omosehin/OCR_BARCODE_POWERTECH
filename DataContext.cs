using Manual_Validation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Manual_Validation
{
    public class DataContext : DbContext
    {
        public DataContext() : base("ManualReadings")
        {

        }
        public DbSet<MeterReading> MeterReadings   { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}