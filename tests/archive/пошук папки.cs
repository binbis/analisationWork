
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
