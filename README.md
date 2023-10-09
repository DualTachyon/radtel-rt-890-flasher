# Support

* If you like my work, you can support me through https://ko-fi.com/DualTachyon

# Flash utility for the Radtel RT-890 radio

I wrote this utility because there was no available flasher, official or otherwise, for this radio.
It was written fairly quickly in C# in .NET and thus targets mainly Windows.
It is known to work correctly under Linux with WINE, although the visual appearance may have some glitches.

# Building

Visual Studio 2022 Community Edition was used to build this utility.

# Instructions

The "Refresh" button is used to update the list box with available COM ports, so you don't have to restart the utility if you plug in your cable later.

The "Backup SPI" button is used to make a backup of the 4MBytes SPI flash on the RT-890.
It is recommended to do this step before flashing any OEFW firmware on your radio as it can be easily restored later.
If you have transfer reliability issues, you may want to use the 19200 baud mode.
You can activate this by holding the Side1 key before powering on your radio. Please the the 19200 baud mode is not available in bootloader mode.

Use the "Pick FW" button to select your firmware file.

The "Flash" button is used to flash the selected firmware file to your radio.
Press and hold the Side1 and Side2 keys before turning on your radio to enable update more.
The green LED will be on when this is done correctly.
The firmware file is reloaded every time the "Flash" button is clicked.

If the file is a full size binary (60416 bytes), the radio will automatically reboot.
Flashing firmware on the RT-890 is safe because the bootloader prevents updating itself and can always be invoked with the key combo mentioned above.

If you're a developer and have printfs going over UART, you may want to catch early boot messages when the radio reboots after flashing.
You can enable the "Auto UART after flash" to automatically switch the "Log" feature and capturing everything right away.

# License

Copyright 2023 Dual Tachyon
https://github.com/DualTachyon

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.