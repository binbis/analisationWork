/*
09,07,2024_v1b
1. виділяєш весь звіт(текст)
2. жмеш "ALT+Shift+L"
3. вставляєш оброблений звіт

Скрипт - додає виділиний текст в буфео обміну, якщо в рядку є зайві пробіли - приберає їх,
виправляє координати, якщо є : додає після неї пробіл.

ХОЧУ 
(*) всі пробіли замінити на 1
(*) обробку координат
(*) після ":" після неї, якщо є, обов'язково повинен бути пробіл 
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
				// Якщо рядок містить ":", додати пробіл після цього символу
				if (output.Contains(":"))
				{
					output = output.Replace(":", ": ");
				}
				// Використовуємо регулярний вираз для заміни 2+ пробілів на 1 пробіл
				output = Regex.Replace(line, @"\s+", " ").Trim();
				
				// обробка роординат
				if (line.Contains("Координа"))
				{
					output = fixCoords(output);
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
		// 
		static string fixCoords(string clipText) {
			string patternFirstEnglishBefore = @".{0,2}[a-zA-Z]"; // шаблон (1) 1 англ + 2 символи попереду 
			string patternFirstEnglishAfter = @"[a-zA-Z].{0,12}"; // шаблон (2) 1 англ + 15 символів після (з запасом)
			string firstEnglishBefore = string.Empty;
			string firstEnglishAfter = string.Empty;
			
			//код працює з +18 символів, цей рядок для підстраховки
			//clipText = "***" + clipText + "***";
			// Видаляємо все окрім цифр та англ букв, переводим в один регістр
			clipText = Regex.Replace(clipText, @"[^a-zA-Z0-9]", "").Trim().ToUpper();
			// виявляємо 1 шаблон
			firstEnglishBefore = PatternExtract(clipText,patternFirstEnglishBefore);
			// виявляємо 2 шаблон
			firstEnglishAfter = PatternExtract(clipText,patternFirstEnglishAfter);
			// Формуємо результат: прибераємо дублі після конкатенації + додаємо пробіли 
			clipText = "Координати: " + InsertSpaces(RemoveDuplicates(firstEnglishBefore + firstEnglishAfter));
			
			return clipText;
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
