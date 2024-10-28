/** 28,10,2024
	створення для створення папок з Планування excel
1. вибираєш ячейку під колонкою А
2. жмеш скрипт
3. обираєш 1. Папка з папками екіпажей - за замовчуванням
4. створюється папка на робочому столі з папками які маєть відповідні назви екіпажей

	створення короткий запис планування
1. вибираєш ячейку під колонкою А
2. жмеш скрипт
3. обираєш 2. Для оперативників
3. отримуєш короткий запис, в буфер обміну, екіпажей

	проект скрипт для (ексель табличка обробників відео) - розуміння хто працював, а хто ні
1. вибираєш ячейку під колонкою А
2. жмеш скрипт
3. обираєш 3. Обробка відео 1 - (скрипт перезапише в буфер обміну назви екіпажей)
4. переходиш в табличку "для обробки відео" знаходиш та обираєш клітинку А1
5. жмеш скрипт
6. обираєш 4. Обробка відео 2 - підготує (до буферу обміну) рядок (відео немає / не працював)
7. знаходиш необхідну дату та через ctrl+shift+v вставляєш
*/

using System.Windows;
using System.Windows.Controls;

class Program {
	static void Main() {
		// вікно діалогу
		string[] examlpelesItem = { "1. Папка з папками екіпажей", "2. Для оперативників", "3. Обробка відео 1", "4. Обробка відео 2" };
		var b = new wpfBuilder("Window").WinSize(400);
		b.R.Add("Назва", out ComboBox itemSelect).Items(examlpelesItem);
		b.R.AddOkCancel();
		b.Window.Topmost = true;
		b.End();
		// show dialog. Exit if closed not with the OK button.
		if (!b.ShowDialog()) return;
		
		string ecipashNameWithTable = Clipboard.GetText(); // це після обробки іншим скриптом
		
		if (itemSelect.Text.Contains("1.") || itemSelect.Text.Contains("2.") || itemSelect.Text.Contains("3.")) {
			keys.send("Ctrl+Space"); //виділяємо весь стовбець
		} else {
			keys.send("Shift+Space*2"); //виділяємо весь рядок
		}
		wait.ms(200);
		keys.send("Ctrl+C");
		wait.ms(200);
		// Зчитуємо вміст з буферу обміну
		string[] ecipashPositionParts = clipboardData.getText().Replace("\"", "").Replace("'", "").Split('\t'); // Розділяємо рядок на частини
		string initialString = clipboard.copy();
		wait.ms(200);
		keys.send("Up");
		// Видалення всіх лапок (подвійних та одинарних) та видалення переходів на нові рядки
		initialString = initialString.Replace("\"", "").Replace("'", "").Replace("\n", "").Replace("\r", "");
		// Розбиваємо текст на масив чергувань
		string[] shifts = initialString.Split(new[] { "ЧЕРГУВАННЯ" }, StringSplitOptions.RemoveEmptyEntries);
		
		if (itemSelect.Text.Contains("1.")) {
			createFolderWithFoldersEkipash(shifts);
		}
		if (itemSelect.Text.Contains("2.")) {
			createMinimalEkipash(shifts);
		}
		if (itemSelect.Text.Contains("3.")) {
			WhoWorkInTime(shifts);
		}
		if (itemSelect.Text.Contains("4.")) {
			WhoWorkinTimeFinal(ecipashPositionParts, ecipashNameWithTable);
		}
		
	}
	// оперативники
	static void createMinimalEkipash(string[] shifts) {
		string minimalEcspashClipbload = Regex.Match(shifts[3], @"(\d{2}\.\d{2}\.\d{4})").Groups[1].Value.Trim() + "\n";
		foreach (string elemets in shifts) {
			
			// Використання регулярних виразів для отримання потрібних частин рядка
			string datePattern = @"(\d{2}\.\d{2}\.\d{4})";
			string crewPattern = @"Екіпаж — (.*?)(?=Початок|Точка|Населений|Зона|Завдання|Завершення|Склад)";
			string pointPattern = @"Точка вильоту — (.*?)(?=Населений|Зона|Завдання|Завершення|Склад)";
			string areaPattern = @"Зона інтересів — (.*?)(?=Початок|Точка|Населений|Завдання|Завершення|Склад)";
			string cityPattent = @"Населений пункт — (.*?)(?=Завдання|Завершення|Склад)";
			string teamPattern = @"Склад: — (.+)";
			
			// Знаходження частин рядка за допомогою регулярних виразів
			string date = Regex.Match(elemets, datePattern).Groups[1].Value.Trim();
			string crew = Regex.Match(elemets, crewPattern).Groups[1].Value.Trim();
			string point = Regex.Match(elemets, pointPattern).Groups[1].Value.Trim();
			string area = Regex.Match(elemets, areaPattern).Groups[1].Value.Trim();
			string city = Regex.Match(elemets, cityPattent).Groups[1].Value.Trim();
			
			// Отримання всіх членів команди
			string team = Regex.Match(elemets, teamPattern).Groups[1].Value.Trim();
			string[] teamMembers = Regex.Split(team, @" — (?!командир)").Select(member => member.Replace("командир ", "")).ToArray();
			string formattedTeam = string.Join(", ", teamMembers);
			
			// Змінна, що змінюється після кожної операції
			string textName = string.Empty;
			if (crew.Contains("Ев. ")) {
				textName = $"{date}\t {crew}\t {point}\t {city}\t {formattedTeam}\n";
			} else {
				textName = $"{date}\t {crew}\t {point}\t {area}\t {formattedTeam}\n";
			}
			
			if (textName.Length > 16) {
				minimalEcspashClipbload += textName;
			}
		}
		Clipboard.SetText(minimalEcspashClipbload);
	}
	// папки
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
	// обробка відео 1
	static void WhoWorkInTime(string[] shifts) {
		//string datePattern = @"(\d{2}\.\d{2}\.\d{4})";
		//string date = Regex.Match(shifts[0], datePattern).Groups[1].Value.Trim();
		
		string minimalEcspashClipbload = string.Empty;
		foreach (string elemets in shifts) {
			// Використання регулярних виразів для отримання потрібних частин рядка
			string crewPattern = @"Екіпаж — (.*?)(?=Початок|Точка|Населений|Зона|Завдання|Завершення|Склад)";
			
			// Знаходження частин рядка за допомогою регулярних виразів
			string crew = Regex.Match(elemets, crewPattern).Groups[1].Value.Trim();
			// додавання типу до бортів
			
			if (!crew.Contains("Мавік")) {
				int index = crew.IndexOf(" ");
				if (index != -1) {
					crew = crew.Substring(0, index + 1);
				}
			}
			/*
			if (crew.Contains("Мавік")) {
				crew = crew.Replace("Мавік", " ").Trim();
			}
			*/
			minimalEcspashClipbload += $"{crew} ";
		}
		Clipboard.SetText(minimalEcspashClipbload);
	}
	// обробка відео 2
	static void WhoWorkinTimeFinal(string[] ecipashPositionParts, string ecipashNameWithTable) {
		string finalWorkOrNot = string.Empty;
		// підготовка до порівняння
		for (int i = 1; i < ecipashPositionParts.Length; i++) {
			// беремо більш менш чистий елемент
			string partArray = ecipashPositionParts[i];
			
			if (!partArray.Contains(",")) {
				int index = partArray.IndexOf("\n");
				if (index != -1) {
					partArray = partArray.Substring(0, index);
				}
				int index1 = partArray.IndexOf(" ");
				if (index1 != -1) {
					partArray = partArray.Substring(0, index1).Trim().TrimEnd();
				}
			}
			
			// Оновлюємо значення елемента в масиві
			ecipashPositionParts[i] = partArray + " ";
			//finalWorkOrNot += " = " + partArray + " = ";
			//Console.WriteLine($"{partArray} ");
		}
		
		for (int i = 1; i < ecipashPositionParts.Length; i++) {
			if (ecipashNameWithTable.Contains(ecipashPositionParts[i]) && ecipashPositionParts[i] != " ") {
				finalWorkOrNot += $"Відео немає\t";
				//Console.WriteLine(true);
			} else {
				finalWorkOrNot += $"Не працював\t";
				//Console.WriteLine(false);
			}
			//finalWorkOrNot += $"{ecipashName}\t";
		}
		
		Clipboard.SetText(finalWorkOrNot);
	}
}

