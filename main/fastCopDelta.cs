/**
05,07,2024_v2.2a
0. відкрий дельту в окремій вкладці та вікні, (!не міняй вкладку!), можеш звернути це вікно
1. виділяєш текст де є координати mgrs
2. жмеш hotkey
3. відкриває дельту(де б вона не була та який розмір вікна б не мала) 
4. вставяє оброблені координати до пошуку та натискає кнопку пошуку

ХОЧУ:
*. ще не придумав як нормально заміняти укр-ру букри на англ. можливо потрібно змінити концет
* щоб не переносився курсор
*/
//using System;
using System.Windows.Forms;
//using System.Text.RegularExpressions;
namespace CSLight
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
			opt.mouse.MoveSpeed = opt.key.KeySpeed = opt.key.TextSpeed = 10;
			//чистим буфер
			Clipboard.Clear();
			//копіюємо код
			keys.send("Ctrl+C");
			
			// Зчитуємо вміст з буферу обміну
			string clipText = Clipboard.GetText();
			//чистим буфер
			Clipboard.Clear();
			
			//string patternFirstEnglish = @"[a-zA-Z]"; // шаблон (3) 1 англ буква
			string patternFirstEnglishBefore = @".{0,2}[a-zA-Z]"; // шаблон (1) 1 англ + 2 символи попереду 
			string patternFirstEnglishAfter = @"[a-zA-Z].{0,12}"; // шаблон (2) 1 англ + 15 символів після (з запасом)
			string firstEnglishBefore = string.Empty;
			string firstEnglishAfter = string.Empty;
			
			if (clipText.Length > 20) {
				// приводимо до онго регістру
				clipText = clipText.ToUpper();
				// Видаляємо все окрім цифр та англ букв, переводим в один регістр
				clipText = Regex.Replace(clipText, @"[^a-zA-Z0-9]", "");
				// виявляємо 1 шаблон
				firstEnglishBefore = PatternExtract(clipText,patternFirstEnglishBefore);
				// виявляємо 2 шаблон
				firstEnglishAfter = PatternExtract(clipText,patternFirstEnglishAfter);
				// Формуємо результат: прибераємо дублі після конкатенації + додаємо пробіли 
				clipText = InsertSpaces(RemoveDuplicates(firstEnglishBefore + firstEnglishAfter));
			}
			if(clipText.Length <= 18) {
				//код працює з +18 символів, цей рядок для підстраховки
				clipText = "***" + clipText + "***";
				// Видаляємо все окрім цифр та англ букв, прибераємо пробіли(якщо є) попереду та позаду, переводим в один регістр
				clipText = Regex.Replace(clipText, @"[^a-zA-Z0-9]", "").Trim().ToUpper();
				// виявляємо 1 шаблон
				firstEnglishBefore = PatternExtract(clipText,patternFirstEnglishBefore);
				// виявляємо 2 шаблон
				firstEnglishAfter = PatternExtract(clipText,patternFirstEnglishAfter);
				// Формуємо результат: прибераємо дублі після конкатенації + додаємо пробіли 
				clipText = InsertSpaces(RemoveDuplicates(firstEnglishBefore + firstEnglishAfter));
			}
			
			// Переписує буфер обміну
			Clipboard.SetText(clipText);
			// Знаходить та активує вікно якщо воно звернуте 
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			// Знаходить пошуковий рядок
			var e = w.Elm["web:COMBOBOX", "Пошук", "@id=search-container-input"].Find(2).MouseClick();
			// вставляє з буферу обміну 
			keys.send("Ctrl+A Ctrl+V Enter");
		}
		// Функція для видалення дублікатів зі строки
		static string RemoveDuplicates(string input) {
			string result = "";
			foreach (char c in input)
			{
				if (char.IsLetter(c) && result.IndexOf(c) != -1)
				{
					continue; // Пропускаємо англійські букви, які вже є в результаті
				}
				result += c;
			}
			return result;
		}
		// отримаємо вміст шаблону з рядку
		static string PatternExtract(string copyText, string pattern) {
			Match match = Regex.Match(copyText, pattern);
            if (match.Success)
            {
                return match.Value;
            }
			 return string.Empty; // or return null, or any default value you prefer
		}
		//додаємо пробіли
		static string InsertSpaces(string input)
		{
			/*
			if (input.Length < 15)
			{
				return input; // If the input length is less than 15, no need to insert spaces
			}
			*/
			// Insert spaces at the specified positions
			input = input.Insert(input.Length - 5, " ");
			input = input.Insert(input.Length - 11, " ");
			input = input.Insert(input.Length - 14, " ");

			return input;
		}
	}
}