/**
24,06,2024_v1 
0. відкрий дельту в окремому вікні, в окремій вкладці, можеш звернути віце вікно
1. виділяєш координати
2. скрипт копіює їх, виправляє, відкриває дельту(де б вона не була та який розмір вікна) вставяє їх та нажимає кнопку пошуку
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
			
			// Оголошуємо STAThread для використання Clipboard
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// Зчитуємо вміст з буферу обміну
			string clipText = Clipboard.GetText();
			//перевірка на вміст кавичок
			if (clipText.Contains('"')){
				//- кавички
				clipText = clipText.Replace("\"", "").Trim();
			}
			//перевірка на вміст крапок
			if (clipText.Contains('.')) {
				//- крапки
				clipText = clipText.Replace(".", "").Trim();
			}
			//перевірка на вміст ком
			if (clipText.Contains(',')) {
				//- коми
				clipText = clipText.Replace(",", "").Trim();
			}
			//перевірка дліни
			if (clipText.Length != 18) {
				//- пробіли
				clipText = clipText.Trim();
				// Розділяємо рядок на частини за допомогою пробілів
				string[] clipTextParts = clipText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				// Об'єднуємо частини назад в рядок, додаючи по одному пробілу між частинами
				clipText = string.Join(" ", clipTextParts);
				//- пробіли
				clipText = clipText.Trim();
			}
			
			// приведення до одного регістру
			clipText = clipText.ToUpper();
			
			Clipboard.SetText(clipText);
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			var e = w.Elm["web:COMBOBOX", "Пошук", "@id=search-container-input"].Find(2).MouseClick();
			keys.send("Ctrl+A Ctrl+V Enter");
			
		}
	}
}
