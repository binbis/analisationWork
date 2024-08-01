/**
06,07,2024_v1
1. відкриваєш ексель файл (тільки один)
2. виділяєш людину "Крейда, Вовк і тд."
3. жмеш дві кнопки "ALT+X"

Скрипт - переходить в вікно екселя, відкриває вікно пошуку, вставляє виділене ім'я,
 намагається знайти, якщо знайшло, закриває вікно пошуку поставивши курсор на знайдене ім'я,
 якщо не знаходить, ексель, сигналізує про помилку.
*/
using System;
using System.Windows.Forms;

//script.setup(trayIcon: true, sleepExit: true);

namespace CSLight {
	class Program {
		static void Main() {
			opt.mouse.MoveSpeed = opt.key.KeySpeed = opt.key.TextSpeed = 10;
			string copyWclipboard = string.Empty;
			Clipboard.Clear();
			keys.send("Ctrl+C");
			copyWclipboard = Clipboard.GetText();
			Clipboard.Clear();
			
			wnd.find(1, null, "XLMAIN", "EXCEL.EXE").Activate().ButtonClick(2);
			
			// - пробіли попереду та позаду
			copyWclipboard = copyWclipboard.Trim();
			
			Clipboard.SetText(copyWclipboard);
			keys.send("Ctrl+F");
			keys.send("Ctrl+V Enter");
			keys.send("ESC");
			
		}
	}
}
