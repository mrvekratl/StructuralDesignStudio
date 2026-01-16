using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StructuralDesignStudio.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class initial_seed_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StructuralMaterials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Density = table.Column<double>(type: "float", nullable: false),
                    CompressiveStrength = table.Column<double>(type: "float", nullable: false),
                    MaterialType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructuralMaterials", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "StructuralElement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaterialID = table.Column<int>(type: "int", nullable: false),
                    ElementType = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    StructuralBeam_Width = table.Column<double>(type: "float", nullable: true),
                    StructuralBeam_Height = table.Column<double>(type: "float", nullable: true),
                    Length = table.Column<double>(type: "float", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true),
                    Depth = table.Column<double>(type: "float", nullable: true),
                    Height = table.Column<double>(type: "float", nullable: true),
                    StructuralSlab_Length = table.Column<double>(type: "float", nullable: true),
                    StructuralSlab_Width = table.Column<double>(type: "float", nullable: true),
                    Thickness = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructuralElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructuralElement_StructuralMaterials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "StructuralMaterials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StructuralMaterials",
                columns: new[] { "MaterialId", "CompressiveStrength", "Density", "MaterialName", "MaterialType" },
                values: new object[,]
                {
                    { 1, 25.0, 2.3999999999999999, "C25 Concrete", 0 },
                    { 2, 30.0, 2.5, "C30 Concrete", 0 },
                    { 3, 35.0, 2.5, "C35 Concrete", 0 },
                    { 4, 40.0, 2.6000000000000001, "C40 Concrete", 0 },
                    { 5, 45.0, 2.6000000000000001, "C45 Concrete", 0 },
                    { 6, 355.0, 7.8499999999999996, "S355 Steel", 1 },
                    { 7, 420.0, 7.8499999999999996, "S420 Steel", 1 },
                    { 8, 500.0, 7.8499999999999996, "S500 Steel", 1 },
                    { 9, 20.0, 1.8, "Lightweight Concrete", 0 },
                    { 10, 60.0, 2.7000000000000002, "High Strength Concrete", 0 }
                });

            migrationBuilder.InsertData(
                table: "StructuralElement",
                columns: new[] { "Id", "CreatedDate", "Depth", "ElementType", "FloorLevel", "Height", "MaterialID", "Name", "Width" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 40.0, "Column", 1, 300.0, 2, "C1", 40.0 },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45.0, "Column", 1, 300.0, 3, "C2", 45.0 },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.0, "Column", 2, 300.0, 4, "C3", 50.0 },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.0, "Column", 2, 300.0, 5, "C4", 60.0 },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.0, "Column", 3, 320.0, 2, "C5", 40.0 },
                    { 6, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 55.0, "Column", 3, 320.0, 3, "C6", 45.0 },
                    { 7, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.0, "Column", 4, 320.0, 4, "C7", 50.0 },
                    { 8, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 65.0, "Column", 4, 320.0, 5, "C8", 55.0 },
                    { 9, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.0, "Column", 5, 350.0, 10, "C9", 60.0 },
                    { 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 65.0, "Column", 5, 350.0, 10, "C10", 65.0 }
                });

            migrationBuilder.InsertData(
                table: "StructuralElement",
                columns: new[] { "Id", "CreatedDate", "ElementType", "FloorLevel", "StructuralBeam_Height", "Length", "MaterialID", "Name", "StructuralBeam_Width" },
                values: new object[,]
                {
                    { 11, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam", 1, 50.0, 600.0, 2, "B1", 30.0 },
                    { 12, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam", 1, 60.0, 650.0, 3, "B2", 30.0 },
                    { 13, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam", 2, 60.0, 700.0, 4, "B3", 35.0 },
                    { 14, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam", 2, 70.0, 750.0, 5, "B4", 40.0 },
                    { 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam", 3, 65.0, 800.0, 2, "B5", 35.0 },
                    { 16, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam", 3, 70.0, 850.0, 3, "B6", 40.0 },
                    { 17, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam", 4, 75.0, 900.0, 4, "B7", 45.0 },
                    { 18, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam", 4, 80.0, 950.0, 5, "B8", 45.0 },
                    { 19, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam", 5, 80.0, 1000.0, 10, "B9", 50.0 },
                    { 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam", 5, 85.0, 1050.0, 10, "B10", 50.0 }
                });

            migrationBuilder.InsertData(
                table: "StructuralElement",
                columns: new[] { "Id", "CreatedDate", "ElementType", "FloorLevel", "StructuralSlab_Length", "MaterialID", "Name", "Thickness", "StructuralSlab_Width" },
                values: new object[,]
                {
                    { 21, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slab", 1, 1000.0, 2, "S1", 15.0, 800.0 },
                    { 22, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slab", 1, 1200.0, 3, "S2", 15.0, 900.0 },
                    { 23, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slab", 2, 1300.0, 4, "S3", 18.0, 950.0 },
                    { 24, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slab", 2, 1400.0, 5, "S4", 18.0, 1000.0 },
                    { 25, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slab", 3, 1500.0, 2, "S5", 20.0, 1100.0 },
                    { 26, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slab", 3, 1600.0, 3, "S6", 20.0, 1200.0 },
                    { 27, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slab", 4, 1700.0, 4, "S7", 22.0, 1300.0 },
                    { 28, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slab", 4, 1800.0, 5, "S8", 22.0, 1400.0 },
                    { 29, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slab", 5, 1900.0, 10, "S9", 25.0, 1500.0 },
                    { 30, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slab", 5, 2000.0, 10, "S10", 25.0, 1600.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StructuralElement_MaterialID",
                table: "StructuralElement",
                column: "MaterialID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StructuralElement");

            migrationBuilder.DropTable(
                name: "StructuralMaterials");
        }
    }
}
