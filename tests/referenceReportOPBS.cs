/*/ nuget -\DocumentFormat.OpenXml; /*/
/*
1. створити папку(будь де)
2. додати в папку файл приклад, в ворді, з ім'ям originalShedule.docx
3. виділити рядки в ексель табличці
4. жмеш скрипт, в діалоговому вікні вказати шлях
5. отримати готові доповіді довідки

скрипт який робить довідки доповіді

*/

using System.Windows;
using System.Windows.Controls;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CSLight {
	class Program {
		static void Main() {
			keys.send("Ctrl+C"); //копіюємо код
			wait.ms(100);
			string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
			string[] parts = clipboardData.Split('\n'); // Розділяємо рядок на частини
			
			// вікно діалогу
			//string[] examlpelesItem = { "" };
			var b = new wpfBuilder("Window").WinSize(800);
			b.R.Add(out Label _, "назва файлу з прикладом = originalShedule.docx"); //read-only text
			b.R.Add("Шляж де лежать приклад", out TextBox originalFilePath).Focus();
			b.R.AddOkCancel();
			b.Window.Topmost = true;
			b.End();
			// show dialog. Exit if closed not with the OK button.
			if (!b.ShowDialog()) return;
			
			string originalFilePathChange = Path.Combine(originalFilePath.Text,"originalShedule.docx");// Шлях до оригінального файлу
			string newFilePath = originalFilePath.Text; // Шлях до нового файлу (копії)
			
			// Шлях до робочого столу користувача
			//string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			//string fileName = $"{DateTime.Now.ToString("dd.MM hh-mm")}"; // Формування назви файлу
			//int countNumber = 1;
			
			foreach (string item in parts) {
				string[] elements = item.Split('\t');
				string bat_m = elements[0]; // Батальйон в місцевому відмінку
				string loss_time = elements[1]; // Час втрати
				string date = elements[2]; // Дата втрати
				string rota_n = elements[3]; // Рота в називному відмінку
				string bat_n = elements[4]; // Батальйон в родовому відмінку
				string dron_name = elements[5]; // Назва борту
				string dron_ID = elements[6]; // ID борту
				string track = elements[7]; // "Маршрут польоту (н.п. точки вильоту - н.п. втрати)
				string flight_time = elements[8]; // Час початку польоту
				string flight_duration = elements[9]; // "протяжність польоту"
				string place_name = elements[10]; // Місце втрати (н.п.)
				string place_coords = elements[11]; // Координати втрати (MGRS)
				string height = elements[12]; // Висота втрати зв'язку
				string loss_details = elements[13]; // Обставини втрати
				string pilots_actions = elements[14]; // Вжитті заходи (дії пілота)
				string full_position = elements[15]; // Повна посада в називному відмінку
				string pilot_rank = elements[16]; // Звання в називному відмінку
				string pilot_name = elements[17]; // ПІП пілота в називному відмінку
				
				// 20241009 Довідка-доповідь про втрату борту (Vampire № SFCF1000016693).docx
				string newFilePathUsing = Path.Combine(newFilePath, $"{date.Replace(".","")} Довідка-доповідь про втрату борту ({dron_name} № {dron_ID}).docx");
				
				// Створити копію файлу
				System.IO.File.Copy(originalFilePathChange, newFilePathUsing, true);
				//countNumber++; // ітерація
				
				// Відкрити копію для редагування
				using (WordprocessingDocument doc = WordprocessingDocument.Open(newFilePathUsing, true)) {
					// Отримати головний документ
					var body = doc.MainDocumentPart.Document.Body;
					// Пройти по кожному текстовому елементу в документі
					foreach (var text in body.Descendants<Text>()) {
						// Заміняємо кожен маркер на відповідну змінну
						text.Text = text.Text.Replace("{{bat_m}}", bat_m)
											 .Replace("{{loss_time}}", loss_time)
											 .Replace("{{date}}", date)
											 .Replace("{{rota_n}}", rota_n)
											 .Replace("{{bat_n}}", bat_n)
											 .Replace("{{dron_name}}", dron_name)
											 .Replace("{{dron_ID}}", dron_ID)
											 .Replace("{{track}}", track)
											 .Replace("{{flight_time}}", flight_time)
											 .Replace("{{flight_duration}}", flight_duration)
											 .Replace("{{place_name}}", place_name)
											 .Replace("{{place_coords}}", place_coords)
											 .Replace("{{height}}", height)
											 .Replace("{{loss_details}}", loss_details)
											 .Replace("{{pilots_actions}}", pilots_actions)
											 .Replace("{{full_position}}", full_position)
											 .Replace("{{pilot_rank}}", pilot_rank)
											 .Replace("{{pilot_name}}", pilot_name);
					}
					// Зберегти зміни
					doc.MainDocumentPart.Document.Save();
				}
			}
			Console.WriteLine("Файли успішно створено");
		}
	}
}
