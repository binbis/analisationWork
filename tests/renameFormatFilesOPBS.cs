/* 05,10,2024
проєкт - переіменування файлів в папці

формат зорі
21.08 П60 (Вампір) Міна 041024683 - Промах
21.08 П10 (FPV) БМП 041024013 - В21 Повторно уражено

формат мій
04.10 (час) Мавік М19 - (виліт) встан/ураж/в русі/стоїть...

який зараз
04.10 21.30 М111 (Лелека) Васабі ББМ  МТ-ЛБ 041024073 - виявлено 1

+(1) знайти та відкрити папку

*/

using System.Windows;
using System.Windows.Controls;

namespace CSLight {
	class Program {
		static void Main() {
			opt.key.KeySpeed = 20;
			opt.key.TextSpeed = 20;
			
			keys.send("Shift+Space*2"); //виділяємо весь рядок
			wait.ms(100);
			keys.send("Ctrl+C"); //копіюємо код
			wait.ms(100);
			string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
			string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
			// Присвоюємо змінним відповідні значення
			string dateJbd = getDDnMM(parts[0]); // 27.07.2024 - 27.07
			string timeJbd = parts[1].Replace(":", "."); // 00:40 - 00.40
			string commentJbd = parts[2].Replace("\n", " "); //коментар
			string numberOFlying = parts[3]; // 5
			string crewTeamJbd = TrimAfterDot(parts[4]); // R-18-1 (Мавка) .Replace("\n\t", " ")
			string whatDidJbd = parts[5]; // Мінування (можливо його видалю)
			string targetClassJbd = parts[7]; // Міна/Вантажівка/...
			string idTargetJbd = parts[9].Replace("/", ""); // Міна 270724043
			string establishedJbd = parts[24].ToLower(); // Встановлено/Уражено/Промах/...
			
			string messageId = parts[34]; // 1725666514064
			string pathDonbasFolder = @"\\SNG-8-sh\CombatLog\Donbas_Combat_Log"; // реальний шлях
			//string pathDonbasFolder = @"C:\Users\User-PM\Desktop\tests";
			
			string pathFilesOpen = Path.Combine(pathDonbasFolder, messageId);
			// спроба перейменувати назву
			var filesInDirectory = Directory.EnumerateFiles(pathFilesOpen, "*").Where(name => !name.EndsWith(".db")).ToArray();
			// перейменування кожного файлу в папці
			for (int i = 0; i < filesInDirectory.Length; i++) {
				
				string dir = Path.GetDirectoryName(filesInDirectory[i]); //ім'я директорії
				string fileName = Path.GetFileName(filesInDirectory[i]); //ім'я файлу
				string extension = Path.GetExtension(filesInDirectory[i]); // розширення
				string newPath = string.Empty;
				if (crewTeamJbd.Contains("FPV")) {
					newPath = Path.Combine(dir, $"{dateJbd} {timeJbd} {crewTeamJbd} В{numberOFlying} {idTargetJbd} - {establishedJbd} {i + 1}{extension}"); // новий шлях з директорії та файлу
				} else {
					newPath = Path.Combine(dir, $"{dateJbd} {timeJbd} {crewTeamJbd} {idTargetJbd} - {establishedJbd} {i + 1}{extension}"); // новий шлях з директорії та файлу
				}
				// перейменування елементу на те що хочу я
				File.Move(filesInDirectory[i], newPath);
				wait.ms(200);
				//Console.WriteLine(filesInDirectory[i] + "\n");
			}
			
			wait.ms(200);
			/*
			if (messageId.Length > 5) {
				//string pathFilesOpen = Path.Combine(pathDonbasFolder, messageId);
				Process.Start("explorer.exe", pathFilesOpen);
			}
			*/
		}
		// обрізати рядок усе після крапки
		static string TrimAfterDot(string str) {
			int dotIndex = str.IndexOf('.');
			if (dotIndex != -1) {
				return str.Substring(0, dotIndex);
			}
			return str;
		}
		// формат для перейменування відео 27.07.2024 - 27.07
		static string getDDnMM(string str) {
			string[] partOfDates = str.Split(".");
			return $"{partOfDates[0]}.{partOfDates[1]}";
		}
	}
}

