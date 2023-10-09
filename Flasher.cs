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

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;

namespace RT_890_Flasher {
	public partial class Flasher : Form {
		RT_890_UART Radio = new RT_890_UART();
		byte[] Firmware;
		BackgroundWorker FlashWorker;
		BackgroundWorker BackupWorker;
		System.IO.FileStream OutputFile;
		bool IsFlashMode;
		bool IsBackupMode;
		bool IsLogMode;
		bool IsAutoUart;
		bool IsFirstTime;
		AutoResetEvent ResetEvent = new AutoResetEvent(false);

		private void PopulateSerialBox()
		{
			ComPorts.Items.Clear();
			ComPorts.Items.AddRange(SerialPort.GetPortNames());
			if (ComPorts.Items.Count > 0 && ComPorts.SelectedIndex < 0) {
				ComPorts.SelectedIndex = 0;
			}
		}

		private bool OpenComPort(string ComPort, bool IsBootloader = true)
		{
			try {
				Radio.Open(ComPort, (IsBootloader || FastButton.Checked) ? 115200 : 19200);
			} catch {
				return false;
			}

			return true;
		}

		public Flasher()
		{
			InitializeComponent();

			ComPorts.SelectedIndexChanged += ComPorts_SelectedIndexChanged;

			PopulateSerialBox();

			FastButton.Checked = true;

			FlashWorker = new BackgroundWorker();
			FlashWorker.DoWork += FlashWorker_DoWork;
			FlashWorker.WorkerReportsProgress = true;
			FlashWorker.WorkerSupportsCancellation = true;
			FlashWorker.ProgressChanged += Worker_ProgressChanged;

			BackupWorker = new BackgroundWorker();
			BackupWorker.DoWork += BackupWorker_DoWork;
			BackupWorker.WorkerReportsProgress = true;
			BackupWorker.WorkerSupportsCancellation = true;
			BackupWorker.ProgressChanged += Worker_ProgressChanged;
		}

		private void ComPorts_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateButtons();
		}

		private bool TestBootloader()
		{
			try {
				if (Radio.IsBootLoaderMode()) {
					return true;
				}
				MessageBox.Show("RT-890 not in bootloader mode!", "Error!");
			} catch {
				MessageBox.Show("Timeout error! Is the radio in bootloader mode?", "Error!");
			}

			return false;
		}

		private bool TestNormalMode()
		{
			try {
				if (!Radio.IsBootLoaderMode()) {
					return true;
				}
				MessageBox.Show("RT-890 not in normal mode!", "Error!");
			} catch {
				MessageBox.Show("Timeout error! Is the radio in normal mode?", "Error!");
			}

			return false;
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			ComPorts.SelectedIndex = -1;
			PopulateSerialBox();
		}

