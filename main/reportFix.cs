/*
06,07,2024_v1
1.виділяєш весь звіт(текст)
2. жмеш "ALT+Shift+L"
3. отримуеш в буфер обміну оброблений звіт

Скрипт - бере виділиний текст, якщо є багато пробілів заміняє хї на 1

ХОЧУ 
* обробку координат
* після ":" після неї, якщо є, обов'язково повинен бути пробіл 
*/
using System.Windows.Forms;
namespace CSLight
{
 class Program
    {
		static void Main()
		{
			// Переписує буфер обміну
			Clipboard.Clear();
			//копіюємо код
			keys.send("Ctrl+C");
			// Зчитуємо вміст з буферу обміну
			string selectText = Clipboard.GetText();
			// Переписує буфер обміну
			Clipboard.Clear();
			// Розбити текст на рядки
			string[] lines = selectText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
			// Створити новий масив для зберігання оброблених рядків
			string[] processedLines = new string[lines.Length];
			int index = 0;
			
			foreach (string line in lines)
			{
				// Використовуємо регулярний вираз для заміни 2+ пробілів на 1 пробіл
				string output = Regex.Replace(line, @"\s+", " ").Trim();

				// Додаємо оброблений рядок до нового масиву, якщо він не порожній
				if (!string.IsNullOrWhiteSpace(output))
				{
					processedLines[index++] = output+"\n";
				}
			}
			// Перетворити перший елемент масиву в верхній регістр
			//processedLines[1] = processedLines[1].ToUpper();
			string outputOurText = string.Empty;
			// Вивести кожен оброблений рядок масиву в ......
			for (int i = 0; i < index; i++)
			{
				outputOurText += processedLines[i];
			}
			Clipboard.SetText(outputOurText);
		}
	}
}
