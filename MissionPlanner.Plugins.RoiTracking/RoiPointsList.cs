namespace MissionPlanner.Plugins.RoiTracking
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Forms;

    public partial class RoiPointsList : Form
    {
        private readonly List<RoiPoint> roiPoints;

        public RoiPointsList(List<RoiPoint> roiPoints)
        {
            this.roiPoints = roiPoints;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dataGridView1.DataSource = new BindingList<RoiPoint>(this.roiPoints);
            base.OnLoad(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveDialog.FileName, Newtonsoft.Json.JsonConvert.SerializeObject(this.roiPoints));
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show("Save failed. " + ex.Message, Strings.ERROR);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var points =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoiPoint>>(
                            File.ReadAllText(openDialog.FileName));

                    this.roiPoints.Clear();
                    this.roiPoints.AddRange(points);

                    CustomMessageBox.Show("Points loaded: " + this.roiPoints.Count, Strings.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show("Open failed. " + ex.Message, Strings.ERROR);
                }
            }
        }
    }
}
