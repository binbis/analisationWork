/**
11,09,2024_v3.1.1
0. відкрий дельту в окремій вкладці та вікні, (!не міняй вкладку!), можеш звернути це вікно
1. виділяєш текст де є координати mgrs, типу delta-google, google-maps без букв, уск-2000
- спершу шукає mgrs, якщо не знаходить, прибирає усе окрім цифр, ком та крапок
2. жмеш hotkey
3. відкриває дельту(де б вона не була та який розмір вікна б не мала) 
4. вставяє оброблені координати до пошуку та натискає кнопку пошуку

*/

using System.Windows.Forms;

namespace CSLight {
	internal class Program {
		[STAThread]
		static void Main(string[] args) {
			opt.key.KeySpeed = opt.key.TextSpeed = 10;
			keys.send("Ctrl+C"); // копіюємо
			string clipText = clipboard.copy(); // Зчитуємо вміст з буферу обміну
			string clipTextTry = clipText; //
			
			// взяти послідовності з 10 цифр 3 букви спереду та ще 2 цифр 
			string patternMGRS = @"(\d{2}[a-zA-Zа-яА-Я]{3})(\d{10})";
			clipTextTry = "rtrt" + clipTextTry + "rtrt"; // для профілактики
			clipTextTry = clipTextTry.ToUpper();
			clipTextTry = Regex.Replace(clipTextTry, "[^a-zA-Zа-яА-Я0-9]", "");
			Match matchMGRS = Regex.Match(clipTextTry, patternMGRS);
			if (matchMGRS.Success) {
				clipTextTry = Transliterate(InsertSpaces(matchMGRS.Groups[1].Value + matchMGRS.Groups[2].Value));
				// вписуємо вміст
				Clipboard.SetText(clipTextTry);
				// Знаходить та активує вікно якщо воно звернуте 
				var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
				// Знаходить пошуковий рядок
				var e = w.Elm["web:COMBOBOX", "Пошук"].Find(2);
				// жмем пишем
				e.PostClick(100);
				e.SendKeys("Ctrl+A", "!" + clipTextTry, "Enter");
			} else {
				// прибрати усе окрім цифр крапки та коми
				clipText = Regex.Replace(clipText, @"[^0-9,.]", "");
				// додати пробіл між половиною рядка
				//clipText = clipText.Insert(clipText.Length / 2, " ");
				Clipboard.SetText(clipText);
				// Знаходить та активує вікно якщо воно звернуте 
				var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
				// Знаходить пошуковий рядок
				var e = w.Elm["web:COMBOBOX", "Пошук"].Find(2);
				// жмем пишем
				e.PostClick(100);
				e.SendKeys("Ctrl+A", "!" + clipText, "Enter");
				
			}
		}
		// додаємо пробіли
		static string InsertSpaces(string input) {
			input = input.Insert(input.Length - 5, " ");
			input = input.Insert(input.Length - 11, " ");
			input = input.Insert(input.Length - 14, " ");
			
			return input;
		}
		// заміняє букви кирилиці на латиницю
		static string Transliterate(string text) {
			// кирил на лат
			Dictionary<char, string> translitMap = new Dictionary<char, string>
			{
				{'Р', "P"}, {'С', "C"}, {'Т', "T"},
				{'Е', "E"}, {'М', "M"}, {'В', "B"},
				{'А', "A"}, {'О', "O"}, {'Н', "H"},
				{'К', "K"}, {'Х', "X"}
			};
			
			StringBuilder result = new StringBuilder();
			
			foreach (char c in text) {
				if (translitMap.ContainsKey(c)) {
					result.Append(translitMap[c]);
				} else {
					result.Append(c);
				}
			}
			
			return result.ToString();
		}
	}
}