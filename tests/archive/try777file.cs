/*
	заповнення 777

для антен - "Радіопеленгація", - 10011000001503000000
для РЕБ - "Створення перешкод", - 10011000001505040000
для Мавіків, FPV та бомберів - "БПЛА вертикального взльоту", - 10010100001104000000
для FPV-крил та розвід. крил - "Безпілотний літак". - 10010100001103000000

object csvData = ['"sidc","id","quantity","name","observation_datetime","reliability_credibility","staff_comments","platform_type","direction","speed","coordinates","comment 1"'];
			
			var csvRow = [
		  '"' + idType +'"',//sidc міна 10031520002103000000 
		  '""',//id
		  '"' + quantity + '"',//quantity
		  '"' + name +'"',
		  '"' + observation_datetime + '"',//observation_datetime
		  '"' + reliability_credibility + '"',//reliability_credibility
		  '"' + staff_comments + '"',//staff_comments rowData[9]
		  '"' + platform_type + '"',//platform_type
		  '"' + direction + '"',//direction
		  '"' + speed + '"',//speed
		  '"POINT(' + wsg84.join(' ') + ')"',//coordinates
		  '"' + comment1 + '"'//comment 1
		];

*/

using System.Windows;
using System.Windows.Controls;

namespace CSLight {
	class Program {
		static void Main() {
			keys.send("Shift+Space*2"); //виділяємо весь рядок
			wait.ms(100);
			keys.send("Ctrl+C"); //копіюємо код
			wait.ms(100);
			string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
			string[] parts = clipboardData.Split('\n'); // Розділяємо рядок на частини
			string dateTimeNow = DateTime.Now.ToString("dd/MM/yyyy HH:mm"); // поточний час
			string startPart = "\"sidc\",\"id\",\"quantity\",\"name\",\"observation_datetime\",\"reliability_credibility\",\"staff_comments\",\"platform_type\",\"direction\",\"speed\",\"coordinates\",\"comment 1\"";
			string finalString = startPart;
			foreach (string item in parts) {
				string[] elements = item.Split('\t');
				
				string sidc = "";
				string id = ""; // Міна 181024013
				string quantity = ""; // кількість 1
				string name = $"{elements[0]} Т.в. \"{elements[1]}\""; // Мавік М09
				
				// превірка для вибору мітки
				if (name.Contains("РЕБ") || name.Contains("РЛС")) {
					sidc = "10011000001505040000";
				} else if (name.Contains("Мавік") || name.Contains("FPV") || Regex.IsMatch(name, @"\bП\d{2}\b")) {
					sidc = "10010100001104000000";
				} else {
					sidc = "10011000001505040000";
				};
				
				string observation_datetime = dateTimeNow; // dd/MM/yyyy HH:mm
				string reliability_credibility = ""; // А2
				string staff_comments = ""; //ід
				string platform_type = ""; //
				string direction = "";
				string speed = "";
				
				string coordinates = $"POINT({elements[9].Trim()}))";
				string comment = "";
				
				finalString += $"\n\"{sidc}\",\"{id}\",\"{quantity}\",\"{name}\",\"{observation_datetime}\",\"{reliability_credibility}\",\"{staff_comments}\",\"{platform_type}\",\"{direction}\",\"{speed}\",\"{coordinates}\",\"{comment}\"";
				
			};
			
			// Шлях до робочого столу користувача
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string fileName = $"{DateTime.Now.ToString("dd.MM ss")} - layer 777.csv"; // Формування назви файлу
			try {
				// Повний шлях до файлу, який ми хочемо створити
				string filePath = Path.Combine(desktopPath, fileName);
				
				// Записуємо рядок у файл
				File.WriteAllText(filePath, finalString);
				
				Console.WriteLine("Файл успішно створено на робочому столі.");
			}
			catch (Exception ex) {
				Console.WriteLine($"Виникла помилка при створенні файлу: {ex.Message}");
			}
			
		}
	}
}
