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
using System.IO.Ports;

public class RT_890_UART {
	private SerialPort Port;

	private void Checksum(byte[] Command)
	{
		byte Sum = 0;
		int i;

		for (i = 0; i < Command.Length - 1; i++) {
			Sum += Command[i];
		}
		Command[i] = Sum;
	}

	private bool Verify(byte[] Command)
	{
		byte Sum = 0;
		int i;

		for (i = 0; i < Command.Length - 1; i++) {
			Sum += Command[i];
		}
		if (Command[i] == Sum) {
			return true;
		}

		return false;
	}

	public bool Command_EraseFlash()
	{
		byte[] Command = new byte[5];

		Command[0] = 0x39;
		Command[3] = 0x55;
		Checksum(Command);
		Port.Write(Command, 0, Command.Length);

		try {
			return (Port.ReadByte() == 0x06) ? true : false;
		} catch {
			return false;
		}
	}

	public bool Command_WriteFlash(ushort Offset, byte[] Firmware)
	{
		byte[] Command = new byte[128 + 4];

		Command[0] = 0x57;
		Command[1] = (byte)((Offset >> 8) & 0xFF);
		Command[2] = (byte)((Offset >> 0) & 0xFF);
		int ChunkLength = (Firmware.Length - Offset);
		if (ChunkLength > 128) {
			ChunkLength = 128;
		}
		Array.Copy(Firmware, Offset, Command, 3, ChunkLength);
		Checksum(Command);
		Port.Write(Command, 0, Command.Length);

		try {
			return (Port.ReadByte() == 0x06) ? true : false;
		} catch {
			return false;
		}
	}

	public byte[] Command_ReadFlash(ushort Offset)
	{
		byte[] Command = new byte[4];

		Command[0] = 0x52;
		Command[1] = (byte)((Offset >> 8) & 0xFF);
		Command[2] = (byte)((Offset >> 0) & 0xFF);
		Checksum(Command);
		Port.Write(Command, 0, Command.Length);

		try {
			int Pos = 0;
			byte[] Block = new byte[128 + 4];

			while (Pos != Block.Length) {
				int ret = Port.Read(Block, Pos, Block.Length - Pos);
				if (Block[0] == 0xFF) {
					return null;
				}
				Pos += ret;
			}
			if (Verify(Block)) {
				byte[] Data = new byte[128];
				Array.Copy(Block, 3, Data, 0, 128);
				return Data;
			}
		} catch {
		}

		return null;
	}

	public void Open(string ComPort, int BaudRate = 115200)
	{
		Port = new SerialPort(ComPort);
		Port.BaudRate = BaudRate;
		Port.Parity = Parity.None;
		Port.StopBits = StopBits.One;
		Port.DataBits = 8;
		Port.ReadTimeout = 2000;
		Port.WriteTimeout = 2000;
		Port.Open();
	}

	public bool IsBootLoaderMode()
	{
		byte[] Data = Command_ReadFlash(0);

		if (Data != null) {
			return false;
		}

		for (int i = 0; i < 8; i++) {
			if (Port.BytesToRead == 0) {
				break;
			}
			Port.ReadByte();
		}

		return true;
	}

	public int Read()
	{
		try {
			return Port.ReadByte();
		} catch {
			return -1;
		}
	}

	public string ReadLine()
	{
		try {
			return Port.ReadLine();
		} catch {
			return "";
		}
	}

	public void AddDelegate(SerialDataReceivedEventHandler DataReceived)
	{
		Port.DataReceived += DataReceived;
	}

	public void Close()
	{
		Port.Close();
	}
}
