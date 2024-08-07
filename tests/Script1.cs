class Program
{
    static void Main()
    {
        // Список дисків, які можуть бути флешками
        string[] removableDrives = Directory.GetLogicalDrives();

        // Шлях до папки, куди будуть копіюватися файли
        string destinationBasePath = @"C:\Backup\";

        int driveNumber = 1;

        foreach (string drive in removableDrives)
        {
            try
            {
                // Перевірка, чи є диск знімним (флешкою)
                DriveInfo driveInfo = new DriveInfo(drive);
                if (driveInfo.DriveType == DriveType.Removable && driveInfo.IsReady)
                {
                    string destinationPath = Path.Combine(destinationBasePath, $"FlashDrive_{driveNumber}");
                    Directory.CreateDirectory(destinationPath);

                    Console.WriteLine($"Копіювання з {drive} в {destinationPath}...");

                    CopyDirectory(drive, destinationPath);

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
            string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
            File.Copy(file, destFile, true);
        }

        // Копіюємо всі підпапки
        foreach (string dir in Directory.GetDirectories(sourceDir))
        {
            string destDir = Path.Combine(destinationDir, Path.GetFileName(dir));
            CopyDirectory(dir, destDir);
        }
    }
}
