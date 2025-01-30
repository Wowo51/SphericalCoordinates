namespace SphericalCoordinatesTest
{
    [TestClass]
    public class SphericalTests
    {
        private const double Delta = 1e-7;
        [TestMethod]
        public void ToCartesian_Origin_ReturnsOrigin()
        {
            Vector3 result = Spherical.ToCartesian(0, 0, 0);
            Assert.AreEqual(0, result.X, Delta);
            Assert.AreEqual(0, result.Y, Delta);
            Assert.AreEqual(0, result.Z, Delta);
        }

        [TestMethod]
        public void ToCartesian_UnitSphere_ReturnsCorrectPoints()
        {
            Vector3 result1 = Spherical.ToCartesian(1, 0, Math.PI / 2);
            Assert.AreEqual(1, result1.X, Delta);
            Assert.AreEqual(0, result1.Y, Delta);
            Assert.AreEqual(0, result1.Z, Delta);
            Vector3 result2 = Spherical.ToCartesian(1, 0, 0);
            Assert.AreEqual(0, result2.X, Delta);
            Assert.AreEqual(0, result2.Y, Delta);
            Assert.AreEqual(1, result2.Z, Delta);
        }

        [TestMethod]
        public void FromCartesian_Origin_ReturnsOrigin()
        {
            Spherical result = Spherical.FromCartesian(new Vector3(0, 0, 0));
            Assert.AreEqual(0, result.Radius, Delta);
            Assert.AreEqual(0, result.Theta, Delta);
            Assert.AreEqual(0, result.Phi, Delta);
        }

        [TestMethod]
        public void FromCartesian_UnitSpherePoints_ReturnsCorrectSpherical()
        {
            Spherical result1 = Spherical.FromCartesian(new Vector3(1, 0, 0));
            Assert.AreEqual(1, result1.Radius, Delta);
            Assert.AreEqual(0, result1.Theta, Delta);
            Assert.AreEqual(Math.PI / 2, result1.Phi, Delta);
            Spherical result2 = Spherical.FromCartesian(new Vector3(0, 0, 1));
            Assert.AreEqual(1, result2.Radius, Delta);
            Assert.AreEqual(0, result2.Theta, Delta);
            Assert.AreEqual(0, result2.Phi, Delta);
        }

        [TestMethod]
        public void SurfaceArea_ValidRadius_ReturnsCorrectArea()
        {
            double result1 = Spherical.SurfaceArea(1);
            Assert.AreEqual(4 * Math.PI, result1, Delta);
            double result2 = Spherical.SurfaceArea(2);
            Assert.AreEqual(16 * Math.PI, result2, Delta);
        }

        [TestMethod]
        public void Volume_ValidRadius_ReturnsCorrectVolume()
        {
            double result1 = Spherical.Volume(1);
            Assert.AreEqual(4 * Math.PI / 3, result1, Delta);
            double result2 = Spherical.Volume(2);
            Assert.AreEqual(32 * Math.PI / 3, result2, Delta);
        }

        [TestMethod]
        public void GreatCircleDistance_SamePoint_ReturnsZero()
        {
            double result = Spherical.GreatCircleDistance(1, Math.PI / 4, Math.PI / 4, Math.PI / 4, Math.PI / 4);
            Assert.AreEqual(0, result, Delta);
        }

        [TestMethod]
        public void GreatCircleDistance_OppositePoints_ReturnsHalfCircumference()
        {
            double result = Spherical.GreatCircleDistance(1, 0, 0, Math.PI, 0);
            Assert.AreEqual(Math.PI, result, Delta);
        }

        [TestMethod]
        public void ToCartesianVector_ValidSpherical_ReturnsCorrectVector()
        {
            Spherical spherical = new Spherical(2, Math.PI / 4, Math.PI / 3);
            Vector3 result = spherical.ToCartesianVector();
            double sinPhi = Math.Sin(Math.PI / 3);
            double expectedX = 2 * sinPhi * Math.Cos(Math.PI / 4);
            double expectedY = 2 * sinPhi * Math.Sin(Math.PI / 4);
            double expectedZ = 2 * Math.Cos(Math.PI / 3);
            Assert.AreEqual(expectedX, result.X, Delta);
            Assert.AreEqual(expectedY, result.Y, Delta);
            Assert.AreEqual(expectedZ, result.Z, Delta);
        }

        [TestMethod]
        public void ToCartesian_NegativeRadius_ReturnsCorrectPoint()
        {
            Vector3 result = Spherical.ToCartesian(-1, 0, Math.PI / 2);
            Assert.AreEqual(-1, result.X, Delta);
            Assert.AreEqual(0, result.Y, Delta);
            Assert.AreEqual(0, result.Z, Delta);
        }

        [TestMethod]
        public void FromCartesian_NegativeCoordinates_ReturnsCorrectSpherical()
        {
            Spherical result = Spherical.FromCartesian(new Vector3(-1, -1, -1));
            double expectedRadius = Math.Sqrt(3);
            Assert.AreEqual(expectedRadius, result.Radius, Delta);
            Assert.AreEqual(-3 * Math.PI / 4, result.Theta, Delta);
            Assert.AreEqual(Math.Acos(-1 / Math.Sqrt(3)), result.Phi, Delta);
        }

        [TestMethod]
        public void GreatCircleDistance_AntipodalPoints_ReturnsHalfCircumference()
        {
            double radius = 1.0;

            double result = Spherical.GreatCircleDistance(radius, 0, 0, Math.PI, 0);

            Assert.AreEqual(Math.PI, result, 1e-10); // Higher precision delta
        }

        [TestMethod]
        public void SphericalCap_ValidInputs_ReturnsCorrectArea()
        {
            double result = Spherical.SphericalCap(2, 1);
            Assert.AreEqual(4 * Math.PI, result, Delta);
        }

        [TestMethod]
        public void SphericalZone_ValidInputs_ReturnsCorrectArea()
        {
            double result = Spherical.SphericalZone(1, 0, 0.5);
            Assert.AreEqual(Math.PI, result, Delta);
        }

        [TestMethod]
        public void SphericalSegmentVolume_ValidInputs_ReturnsCorrectVolume()
        {
            double result = Spherical.SphericalSegmentVolume(1, 0.5);
            double expected = Math.PI * 0.25 * (3 - 0.5) / 3;
            Assert.AreEqual(expected, result, Delta);
        }

        [TestMethod]
        public void SphericalSectorVolume_ValidInputs_ReturnsCorrectVolume()
        {
            double result = Spherical.SphericalSectorVolume(1, Math.PI / 2);
            double expected = (2.0 / 3.0) * Math.PI * (1 - Math.Cos(Math.PI / 2));
            Assert.AreEqual(expected, result, Delta);
        }
    }
}
