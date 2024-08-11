/**
вставляєш в свій пк будь-яку кількість флешок
вказуєш шлях в коді, повний шлях до папки куди хочеш 
жмеш скрить
він послідовно вивантажить, кожну флешку в окрему папку, щей підпише хї 

* Для кожної флешки унікальна назва
- покищо тільки в коді 1) Місце, куди вказати шлях для вивантаження папок
* Інформація який № флешки воно вивантажує;
* Кількість файлів
* Приблизну швидкість викачки
* Перевірка файлів на цілісність після завершення викачки;


- Якщо вилетів провідник - продовжувати проштовхувати файл поки знов не продовжиться закачка
- Кнопку ребута, якщо закачка не продовжилась після 10 автоматичних спроб (1 спроба - кожні 10 секунд);
2) loadbar про завантаження;
4) Відсоток від 100% скільки воно вже вивантажили
6) "Темна тема"
*/

class Program
{
    static void Main()
    {
        // Список дисків, які можуть бути флешками
        string[] removableDrives = Directory.GetLogicalDrives();

        // Шлях до папки, куди будуть копіюватися файли
        string destinationBasePath = @"\\SNG-8-sh\Аеророзвідка\(4) Буфер";

        int driveNumber = 1;

        foreach (string drive in removableDrives)
        {
            try
            {
                // Перевірка, чи є диск знімним (флешкою)
                DriveInfo driveInfo = new DriveInfo(drive);
                if (driveInfo.DriveType == DriveType.Removable && driveInfo.IsReady)
                {
                    string sourceDir = Path.Combine(drive, ""); // Шлях до кореневої папки флешки
					string destinationPath = Path.Combine(destinationBasePath, $"FlashDrive_{driveNumber}_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
                    Directory.CreateDirectory(destinationPath);

                    Console.WriteLine($"Копіювання з {sourceDir} в {destinationPath}...");

                    CopyDirectory(sourceDir, destinationPath);

                    driveNumber++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при доступі до {drive}: {ex.Message}");
            }
        }

        Console.WriteLine("Копіювання завершено.");
    }

    // Функція для копіювання директорії з вмістом
    private static void CopyDirectory(string sourceDir, string destinationDir)
    {
        string[] allFiles = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
        string[] allDirectories = Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories);

        int totalItems = allFiles.Length + allDirectories.Length;
        int copiedItems = 0;

        long totalSizeBytes = 0;
        long copiedSizeBytes = 0;

        // Обчислюємо загальний розмір всіх файлів
        foreach (string file in allFiles)
        {
            FileInfo fileInfo = new FileInfo(file);
            totalSizeBytes += fileInfo.Length;
        }

        // Використовуємо Stopwatch для вимірювання часу копіювання
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Копіюємо всі файли
        foreach (string file in allFiles)
        {
            try
            {
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
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка копіювання файлу {file}: {ex.Message}");
            }
        }

        // Копіюємо всі підпапки
        foreach (string dir in allDirectories)
        {
            try
            {
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
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка копіювання папки {dir}: {ex.Message}");
            }
        }

        stopwatch.Stop();
    }

    // Функція для форматування розміру файлу
    private static string FormatSize(long bytes)
    {
        const long OneGb = 1024 * 1024 * 1024;

        return $"{(double)bytes / OneGb:F2} GB";
    }
}
