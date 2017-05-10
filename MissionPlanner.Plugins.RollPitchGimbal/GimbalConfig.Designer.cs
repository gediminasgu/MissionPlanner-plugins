namespace MissionPlanner.Plugins.RollPitchGimbal
{
    partial class GimbalConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pitchOffset = new System.Windows.Forms.NumericUpDown();
            this.rollOffset = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pitchOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollOffset)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pitch";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Roll";
            // 
            // pitchOffset
            // 
            this.pitchOffset.DecimalPlaces = 2;
            this.pitchOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.pitchOffset.Location = new System.Drawing.Point(54, 19);
            this.pitchOffset.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.pitchOffset.Name = "pitchOffset";
            this.pitchOffset.Size = new System.Drawing.Size(93, 20);
            this.pitchOffset.TabIndex = 2;
            // 
            // rollOffset
            // 
            this.rollOffset.DecimalPlaces = 2;
            this.rollOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.rollOffset.Location = new System.Drawing.Point(54, 45);
            this.rollOffset.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.rollOffset.Name = "rollOffset";
            this.rollOffset.Size = new System.Drawing.Size(93, 20);
            this.rollOffset.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pitchOffset);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rollOffset);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 111);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gimbal angle offsets";
            // 
            // GimbalConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 111);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Name = "GimbalConfig";
            this.Text = "Gimbal config";
            ((System.ComponentModel.ISupportInitialize)(this.pitchOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollOffset)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown pitchOffset;
        private System.Windows.Forms.NumericUpDown rollOffset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}