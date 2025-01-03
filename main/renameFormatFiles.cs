/*/ 
c \analisationWork\globalClass\Bisbin.cs; 
c \analisationWork\globalClass\RowByParts.cs; 
/*/
/* 03,01,2025 v1.0.1

!!renameAll!!:
проєкт - переіменування файлів в папці

+ знайти перейменувати вміст та відкрити папку
+ обійти екіпаж crewTeamJbd з кінця та обрізати усе до )
+ розділення на готове речення в буфер обміну

!!recordPathToMemory!!:
на основі жбд створує папку в папці з ієрархією
folde -> нава екіпажу -> ід цілі
та відкриває її

потім зможе щей перейменовувати, до ід цілі номер вильоту приписувати інший вже до існуючого

*/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CSLight {
	class Program {
		static int Main() {
			opt.key.KeySpeed = 20;
			opt.key.TextSpeed = 20;
			
			var bMain = new wpfBuilder("Window").WinSize(600); // основне вікно
			var b = bMain; // 
			b.Row(-1).Add(out TabControl tc).Height(250..); // підтримка табуляції + фіксована висота
			b.R.AddOkCancel(apply: "_Apply").Font(size: 14, bold: false); // додаткова кнопка до ok cansel
			b.Window.Topmost = true; // поверх усіх вікон
			// 
			wpfBuilder _Page(string name, WBPanelType panelType = WBPanelType.Grid) {
				var tp = new TabItem { Header = name };
				tc.Items.Add(tp);
				return new wpfBuilder(tp, panelType);
			}
			
			var b1 = b = _Page("Скрипти");
			//Brush
			b.Font(size: 17, bold: true);
			b.Brush(Brushes.DarkGray);
			// insider
			b.R.AddButton("1. Готове речення в буфер обміну", 2).Brush(Brushes.LightCoral);
			b.R.AddButton("2. Створти папку екіпаж -> ід", 5).Brush(Brushes.LightCyan);
			b.R.AddButton("3. Перейменування в папці Бамбас", 1).Brush(Brushes.LightSalmon);
			b.R.AddButton("4. Перейменування в папці Уголь", 3).Brush(Brushes.LightGoldenrodYellow);
			b.R.AddButton("5. Перейменування в папці Суджа", 4).Brush(Brushes.LightGoldenrodYellow);
			b.End();
			
			var b2 = b = _Page("Технічне налаштування");
			//Brush
			b.Font(size: 16, bold: true);
			b.Brush(Brushes.DarkGray);
			// insider
			b.R.Add(out Label _, "Шлях до папки де буде створено папки в папках").AlignContent(HorizontalAlignment.Center);
			b.R.Add(out TextBox rememberedTextPath).Font(size: 14, bold: false).Size(400, 40).AddButton("Запам'ятати", 99).Brush(Brushes.LightCoral).Width(125);
			b.End();
			b = bMain.End();
			if (!b.ShowDialog()) return 1;
			
			int dialogButtonResOne = b1.ResultButton; // значення нажатої кнопки
			int dialogButtonResTwo = b2.ResultButton; // значення нажатої кнопки
			string rememberPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"LibreAutomate\Main\files\analisationWork\customTemp\pathes.txt"); // шлях до файлу зі збереженим шляхом
			
			if (dialogButtonResTwo == 99) {
				recordPathToMemory(rememberedTextPath.Text, rememberPath);
				return 0;
			}
			
			// основа
			if (dialogButtonResOne <= 4) {
				keys.send("Ctrl+C"); // копіюємо код
				wait.ms(100);
				renameAll(clipboard.copy(), dialogButtonResOne);
				return 0;
			}
			
			if (dialogButtonResOne == 5) {
				keys.send("Ctrl+C"); // копіюємо код
				wait.ms(100);
				PathWeaver(clipboard.copy(), rememberPath);
				return 0;
			}
			return 0;
		}
		// функ перейменування 
		static void renameAll(string clipboardData, int dialogButtonRes) {
			string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
			Bisbin Bisbin = new Bisbin();
			RowByParts instance = new RowByParts(parts);
			
			// Присвоюємо змінним відповідні значення
			string dateJbd = getDDnMM(instance.Date); // 27.07.2024 - 27.07
			string timeJbd = instance.Time.Replace(":", "."); // 00:40 - 00.40
			string commentJbd = instance.DescrtiptionComment.Replace("\n", " "); // коментар
			string crewTeamJbd = Bisbin.StringReducer.TrimAfterFirstClosingParenthes(Bisbin.StringReducer.TrimAfterFirstDot(instance.CrewTeam.Replace("\n", ""))); // R-18 (Мавка)
			string idTargetJbd = instance.TargetId.Replace("/", ""); // Міна 270724043
			
			string pathDonbasFolder = @"\\SNG-8-sh\CombatLog\Donbas_Combat_Log"; // 
			string pathCoalFolderDon = @"\\Sng-2\аеророзвідка\combatlog\Vugl_Combat_Log"; //
			string pathCoalFolderSsy = @"\\Sng-2\аеророзвідка\combatlog\Sumy_Combat_Log"; // 
			
			if (dialogButtonRes == 2) {
				if (crewTeamJbd.Contains("FPV")) {
					Clipboard.SetText($"{dateJbd} {timeJbd} {crewTeamJbd} В{instance.FlightNumber} - {instance.Status.ToLower()}");
					return;
				} else {
					Clipboard.SetText($"{dateJbd} {timeJbd} {crewTeamJbd} {idTargetJbd} - {instance.Status.ToLower()}");
					return;
				}
			}
			
			if (instance.MessageId.Length > 3) {
				string pathFilesOpen = string.Empty;
				if (dialogButtonRes == 3) {
					pathFilesOpen = Path.Combine(pathCoalFolderDon, instance.MessageId);
				} else if (dialogButtonRes == 4) {
					pathFilesOpen = Path.Combine(pathCoalFolderSsy, instance.MessageId);
				} else {
					pathFilesOpen = Path.Combine(pathDonbasFolder, instance.MessageId);
				}
				
				if (!filesystem.exists(pathFilesOpen)) {
					//osdText.showTransparentText("Transparent text");
					dialog.show("Помилка", $"Незнайдено подібної папки {pathDonbasFolder}/{instance.MessageId} \nрекомендація: преевірити бота, можливо він вимкнений, також перевірити чи є досуп до серверу", secondsTimeout: 5);
					Console.WriteLine($"Незнайдено подібної папки {pathDonbasFolder}/{instance.MessageId}, рекомендація: преевірити бота, можливо він вимкнений, також перевірити чи є досуп до серверу");
					return;
				}
				
				// спроба перейменувати назву
				var filesInDirectory = Directory.EnumerateFiles(pathFilesOpen, "*").Where(name => !name.EndsWith(".db")).ToArray();
				
				// підготовка для скорочення
				// превірка статусу
				string establishedJbd = getTrueEstablished(instance.Status, commentJbd);
				if (instance.Goal == "Дорозвідка") {
					establishedJbd = instance.Goal.ToLower() + " - " + establishedJbd;
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
						newPath = Path.Combine(dir, $"{dateJbd} {timeJbd} {crewTeamJbd} В{instance.FlightNumber} - {establishedJbd.ToLower()} {i + 1}{extension}"); // новий шлях з директорії та файлу
					} else {
						newPath = Path.Combine(dir, $"{dateJbd} {timeJbd} {crewTeamJbd} {idTargetJbd} - {establishedJbd.ToLower()} {i + 1}{extension}"); // новий шлях з директорії та файлу
					}
					// перейменування елементу на те що хочу я
					File.Move(filesInDirectory[i], newPath);
					wait.ms(200);
					//Console.WriteLine(filesInDirectory[i] + "\n");
				}
				if (dialogButtonRes == 1 || dialogButtonRes == 3 || dialogButtonRes == 4) {
					// відкриття папки за шляхом
					Process.Start("explorer.exe", pathFilesOpen);
				}
			}
		}
		// папки з папками
		static void PathWeaver(string clipboardData, string rememberPath) {
			string pathWithRemebers = File.ReadAllText(rememberPath);
			string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
			
			Bisbin Bisbin = new Bisbin();
			RowByParts instance = new RowByParts(parts);
			
			// день
			if (instance.CrewTeam.Contains("FPV") || instance.Goal.Contains("Зйомка")) {
				string existDictName = string.Empty;
				// отримати масив усіх імен папок
				string[] arrAllDicNames = filesystem.enumDirectories(pathWithRemebers).Select(o => o.Name).ToArray();
				
				// перевірити чи існує вже така папка
				foreach (string elem in arrAllDicNames) {
					if (elem.Contains(instance.TargetId)) {
						Process.Start("explorer.exe", pathname.combine(pathWithRemebers, elem)); // відкриваємо папку
						script.end();
					}
				}
				// схоже такої папки немає тому створимо нову
				string tryCreateFolder = pathname.combine(pathWithRemebers, $"{arrAllDicNames.Length}. {instance.TargetId}"); // збираю шлях
				filesystem.createDirectory(tryCreateFolder);// пробую створити папку
				Process.Start("explorer.exe", tryCreateFolder); // відкриваємо папку
				//Console.WriteLine(existDictName);
				
			} else {
				// нічна
				string partFolderByCrew = $"{instance.CrewTeam}"; // заготовка екіпажа
				string partFolderByFlyNumberAndId = $"({instance.FlightNumber}) {instance.TargetId}"; // заготовка ід
				
				string pathToFolderByCrew = pathname.combine(pathWithRemebers, partFolderByCrew); // збирає шляхи папки екіпаж
				bool f = filesystem.createDirectory(pathToFolderByCrew); // пробую створити папку
				//перевірка
				if (!f) {
					Console.WriteLine($"папка {instance.CrewTeam} вже існує");
				}
				wait.ms(400);
				string pathToFolderById = pathname.combine(pathToFolderByCrew, partFolderByFlyNumberAndId); // збирає шляхи папки ід
				f = filesystem.createDirectory(pathToFolderById); // пробую створити папку
				//перевірка
				if (!f) {
					Console.WriteLine($"папка {partFolderByFlyNumberAndId} в {partFolderByCrew} папці вже існує");
				}
				//Console.WriteLine($"{}");
				Process.Start("explorer.exe", pathToFolderById); // відкриваємо папку
			}
		}
		// запам'ятовувалка
		static void recordPathToMemory(string rememberedTextPath, string rememberPath) {
			File.WriteAllText(rememberPath, rememberedTextPath); //creates new file or overwrites existing file
			osdText.showTransparentText("Успішно перезеписав шлях");
		}
		// формат для перейменування відео 27.07.2024 - 27.07
		static string getDDnMM(string str) {
			string[] partOfDates = str.Split(".");
			return $"{partOfDates[0]}.{partOfDates[1]}";
		}
		// перевірка для статусу з жбд
		static string getTrueEstablished(string establishedJbd, string commentJbd) {
			if (establishedJbd == "Підтв. ураж.") {
				return "зйом.ураж";
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

