namespace DotNetExample
{
    partial class Form1
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
            this.FindButton = new System.Windows.Forms.Button();
            this.CameraComboBox = new System.Windows.Forms.ComboBox();
            this.Connect = new System.Windows.Forms.Button();
            this.StartExposureButton = new System.Windows.Forms.Button();
            this.StartCoolingButton = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.StopExposure = new System.Windows.Forms.Button();
            this.Disconnect = new System.Windows.Forms.Button();
            this.StopCooling = new System.Windows.Forms.Button();
            this.ConnectedLabel = new System.Windows.Forms.Label();
            this.ExposureStatus = new System.Windows.Forms.Label();
            this.Temperature = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ExposureLength = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TargetTemp = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExposureLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // FindButton
            // 
            this.FindButton.Location = new System.Drawing.Point(13, 13);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(75, 23);
            this.FindButton.TabIndex = 0;
            this.FindButton.Text = "Find Cameras";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.Click += new System.EventHandler(this.Find_Click);
            // 
            // CameraComboBox
            // 
            this.CameraComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CameraComboBox.FormattingEnabled = true;
            this.CameraComboBox.Location = new System.Drawing.Point(94, 12);
            this.CameraComboBox.Name = "CameraComboBox";
            this.CameraComboBox.Size = new System.Drawing.Size(121, 21);
            this.CameraComboBox.TabIndex = 1;
            this.CameraComboBox.SelectedIndexChanged += new System.EventHandler(this.CameraComboBox_SelectedIndexChanged);
            // 
            // Connect
            // 
            this.Connect.Enabled = false;
            this.Connect.Location = new System.Drawing.Point(222, 13);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 23);
            this.Connect.TabIndex = 2;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // StartExposureButton
            // 
            this.StartExposureButton.Enabled = false;
            this.StartExposureButton.Location = new System.Drawing.Point(13, 50);
            this.StartExposureButton.Name = "StartExposureButton";
            this.StartExposureButton.Size = new System.Drawing.Size(75, 35);
            this.StartExposureButton.TabIndex = 3;
            this.StartExposureButton.Text = "Start Exposure";
            this.StartExposureButton.UseVisualStyleBackColor = true;
            this.StartExposureButton.Click += new System.EventHandler(this.StartExposureButton_Click);
            // 
            // StartCoolingButton
            // 
            this.StartCoolingButton.Enabled = false;
            this.StartCoolingButton.Location = new System.Drawing.Point(13, 197);
            this.StartCoolingButton.Name = "StartCoolingButton";
            this.StartCoolingButton.Size = new System.Drawing.Size(75, 23);
            this.StartCoolingButton.TabIndex = 4;
            this.StartCoolingButton.Text = "Start Cooling";
            this.StartCoolingButton.UseVisualStyleBackColor = true;
            this.StartCoolingButton.Click += new System.EventHandler(this.StartCoolingButton_Click);
            // 
            // PictureBox
            // 
            this.PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox.Location = new System.Drawing.Point(122, 50);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(694, 636);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox.TabIndex = 5;
            this.PictureBox.TabStop = false;
            // 
            // StopExposure
            // 
            this.StopExposure.Enabled = false;
            this.StopExposure.Location = new System.Drawing.Point(13, 92);
            this.StopExposure.Name = "StopExposure";
            this.StopExposure.Size = new System.Drawing.Size(75, 34);
            this.StopExposure.TabIndex = 6;
            this.StopExposure.Text = "Stop Exposure";
            this.StopExposure.UseVisualStyleBackColor = true;
            this.StopExposure.Click += new System.EventHandler(this.StopExposure_Click);
            // 
            // Disconnect
            // 
            this.Disconnect.Enabled = false;
            this.Disconnect.Location = new System.Drawing.Point(304, 13);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(75, 23);
            this.Disconnect.TabIndex = 7;
            this.Disconnect.Text = "Disconnect";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // StopCooling
            // 
            this.StopCooling.Enabled = false;
            this.StopCooling.Location = new System.Drawing.Point(13, 226);
            this.StopCooling.Name = "StopCooling";
            this.StopCooling.Size = new System.Drawing.Size(75, 23);
            this.StopCooling.TabIndex = 8;
            this.StopCooling.Text = "Stop Cooling";
            this.StopCooling.UseVisualStyleBackColor = true;
            this.StopCooling.Click += new System.EventHandler(this.StopCooling_Click);
            // 
            // ConnectedLabel
            // 
            this.ConnectedLabel.AutoSize = true;
            this.ConnectedLabel.Location = new System.Drawing.Point(385, 18);
            this.ConnectedLabel.Name = "ConnectedLabel";
            this.ConnectedLabel.Size = new System.Drawing.Size(79, 13);
            this.ConnectedLabel.TabIndex = 9;
            this.ConnectedLabel.Text = "Not Connected";
            // 
            // ExposureStatus
            // 
            this.ExposureStatus.AutoSize = true;
            this.ExposureStatus.Location = new System.Drawing.Point(19, 181);
            this.ExposureStatus.Name = "ExposureStatus";
            this.ExposureStatus.Size = new System.Drawing.Size(24, 13);
            this.ExposureStatus.TabIndex = 10;
            this.ExposureStatus.Text = "Idle";
            // 
            // Temperature
            // 
            this.Temperature.AutoSize = true;
            this.Temperature.Location = new System.Drawing.Point(19, 309);
            this.Temperature.Name = "Temperature";
            this.Temperature.Size = new System.Drawing.Size(60, 13);
            this.Temperature.TabIndex = 11;
            this.Temperature.Text = "No Camera";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 296);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Temperature:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Exposure:";
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(741, 13);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 14;
            this.SaveButton.Text = "Save Image";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ExposureLength
            // 
            this.ExposureLength.DecimalPlaces = 2;
            this.ExposureLength.Location = new System.Drawing.Point(14, 145);
            this.ExposureLength.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.ExposureLength.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            131072});
            this.ExposureLength.Name = "ExposureLength";
            this.ExposureLength.Size = new System.Drawing.Size(74, 20);
            this.ExposureLength.TabIndex = 15;
            this.ExposureLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Exposure Length:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Target Temp";
            // 
            // TargetTemp
            // 
            this.TargetTemp.Location = new System.Drawing.Point(12, 273);
            this.TargetTemp.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TargetTemp.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.TargetTemp.Name = "TargetTemp";
            this.TargetTemp.Size = new System.Drawing.Size(76, 20);
            this.TargetTemp.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(828, 698);
            this.Controls.Add(this.TargetTemp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExposureLength);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Temperature);
            this.Controls.Add(this.ExposureStatus);
            this.Controls.Add(this.ConnectedLabel);
            this.Controls.Add(this.StopCooling);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.StopExposure);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.StartCoolingButton);
            this.Controls.Add(this.StartExposureButton);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.CameraComboBox);
            this.Controls.Add(this.FindButton);
            this.Name = "Form1";
            this.Text = "DotNet Example";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExposureLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetTemp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.ComboBox CameraComboBox;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Button StartExposureButton;
        private System.Windows.Forms.Button StartCoolingButton;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Button StopExposure;
        private System.Windows.Forms.Button Disconnect;
        private System.Windows.Forms.Button StopCooling;
        private System.Windows.Forms.Label ConnectedLabel;
        private System.Windows.Forms.Label ExposureStatus;
        private System.Windows.Forms.Label Temperature;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.NumericUpDown ExposureLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown TargetTemp;
    }
}

