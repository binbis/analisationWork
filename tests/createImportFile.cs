/*/ nuget -\CoordinateSharp; /*/

/* 07.11.24 
бібліотека CoordinateSharp

для антен - "Радіопеленгація", - 10011000001503000000
для РЕБ - "Створення перешкод", - 10011000001505040000
для Мавіків, FPV та бомберів - "БПЛА вертикального взльоту", - 10010100001104000000
для розвід. крил - "Безпілотний літак". - 10010100001103000000
для евака "матеріально технічне забезпечення" - 10032500003211000000

*/

using System.Windows;
using System.Windows.Controls;
using CoordinateSharp;

namespace CSLight {
	class Program {
		static void Main() {
			keys.send("Ctrl+C"); //копіюємо код
			wait.ms(100);
			string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
			string[] parts = clipboardData.Split('\n'); // Розділяємо рядок на частини
			string dateTimeNow = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"); // поточний час
			var features = new List<Object>(); //
			
			foreach (string item in parts) {
				string[] elements = item.Split('\t'); // ділимо рядок на елементи
				if (elements.Length < 10) continue; // Пропускаємо, якщо елементів недостатньо
				if (elements[10].Length > 5) {
					// Парсимо елементи з буфера обміну
					string sidc = string.Empty;
					string outlineСolor = ""; // колір обведення
					
					if (elements[1].Contains("група")) { // якщо це евак
						sidc = "10032500003211000000";
					} else if (elements[1].Contains("Маві")) { // якщо це мавік
						sidc = "10030120001104000000";
						outlineСolor = "#4dc04d";
					} else if (elements[1].Contains("(FPV)")) { //  або фпв
						sidc = "10030120001104000000";
						outlineСolor = "#3bd5e7";
					} else if (Regex.IsMatch(elements[1], @"\bП\d{2}\b")) { // якщо ти бомбер або дартс
						sidc = "10030120001104000000";
						outlineСolor = "#597380";
					} else { // інші
						sidc = "10010120001103000000";
						outlineСolor = "#ff9327";
					}
					
					string name = $"{elements[1]} Т.в. ({elements[2]})";
					
					//координати обробка
					Coordinate c = Coordinate.Parse(elements[10]); // в строку переробляємо
					c.FormatOptions.Format = CoordinateFormatType.Decimal; // 40.577 -70.757
					string coordinates = c.ToString().Replace(' ', ',');
					string coordX = coordinates.Split(',')[0];
					string coordY = coordinates.Split(',')[1];
					string wgsCoord = $"{coordY}, {coordX}";
					
					// Формуємо JSON для однієї мітки (Feature) вручну
					var feature = new StringBuilder();
					feature.AppendLine("{");
					feature.AppendLine("  \"type\": \"Feature\",");
					feature.AppendLine("  \"properties\": {");
					feature.AppendLine($"    \"sidc\": \"{sidc}\",");
					feature.AppendLine($"    \"name\": \"{name}\",");
					feature.AppendLine($"    \"observation_datetime\": \"{dateTimeNow}\",");
					feature.AppendLine($"    	\"outline-color\": \"{outlineСolor}\"");
					feature.AppendLine("  },");
					feature.AppendLine("  \"geometry\": {");
					feature.AppendLine("    \"type\": \"Point\",");
					feature.AppendLine($"    \"coordinates\": [{wgsCoord}]");
					feature.AppendLine("  }");
					feature.AppendLine("}");
					
					features.Add(feature.ToString());
				} else {
					Console.WriteLine("одне з речень не містить mgrs координат");
				}
			};
			
			// Формуємо повний JSON для FeatureCollection
			var geoJson = new StringBuilder();
			geoJson.AppendLine("{");
			geoJson.AppendLine("  \"type\": \"FeatureCollection\",");
			geoJson.AppendLine("  \"features\": [");
			geoJson.AppendLine(string.Join(",\n", features));
			geoJson.AppendLine("  ]");
			geoJson.AppendLine("}");
			
			// Шлях до робочого столу користувача
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string fileName = $"{DateTime.Now.ToString("dd mmss")} - layer 777.geojson"; // Формування назви файлу
			try {
				// Повний шлях до файлу, який ми хочемо створити
				string filePath = Path.Combine(desktopPath, fileName);
				
				// Записуємо рядок у файл
				File.WriteAllText(filePath, geoJson.ToString());
				
				Console.WriteLine("Файл успішно створено на робочому столі.");
			}
			catch (Exception ex) {
				Console.WriteLine($"Виникла помилка при створенні файлу: {ex.Message}");
			}
		}
	}
}
