/*/ c \analisationWork\globalClass\Bisbin.cs; /*/
/* міни кліке для юпітера 24,11,2024

*/
using System.Windows;
using System.Windows.Controls;

namespace CSLight {
	
	class Program {
		static int Main() {
			opt.key.KeySpeed = 20;
			opt.key.TextSpeed = 20;
			
			// вікно налаштувань
			var b = new wpfBuilder("Window").WinSize(500);
			b.R.Add("Затримка між діями", out TextBox dbe, "3").Focus(); // вказати затримку між діями
			b.R.Add("кількість ітерацій", out TextBox cucleCount); // кількість ітерацій
			b.R.AddOkCancel(); //row with OK and Cancel buttons that close the dialog
			b.End();
			if (!b.ShowDialog()) return 1; //show dialog. Exit if closed not with the OK button.
			
			double delayBetweenAction = dbe.Text.ToNumber();
			string controlFrase = "414 ОПБС";
			var window = wnd.find(1, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			var captureColor = window.Child(1, "Chrome Legacy Window", "Chrome_RenderWidgetHostHWND");
			
			for (int i = 0; i < cucleCount.Text.ToNumber(); i++) {
				var imgColor = uiimage.find(-1, captureColor, 0x80E0FF);
				if (imgColor == null) {
					imgColor = uiimage.find(-1, captureColor, 0xFFFF80);
				}
				imgColor.PostClick();
				wait.s(delayBetweenAction);
				// обрати елемент зі списку
				var listItem = window.Elm["web:GROUPING", "Оберіть необхідний об'єкт", navig: "next5 child"].Find(-1);
				if (listItem == null) {
					errorWindow("не знайшов елемент зі списку");
					return 1;
				}
				listItem.PostClick();
				wait.s(delayBetweenAction);
				// кнопка редагувати
				var editButton = window.Elm["web:BUTTON", "Редагувати"].Find(-1);
				if (editButton == null) {
					errorWindow("не знайшов кнопку редагування");
					return 1;
				}
				editButton.PostClick();
				wait.s(delayBetweenAction);
				// зауваження штабу поле
				var idPurchaseWindow = window.Elm["STATICTEXT", "Зауваження штабу", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next"].Find(-1);
				if (idPurchaseWindow == null) {
					errorWindow("не знайшов поле Зауваження штабу");
					return 1;
				}
				idPurchaseWindow.PostClick(scroll: 500);
				keys.sendL("Ctrl+A", "!" + controlFrase);
				wait.s(delayBetweenAction);
				// збереження
				var acceptButton = window.Elm["web:BUTTON", "Зберегти"].Find(-1);
				if (acceptButton == null) {
					errorWindow("не знайшов кнопку збереження змін для мітки");
					return 1;
				}
				acceptButton.PostClick(scroll: 500);
				wait.s(delayBetweenAction);
				//попередження
				var continueButton = window.Elm["web:BUTTON", "Продовжити"].Find(-1);
				if (acceptButton == null) {
					errorWindow("не знайшов кнопку підтвердження");
					return 1;
				}
				continueButton.PostClick();
				wait.s(delayBetweenAction);
			}
			return 0;
		}
		static void errorWindow(string erorText) {
			// вікно налаштувань
			var b = new wpfBuilder("Window").WinSize(400);
			b.R.Add(out Label _, erorText).Focus(); // текст помилки
			
			b.R.AddOkCancel(); //row with OK and Cancel buttons that close the dialog
			b.End();
			if (!b.ShowDialog()) return; //show dialog. Exit if closed not with the OK button.
		}
	}
}

