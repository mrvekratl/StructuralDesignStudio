using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace StructuralDesignStudio.UI.Helpers
{
    public static class Model3DFactory
    {
        public static Model3D CreateBox(
            double width,
            double depth,
            double height,
            Color color)
        {
            var mesh = new MeshGeometry3D();

            // Merkeze hizala
            double offsetX = -width / 2;
            double offsetY = -depth / 2;
            double offsetZ = -height / 2;

            // 8 köşe (merkezlenmiş)
            var p0 = new Point3D(offsetX, offsetY, offsetZ);
            var p1 = new Point3D(offsetX + width, offsetY, offsetZ);
            var p2 = new Point3D(offsetX + width, offsetY + depth, offsetZ);
            var p3 = new Point3D(offsetX, offsetY + depth, offsetZ);
            var p4 = new Point3D(offsetX, offsetY, offsetZ + height);
            var p5 = new Point3D(offsetX + width, offsetY, offsetZ + height);
            var p6 = new Point3D(offsetX + width, offsetY + depth, offsetZ + height);
            var p7 = new Point3D(offsetX, offsetY + depth, offsetZ + height);

            mesh.Positions = new Point3DCollection
            {
                p0, p1, p2, p3,
                p4, p5, p6, p7
            };

            mesh.TriangleIndices = new Int32Collection
            {
                // Bottom
                0,1,2, 0,2,3,
                // Top
                4,6,5, 4,7,6,
                // Front
                0,4,5, 0,5,1,
                // Back
                2,6,7, 2,7,3,
                // Left
                0,3,7, 0,7,4,
                // Right
                1,5,6, 1,6,2
            };

            var material = new DiffuseMaterial(
                new SolidColorBrush(color)
            );

            return new GeometryModel3D(mesh, material);
        }
    }
}