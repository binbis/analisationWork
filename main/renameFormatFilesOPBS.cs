/*/ c \analisationWork-main\globalClass\Bisbin.cs; /*/
/* 30,10,2024
проєкт - переіменування файлів в папці

+ знайти перейменувати вміст та відкрити папку
+ обійти екіпаж crewTeamJbd з кінця та обрізати усе до )
+ розділення на готове речення в буфер обміну

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
			keys.send("Ctrl+C"); // копіюємо код
			wait.ms(100);
			string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
			string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
			
			// вікно діалогу
			string[] examlpelesItem = {
				"1. Перейменування в папці Бамбас",
				"2. Готове речення в буфер обміну",
				"3. Перейменування в папці Уголь-дон",
				"4. Перейменування в папці Уголь-ссу",
			};
			var b = new wpfBuilder("Window").WinSize(400);
			b.R.Add("Назва", out ComboBox itemSelect).Items(examlpelesItem);
			b.R.AddOkCancel();
			b.Window.Topmost = true;
			b.End();
			// show dialog. Exit if closed not with the OK button.
			if (!b.ShowDialog()) return;
			
			// Присвоюємо змінним відповідні значення
			string dateJbd = getDDnMM(parts[0]); // 27.07.2024 - 27.07
			string timeJbd = parts[1].Replace(":", "."); // 00:40 - 00.40
			string commentJbd = parts[2].Replace("\n", " "); // коментар
			string numberOFlying = parts[3]; // 5
			string crewTeamJbd = Bisbin.GetCutsEcipash(Bisbin.TrimAfterDot(parts[4].Replace("\n", ""))); // R-18 (Мавка)
			string whatToDo = parts[5];// Дорозвідка / Мінування ....
			string targetClassJbd = parts[7]; // Міна/Вантажівка/...
			string idTargetJbd = parts[9].Replace("/", ""); // Міна 270724043
			string establishedJbd = parts[24]; // Встановлено/Уражено/Промах/...
			
			string messageId = parts[34]; // 1725666514064
			string pathDonbasFolder = @"\\SNG-8-sh\CombatLog\Donbas_Combat_Log"; // 
			string pathCoalFolderDon = @"\\Sng-2\аеророзвідка\combatlog\Vugl_Combat_Log"; //
			string pathCoalFolderSsy = @"\\Sng-2\аеророзвідка\combatlog\Sumy_Combat_Log"; // 
			
			if (itemSelect.Text.Contains("2.")) {
				if (crewTeamJbd.Contains("FPV")) {
					Clipboard.SetText($"{dateJbd} {timeJbd} {crewTeamJbd} В{numberOFlying} - {establishedJbd.ToLower()}");
					return;
				} else {
					Clipboard.SetText($"{dateJbd} {timeJbd} {crewTeamJbd} {idTargetJbd} - {establishedJbd.ToLower()}");
					return;
				}
			}
			
			if (messageId.Length > 3) {
				string pathFilesOpen = string.Empty;
				if (itemSelect.Text.Contains("3.")) {
					pathFilesOpen = Path.Combine(pathCoalFolderDon, messageId);
				} else if (itemSelect.Text.Contains("4.")) {
					pathFilesOpen = Path.Combine(pathCoalFolderSsy, messageId);
				} else {
					pathFilesOpen = Path.Combine(pathDonbasFolder, messageId);
				}
				
				// спроба перейменувати назву
				var filesInDirectory = Directory.EnumerateFiles(pathFilesOpen, "*").Where(name => !name.EndsWith(".db")).ToArray();
				
				// підготовка для скорочення
				// превірка статусу
				establishedJbd = getTrueEstablished(establishedJbd, commentJbd, whatToDo);
				if (whatToDo == "Дорозвідка") {
					establishedJbd = whatToDo.ToLower() + " - " + establishedJbd;
				}
				
				// перейменування кожного файлу в папці
				for (int i = 0; i < filesInDirectory.Length; i++) {
					
					string dir = Path.GetDirectoryName(filesInDirectory[i]); // ім'я директорії
					string fileName = Path.GetFileName(filesInDirectory[i]); // ім'я файлу
					string extension = Path.GetExtension(filesInDirectory[i]); // розширення .jpg .mp4 ..
					string newPath = string.Empty;
					
					if (extension.Length < 2) {
						extension = "";
					}
					if (crewTeamJbd.Contains("FPV")) {
						newPath = Path.Combine(dir, $"{dateJbd} {timeJbd} {crewTeamJbd} В{numberOFlying} - {establishedJbd.ToLower()} {i + 1}{extension}"); // новий шлях з директорії та файлу
					} else {
						newPath = Path.Combine(dir, $"{dateJbd} {timeJbd} {crewTeamJbd} {idTargetJbd} - {establishedJbd.ToLower()} {i + 1}{extension}"); // новий шлях з директорії та файлу
					}
					// перейменування елементу на те що хочу я
					File.Move(filesInDirectory[i], newPath);
					wait.ms(200);
					//Console.WriteLine(filesInDirectory[i] + "\n");
				}
				if (itemSelect.Text.Contains("1.") || itemSelect.Text.Contains("3.") || itemSelect.Text.Contains("4.")) {
					// відкриття папки за шляхом
					Process.Start("explorer.exe", pathFilesOpen);
				}
			}
			
		}
		// формат для перейменування відео 27.07.2024 - 27.07
		static string getDDnMM(string str) {
			string[] partOfDates = str.Split(".");
			return $"{partOfDates[0]}.{partOfDates[1]}";
		}
		// перевірка для статусу з жбд
		static string getTrueEstablished(string establishedJbd, string commentJbd, string whatToDo) {
			if (establishedJbd == "Підтв. ураж.") {
				return "зйом.ураж";
				//return whatToDo;
			} else if (establishedJbd.ToLower().Contains("знищ")) {
				return "знищ";
			}
			if (establishedJbd.ToLower().Contains("ураж")) {
				return "ураж";
			}
			
			if (establishedJbd.Contains("Встановлено")) {
				return "встан";
			}
			if (establishedJbd == "Підтверджено") {
				if (commentJbd.ToLower().Contains("знищ")) {
					return "знищ";
				}
				if (commentJbd.ToLower().Contains("ураж")) {
					return "ураж";
				}
			}
			return establishedJbd;
		}
	}
}

