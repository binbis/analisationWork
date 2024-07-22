/*
22,07,2024_v1.5
1. виділяєш весь звіт(текст)
2. жмеш "ALT+Shift+L"
3. вставляєш оброблений звіт

Скрипт - додає виділиний текст в буфео обміну, якщо в рядку є зайві пробіли - приберає їх,
виправляє координати, якщо є : додає після неї пробіл.

ХОЧУ 
(*) всі пробіли замінити на 1
* обробку координат - все ще не працює
(*) після ":" після неї, якщо є, обов'язково повинен бути пробіл 
* перевірка на присутність дати якщо немає додати
*/
using System.Windows.Forms;
namespace CSLight
{
 class Program
    {
		static void Main()
		{
			// Чистим буфер обміну
			Clipboard.Clear();
			//копіюємо код
			keys.send("Ctrl+C");
			// Зчитуємо вміст з буферу обміну
			string selectText = Clipboard.GetText();
			// Чистим буфер обміну
			Clipboard.Clear();
			
			// Розбити текст на рядки
			string[] lines = selectText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
			// Створити новий масив для зберігання оброблених рядків
			string[] processedLines = new string[lines.Length];
			int index = 0;
			
			foreach (string line in lines)
			{
				string output = line;
				// обробка роординат
				if (output.Contains("ордина"))
				{
					output = fixCoords(output);
					Console.WriteLine("sucsess");
				}
				
				// Використовуємо регулярний вираз для заміни 2+ пробілів на 1 пробіл
				output = Regex.Replace(line, @"\s+", " ").Trim();
				
				// Якщо рядок містить ":", додати пробіл після цього символу
				if (output.Contains(":"))
				{
					output = output.Replace(":", ": ");
				}
				
				// Додаємо оброблений рядок до нового масиву, якщо він не порожній
				if (!string.IsNullOrWhiteSpace(output))
				{
					processedLines[index++] = output+"\n";
				}
			}
			string outputOurText = string.Empty;
			// Вивести кожен оброблений рядок масиву в ......
			for (int i = 0; i < index; i++)
			{
				outputOurText += processedLines[i];
			}
			// додає в буфер обміну оброблений текст
			Clipboard.SetText(outputOurText);
		}
		// виправлення координат
		static string fixCoords(string clipText) {
			string clipTextRet = clipText;
			if (clipText.Length!=0) {
				clipTextRet = clipTextRet.ToUpper();
				clipTextRet = Regex.Replace(clipTextRet, "[^a-zA-Zа-яА-Я0-9]", "");
			}
			
			string pattern = @"(.{5})(\d{10})";
        
			Match match = Regex.Match(clipTextRet, pattern);
			
			if (match.Success)
			{
				clipTextRet = "Координати: " + Transliterate(InsertSpaces(match.Groups[1].Value + match.Groups[2].Value));
			}
			return clipTextRet;
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
