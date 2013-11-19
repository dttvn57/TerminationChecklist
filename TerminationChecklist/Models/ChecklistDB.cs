using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TerminationChecklist.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TerminationChecklist.Models
{
    public class TerminationChecklistDB : DbContext
    {
        public TerminationChecklistDB()
            : base("name=DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TerminationChecklistDB, TerminationChecklist.Migrations.Configuration>("DefaultConnection"));
            //  Database.SetInitializer<PerformersDB>(new DropCreateDatabaseAlways<TerminationChecklistDB>());
        }

        public DbSet<Checklist> Checklists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
