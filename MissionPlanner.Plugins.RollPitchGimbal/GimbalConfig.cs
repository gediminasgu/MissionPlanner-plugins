namespace MissionPlanner.Plugins.RollPitchGimbal
{
    using System.Windows.Forms;

    public partial class GimbalConfig : Form
    {
        private readonly Settings settings;

        public GimbalConfig(Settings settings)
        {
            this.settings = settings;
            InitializeComponent();

            this.pitchOffset.DataBindings.Add(nameof(this.pitchOffset.Value), this.settings, nameof(this.settings.PitchOffset));
            this.rollOffset.DataBindings.Add(nameof(this.rollOffset.Value), this.settings, nameof(this.settings.RollOffset));
        }
    }
}
