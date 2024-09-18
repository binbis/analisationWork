/** 19.09.2024
створення для створення папок з Планування excel
1. вибираєш ячейку
2. жмеш скрипт
3. короткий запис в буфер обміну екіпажей
*/

using System.Windows.Forms;

class Program {
	static void Main() {
		string minimalEcspashClipbload = string.Empty;
		clipboard.clear();
		//виділяємо стовбець
		keys.send("Ctrl+Space");
		wait.ms(200);
		//копіюємо код
		keys.send("Ctrl+C");
		wait.ms(200);
		// Зчитуємо вміст з буферу обміну
		string initialString = clipboard.copy();
		wait.ms(200);
		keys.send("Up");
		
		// Видалення всіх лапок (подвійних та одинарних) та видалення переходів на нові рядки
		initialString = initialString.Replace("\"", "").Replace("'", "").Replace("\n", "").Replace("\r", "");
		// Розбиваємо текст на масив чергувань
		string[] shifts = initialString.Split(new[] { "ЧЕРГУВАННЯ" }, StringSplitOptions.RemoveEmptyEntries);
		
		foreach (string elemets in shifts) {
			
			// Використання регулярних виразів для отримання потрібних частин рядка
			string datePattern = @"(\d{2}\.\d{2}\.\d{4})";
			string crewPattern = @"Екіпаж — (.*?)(?=Початок|Точка|Населений|Зона|Завдання|Завершення)";
			string pointPattern = @"Точка вильоту — (.*?)(?=Населений|Зона|Завдання|Завершення)";
			string areaPattern = @"Зона інтересів — (.*?)(?=Початок|Точка|Населений|Завдання|Завершення)";
			string teamPattern = @"Склад: — (.+)";
			
			// Знаходження частин рядка за допомогою регулярних виразів
			string date = Regex.Match(elemets, datePattern).Groups[1].Value.Trim();
			string crew = Regex.Match(elemets, crewPattern).Groups[1].Value.Trim();
			string point = Regex.Match(elemets, pointPattern).Groups[1].Value.Trim();
			string area = Regex.Match(elemets, areaPattern).Groups[1].Value.Trim();
			
			// Отримання всіх членів команди
			string team = Regex.Match(elemets, teamPattern).Groups[1].Value.Trim();
			string[] teamMembers = Regex.Split(team, @" — (?!командир)").Select(member => member.Replace("командир ", "")).ToArray();
			string formattedTeam = string.Join(", ", teamMembers);
			
			// Змінна, що змінюється після кожної операції
			string textName = $"Екіпаж — {crew} т.в. {point} ({area}) \n\n\t ({formattedTeam})\n";
			
			minimalEcspashClipbload += textName;
		}
		Clipboard.SetText(minimalEcspashClipbload);
	}
}