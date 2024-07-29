/**
22,07,2024_v3.0
0. відкрий дельту в окремій вкладці та вікні, (!не міняй вкладку!), можеш звернути це вікно
1. виділяєш текст де є координати mgrs
2. жмеш hotkey
3. відкриває дельту(де б вона не була та який розмір вікна б не мала) 
4. вставяє оброблені координати до пошуку та натискає кнопку пошуку

ХОЧУ:
(*) виправлення букв присутнє
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
			// для профілактики
			clipText = "rtrt" + clipText + "rtrt";
			//чистим буфер
			Clipboard.Clear();
			//банальна переввірка
			if (clipText.Length!=0) {
				clipText = clipText.ToUpper();
				clipText = Regex.Replace(clipText, "[^a-zA-Zа-яА-Я0-9]", "");
			}
			// взяти послідовність з 10 чисел та 5 символів перед нею
			string pattern = @"(.{5})(\d{10})";
			Match match = Regex.Match(clipText, pattern);
			
			// банальна перевірка
			if (match.Success)
			{
				// додаємо пробіли в текст та заміняємо невірну букви
				clipText = Transliterate(InsertSpaces(match.Groups[1].Value + match.Groups[2].Value));
			}
			
			// Переписує буфер обміну
			Clipboard.SetText(clipText);
			// Знаходить та активує вікно якщо воно звернуте 
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			// Знаходить пошуковий рядок
			var e = w.Elm["web:COMBOBOX", "Пошук"].Find(2).MouseClick();
			// вставляє з буферу обміну 
			keys.send("Ctrl+A Ctrl+V Enter");
		}
		// додаємо пробіли
		static string InsertSpaces(string input)
		{
			input = input.Insert(input.Length - 5, " ");
			input = input.Insert(input.Length - 11, " ");
			input = input.Insert(input.Length - 14, " ");

			return input;
		}
		// заміняє букви кирилиці на латиницю
		static string Transliterate(string text)
		{
			// кирил на лат
			Dictionary<char, string> translitMap = new Dictionary<char, string>
			{
				{'Р', "P"}, {'С', "C"}, {'Т', "T"},
				{'Е', "E"}, {'М', "M"}, {'В', "B"},
				{'А', "A"}, {'О', "O"}, {'Н', "H"},
				{'К', "K"}, {'Х', "X"}
			};

			StringBuilder result = new StringBuilder();

			foreach (char c in text)
			{
				if (translitMap.ContainsKey(c))
				{
					result.Append(translitMap[c]);
				}
				else
				{
					result.Append(c);
				}
			}

			return result.ToString();
		}
	}
}