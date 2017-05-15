namespace MissionPlanner.Plugins.RollPitchGimbal
{
    using System;

    using MissionPlanner.Utilities;

    public class GimbalPoint
    {
        private const double DegreesToRadians = Math.PI / 180.0;
        private const double RadiansToDegrees = 180.0 / Math.PI;
        private const double EarthRadius = 6378137.0;

        public PointLatLngAlt ProjectPoint(double currentLocationLat, double currentLocationLng, float altitude, float heading, float cameraPitch, float cameraRoll)
        {
            if (Math.Abs(cameraPitch) >= 90 || Math.Abs(cameraRoll) >= 90)
                return PointLatLngAlt.Zero;

#if DEBUG
            if (altitude < 5) altitude = 100;   // for testing purposes on the ground
#endif
            var pitchDistance = this.GetDistanceByAltitudeAndAngle(altitude, cameraPitch);
            var rollDistance = this.GetDistanceByAltitudeAndAngle(altitude, -cameraRoll);

            var headingRad = heading * DegreesToRadians;
            
            // formula taken from http://keisan.casio.com/exec/system/1223522781
            var pitchRotatedByHeading = (-rollDistance * Math.Sin(headingRad)) + (pitchDistance * Math.Cos(headingRad));
            var rollRotatedByHeading = (rollDistance * Math.Cos(headingRad)) + (pitchDistance * Math.Sin(headingRad));

            var posLat = this.CalculateDerivedPosition(currentLocationLat, currentLocationLng, pitchRotatedByHeading, 0);
            var posLng = this.CalculateDerivedPosition(currentLocationLat, currentLocationLng, rollRotatedByHeading, 90);

            return new PointLatLngAlt(posLat.Lat, posLng.Lng);
        }

        private double GetDistanceByAltitudeAndAngle(float altitude, float angle)
        {
            return Math.Sin(angle * DegreesToRadians) / Math.Cos(angle * DegreesToRadians) * altitude;
        }

        /// <summary>
        /// Calculates the end-point from a given source at a given range (meters) and bearing (degrees).
        /// This methods uses simple geometry equations to calculate the end-point.
        /// </summary>
        /// <param name="startLat">Point of origin - latitude</param>
        /// <param name="startLng">Point of origin - longitude</param>
        /// <param name="range">Range in meters</param>
        /// <param name="bearing">Bearing in degrees</param>
        /// <returns>End-point from the source given the desired range and bearing.</returns>
        private PointLatLngAlt CalculateDerivedPosition(double startLat, double startLng, double range, double bearing)
        {
            var latA = startLat * DegreesToRadians;
            var lonA = startLng * DegreesToRadians;
            var angularDistance = range / EarthRadius;
            var trueCourse = bearing * DegreesToRadians;

            var lat = Math.Asin(
                (Math.Sin(latA) * Math.Cos(angularDistance)) +
                (Math.Cos(latA) * Math.Sin(angularDistance) * Math.Cos(trueCourse)));

            var dlon = Math.Atan2(
                Math.Sin(trueCourse) * Math.Sin(angularDistance) * Math.Cos(latA),
                Math.Cos(angularDistance) - (Math.Sin(latA) * Math.Sin(lat)));

            var lon = ((lonA + dlon + Math.PI) % (Math.PI * 2)) - Math.PI;

            return new PointLatLngAlt(lat * RadiansToDegrees, lon * RadiansToDegrees);
        }
    }
}
