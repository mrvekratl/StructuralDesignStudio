using Microsoft.EntityFrameworkCore;
using StructuralDesignStudio.EntityLayer.Concrete;
using StructuralDesignStudio.EntityLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.DataAccessLayer.Concrete.Context
{
    public class StructuralContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=StructuralDesignStudioDB;Integrated Security=true;TrustServerCertificate=true;");
        }
        public DbSet<StructuralColumn> StructuralColumns { get; set; }
        public DbSet<StructuralBeam> StructuralBeams { get; set; }
        public DbSet<StructuralSlab> StructuralSlabs { get; set; }
        public DbSet<StructuralMaterial> StructuralMaterials { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TPH (Table Per Hierarchy) - Tek tablo stratejisi
            modelBuilder.Entity<StructuralElement>()
                .HasDiscriminator<string>("ElementType")
                .HasValue<StructuralColumn>("Column")
                .HasValue<StructuralBeam>("Beam")
                .HasValue<StructuralSlab>("Slab");

            modelBuilder.Entity<StructuralMaterial>().HasData(
     new StructuralMaterial { MaterialId = 1, MaterialName = "C25 Concrete", Density = 2.4, CompressiveStrength = 25, MaterialType = MaterialType.Concrete },
     new StructuralMaterial { MaterialId = 2, MaterialName = "C30 Concrete", Density = 2.5, CompressiveStrength = 30, MaterialType = MaterialType.Concrete },
     new StructuralMaterial { MaterialId = 3, MaterialName = "C35 Concrete", Density = 2.5, CompressiveStrength = 35, MaterialType = MaterialType.Concrete },
     new StructuralMaterial { MaterialId = 4, MaterialName = "C40 Concrete", Density = 2.6, CompressiveStrength = 40, MaterialType = MaterialType.Concrete },
     new StructuralMaterial { MaterialId = 5, MaterialName = "C45 Concrete", Density = 2.6, CompressiveStrength = 45, MaterialType = MaterialType.Concrete },
     new StructuralMaterial { MaterialId = 6, MaterialName = "S355 Steel", Density = 7.85, CompressiveStrength = 355, MaterialType = MaterialType.Steel },
     new StructuralMaterial { MaterialId = 7, MaterialName = "S420 Steel", Density = 7.85, CompressiveStrength = 420, MaterialType = MaterialType.Steel },
     new StructuralMaterial { MaterialId = 8, MaterialName = "S500 Steel", Density = 7.85, CompressiveStrength = 500, MaterialType = MaterialType.Steel },
     new StructuralMaterial { MaterialId = 9, MaterialName = "Lightweight Concrete", Density = 1.8, CompressiveStrength = 20, MaterialType = MaterialType.Concrete },
     new StructuralMaterial { MaterialId = 10, MaterialName = "High Strength Concrete", Density = 2.7, CompressiveStrength = 60, MaterialType = MaterialType.Concrete }
 );
            modelBuilder.Entity<StructuralColumn>().HasData(
    new StructuralColumn { Id = 1, Name = "C1", FloorLevel = 1, Width = 40, Depth = 40, Height = 300, MaterialID = 2, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralColumn { Id = 2, Name = "C2", FloorLevel = 1, Width = 45, Depth = 45, Height = 300, MaterialID = 3, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralColumn { Id = 3, Name = "C3", FloorLevel = 2, Width = 50, Depth = 50, Height = 300, MaterialID = 4, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralColumn { Id = 4, Name = "C4", FloorLevel = 2, Width = 60, Depth = 60, Height = 300, MaterialID = 5, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralColumn { Id = 5, Name = "C5", FloorLevel = 3, Width = 40, Depth = 50, Height = 320, MaterialID = 2, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralColumn { Id = 6, Name = "C6", FloorLevel = 3, Width = 45, Depth = 55, Height = 320, MaterialID = 3, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralColumn { Id = 7, Name = "C7", FloorLevel = 4, Width = 50, Depth = 60, Height = 320, MaterialID = 4, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralColumn { Id = 8, Name = "C8", FloorLevel = 4, Width = 55, Depth = 65, Height = 320, MaterialID = 5, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralColumn { Id = 9, Name = "C9", FloorLevel = 5, Width = 60, Depth = 60, Height = 350, MaterialID = 10, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralColumn { Id = 10, Name = "C10", FloorLevel = 5, Width = 65, Depth = 65, Height = 350, MaterialID = 10, CreatedDate = new DateTime(2025, 1, 1) }
);
            modelBuilder.Entity<StructuralBeam>().HasData(
    new StructuralBeam { Id = 11, Name = "B1", FloorLevel = 1, Width = 30, Height = 50, Length = 600, MaterialID = 2, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralBeam { Id = 12, Name = "B2", FloorLevel = 1, Width = 30, Height = 60, Length = 650, MaterialID = 3, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralBeam { Id = 13, Name = "B3", FloorLevel = 2, Width = 35, Height = 60, Length = 700, MaterialID = 4, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralBeam { Id = 14, Name = "B4", FloorLevel = 2, Width = 40, Height = 70, Length = 750, MaterialID = 5, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralBeam { Id = 15, Name = "B5", FloorLevel = 3, Width = 35, Height = 65, Length = 800, MaterialID = 2, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralBeam { Id = 16, Name = "B6", FloorLevel = 3, Width = 40, Height = 70, Length = 850, MaterialID = 3, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralBeam { Id = 17, Name = "B7", FloorLevel = 4, Width = 45, Height = 75, Length = 900, MaterialID = 4, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralBeam { Id = 18, Name = "B8", FloorLevel = 4, Width = 45, Height = 80, Length = 950, MaterialID = 5, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralBeam { Id = 19, Name = "B9", FloorLevel = 5, Width = 50, Height = 80, Length = 1000, MaterialID = 10, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralBeam { Id = 20, Name = "B10", FloorLevel = 5, Width = 50, Height = 85, Length = 1050, MaterialID = 10, CreatedDate = new DateTime(2025, 1, 1) }
);
            modelBuilder.Entity<StructuralSlab>().HasData(
    new StructuralSlab { Id = 21, Name = "S1", FloorLevel = 1, Length = 1000, Width = 800, Thickness = 15, MaterialID = 2, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralSlab { Id = 22, Name = "S2", FloorLevel = 1, Length = 1200, Width = 900, Thickness = 15, MaterialID = 3, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralSlab { Id = 23, Name = "S3", FloorLevel = 2, Length = 1300, Width = 950, Thickness = 18, MaterialID = 4, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralSlab { Id = 24, Name = "S4", FloorLevel = 2, Length = 1400, Width = 1000, Thickness = 18, MaterialID = 5, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralSlab { Id = 25, Name = "S5", FloorLevel = 3, Length = 1500, Width = 1100, Thickness = 20, MaterialID = 2, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralSlab { Id = 26, Name = "S6", FloorLevel = 3, Length = 1600, Width = 1200, Thickness = 20, MaterialID = 3, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralSlab { Id = 27, Name = "S7", FloorLevel = 4, Length = 1700, Width = 1300, Thickness = 22, MaterialID = 4, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralSlab { Id = 28, Name = "S8", FloorLevel = 4, Length = 1800, Width = 1400, Thickness = 22, MaterialID = 5, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralSlab { Id = 29, Name = "S9", FloorLevel = 5, Length = 1900, Width = 1500, Thickness = 25, MaterialID = 10, CreatedDate = new DateTime(2025, 1, 1) },
    new StructuralSlab { Id = 30, Name = "S10", FloorLevel = 5, Length = 2000, Width = 1600, Thickness = 25, MaterialID = 10, CreatedDate = new DateTime(2025, 1, 1) }
);




        }


    }
}
