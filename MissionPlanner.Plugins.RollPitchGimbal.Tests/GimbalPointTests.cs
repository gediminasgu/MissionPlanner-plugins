namespace MissionPlannerPlugins.Tests
{
    using MissionPlanner.Plugins.RollPitchGimbal;
    using MissionPlanner.Utilities;

    using NUnit.Framework;

    public class GimbalPointTests
    {
        [Test]
        public void Given_alt0_heading0_pitch0_roll0_returns_same_coords()
        {
            var result = new GimbalPoint().ProjectPoint(55.15, 24.13, 0, 0, 0, 0);
            Assert.AreEqual(55.15, result.Lat, 0.0001);
            Assert.AreEqual(24.13, result.Lng, 0.0001);
        }

        [Test]
        public void Given_alt100_pitch0_roll0_returns_same_coords()
        {
            var result = new GimbalPoint().ProjectPoint(55.15, 24.13, 100, 0, 0, 0);
            Assert.AreEqual(55.15, result.Lat, 0.0001);
            Assert.AreEqual(24.13, result.Lng, 0.0001);
            Assert.AreEqual(0, result.Alt);
        }

        [Test]
        public void Given_alt100_pitch45_roll0_returns_coords_100m_in_front()
        {
            var result = new GimbalPoint().ProjectPoint(55.15, 24.13, 100, 0, 45, 0);
            Assert.AreEqual(55.150898315284131, result.Lat, 0.0001);
            Assert.AreEqual(24.13, result.Lng, 0.0001);
        }

        [Test]
        public void Given_alt100_pitch45_roll45_returns_coords_to_NE()
        {
            var result = new GimbalPoint().ProjectPoint(55.15, 24.13, 100, 0, 45, -45);
            Assert.AreEqual(55.150898315284131, result.Lat, 0.0001);
            Assert.AreEqual(24.1315720479, result.Lng, 0.0001);
        }

        [Test]
        public void Given_alt100_pitch45_roll45_heading45_returns_coords_to_E()
        {
            var result = new GimbalPoint().ProjectPoint(55.15, 24.13, 100, 45, 45, -45);
            Assert.AreEqual(55.15, result.Lat, 0.0001);
            Assert.AreEqual(24.132223211, result.Lng, 0.0001);
        }

        [Test]
        public void Given_alt100_pitch0_roll45_returns_coords_100m_to_E()
        {
            var result = new GimbalPoint().ProjectPoint(55.15, 24.13, 100, 0, 0, -45);
            Assert.AreEqual(55.15, result.Lat, 0.0001);
            Assert.AreEqual(24.13157204798, result.Lng, 0.0001);
        }

        [Test]
        public void Given_alt100_pitch0_rollN45_returns_coords_100m_to_W()
        {
            var result = new GimbalPoint().ProjectPoint(55.15, 24.13, 100, 0, 0, 45);
            Assert.AreEqual(55.15, result.Lat, 0.0001);
            Assert.AreEqual(24.12842795202, result.Lng, 0.0001);
        }

        [Test]
        public void Given_alt100_pitchN45_roll45_returns_coords_to_SE()
        {
            var result = new GimbalPoint().ProjectPoint(55.15, 24.13, 100, 0, -45, -45);
            Assert.AreEqual(55.149101685, result.Lat, 0.0001);
            Assert.AreEqual(24.1315720479, result.Lng, 0.0001);
        }

        [Test]
        public void Given_alt100_pitchN45_rollN45_returns_coords_to_SW()
        {
            var result = new GimbalPoint().ProjectPoint(55.15, 24.13, 100, 0, -45, 45);
            Assert.AreEqual(55.149101685, result.Lat, 0.0001);
            Assert.AreEqual(24.12842795202, result.Lng, 0.0001);
        }

        [TestCase(90)]
        [TestCase(95)]
        [TestCase(-90)]
        [TestCase(-95)]
        public void Given_pitch_angle_90_or_more_returns_no_result(int angle)
        {
            var result = new GimbalPoint().ProjectPoint(55.15, 24.13, 100, 0, angle, 0);
            Assert.AreEqual(PointLatLngAlt.Zero, result);
        }
    }
}