		private void PickButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog Dialog = new OpenFileDialog();
			DialogResult Result = Dialog.ShowDialog();
			if (Result == DialogResult.OK) {
				FilenameBox.Text = Dialog.FileName;
				UpdateButtons();
			}
		}

		private void FlashButton_Click(object sender, EventArgs e)
		{
			if (IsFlashMode) {
				FlashWorker.CancelAsync();
				ResetEvent.WaitOne();
				UpdateButtons();
			} else {
				try {
					Firmware = System.IO.File.ReadAllBytes(FilenameBox.Text);
				} catch {
					FilenameBox.Text = null;
					MessageBox.Show("Error loading file!");
					UpdateButtons();
					return;
				}
				if (OpenComPort(ComPorts.SelectedItem as string, true)) {
					if (!TestBootloader()) {
						Radio.Close();
						return;
					}

					if (Radio.Command_EraseFlash()) {
						Progress.Value = 0;
						Progress.Minimum = 0;
						Progress.Maximum = 100;
						Progress.Step = 1;
						IsFlashMode = true;
						UpdateButtons();
						FlashWorker.RunWorkerAsync();
					} else {
						MessageBox.Show("Failed to erase flash!", "Error!");
						Radio.Close();
					}
				} else {
					MessageBox.Show("Failed to open port!", "Error!");
				}
			}
		}

		private void EraseButton_Click(object sender, EventArgs e)
		{
			if (OpenComPort(ComPorts.SelectedItem as string)) {
				if (!TestBootloader()) {
					Radio.Close();
					return;
				}

				if (Radio.Command_EraseFlash()) {
					MessageBox.Show("Flash erase complete!", "Success");
				} else {
					MessageBox.Show("Failed to erase flash!", "Error!");
				}

				Radio.Close();
			} else {
				MessageBox.Show("Failed to open port!", "Error!");
			}
		}

		private void BackupButton_Click(object sender, EventArgs e)
		{
			if (IsBackupMode) {
				BackupWorker.CancelAsync();
				ResetEvent.WaitOne();
				UpdateButtons();
			} else {
				SaveFileDialog Dialog = new SaveFileDialog();
				DialogResult Result = Dialog.ShowDialog();

				if (Result == DialogResult.OK) {
					try {
						OutputFile = System.IO.File.Create(Dialog.FileName);
					} catch {
						MessageBox.Show("Failed to create file!", "Error!");
						return;
					}

					if (OpenComPort(ComPorts.SelectedItem as string)) {
						if (!TestNormalMode()) {
							Radio.Close();
							return;
						}

						IsBackupMode = true;
						UpdateButtons();
						Progress.Value = 0;
						Progress.Minimum = 0;
						Progress.Maximum = 32768;
						Progress.Step = 1;
						BackupWorker.RunWorkerAsync();
					} else {
						MessageBox.Show("Failed to open port!", "Error!");
						OutputFile.Close();
						try {
							System.IO.File.Delete(Dialog.FileName);
						} catch {
						}
					}
				}
			}
		}

		private void LogButton_Click(object sender, EventArgs e)
		{
			if (IsLogMode) {
				IsLogMode = false;
				UpdateButtons();
				Radio.Close();
			} else {
				UartLog.Clear();
				if (OpenComPort(ComPorts.SelectedItem as string)) {
					IsLogMode = true;
					UpdateButtons();
					Radio.AddDelegate(DataReceiver);
				} else {
					MessageBox.Show("Failed to open port!", "Error!");
				}
			}
		}

		private void DataReceiver(object sender, SerialDataReceivedEventArgs e)
		{
			try {
				while (true) {
					string s;
					int c = Radio.Read();
					if (c > 127) {
						s = c.ToString("\\xX2");
					} else {
						s = Convert.ToChar(c).ToString();
					}
					this.Invoke((MethodInvoker)delegate { UartLog.AppendText(s); });
				}
			} catch {
			}
		}

		// Workers

		private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			Progress.Value = e.ProgressPercentage;
			Progress.Refresh();
		}

		private void FlashWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			ushort i;

			for (i = 0; i < Firmware.Length; i += 128) {
				if (FlashWorker.CancellationPending) {
					MessageBox.Show("Cancelled!", "Cancellation");
					e.Cancel = true;
					break;
				}
				if (!Radio.Command_WriteFlash(i, Firmware)) {
					MessageBox.Show("Failed to write flash at address 0x" + i.ToString("X4"), "Error!");
					e.Cancel = true;
					break;
				}
				FlashWorker.ReportProgress((100 * i) / Firmware.Length);
			}
			FlashWorker.ReportProgress(0);
			IsFlashMode = false;
			Radio.Close();

			if (!e.Cancel) {
				this.Invoke((MethodInvoker)delegate { UpdateButtons(); });
			}

			if (IsAutoUart) {
				this.Invoke((MethodInvoker)delegate { LogButton_Click(null, null); });
			}
			if (e.Cancel == false) {
				if (Firmware.Length != 0xEC00) {
					MessageBox.Show("Flash update complete!", "Success");
				}
			} else {
				ResetEvent.Set();
			}
		}

		private void BackupWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			for (ushort i = 0; i < 32768; i++) {
				if (BackupWorker.CancellationPending) {
					MessageBox.Show("Cancelled!", "Cancellation");
					e.Cancel = true;
					break;
				}
				byte[] Data = Radio.Command_ReadFlash(i);
				if (Data == null) {
					MessageBox.Show("Failed to read SPI flash!", "Error!");
					e.Cancel = true;
					break;
				}
				OutputFile.Write(Data, 0, Data.Length);
				BackupWorker.ReportProgress(i);
			}
			OutputFile.Close();
			BackupWorker.ReportProgress(0);
			IsBackupMode = false;
			Radio.Close();
			if (!e.Cancel) {
				this.Invoke((MethodInvoker)delegate { UpdateButtons(); });
				MessageBox.Show("SPI Flash backup complete!", "Success");
			} else {
				ResetEvent.Set();
			}
		}

		private void UpdateButtons()
		{
			if (IsLogMode) {
				RefreshButton.Enabled = false;
				ComPorts.Enabled = false;
				FastButton.Enabled = false;
				SlowButton.Enabled = false;
				AutoUART.Enabled = false;
				PickButton.Enabled = false;
				FlashButton.Enabled = false;
				BackupButton.Enabled = false;
				EraseButton.Enabled = false;
				LogButton.Text = "&Stop";
				LogButton.ForeColor = System.Drawing.Color.Red;
			} else if (IsFlashMode) {
				RefreshButton.Enabled = false;
				ComPorts.Enabled = false;
				FastButton.Enabled = false;
				SlowButton.Enabled = false;
				AutoUART.Enabled = false;
				PickButton.Enabled = false;
				BackupButton.Enabled = false;
				EraseButton.Enabled = false;
				LogButton.Enabled = false;
				FlashButton.Text = "&Stop";
				FlashButton.ForeColor = System.Drawing.Color.Red;
			} else if (IsBackupMode) {
				RefreshButton.Enabled = false;
				ComPorts.Enabled = false;
				FastButton.Enabled = false;
				SlowButton.Enabled = false;
				AutoUART.Enabled = false;
				PickButton.Enabled = false;
				FlashButton.Enabled = false;
				EraseButton.Enabled = false;
				LogButton.Enabled = false;
				BackupButton.Text = "&Stop";
				BackupButton.ForeColor = System.Drawing.Color.Red;
			} else {
				RefreshButton.Enabled = true;
				ComPorts.Enabled = true;
				FastButton.Enabled = true;
				SlowButton.Enabled = true;
				AutoUART.Enabled = true;
				PickButton.Enabled = true;
				FlashButton.Text = "&Flash";
				FlashButton.ForeColor = System.Drawing.Color.Black;
				BackupButton.Text = "&Backup SPI";
				BackupButton.ForeColor = System.Drawing.Color.Black;
				LogButton.Text = "&Log";
				LogButton.ForeColor = System.Drawing.Color.Black;
				if (FilenameBox.Text != null && FilenameBox.Text != "" && ComPorts.SelectedIndex >= 0) {
					FlashButton.Enabled = true;
				} else {
					FlashButton.Enabled = false;
				}
				BackupButton.Enabled = true;
				if (ComPorts.SelectedIndex >= 0) {
					EraseButton.Enabled = true;
				} else {
					EraseButton.Enabled = false;
				}
				LogButton.Enabled = true;
			}
		}

		private void AutoUART_CheckedChanged(object sender, EventArgs e)
		{
			IsAutoUart = AutoUART.Checked;
		}

		private void FastButton_CheckedChanged(object sender, EventArgs e)
		{
			SlowButton.Checked = !FastButton.Checked;
		}

		private void SlowButton_CheckedChanged(object sender, EventArgs e)
		{
			FastButton.Checked = !SlowButton.Checked;
			if (IsFirstTime && SlowButton.Checked) {
				IsFirstTime = false;
				MessageBox.Show("Press and hold Side Key 1 before turning your radio!", "Information");
			}
		}
	}
}
