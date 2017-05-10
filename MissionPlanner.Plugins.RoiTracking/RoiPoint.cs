namespace MissionPlanner.Plugins.RoiTracking
{
    using System;

    using GeoUtility.GeoSystem;

    using GMap.NET.WindowsForms.Markers;

    using MissionPlanner.Utilities;
    public class RoiPoint
    {
        public RoiPoint(PointLatLngAlt point, int no)
        {
            this.CreatedAt = DateTime.Now;
            this.Point = point;
            this.No = no;
            this.Mgrs = ((MGRS)new Geographic(point.Lng, point.Lat)).ToString();
            this.Marker = GMarkerGoogleType.yellow_small;
        }

        public string Mgrs { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public PointLatLngAlt Point { get; private set; }

        public int No { get; private set; }

        public GMarkerGoogleType Marker { get; set; }

        public string Notes { get; set; }

        public string ScreenshotPath { get; set; }
    }
}