/** 01.10.2024
створення короткий запис планування
1. вибираєш ячейку під колонкою А
2. жмеш скрипт
3. отримуєш короткий запис, в буфер обміну, екіпажей

створення для створення папок з Планування excel
1. вибираєш ячейку
2. жмеш скрипт
3. створюється папка на робочому столі з папкою в середині з відповідною назвою екіпажей
*/

using System.Windows;
using System.Windows.Controls;

class Program {
	static void Main() {
		
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
		
		string[] examlpelesItem = { "1. Папка з папками екіпажей", "2. Для оперативників" };
		// вікно діалогу
		var b = new wpfBuilder("Window").WinSize(400);
		b.R.Add("Назва", out ComboBox itemSelect).Items(examlpelesItem);
		b.R.AddOkCancel();
		b.Window.Topmost = true;
		b.End();
		// show dialog. Exit if closed not with the OK button.
		if (!b.ShowDialog()) return;
		
		if (itemSelect.Text.Contains("1.")) {
			createFolderWithFoldersEkipash(shifts);
		}
		if (itemSelect.Text.Contains("2.")) {
			createMinimalEkipash(shifts);
		}
		
	}
	// 
	static void createMinimalEkipash(string[] shifts) {
		string minimalEcspashClipbload = string.Empty;
		minimalEcspashClipbload += Regex.Match(shifts[3], @"(\d{2}\.\d{2}\.\d{4})").Groups[1].Value.Trim() + "\n";
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
			string textName = $"{crew}\t {point}\t {area}\t {formattedTeam}\n";
			if (textName.Length > 16) {
				minimalEcspashClipbload += textName;
			}
		}
		Clipboard.SetText(minimalEcspashClipbload);
	}
	//
	static void createFolderWithFoldersEkipash(string[] shifts) {
		// Отримати шлях до робочого столу поточного користувача
		string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		
		foreach (string elemets in shifts) {
			
			// Використання регулярних виразів для отримання потрібних частин рядка
			string datePattern = @"(\d{2}\.\d{2}\.\d{4})";
			string crewPattern = @"Екіпаж — (.*?)(?=Початок|Точка|Населений|Зона|Завдання|завершення)";
			string pointPattern = @"Точка вильоту — (.*?)(?=Населений|Зона|Завдання|завершення)";
			string teamPattern = @"Склад: — (.+)";
			
			// Знаходження частин рядка за допомогою регулярних виразів
			string date = Regex.Match(elemets, datePattern).Groups[1].Value.Trim();
			string crew = Regex.Match(elemets, crewPattern).Groups[1].Value.Trim();
			string point = Regex.Match(elemets, pointPattern).Groups[1].Value.Trim();
			
			// Отримання всіх членів команди
			string team = Regex.Match(elemets, teamPattern).Groups[1].Value.Trim();
			string[] teamMembers = Regex.Split(team, @" — (?!командир)").Select(member => member.Replace("командир ", "")).ToArray();
			string formattedTeam = string.Join(", ", teamMembers);
			
			// Формування кінцевого рядка
			string folderName = $"{crew} - {point} ({formattedTeam})";
			if (folderName.Length > 16) {
				// Створити повний шлях до кореневої папки на робочому столі
				string parentFolderPath = Path.Combine(desktopPath, date);
				
				// Перевірити, чи існує батьківська папка
				if (!Directory.Exists(parentFolderPath)) {
					// Створити нову батьківську папку
					Directory.CreateDirectory(parentFolderPath);
					Console.WriteLine("Коренева папка створена успішно.");
				}
				
				// Створити повний шлях до вкладеної папки
				string childFolderPath = Path.Combine(parentFolderPath, folderName);
				
				// Перевірити, чи існує вкладена папка
				if (!Directory.Exists(childFolderPath)) {
					// Створити нову вкладену папку
					Directory.CreateDirectory(childFolderPath);
					Console.WriteLine("Вкладена папка створена успішно.");
				}
			}
		}
	}
}
