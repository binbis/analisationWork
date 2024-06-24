/**
24,06,2024_v2 
0. відкрий дельту в окремому вікні, не міняй вкладку, (можеш звернути це вікно)
1. виділяєш текст де є координати mgrs
2. скрипт копіює їх, прибирає та виправляє їх,
3. відкриває дельту(де б вона не була та який розмір вікна б не мала) 
4. вставяє оброблені координати до пошуку та натискає кнопку пошуку
*/
using System;
using System.Windows.Forms;
namespace CSLight
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
			opt.mouse.MoveSpeed = opt.key.KeySpeed = opt.key.TextSpeed = 10;
			//копіюємо код
			keys.send("Ctrl+C");
			string searchString = "37U";
			string letters = "CR";
			string pattern = @"37U.{12}";
			string result = string.Empty;
			string resultWclipText = string.Empty;
			// Зчитуємо вміст з буферу обміну
			string clipText = Clipboard.GetText();
			// Видаляємо все окрім цифр та букв, переводим до одного регістру
            clipText = Regex.Replace(clipText, @"[^a-zA-Z0-9]", "").ToUpper();
			
            // Знаходимо підрядок за допомогою регулярного виразу
            Match match = Regex.Match(clipText, pattern);
            if (match.Success)
            {
                clipText = match.Value;
            }
			// Знаходимо індекс підрядка "37U"
			int index = clipText.IndexOf(searchString);
			if (index != -1)
			{
				//Залишаємо сам "37U" та все після нього
				//clipText = clipText.Substring(index);
				
				// Витягуємо частину після "37U"
                string afterSearchString = clipText.Substring(index + searchString.Length);
				
                // Перевіряємо, чи є після "37U" дві літери
                if (!Regex.IsMatch(afterSearchString, @"^[A-Z]{2}"))
                {
                    // Якщо немає, додаємо "CR"
                    afterSearchString = afterSearchString.Insert(index + searchString.Length, " " + letters + " ");
                }
				else if (afterSearchString.StartsWith(letters))
				{
					// Якщо "CR" вже присутній, додаємо пробіли перед і після нього
					afterSearchString = afterSearchString.Replace(letters, " " + letters + " ");
				}
				// Знаходимо частину з цифрами після двох літер
                string numbersPart = afterSearchString.Substring(index + searchString.Length + 2);
                // Знаходимо середину цифр
                int middleIndex = numbersPart.Length / 2;
                // Вставляємо пробіл посередині
                numbersPart = numbersPart.Insert(middleIndex, " ");
				// Формуємо результат
                clipText = searchString + afterSearchString.Substring(0, index + searchString.Length + 2) + numbersPart;
			}
			Clipboard.SetText(clipText);
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			var e = w.Elm["web:COMBOBOX", "Пошук", "@id=search-container-input"].Find(2).MouseClick();
			keys.send("Ctrl+A Ctrl+V Enter");
			
		}
	}
}
