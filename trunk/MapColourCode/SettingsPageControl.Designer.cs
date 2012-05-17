namespace RouteColourCode
{
    partial class SettingsPageControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.ActivateCheckBox = new System.Windows.Forms.CheckBox();
            this.SegmentsLabel = new System.Windows.Forms.Label();
            this.MinSegmentLabel = new System.Windows.Forms.Label();
            this.MinSpeedTextBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.MinSpeedLabel = new System.Windows.Forms.Label();
            this.MinSegmentTextBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.SegmentsTextBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.TrackComboBox = new System.Windows.Forms.ComboBox();
            this.TrackLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "RouteColourCode settings page";
            // 
            // ActivateCheckBox
            // 
            this.ActivateCheckBox.AutoSize = true;
            this.ActivateCheckBox.Location = new System.Drawing.Point(22, 46);
            this.ActivateCheckBox.Name = "ActivateCheckBox";
            this.ActivateCheckBox.Size = new System.Drawing.Size(138, 17);
            this.ActivateCheckBox.TabIndex = 1;
            this.ActivateCheckBox.Text = "Activate route colouring";
            this.ActivateCheckBox.UseVisualStyleBackColor = true;
            this.ActivateCheckBox.CheckedChanged += new System.EventHandler(this.ActivateCheckBox_CheckedChanged);
            // 
            // SegmentsLabel
            // 
            this.SegmentsLabel.AutoSize = true;
            this.SegmentsLabel.Location = new System.Drawing.Point(76, 75);
            this.SegmentsLabel.Name = "SegmentsLabel";
            this.SegmentsLabel.Size = new System.Drawing.Size(104, 13);
            this.SegmentsLabel.TabIndex = 3;
            this.SegmentsLabel.Text = "Number of segments";
            // 
            // MinSegmentLabel
            // 
            this.MinSegmentLabel.AutoSize = true;
            this.MinSegmentLabel.Location = new System.Drawing.Point(76, 101);
            this.MinSegmentLabel.Name = "MinSegmentLabel";
            this.MinSegmentLabel.Size = new System.Drawing.Size(116, 13);
            this.MinSegmentLabel.TabIndex = 5;
            this.MinSegmentLabel.Text = "Min segment length (m)";
            // 
            // MinSpeedTextBox
            // 
            this.MinSpeedTextBox.AcceptsReturn = false;
            this.MinSpeedTextBox.AcceptsTab = false;
            this.MinSpeedTextBox.BackColor = System.Drawing.Color.White;
            this.MinSpeedTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.MinSpeedTextBox.ButtonImage = null;
            this.MinSpeedTextBox.Location = new System.Drawing.Point(22, 121);
            this.MinSpeedTextBox.MaxLength = 32767;
            this.MinSpeedTextBox.Multiline = false;
            this.MinSpeedTextBox.Name = "MinSpeedTextBox";
            this.MinSpeedTextBox.ReadOnly = false;
            this.MinSpeedTextBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.MinSpeedTextBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.MinSpeedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MinSpeedTextBox.Size = new System.Drawing.Size(36, 19);
            this.MinSpeedTextBox.TabIndex = 6;
            this.MinSpeedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.MinSpeedTextBox.Validated += new System.EventHandler(this.MinSpeedTextBox_Validated);
            // 
            // MinSpeedLabel
            // 
            this.MinSpeedLabel.AutoSize = true;
            this.MinSpeedLabel.Location = new System.Drawing.Point(76, 127);
            this.MinSpeedLabel.Name = "MinSpeedLabel";
            this.MinSpeedLabel.Size = new System.Drawing.Size(167, 13);
            this.MinSpeedLabel.TabIndex = 7;
            this.MinSpeedLabel.Text = "Ignore speed lower than this (m/s)";
            // 
            // MinSegmentTextBox
            // 
            this.MinSegmentTextBox.AcceptsReturn = false;
            this.MinSegmentTextBox.AcceptsTab = false;
            this.MinSegmentTextBox.BackColor = System.Drawing.Color.White;
            this.MinSegmentTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.MinSegmentTextBox.ButtonImage = null;
            this.MinSegmentTextBox.Location = new System.Drawing.Point(22, 95);
            this.MinSegmentTextBox.MaxLength = 32767;
            this.MinSegmentTextBox.Multiline = false;
            this.MinSegmentTextBox.Name = "MinSegmentTextBox";
            this.MinSegmentTextBox.ReadOnly = false;
            this.MinSegmentTextBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.MinSegmentTextBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.MinSegmentTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MinSegmentTextBox.Size = new System.Drawing.Size(36, 19);
            this.MinSegmentTextBox.TabIndex = 8;
            this.MinSegmentTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.MinSegmentTextBox.Validated += new System.EventHandler(this.MinSegmentTextBox_Validated);
            // 
            // SegmentsTextBox
            // 
            this.SegmentsTextBox.AcceptsReturn = false;
            this.SegmentsTextBox.AcceptsTab = false;
            this.SegmentsTextBox.BackColor = System.Drawing.Color.White;
            this.SegmentsTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.SegmentsTextBox.ButtonImage = null;
            this.SegmentsTextBox.Location = new System.Drawing.Point(22, 70);
            this.SegmentsTextBox.MaxLength = 32767;
            this.SegmentsTextBox.Multiline = false;
            this.SegmentsTextBox.Name = "SegmentsTextBox";
            this.SegmentsTextBox.ReadOnly = false;
            this.SegmentsTextBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.SegmentsTextBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.SegmentsTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SegmentsTextBox.Size = new System.Drawing.Size(36, 19);
            this.SegmentsTextBox.TabIndex = 9;
            this.SegmentsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SegmentsTextBox.Validated += new System.EventHandler(this.SegmentsTextBox_Validated);
            // 
            // TrackComboBox
            // 
            this.TrackComboBox.FormattingEnabled = true;
            this.TrackComboBox.Items.AddRange(new object[] {
            "Speed",
            "Altitude",
            "Heart rate"});
            this.TrackComboBox.Location = new System.Drawing.Point(22, 146);
            this.TrackComboBox.Name = "TrackComboBox";
            this.TrackComboBox.Size = new System.Drawing.Size(121, 21);
            this.TrackComboBox.TabIndex = 10;
            this.TrackComboBox.Validated += new System.EventHandler(this.TrackComboBox_Validated);
            // 
            // TrackLabel
            // 
            this.TrackLabel.AutoSize = true;
            this.TrackLabel.Location = new System.Drawing.Point(149, 154);
            this.TrackLabel.Name = "TrackLabel";
            this.TrackLabel.Size = new System.Drawing.Size(272, 13);
            this.TrackLabel.TabIndex = 11;
            this.TrackLabel.Text = "Choose the data track to use as a base for the colouring";
            // 
            // SettingsPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TrackLabel);
            this.Controls.Add(this.TrackComboBox);
            this.Controls.Add(this.SegmentsTextBox);
            this.Controls.Add(this.MinSegmentTextBox);
            this.Controls.Add(this.MinSpeedLabel);
            this.Controls.Add(this.MinSpeedTextBox);
            this.Controls.Add(this.MinSegmentLabel);
            this.Controls.Add(this.SegmentsLabel);
            this.Controls.Add(this.ActivateCheckBox);
            this.Controls.Add(this.label1);
            this.Name = "SettingsPageControl";
            this.Size = new System.Drawing.Size(436, 185);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ActivateCheckBox;
        private System.Windows.Forms.Label SegmentsLabel;
        private System.Windows.Forms.Label MinSegmentLabel;
        private ZoneFiveSoftware.Common.Visuals.TextBox MinSpeedTextBox;
        private System.Windows.Forms.Label MinSpeedLabel;
        private ZoneFiveSoftware.Common.Visuals.TextBox MinSegmentTextBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox SegmentsTextBox;
        private System.Windows.Forms.ComboBox TrackComboBox;
        private System.Windows.Forms.Label TrackLabel;
    }
}
