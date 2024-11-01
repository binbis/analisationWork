/*

код форматує усі накопичувачі типу флешка

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
*/

class Program {
	static void Main() {
		// Отримуємо всі диски типу "Знімний диск" (флешки)
		var removableDrives = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Removable && d.IsReady);
		
		foreach (var drive in removableDrives) {
			Console.WriteLine($"Форматуємо диск {drive.Name}...");
			FormatDriveWithDiskpart(drive.Name.TrimEnd('\\')); // Передаємо букву диска без слешу
		}
		
		Console.WriteLine("Спроба форматування усіх флешок завершена.");
	}
	
	static void FormatDriveWithDiskpart(string driveLetter) {
		try {
			// Створюємо файл команд для diskpart
			string diskpartScriptPath = Path.GetTempFileName();
			File.WriteAllText(diskpartScriptPath, $"select volume {driveLetter}\nclean\ncreate partition primary\nformat fs=ntfs quick\nassign letter={driveLetter}\nexit");
			
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
