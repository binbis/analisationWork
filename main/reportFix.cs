/*
22,07,2024_v1.5
1. виділяєш весь звіт(текст)
2. жмеш "ALT+Shift+L"
3. вставляєш оброблений звіт

Скрипт - додає виділиний текст в буфео обміну, якщо в рядку є зайві пробіли - приберає їх,
виправляє координати, якщо є : додає після неї пробіл.

недороблено

ХОЧУ 
(*) всі пробіли замінити на 1
не прац (*) обробку координат - все ще не працює
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
			// об'єднати кожен оброблений рядок масиву в ......
			for (int i = 0; i < index; i++)
			{
				outputOurText += processedLines[i];
			}
			// додає в буфер обміну оброблений текст
			Clipboard.SetText(outputOurText);
		}
	}
}
