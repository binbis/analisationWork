/** 18.10.2024

* Для кожної флешки унікальна назва
* пропонує місце, куди вказати шлях для вивантаження папок (можна вписати самому та пропонується за замовчуваннм)
* Інформація який № флешки воно вивантажує;
* Кількість файлів
* Приблизну швидкість викачки
* Перевірка файлів на цілісність після завершення викачки;
* перевірка вільного місця для поточної флешки, якщо його недостатньо зупиняє копіювання та викликає діалогове вікно
- loadbar про завантаження(реалізовано в конлоних логах)
* фінальне вікно: кількість флешок, людино годин, сумарно гігов

4) Відсоток від 100% скільки воно вже вивантажили
6) "Темна тема"
*/

using System.Windows.Controls;

class Program {
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	[return: MarshalAs(UnmanagedType.Bool)]
	static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
		out ulong lpFreeBytesAvailable,
		out ulong lpTotalNumberOfBytes,
		out ulong lpTotalNumberOfFreeBytes);
	
	static int Main() {
		// Список дисків, які можуть бути флешками
		string[] removableDrives = Directory.GetLogicalDrives();
		
		var b = new wpfBuilder("Window").WinSize(600);
		b.R.Add("Шлях до папки", out TextBox recomendedPath, @"\\SNG-8-sh\Аеророзвідка\(4) Буфер").Focus();
		b.R.AddOkCancel();
		b.Window.Topmost = true;
		b.End();
		// show dialog. Exit if closed not with the OK button.
		if (!b.ShowDialog()) return 0;
		// Шлях до папки, куди будуть копіюватися файли
		string destinationBasePath = recomendedPath.Text;
		
		int driveNumber = 1; // кількість флешок
		long summaGiGybite = 0; // сумарна кількість
		string timeMs = string.Empty; // загальний час
			
		foreach (string drive in removableDrives) {
			// старт таймера
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			
			try {
				// Перевірка, чи є диск знімним (флешкою)
				DriveInfo driveInfo = new DriveInfo(drive);
				if (driveInfo.DriveType == DriveType.Removable && driveInfo.IsReady) {
					string sourceDir = Path.Combine(drive, ""); // Шлях до кореневої папки флешки
					string destinationPath = Path.Combine(destinationBasePath, $"FlashDrive_{driveNumber}_" + DateTime.Now.ToString("MM-dd_HH-mm-ss"));
					// створення унікальеої папки
					Directory.CreateDirectory(destinationPath);
					Console.WriteLine($"Копіювання з {sourceDir} в {destinationPath}...");
					wait.s(0.5);
					
					if (HasEnoughSpaceForCopy(destinationBasePath, sourceDir)) {
						summaGiGybite += GetDirectorySize(sourceDir);
						CopyDirectory(sourceDir, destinationPath);
						driveNumber++;
					} else {
						// перші спроби з формаим
						var br = new wpfBuilder("Window").WinSize(500);
						br.R.Add(out Label _, $"Недостатньо місця на диску для копіювання в {destinationBasePath}.");
						br.R.AddOkCancel();
						br.End();
						br.Window.Topmost = true;
						br.ShowDialog();
						return 0;
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
				if (!br.ShowDialog()) return 0;
			}
			// зупиняємо таймер та вимірюємо кількість часу, затраченого
			double elapsedTotal = stopWatch.Elapsed.TotalSeconds / (driveNumber - 1);
			timeMs = $"{TimeSpan.FromSeconds(elapsedTotal):hh\\:mm\\:ss}";
			stopWatch.Stop();
		}
		
		// перші спроби з формаим
		var brr = new wpfBuilder("Window").WinSize(500);
		brr.R.Add(out Label _, $"Копіювання завершено");
		brr.R.Add("", out TextBox _, $"Кількість флешок = {driveNumber - 1}").Readonly(); //read-only text
		brr.R.Add("", out TextBox _, $"Кількість копійованих гігабайт = {FormatSize(summaGiGybite)}").Readonly(); //read-only text
		brr.R.Add("", out TextBox _, $"Людино годин затрачених на копіювання = {timeMs}").Readonly(); //read-only text
		brr.R.AddOkCancel();
		brr.End();
		brr.Window.Topmost = true;
		if (!brr.ShowDialog()) return 0;
		return 0;
	}
	
	// Функція для перевірки, чи достатньо вільного місця для копіювання
	private static bool HasEnoughSpaceForCopy(string destinationBasePath, string sourceDir) {
		double totalSizeToCopy = GetDirectorySize(sourceDir) / (1024.0 * 1024 * 1024);
		double freeSpaceInGB = 0.0;
		if (GetDiskFreeSpaceEx(destinationBasePath, out ulong freeBytesAvailable, out _, out _)) {
			// Перетворюємо байти в гігабайти
			freeSpaceInGB = freeBytesAvailable / (1024.0 * 1024 * 1024);
			Console.WriteLine($"Загальний розмір файлів для копіювання поточної флешки: {totalSizeToCopy:F2} GB");
			Console.WriteLine($"Доступний простір на диску: {freeSpaceInGB:F2} GB");
			return freeSpaceInGB > totalSizeToCopy;
		} else {
			Console.WriteLine("Error retrieving disk space information.");
			return false;
		}
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
		//Stopwatch stopwatch = new Stopwatch();
		//stopwatch.Start();
		
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
				//double averageTimePerItem = stopwatch.Elapsed.TotalSeconds / copiedItems;
				//double estimatedRemainingTime = averageTimePerItem * (totalItems - copiedItems);
				
				// Виведення прогресу копіювання
				Console.WriteLine($"Копіювання файлу: {file} ({copiedItems}/{totalItems}) - " +
								  $"Скопійовано {FormatSize(copiedSizeBytes)} із {FormatSize(totalSizeBytes)} - "
								 //$"Залишилося приблизно {TimeSpan.FromSeconds(estimatedRemainingTime):hh\\:mm\\:ss}");
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
				//double averageTimePerItem = stopwatch.Elapsed.TotalSeconds / copiedItems;
				//double estimatedRemainingTime = averageTimePerItem * (totalItems - copiedItems);
				
				// Виведення прогресу копіювання для папки
				Console.WriteLine($"Копіювання папки: {dir} ({copiedItems}/{totalItems}) - " +
								  $"Скопійовано {FormatSize(copiedSizeBytes)} із {FormatSize(totalSizeBytes)} - "
								  //$"Залишилося приблизно {TimeSpan.FromSeconds(estimatedRemainingTime):hh\\:mm\\:ss}");
			}
			catch (Exception ex) {
				Console.WriteLine($"Помилка копіювання папки {dir}: {ex.Message}");
			}
		}
		
		//stopwatch.Stop();
	}
	
	// Функція для форматування розміру файлу
	private static string FormatSize(long bytes) {
		const long OneGb = 1024 * 1024 * 1024;
		
		return $"{(double)bytes / OneGb:F2} GB";
	}
	// Функція для форматування розміру файлу
	private static double FormatSizeOne(long bytes) {
		const long OneGb = 1024 * 1024 * 1024;
		
		return (double)bytes / OneGb;
	}
}