/**
07,08,2024_v3.1
0. відкрий дельту в окремій вкладці та вікні, (!не міняй вкладку!), можеш звернути це вікно
1. виділяєш текст де є координати mgrs, google, уск-2000
2. жмеш hotkey
3. відкриває дельту(де б вона не була та який розмір вікна б не мала) 
4. вставяє оброблені координати до пошуку та натискає кнопку пошуку

*/

using System.Windows.Forms;

namespace CSLight
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
			opt.mouse.MoveSpeed = opt.key.KeySpeed = opt.key.TextSpeed = 10;
			//чистим буфер
			clipboard.clear();
			//копіюємо код
			keys.send("Ctrl+C");
			// Зчитуємо вміст з буферу обміну
			string clipText = clipboard.copy();
			// для профілактики
			clipText = "rtrt" + clipText + "rtrt";
			//чистим буфер
			clipboard.clear();
			//банальна переввірка
			if (clipText.Length!=0) {
				clipText = clipText.ToUpper();
				clipText = Regex.Replace(clipText, "[^a-zA-Zа-яА-Я0-9]", "");
			}
			
			// взяти послідовність з 10 чисел та 5 символів перед нею
			string patternMGRS = @"(.{5})(\d{10})";
			// взяти послідовність з 18 чисел
			//string patternGoogle = @".\d{18}";
			string patternGoogle = @"(\d{2})(\d{7})(\d{2})(\d{7})";
			// взяти послідовність з 14 чисел, такий запис для коми з пробілом
			string patternYSK = @"(\d{7})(\d{7})";
			
			// Перевірка google паттерну
			Match match1 = Regex.Match(clipText, patternGoogle);
			if (match1.Success)
			{
				clipText = $"{match1.Groups[1].Value}.{match1.Groups[2].Value}, {match1.Groups[3].Value}.{match1.Groups[4].Value}";
				//Console.WriteLine("знайшов google = "+ match1);
			}
			else
			{
				// Перевірка уск-2000 паттерну
				Match match2 = Regex.Match(clipText, patternYSK);
				if (match2.Success)
				{
					clipText = $"{match2.Groups[1].Value}, {match2.Groups[2].Value}";
					//Console.WriteLine("знайшов уск-2000 = "+ match2);
				}
				else
				{
					// Перевірка mgrs паттерну
					Match match3 = Regex.Match(clipText, patternMGRS);
					if (match3.Success)
					{
						clipText = Transliterate(InsertSpaces(match3.Groups[1].Value + match3.Groups[2].Value));
						//Console.WriteLine("знайшов mgrs = " + match3);
					}
					else
					{
						Console.WriteLine("Жодна послідовність не знайдена.");
					}
				}
			}
			Clipboard.SetText(clipText);
			// Знаходить та активує вікно якщо воно звернуте 
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			// Знаходить пошуковий рядок
			var e = w.Elm["web:COMBOBOX", "Пошук"].Find(2);
			// жмем пишем
			e.PostClick(1);
			e.SendKeys("Ctrl+A", "!" + clipText, "Enter");
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