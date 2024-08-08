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
        string destinationBasePath = @"\\SNG-8-sh\Аеророзвідка\(1) Записи пілотів\еуіеуі";

        // Назва папки, яку потрібно скопіювати
        //string folderToCopy = "DCIM";

        int driveNumber = 1;
		//тут захована можливість обрати конкретну папку з флешки
/*
        foreach (string drive in removableDrives)
        {
            try
            {
                // Перевірка, чи є диск знімним (флешкою)
                DriveInfo driveInfo = new DriveInfo(drive);
                if (driveInfo.DriveType == DriveType.Removable && driveInfo.IsReady)
                {
                    string sourceDir = Path.Combine(drive, folderToCopy); // Шлях до папки "Documents"
                    if (Directory.Exists(sourceDir))
                    {
                        string destinationPath = Path.Combine(destinationBasePath, $"FlashDrive_{driveNumber}");
                        Directory.CreateDirectory(destinationPath);

                        Console.WriteLine($"Копіювання з {sourceDir} в {destinationPath}...");

                        CopyDirectory(sourceDir, destinationPath);

                        driveNumber++;
                    }
					
                    else
                    {
                        Console.WriteLine($"Папка {folderToCopy} не знайдена на диску {drive}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при доступі до {drive}: {ex.Message}");
            }
        }

        Console.WriteLine("Копіювання завершено.");
    }
*/
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
        string[] allFiles = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
        string[] allDirectories = Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories);

        int totalItems = allFiles.Length + allDirectories.Length;
        int copiedItems = 0;

        // Копіюємо всі файли
        foreach (string file in allFiles)
        {
            try
            {
                string destFile = Path.Combine(destinationDir, Path.GetRelativePath(sourceDir, file));
                Directory.CreateDirectory(Path.GetDirectoryName(destFile));
                File.Copy(file, destFile, true);

                copiedItems++;
                Console.WriteLine($"Копіювання файлу: {file} ({copiedItems}/{totalItems})");
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
                Console.WriteLine($"Копіювання папки: {dir} ({copiedItems}/{totalItems})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка копіювання папки {dir}: {ex.Message}");
            }
        }
    }
}
