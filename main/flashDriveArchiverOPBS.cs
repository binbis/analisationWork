//using System;
//using System.IO;

/**
вставляєш в свій пк будь-яку кількість флешок
вказуєш шлях в коді, повний шлях до папки куди хочеш 
жмеш скрить
він послідовно вивантажить, кожну флешку в окрему папку, щей підпише хї 
*/

class Program
{
    static void Main()
    {
        // Список дисків, які можуть бути флешками
        string[] removableDrives = Directory.GetLogicalDrives();

        // Шлях до папки, куди будуть копіюватися файли
        string destinationBasePath = @"\\SNG-8-sh\Аеророзвідка\(1) Записи пілотів\тест";

        int driveNumber = 1;

        foreach (string drive in removableDrives)
        {
            try
            {
                // Перевірка, чи є диск знімним (флешкою)
                DriveInfo driveInfo = new DriveInfo(drive);
                if (driveInfo.DriveType == DriveType.Removable && driveInfo.IsReady)
                {
                    string sourceDir = Path.Combine(drive, ""); // Додає слеш, якщо його немає
                    string destinationPath = Path.Combine(destinationBasePath, $"FlashDrive_{driveNumber}");
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
        // Копіюємо всі файли
        foreach (string file in Directory.GetFiles(sourceDir))
        {
            try
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка копіювання файлу {file}: {ex.Message}");
            }
        }

        // Копіюємо всі підпапки
        foreach (string dir in Directory.GetDirectories(sourceDir))
        {
            try
            {
                string destDir = Path.Combine(destinationDir, Path.GetFileName(dir));
                Directory.CreateDirectory(destDir); // Переконатися, що папка існує
                CopyDirectory(dir, destDir);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка копіювання папки {dir}: {ex.Message}");
            }
        }
    }
}
