
/**
вставляєш в свій пк будь-яку кількість флешок
вказуєш шлях в коді, повний шлях до папки куди хочеш 
жмеш скрить
він послідовно вивантажить, кожну флешку в окрему папку, щей підпише хї 

* Для кожної флешки унікальна назва
* пропонує місце, куди вказати шлях для вивантаження папок (можна вписати самому та пропонується за замовчуваннм)
* Інформація який № флешки воно вивантажує;
* Кількість файлів
* Приблизну швидкість викачки
* Перевірка файлів на цілісність після завершення викачки;
* перевірка вільного місця для поточної флешки, якщо його недостатньо зупиняє копіювання та викликає діалогове вікно


2) loadbar про завантаження;
4) Відсоток від 100% скільки воно вже вивантажили
6) "Темна тема"
*/

using System.Windows.Controls;


class Program {
	static void Main() {
		// Список дисків, які можуть бути флешками
		string[] removableDrives = Directory.GetLogicalDrives();
		
		var b = new wpfBuilder("Window").WinSize(600);
		b.R.Add("Шлях до папки", out TextBox recomendedPath, @"\\SNG-8-sh\Аеророзвідка\(4) Буфер").Focus();
		b.R.AddOkCancel();
		b.Window.Topmost = true;
		b.End();
		// show dialog. Exit if closed not with the OK button.
		if (!b.ShowDialog()) return;
		// Шлях до папки, куди будуть копіюватися файли
		string destinationBasePath = recomendedPath.Text;
		
		int driveNumber = 1;
		
		foreach (string drive in removableDrives) {
			try {
				// Перевірка, чи є диск знімним (флешкою)
				DriveInfo driveInfo = new DriveInfo(drive);
				if (driveInfo.DriveType == DriveType.Removable && driveInfo.IsReady) {
					string sourceDir = Path.Combine(drive, ""); // Шлях до кореневої папки флешки
					string destinationPath = Path.Combine(destinationBasePath, $"FlashDrive_{driveNumber}_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
					Directory.CreateDirectory(destinationPath);
					
					Console.WriteLine($"Копіювання з {sourceDir} в {destinationPath}...");
					
					if (!HasEnoughSpaceForCopy(driveInfo, sourceDir)) {
						// перші спроби з формаим
						var br = new wpfBuilder("Window").WinSize(500);
						br.R.Add(out Label _, $"Недостатньо місця на диску для копіювання з {sourceDir}.");
						br.R.AddOkCancel();
						br.End();
						br.Window.Topmost = true;
						if (!br.ShowDialog()) return;
						
					} else {
						CopyDirectory(sourceDir, destinationPath);
						driveNumber++;
					}
				}
			}
			catch (Exception ex) {
				// перші спроби з формаим
				var br = new wpfBuilder("Window").WinSize(500);
				br.R.Add(out Label _, $"Помилка при доступі до {drive}: {ex.Message}");
				br.R.AddOkCancel();
				br.End();
				br.Window.Topmost = true;
				if (!br.ShowDialog()) return;
			}
		}
		
		// перші спроби з формаим
		var brr = new wpfBuilder("Window").WinSize(500);
		brr.R.Add(out Label _, $"Копіювання завершено");
		brr.R.AddOkCancel();
		brr.End();
		brr.Window.Topmost = true;
		if (!brr.ShowDialog()) return;
	}
	
	// Функція для перевірки, чи достатньо вільного місця для копіювання
	private static bool HasEnoughSpaceForCopy(DriveInfo driveInfo, string sourceDir) {
		long totalSizeToCopy = GetDirectorySize(sourceDir);
		long availableSpace = driveInfo.AvailableFreeSpace;
		
		Console.WriteLine($"Загальний розмір файлів для копіювання: {FormatSize(totalSizeToCopy)}");
		Console.WriteLine($"Доступний простір на диску: {FormatSize(availableSpace)}");
		
		return availableSpace >= totalSizeToCopy;
	}
	
	// Функція для обчислення загального розміру директорії
	private static long GetDirectorySize(string dirPath) {
		string[] allFiles = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories);
		long totalSize = 0;
		
		foreach (string file in allFiles) {
			FileInfo fileInfo = new FileInfo(file);
			totalSize += fileInfo.Length;
		}
		
		return totalSize;
	}
	
	// Функція для копіювання директорії з вмістом
	private static void CopyDirectory(string sourceDir, string destinationDir) {
		string[] allFiles = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
		string[] allDirectories = Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories);
		
		int totalItems = allFiles.Length + allDirectories.Length;
		int copiedItems = 0;
		
		long totalSizeBytes = 0;
		long copiedSizeBytes = 0;
		
		// Обчислюємо загальний розмір всіх файлів
		foreach (string file in allFiles) {
			FileInfo fileInfo = new FileInfo(file);
			totalSizeBytes += fileInfo.Length;
		}
		
		// Використовуємо Stopwatch для вимірювання часу копіювання
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		
		// Копіюємо всі файли
		foreach (string file in allFiles) {
			try {
				string destFile = Path.Combine(destinationDir, Path.GetRelativePath(sourceDir, file));
				Directory.CreateDirectory(Path.GetDirectoryName(destFile));
				
				FileInfo fileInfo = new FileInfo(file);
				File.Copy(file, destFile, true);
				
				copiedItems++;
				copiedSizeBytes += fileInfo.Length;
				
				// Обчислення часу, що залишився
				double averageTimePerItem = stopwatch.Elapsed.TotalSeconds / copiedItems;
				double estimatedRemainingTime = averageTimePerItem * (totalItems - copiedItems);
				
				// Виведення прогресу копіювання
				Console.WriteLine($"Копіювання файлу: {file} ({copiedItems}/{totalItems}) - " +
								  $"Скопійовано {FormatSize(copiedSizeBytes)} із {FormatSize(totalSizeBytes)} - " +
								  $"Залишилося приблизно {TimeSpan.FromSeconds(estimatedRemainingTime):hh\\:mm\\:ss}");
			}
			catch (Exception ex) {
				Console.WriteLine($"Помилка копіювання файлу {file}: {ex.Message}");
			}
		}
		
		// Копіюємо всі підпапки
		foreach (string dir in allDirectories) {
			try {
				string destDir = Path.Combine(destinationDir, Path.GetRelativePath(sourceDir, dir));
				Directory.CreateDirectory(destDir);
				
				copiedItems++;
				
				// Обчислення часу, що залишився
				double averageTimePerItem = stopwatch.Elapsed.TotalSeconds / copiedItems;
				double estimatedRemainingTime = averageTimePerItem * (totalItems - copiedItems);
				
				// Виведення прогресу копіювання для папки
				Console.WriteLine($"Копіювання папки: {dir} ({copiedItems}/{totalItems}) - " +
								  $"Скопійовано {FormatSize(copiedSizeBytes)} із {FormatSize(totalSizeBytes)} - " +
								  $"Залишилося приблизно {TimeSpan.FromSeconds(estimatedRemainingTime):hh\\:mm\\:ss}");
			}
			catch (Exception ex) {
				Console.WriteLine($"Помилка копіювання папки {dir}: {ex.Message}");
			}
		}
		
		stopwatch.Stop();
	}
	
	// Функція для форматування розміру файлу
	private static string FormatSize(long bytes) {
		const long OneGb = 1024 * 1024 * 1024;
		
		return $"{(double)bytes / OneGb:F2} GB";
	}
}

