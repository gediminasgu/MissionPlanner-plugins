namespace MissionPlanner.Plugins.RollPitchGimbal
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using MissionPlanner.Plugins.RollPitchGimbal.Annotations;

    public class Settings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private decimal pitchOffset;
        public decimal PitchOffset
        {
            get
            {
                return this.pitchOffset;
            }
            set
            {
                this.pitchOffset = value;
                this.InvokePropertyChanged(nameof(this.PitchOffset));
            }
        }

        private decimal rollOffset;

        private Utilities.Settings config;

        public Settings(Utilities.Settings config)
        {
            this.config = config;
            if (config.ContainsKey("gimbal-offsets"))
            {
                var s = config["gimbal-offsets"];
                if (s.Contains(":"))
                {
                    var t = s.Split(':');
                    this.PitchOffset = Convert.ToDecimal(t[0]);
                    this.RollOffset = Convert.ToDecimal(t[1]);
                }
            }
        }

        public void Close()
        {
            this.config["gimbal-offsets"] = this.PitchOffset + ":" + this.RollOffset;
        }

        public decimal RollOffset
        {
            get
            {
                return this.rollOffset;
            }
            set
            {
                this.rollOffset = value;
                this.InvokePropertyChanged(nameof(this.RollOffset));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void InvokePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
