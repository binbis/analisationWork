/* 29,07,2024_v1.4 

*/
using System.Windows.Forms;

namespace CSLight {
	class Program {
		static void Main() {
			//opt.mouse.MoveSpeed = opt.key.KeySpeed = opt.key.TextSpeed = 20;
			//виділяємо весь рядок
			keys.send("Shift+Space Space");
			//копіюємо код
			keys.send("Ctrl+C");
			keys.send("11 Tab");
			// зчитуємо буфер обміну
			string clipboardData = Clipboard.GetText();
			
			// Розділяємо рядок на частини
			string[] parts = clipboardData.Split('\t');
			
			// Присвоюємо змінним відповідні значення
			string dateJbd = parts[0]; // 27.07.2024
			string timeJbd = parts[1]; //00:40
			string commentJbd = parts[2]; //коментар (для ідентифікації скоріш за все)
			string crewTeamJbd = parts[4]; // R-18-1 (Мавка)
			string whatDidJbd = parts[5]; // Мінування (можливо його видалю)
			string targetClassJbd = parts[6]; // Міна/Вантажівка/Військ. баггі/Скупчення ОС/Укриття
			string idTargetJbd = parts[9]; // Міна 270724043
			// Встановлено/Уражено/Промах/Авар. скид/Повторно уражено
			string establishedJbd = parts[24];
			//Console.WriteLine(establishedJbd);
			//приводжу дату до формату дельти
			string dateDeltaFormat = dateJbd.Replace('.','/');
			// массив техніки
			string[] machineryArray = {
				"ББМ / МТ-ЛБ","Авто","Вантажівка"
			};
			// для мін
			string targetMinePTM = "Міна";
			
			// Знаходить та активує вікно якщо воно звернуте 
			
			// поле шар
			
			// поле назва
			
			// поле дата виявлення
			
			// поле час виявлення
			
			// поле кількість
			
			// поле боєздатність
			
			// ідетнифікація
			
			// достовірність
			
			// тип джерела
			
			// завйваження штабу ід
			
			// коментар
			
		}
		static string datePlasFourteen(string date) {
			// Перетворюємо рядок дати у DateTime
			DateTime originalDate = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
			// Додаємо 14 днів
			DateTime newDate = originalDate.AddDays(60);
			// Перетворюємо нову дату назад у рядок
			//string newDateString = newDate.ToString("dd.MM.yyyy");
			string newDateString = newDate.ToString("dd.MM.yyyy");
			return newDateString;
		}
	}
}

