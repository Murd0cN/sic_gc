using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.Data
{
    public class ClosetContext : DbContext
    {
        public ClosetContext(DbContextOptions<ClosetContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Finish> Finishes { get; set; }
        public DbSet<DimensionsRestriction> DimensionsRestrictions { get; set; }
        public DbSet<MaterialRestriction> MaterialRestrictions { get; set; }
        public DbSet<PercentageRestriction> PercentageRestrictions { get; set; }
        /*public DbSet<Restriction> Restrictions { get; set; }*/
        public DbSet<DiscretePossibleValues> DiscretePossibleValues { get; set; }
        public DbSet<ContinuousPossibleValues> ContinuousPossibleValues { get; set; }
        public DbSet<PossibleDimensions> PossibleDimensions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRelationship> ProductRelationships { get; set; }
        public DbSet<ProductMaterialRelationship> ProductMaterialRelationships { get; set; }
    }
}
