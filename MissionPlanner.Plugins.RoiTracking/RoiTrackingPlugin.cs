namespace MissionPlanner.Plugins.RoiTracking
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using GMap.NET.WindowsForms;
    using GMap.NET.WindowsForms.Markers;

    using MissionPlanner;
    using MissionPlanner.GCSViews;
    using MissionPlanner.Utilities;

    public partial class RoiTrackingPlugin : MissionPlanner.Plugin.Plugin
    {
        private PointLatLngAlt lastLoiter = null;
        private GMapOverlay mapOverlay;
        private ToolStripMenuItem roiListMenuItem;

        private List<RoiPoint> roiPoints = new List<RoiPoint>();
                                               /*{
                                                   new RoiPoint(PointLatLngAlt.Zero, 1),
                                                   new RoiPoint(new PointLatLngAlt(54.13, 24.15), 2)
                                               };*/

        public override string Name => "ROI tracking plugin";

        public override string Version => "1.0";

        public override string Author => "GG";

        public override bool Init()
        {
            return true;
        }

        public override bool Loaded()
        {
            this.mapOverlay = new GMapOverlay("points");
            FlightData.instance.gMapControl1.Overlays.Add(this.mapOverlay);
            MainV2.instance.KeyDown += this.InstanceOnKeyDown;

            this.roiListMenuItem = new ToolStripMenuItem("ROI list");
            this.roiListMenuItem.Click += this.RoiListMenuItemClick;
            this.Host.FDMenuMap.Items.Add(this.roiListMenuItem);

            return true;
        }

        public override bool Exit()
        {
            return true;
        }

        private void InstanceOnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
                return;

            var gimbalPoint = MainV2.comPort.MAV.cs.GimbalPoint;
            if (gimbalPoint != null && gimbalPoint != PointLatLngAlt.Zero)
            {
                if (keyEventArgs.Alt && keyEventArgs.KeyCode == Keys.L)
                {
                    this.TrackPoint(gimbalPoint);
                    this.LoiterAroundPoint(gimbalPoint);
                    this.lastLoiter = new PointLatLngAlt(gimbalPoint);
                    keyEventArgs.Handled = true;
                }

                if (keyEventArgs.Alt && keyEventArgs.KeyCode == Keys.T)
                {
                    this.TrackPoint(gimbalPoint);
                    keyEventArgs.Handled = true;
                }
            }

            if (this.lastLoiter != null && keyEventArgs.Alt && keyEventArgs.KeyCode == Keys.R)
            {
                this.LoiterAroundPoint(this.lastLoiter);
                keyEventArgs.Handled = true;
            }
        }

        private void TrackPoint(PointLatLngAlt gimbalPoint)
        {
            var screenshotPath = DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".jpg";
            var result = false; //Capturer.ScreenCapture.Capture("gst-launch-1.0", screenshotPath);

            var point = new PointLatLngAlt(gimbalPoint);
            var p = new RoiPoint(point, this.roiPoints.Count + 1) { ScreenshotPath = result ? screenshotPath : null };

            this.roiPoints.Add(p);
            this.AddMarker(p);
        }

        private void AddMarker(RoiPoint point)
        {
            var m = new GMarkerGoogle(point.Point, point.Marker)
                        {
                            ToolTipText = $"ROI #{point.No}\n" + point.Point,
                            ToolTipMode = MarkerTooltipMode.OnMouseOver,
                        };

            this.mapOverlay.Markers.Add(m);
        }

        private void LoiterAroundPoint(PointLatLngAlt gimbalPoint)
        {
            Locationwp gotohere = new Locationwp
                                      {
                                          id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
                                          alt = MainV2.comPort.MAV.GuidedMode.z > 0 ? MainV2.comPort.MAV.GuidedMode.z : 100,
                                          lat = gimbalPoint.Lat,
                                          lng = gimbalPoint.Lng
                                      };

            try
            {
                MainV2.comPort.setGuidedModeWP(gotohere);
            }
            catch (Exception ex)
            {
                MainV2.comPort.giveComport = false;
                CustomMessageBox.Show(Strings.CommandFailed + ex.Message, Strings.ERROR);
            }
        }

        private void RoiListMenuItemClick(object sender, EventArgs e)
        {
            var win = new RoiPointsList(this.roiPoints);
            win.Closed += (o, args) =>
                {
                    this.mapOverlay.Markers.Clear();
                    foreach (var point in this.roiPoints)
                    {
                        this.AddMarker(point);
                    }
                };
            win.Show();
        }
    }
}
