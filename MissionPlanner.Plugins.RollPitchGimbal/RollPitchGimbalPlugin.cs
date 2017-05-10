namespace MissionPlanner.Plugins.RollPitchGimbal
{
    using System;
    using System.Windows.Forms;

    using GMap.NET.WindowsForms;
    using GMap.NET.WindowsForms.Markers;

    using MissionPlanner;
    using MissionPlanner.GCSViews;
    using MissionPlanner.Utilities;

    public class RollPitchGimbalPlugin : MissionPlanner.Plugin.Plugin
    {
        private GMapOverlay mapOverlay;
        private PointLatLngAlt marker = null;
        private ToolStripMenuItem configMenuItem;
        private Settings settings;

        public override string Name => "GG Pitch and Roll Gimbal plugin";

        public override string Version => "1.0";

        public override string Author => "GG";

        public override bool Init()
        {
            return true;
        }

        public override bool Loaded()
        {
            this.mapOverlay = new GMapOverlay("rally points");
            FlightData.instance.gMapControl1.Overlays.Add(this.mapOverlay);

            this.configMenuItem = new ToolStripMenuItem("Gimbal config");
            this.configMenuItem.Click += this.ConfigMenuItemClick;
            this.Host.FDMenuMap.Items.Add(this.configMenuItem);

            this.settings = new Settings(this.Host.config);
            return true;
        }

        public override bool Exit()
        {
            this.settings.Close();
            return true;
        }

        public override bool Loop()
        {
            if (MainV2.comPort.MAV.param.ContainsKey("MNT_TYPE") && (int)MainV2.comPort.MAV.param["MNT_TYPE"] == 4)
            {
                /*
                 * Verify that yawchannel is not in use
                 * 
                yawchannel = (int) (float) MainV2.comPort.MAV.param["MNT_RC_IN_PAN"];
                 * */

                //MainV2.comPort.GetMountStatus();
                this.mapOverlay.Markers.Clear();
                this.marker = new GimbalPoint().ProjectPoint(
                    MainV2.comPort.MAV.cs.lat,
                    MainV2.comPort.MAV.cs.lng,
                    MainV2.comPort.MAV.cs.alt,
                    MainV2.comPort.MAV.cs.yaw,
                    MainV2.comPort.MAV.cs.campointa + Convert.ToSingle(this.settings.PitchOffset),
                    MainV2.comPort.MAV.cs.campointb + Convert.ToSingle(this.settings.RollOffset));

                Console.WriteLine(
                    "Lat: {0} Lon: {1} Alt: {2} Heading: {3} Cam pitch: {4} Cam roll: {5} Pitch offset: {6} Roll offset: {7}",
                    MainV2.comPort.MAV.cs.lat,
                    MainV2.comPort.MAV.cs.lng,
                    MainV2.comPort.MAV.cs.alt,
                    MainV2.comPort.MAV.cs.yaw,
                    MainV2.comPort.MAV.cs.campointa,
                    MainV2.comPort.MAV.cs.campointb,
                    Convert.ToSingle(this.settings.PitchOffset),
                    Convert.ToSingle(this.settings.RollOffset));

                if (this.marker != PointLatLngAlt.Zero)
                {
                    MainV2.comPort.MAV.cs.GimbalPoint = this.marker;

                    var m = new GMarkerGoogle(this.marker, GMarkerGoogleType.arrow)
                                {
                                    ToolTipText = "Camera Target\n" + this.marker,
                                    ToolTipMode = MarkerTooltipMode.OnMouseOver,
                                };

                    this.mapOverlay.Markers.Add(m);
                }
            }

            this.NextRun = DateTime.Now.AddSeconds(1);

            return base.Loop();
        }

        private void ConfigMenuItemClick(object sender, EventArgs e)
        {
            new GimbalConfig(this.settings).Show();
        }
    }
}
