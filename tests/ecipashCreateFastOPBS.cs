/** 20.08.2024
створення для створення папок з Планування excel
1. вибираєш ячейку
2. жмеш скрипт
3. створюється папка на робочому столі з папкою в середині з відповідною назвою
*/

using System.Windows.Forms;

class Program
{
	static void Main() {
		string initialString = string.Empty;
		while(true){
			//виділяємо рядок
			keys.send("Shift+Space");
			wait.ms(100);
			//копіюємо код
			keys.send("Ctrl+C");
			wait.ms(200);
			// Зчитуємо вміст з буферу обміну
			initialString = clipboard.copy();
			wait.ms(200);
			if (initialString.Length < 30) { break; }
			//Console.WriteLine(initialString.Length);
			// Видалення всіх лапок (подвійних та одинарних) та видалення переходів на нові рядки
			initialString = initialString.Replace("\"", "").Replace("'", "").Replace("\n", "").Replace("\r", "");
			
			// Використання регулярних виразів для отримання потрібних частин рядка
			string datePattern = @"(\d{2}\.\d{2}\.\d{4})";
			string crewPattern = @"Екіпаж — (.*?)(?=Точка|Початок|Завдання)";
			string pointPattern = @"Точка вильоту — ([^З]*)";
			string teamPattern = @"Склад: — (.+)";
			
			// Знаходження частин рядка за допомогою регулярних виразів
			string date = Regex.Match(initialString, datePattern).Groups[1].Value.Trim();
			string crew = Regex.Match(initialString, crewPattern).Groups[1].Value.Trim();
			string point = Regex.Match(initialString, pointPattern).Groups[1].Value.Trim();
			
			// Отримання всіх членів команди
			string team = Regex.Match(initialString, teamPattern).Groups[1].Value.Trim();
			string[] teamMembers = Regex.Split(team, @" — (?!командир)").Select(member => member.Replace("командир ", "")).ToArray();
			string formattedTeam = string.Join(", ", teamMembers);
			
			// Формування кінцевого рядка
			string folderName = $"{crew} - {point} ({formattedTeam})";
			
			// Отримати шлях до робочого столу поточного користувача
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			
			// Створити повний шлях до кореневої папки на робочому столі
			string parentFolderPath = Path.Combine(desktopPath, date);
			
			// Перевірити, чи існує батьківська папка
			if (!Directory.Exists(parentFolderPath)) {
				// Створити нову батьківську папку
				Directory.CreateDirectory(parentFolderPath);
				Console.WriteLine("Коренева папка створена успішно.");
			} else {
				Console.WriteLine("Коренева папка вже існує.");
			}
			
			// Створити повний шлях до вкладеної папки
			string childFolderPath = Path.Combine(parentFolderPath, folderName);
			
			// Перевірити, чи існує вкладена папка
			if (!Directory.Exists(childFolderPath)) {
				// Створити нову вкладену папку
				Directory.CreateDirectory(childFolderPath);
				Console.WriteLine("Вкладена папка створена успішно.");
			} else {
				Console.WriteLine("Вкладена папка вже існує.");
			}
			wait.ms(200);
			keys.send("Down");
			wait.ms(200);
			keys.send("Down");
		}
		keys.send("Ctrl+Home");
    }
}