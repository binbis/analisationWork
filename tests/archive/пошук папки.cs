
using System.Windows.Controls;

//. Метод для пошуку папки за іменем folderId серед усіх папок, крім #recycle
static string FindFolderById(string rootPath, string folderId) {
	try {
		// Отримати всі підкаталоги за вказаним шляхом, виключаючи #recycle
		var directories = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly)
								   .Where(d => Path.GetFileName(d) != "#recycle") // Виключаємо #recycle
								   .OrderByDescending(d => d) // Сортуємо з кінця
								   .ToArray();
		
		// Перевіряємо кожну з підкаталогів
		foreach (string specificFolderPath in directories) {
			// Перевіряємо наявність підкаталогів всередині папок (крім #recycle)
			var subDirectories = Directory.GetDirectories(specificFolderPath, "*", SearchOption.AllDirectories)
										  .OrderByDescending(d => d) // Сортуємо з кінця
										  .ToArray();
			
			// Перевірка кожної підкаталогової папки
			foreach (string directory in subDirectories) {
				// Перевірка, чи назва папки збігається з folderId
				if (Path.GetFileName(directory).Contains(folderId)) {
					return directory; // Повертає шлях до знайденої папки
				}
			}
		}
	}
	catch (UnauthorizedAccessException) {
		// перші спроби з формаим
		var b = new wpfBuilder("Window").WinSize(400);
		b.R.Add(out Label _, "Немає доступу до деяких підкаталогів на сервері.");
		b.R.AddOkCancel();
		b.End();
		b.Window.Topmost = true;
		b.ShowDialog();
	}
	catch (DirectoryNotFoundException) {
		// перші спроби з формаим
		var b = new wpfBuilder("Window").WinSize(400);
		b.R.Add(out Label _, "Шлях до серверної директорії не знайдено.");
		b.R.AddOkCancel();
		b.End();
		b.Window.Topmost = true;
		b.ShowDialog();
	}
	catch (IOException ex) {
		// перші спроби з формаим
		var b = new wpfBuilder("Window").WinSize(400);
		b.R.Add(out Label _, $"Помилка доступу до файлової системи: {ex.Message}");
		b.R.AddOkCancel();
		b.End();
		b.Window.Topmost = true;
		b.ShowDialog();
	}
	
	// Повернути null, якщо папку не знайдено
	return null;
}
//..

//. швидке відкриття папки по ід повідомлення

using System.Windows;
using System.Windows.Controls;

namespace CSLight {
	class Program {
		static void Main() {
			opt.key.KeySpeed = 25;
			opt.key.TextSpeed = 30;
			
			keys.send("Shift+Space*2"); //виділяємо весь рядок
			wait.ms(100);
			keys.send("Ctrl+C"); //копіюємо код
			wait.ms(100);
			string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
			string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
			// Присвоюємо змінним відповідні значення
			
			string combatLogId = parts[33]; // 1725666514064
			// шлях до папки з ід повідомленням
			string pathTo_combatLogId = @"\\SNG-8-sh\CombatLog\Donbas_Combat_Log";
			
			if (combatLogId.Length > 6) {
				deltaImportFiles(combatLogId, pathTo_combatLogId);
			}
		}
		// пошук файлів за ід для прикріплення (поки що не використовується)
		static void deltaImportFiles(string combatLogId, string pathTo_combatLogId) {
			
			if (combatLogId.Length > 6) {
				
				Process.Start("explorer.exe", Path.Combine(pathTo_combatLogId, combatLogId));
				wait.ms(450);
				
				
			}
		}
	}
}
//..
