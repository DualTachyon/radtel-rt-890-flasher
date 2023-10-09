/* Copyright 2023 Dual Tachyon
 * https://github.com/DualTachyon
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *     Unless required by applicable law or agreed to in writing, software
 *     distributed under the License is distributed on an "AS IS" BASIS,
 *     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *     See the License for the specific language governing permissions and
 *     limitations under the License.
 */

namespace RT_890_Flasher {
	partial class Flasher {
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
			if (disposing && (components != null)) {
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
			this.RefreshButton = new System.Windows.Forms.Button();
			this.ComPorts = new System.Windows.Forms.ComboBox();
			this.FilenameBox = new System.Windows.Forms.TextBox();
			this.PickButton = new System.Windows.Forms.Button();
			this.FlashButton = new System.Windows.Forms.Button();
			this.EraseButton = new System.Windows.Forms.Button();
			this.BackupButton = new System.Windows.Forms.Button();
			this.Progress = new System.Windows.Forms.ProgressBar();
			this.Copyright = new System.Windows.Forms.Label();
			this.UartLog = new System.Windows.Forms.TextBox();
			this.LogButton = new System.Windows.Forms.Button();
			this.AutoUART = new System.Windows.Forms.CheckBox();
			this.FastButton = new System.Windows.Forms.RadioButton();
			this.SlowButton = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// RefreshButton
			// 
			this.RefreshButton.BackColor = System.Drawing.Color.White;
			this.RefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.RefreshButton.Location = new System.Drawing.Point(15, 12);
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.Size = new System.Drawing.Size(136, 48);
			this.RefreshButton.TabIndex = 1;
			this.RefreshButton.Text = "&Refresh";
			this.RefreshButton.UseVisualStyleBackColor = false;
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// ComPorts
			// 
			this.ComPorts.FormattingEnabled = true;
			this.ComPorts.Location = new System.Drawing.Point(159, 21);
			this.ComPorts.Name = "ComPorts";
			this.ComPorts.Size = new System.Drawing.Size(136, 32);
			this.ComPorts.TabIndex = 2;
			// 
			// FilenameBox
			// 
			this.FilenameBox.Location = new System.Drawing.Point(159, 113);
			this.FilenameBox.Name = "FilenameBox";
			this.FilenameBox.ReadOnly = true;
			this.FilenameBox.Size = new System.Drawing.Size(420, 29);
			this.FilenameBox.TabIndex = 3;
			// 
			// PickButton
			// 
			this.PickButton.BackColor = System.Drawing.Color.White;
			this.PickButton.Location = new System.Drawing.Point(15, 104);
			this.PickButton.Name = "PickButton";
			this.PickButton.Size = new System.Drawing.Size(136, 48);
			this.PickButton.TabIndex = 4;
			this.PickButton.Text = "&Pick FW";
			this.PickButton.UseVisualStyleBackColor = false;
			this.PickButton.Click += new System.EventHandler(this.PickButton_Click);
			// 
			// FlashButton
			// 
			this.FlashButton.BackColor = System.Drawing.Color.White;
			this.FlashButton.Enabled = false;
			this.FlashButton.Location = new System.Drawing.Point(157, 168);
			this.FlashButton.Name = "FlashButton";
			this.FlashButton.Size = new System.Drawing.Size(136, 48);
			this.FlashButton.TabIndex = 5;
			this.FlashButton.Text = "&Flash";
			this.FlashButton.UseVisualStyleBackColor = false;
			this.FlashButton.Click += new System.EventHandler(this.FlashButton_Click);
			// 
			// EraseButton
			// 
			this.EraseButton.BackColor = System.Drawing.Color.White;
			this.EraseButton.Enabled = false;
			this.EraseButton.Location = new System.Drawing.Point(301, 168);
			this.EraseButton.Name = "EraseButton";
			this.EraseButton.Size = new System.Drawing.Size(136, 48);
			this.EraseButton.TabIndex = 6;
			this.EraseButton.Text = "&Erase";
			this.EraseButton.UseVisualStyleBackColor = false;
			this.EraseButton.Click += new System.EventHandler(this.EraseButton_Click);
			// 
			// BackupButton
			// 
			this.BackupButton.BackColor = System.Drawing.Color.White;
			this.BackupButton.Location = new System.Drawing.Point(15, 168);
			this.BackupButton.Name = "BackupButton";
			this.BackupButton.Size = new System.Drawing.Size(136, 48);
			this.BackupButton.TabIndex = 8;
			this.BackupButton.Text = "&Backup SPI";
			this.BackupButton.UseVisualStyleBackColor = false;
			this.BackupButton.Click += new System.EventHandler(this.BackupButton_Click);
			// 
			// Progress
			// 
			this.Progress.BackColor = System.Drawing.Color.White;
			this.Progress.Location = new System.Drawing.Point(15, 222);
			this.Progress.Name = "Progress";
			this.Progress.Size = new System.Drawing.Size(562, 48);
			this.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.Progress.TabIndex = 10;
			// 
			// Copyright
			// 
			this.Copyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Copyright.Location = new System.Drawing.Point(17, 273);
			this.Copyright.Name = "Copyright";
			this.Copyright.Size = new System.Drawing.Size(562, 48);
			this.Copyright.TabIndex = 11;
			this.Copyright.Text = "Copyright (c) 2023 Dual Tachyon";
			this.Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UartLog
			// 
			this.UartLog.BackColor = System.Drawing.Color.White;
			this.UartLog.Location = new System.Drawing.Point(24, 324);
			this.UartLog.Multiline = true;
			this.UartLog.Name = "UartLog";
			this.UartLog.ReadOnly = true;
			this.UartLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.UartLog.Size = new System.Drawing.Size(562, 611);
			this.UartLog.TabIndex = 12;
			// 
			// LogButton
			// 
			this.LogButton.BackColor = System.Drawing.Color.White;
			this.LogButton.Location = new System.Drawing.Point(444, 168);
			this.LogButton.Name = "LogButton";
			this.LogButton.Size = new System.Drawing.Size(136, 48);
			this.LogButton.TabIndex = 13;
			this.LogButton.Text = "&Log";
			this.LogButton.UseVisualStyleBackColor = false;
			this.LogButton.Click += new System.EventHandler(this.LogButton_Click);
			// 
			// AutoUART
			// 
			this.AutoUART.AutoSize = true;
			this.AutoUART.Location = new System.Drawing.Point(305, 59);
			this.AutoUART.Name = "AutoUART";
			this.AutoUART.Size = new System.Drawing.Size(227, 29);
			this.AutoUART.TabIndex = 14;
			this.AutoUART.Text = "&Auto UART after flash";
			this.AutoUART.UseVisualStyleBackColor = true;
			this.AutoUART.CheckedChanged += new System.EventHandler(this.AutoUART_CheckedChanged);
			// 
			// FastButton
			// 
			this.FastButton.AutoSize = true;
			this.FastButton.Location = new System.Drawing.Point(305, 21);
			this.FastButton.Name = "FastButton";
			this.FastButton.Size = new System.Drawing.Size(103, 29);
			this.FastButton.TabIndex = 15;
			this.FastButton.TabStop = true;
			this.FastButton.Text = "&115200";
			this.FastButton.UseVisualStyleBackColor = true;
			this.FastButton.CheckedChanged += new System.EventHandler(this.FastButton_CheckedChanged);
			// 
			// SlowButton
			// 
			this.SlowButton.AutoSize = true;
			this.SlowButton.Location = new System.Drawing.Point(414, 21);
			this.SlowButton.Name = "SlowButton";
			this.SlowButton.Size = new System.Drawing.Size(92, 29);
			this.SlowButton.TabIndex = 16;
			this.SlowButton.TabStop = true;
			this.SlowButton.Text = "1&9200";
			this.SlowButton.UseVisualStyleBackColor = true;
			this.SlowButton.CheckedChanged += new System.EventHandler(this.SlowButton_CheckedChanged);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Black;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Location = new System.Drawing.Point(14, 91);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(565, 10);
			this.label1.TabIndex = 17;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Black;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label2.Location = new System.Drawing.Point(15, 155);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(565, 10);
			this.label2.TabIndex = 18;
			// 
			// Flasher
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(591, 951);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.SlowButton);
			this.Controls.Add(this.FastButton);
			this.Controls.Add(this.AutoUART);
			this.Controls.Add(this.LogButton);
			this.Controls.Add(this.UartLog);
			this.Controls.Add(this.Copyright);
			this.Controls.Add(this.Progress);
			this.Controls.Add(this.BackupButton);
			this.Controls.Add(this.EraseButton);
			this.Controls.Add(this.FlashButton);
			this.Controls.Add(this.PickButton);
			this.Controls.Add(this.FilenameBox);
			this.Controls.Add(this.ComPorts);
			this.Controls.Add(this.RefreshButton);
			this.Name = "Flasher";
			this.Text = "Radtel RT-890 Flasher";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button RefreshButton;
		private System.Windows.Forms.ComboBox ComPorts;
		private System.Windows.Forms.TextBox FilenameBox;
		private System.Windows.Forms.Button PickButton;
		private System.Windows.Forms.Button FlashButton;
		private System.Windows.Forms.Button EraseButton;
		private System.Windows.Forms.Button BackupButton;
		private System.Windows.Forms.ProgressBar Progress;
		private System.Windows.Forms.Label Copyright;
		private System.Windows.Forms.TextBox UartLog;
		private System.Windows.Forms.Button LogButton;
		private System.Windows.Forms.CheckBox AutoUART;
		private System.Windows.Forms.RadioButton FastButton;
		private System.Windows.Forms.RadioButton SlowButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

