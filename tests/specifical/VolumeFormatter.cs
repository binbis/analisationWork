/*

код форматує усі накопичувачі типу флешка

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
*/

using System.Windows.Controls;
using System.Windows.Media;

class Program {
	static int Main() {
		
		var b = new wpfBuilder("Window").WinSize(600);
		b.Brush(Brushes.Moccasin); //dialog background color
		b.Font(size: 18, bold: true); //default font for all controls
		b.R.Add(out Label recomendedPath, "            Ти певний?\n Я ж форматну усі флешки.").Align("c", "c");
		b.R.AddOkCancel().Focus();
		b.Window.Topmost = true;
		b.End();
		// show dialog. Exit if closed not with the OK button.
		if (!b.ShowDialog()) return 0;
		
		// Отримуємо всі диски типу "Знімний диск" (флешки)
		var removableDrives = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Removable && d.IsReady);
		
		foreach (var drive in removableDrives) {
			Console.WriteLine($"Форматуємо диск {drive.Name}...");
			FormatDriveWithDiskpart(drive.Name.TrimEnd('\\')); // Передаємо букву диска без слешу
		}
		
		Console.WriteLine("Спроба форматування усіх флешок завершена.");
		return 0;
	}
	
	static void FormatDriveWithDiskpart(string driveLetter) {
		try {
			// Створюємо файл команд для diskpart
			string diskpartScriptPath = Path.GetTempFileName();
			File.WriteAllText(diskpartScriptPath, $"select volume {driveLetter}\nclean\ncreate partition primary\nformat fs=exfat quick\nassign letter={driveLetter}\nexit");
			
			// Створюємо процес для запуску diskpart з нашим файлом команд
			var diskpartProcess = new Process {
				StartInfo = new ProcessStartInfo {
					FileName = "diskpart",
					Arguments = $"/s \"{diskpartScriptPath}\"",
					UseShellExecute = false,
					CreateNoWindow = true,
					RedirectStandardOutput = true,
					RedirectStandardError = true
				}
			};
			
			diskpartProcess.Start();
			diskpartProcess.WaitForExit();
			
			// Вивід результатів
			string output = diskpartProcess.StandardOutput.ReadToEnd();
			string error = diskpartProcess.StandardError.ReadToEnd();
			
			if (diskpartProcess.ExitCode == 0) {
				Console.WriteLine($"Диск {driveLetter} успішно відформатовано.");
			} else {
				Console.WriteLine($"Помилка форматування диска {driveLetter}: {error}");
			}
			
			// Видаляємо тимчасовий файл
			File.Delete(diskpartScriptPath);
		}
		catch (Exception ex) {
			Console.WriteLine($"Помилка при спробі форматування диска {driveLetter}: {ex.Message}");
		}
	}
}
