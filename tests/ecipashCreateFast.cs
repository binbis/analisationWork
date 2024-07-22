/**
23,07,2024

тільки почав думати

виділяється чергування
жметься скрипт
створюється папка вірними підписами з папками екіпажів

1. створює папку на робочому столі
2. створює папку в папці яку створив


* екіпаж
* точка вильоту
* склад

*/

using System.Windows.Forms;

class Program
{
    static void Main()
    {
		// Зчитуємо вміст з буферу обміну
		//string clipText = Clipboard.GetText();
		
		// Отримати шлях до робочого столу поточного користувача
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // Вказати ім'я нової папки на робочому столі
        string parentFolderName = "ParentFolderName";

        // Створити повний шлях до нової папки на робочому столі
        string parentFolderPath = Path.Combine(desktopPath, parentFolderName);

        // Перевірити, чи існує батьківська папка
        if (!Directory.Exists(parentFolderPath))
        {
            // Створити нову батьківську папку
            Directory.CreateDirectory(parentFolderPath);
            Console.WriteLine("Батьківська папка створена успішно.");
        }
        else
        {
            Console.WriteLine("Батьківська папка вже існує.");
        }
		/**
			пробував повторити дію декілька разів
			буду проходити по масиву у створювати папки
		*/ 
		for (int i = 0; i < 4; i++) {
			// Вказати ім'я вкладеної папки
			string childFolderName = "ChildFolderName" + i;

			// Створити повний шлях до вкладеної папки
			string childFolderPath = Path.Combine(parentFolderPath, childFolderName);

			// Перевірити, чи існує вкладена папка
			if (!Directory.Exists(childFolderPath))
			{
				// Створити нову вкладену папку
				Directory.CreateDirectory(childFolderPath);
				Console.WriteLine("Вкладена папка створена успішно.");
			}
			else
			{
				Console.WriteLine("Вкладена папка вже існує.");
			}	
		}
        
    }
}