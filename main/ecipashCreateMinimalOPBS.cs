/** 10.09.2024
створення для створення папок з Планування excel
1. вибираєш ячейку
2. жмеш скрипт
3. створюється textfile.txt на робочому столі з текстом
*/

using System.Windows.Forms;

class Program {
	static void Main() {
		string initialString = string.Empty;
		
		// Отримуємо шлях до робочого столу
		string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		
		// Створюємо шлях до файлу на робочому столі
		string filePath = Path.Combine(desktopPath, "textfile.txt");
		
		// Якщо файл існує, то перезаписуємо його
		if (File.Exists(filePath)) {
			File.Delete(filePath); // Видаляємо файл
		}
		
		// Створюємо новий файл (перезаписуємо)
		File.Create(filePath).Close(); // Створюємо і закриваємо файл для подальшого запису
		
		while (true) {
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
			// Видалення всіх лапок (подвійних та одинарних) та видалення переходів на нові рядки
			initialString = initialString.Replace("\"", "").Replace("'", "").Replace("\n", "").Replace("\r", "");
			
			// Використання регулярних виразів для отримання потрібних частин рядка
			string datePattern = @"(\d{2}\.\d{2}\.\d{4})";
			string crewPattern = @"Екіпаж — (.*?)(?=Точка|Початок|Завдання|Склад|Зона)";
			string pointPattern = @"Точка вильоту — (.*?)(?=Зона|Завдання)";
			string teamPattern = @"Склад: — (.+)";
			
			// Знаходження частин рядка за допомогою регулярних виразів
			string date = Regex.Match(initialString, datePattern).Groups[1].Value.Trim();
			string crew = Regex.Match(initialString, crewPattern).Groups[1].Value.Trim();
			string point = Regex.Match(initialString, pointPattern).Groups[1].Value.Trim();
			
			// Отримання всіх членів команди
			string team = Regex.Match(initialString, teamPattern).Groups[1].Value.Trim();
			string[] teamMembers = Regex.Split(team, @" — (?!командир)").Select(member => member.Replace("командир ", "")).ToArray();
			string formattedTeam = string.Join(", ", teamMembers);
			
			// Змінна, що змінюється після кожної операції
			string textName = $"{crew} - {point} \n\t ({formattedTeam})\n";
			
			// Додаємо новий текст у файл
			AddTextToFile(filePath, textName);
			
			wait.ms(200);
			keys.send("Down");
			wait.ms(200);
			keys.send("Down");
		}
		keys.send("Ctrl+Home");
	}
	// Метод для додавання тексту у файл
	static void AddTextToFile(string filePath, string text) {
		using (StreamWriter writer = new StreamWriter(filePath, true)) // true дозволяє додавати текст у кінець файлу
		{
			writer.WriteLine(text);
		}
	}
}