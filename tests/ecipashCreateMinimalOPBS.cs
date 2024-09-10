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
		
		string minimalEcspashClipbload = string.Empty;
		
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
			
			minimalEcspashClipbload += textName;
			
			wait.ms(200);
			keys.send("Down");
			wait.ms(200);
			keys.send("Down");
		}
		keys.send("Ctrl+Home");
	}
}