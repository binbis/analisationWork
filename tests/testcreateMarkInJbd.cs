/* 01,08,2024_v1.3
* в тестовому режимі
функціонал заповнення мітки в тебе є
тепер треба додати умови, розбити на класи, чи щось таке

ще є така помилка для массива 
Index was outside the bounds of the array. це от тут parts[]
*/

using System.Windows.Forms;

namespace CSLight {
	class Program {
		static void Main() {
			
			opt.mouse.MoveSpeed = opt.key.KeySpeed = 25;
			opt.key.TextSpeed = 20;
			//виділяємо весь рядок
			keys.send("Shift+Space*2");
			//копіюємо код
			keys.send("Ctrl+C");
			// зчитуємо буфер обміну
			string clipboardData = clipboard.copy();
			
			// Розділяємо рядок на частини
			string[] parts = clipboardData.Split('\t');
			
			// Присвоюємо змінним відповідні значення
			string dateJbd = parts[0]; // 27.07.2024
			string timeJbd = parts[1]; //00:40
			string commentJbd = parts[2]; //коментар (для ідентифікації скоріш за все)
			string crewTeamJbd = parts[4]; // R-18-1 (Мавка)
			string whatDidJbd = parts[5]; // Мінування (можливо його видалю)
			string targetClassJbd = parts[6]; // Міна/Вантажівка/Військ. баггі/Скупчення ОС/Укриття
			string idTargetJbd = parts[9]; // Міна 270724043
			// Встановлено/Уражено/Промах/Авар. скид/Повторно уражено
			string establishedJbd = parts[24];
			//приводжу дату до формату дельти
			string dateDeltaFormat = dateJbd.Replace('.','/');
			/*
			// массив техніки
			string[] machineryArray = {
				"ББМ / МТ-ЛБ","Авто","Вантажівка"
			};
			// для мін
			string targetMinePTM = "Міна";
			*/
			// Знаходить та активує вікно якщо воно звернуте 
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			// поле шар
			var layerWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-layer"].Find(3);
			layerWindow.Select();
			wait.ms(100);
			layerWindow.PostClickD(2);
			wait.ms(100);
			layerWindow.SendKeys("Ctrl+A","!11","Enter");
			
			// поле назва
			string markName = "ПТМ-3 до ("+ datePlasFourteen(dateJbd)+")";
			var nameOfMarkWindow = w.Elm["web:TEXT", prop: new("@data-testid=T")].Find(3);
			nameOfMarkWindow.PostClick(2);
			wait.ms(100);
			nameOfMarkWindow.SendKeys("Ctrl+A","!"+markName);
			
			// поле дата / час
			var dateDeltaWindow = w.Elm["web:TEXT", prop: "@data-testid=W"].Find(3);
			dateDeltaWindow.PostClick(2);
			dateDeltaWindow.SendKeys("Ctrl+A","!"+dateDeltaFormat);
			wait.ms(100);
			var timeDeltaWindow = w.Elm["web:TEXT", prop: "@data-testid=W-time-input"].Find(1);
			timeDeltaWindow.SendKeys("!"+timeJbd);
			wait.ms(100);
			nameOfMarkWindow.PostClick(2);
			
			// поле кількість
			var numberOfnumberWindow = w.Elm["web:SPINBUTTON", prop: new("@data-testid=C", "@type=number")].Find(3);
			numberOfnumberWindow.PostClick(1);
			wait.ms(100);
			numberOfnumberWindow.SendKeys("Ctrl+A", "!1");
			
			// поле боєздатність
			string fullaim = "Повні";
			var combatCapabilityWindow = w.Elm["web:GROUPING", prop: "@data-testid=operational-condition-select"].Find(3);
			combatCapabilityWindow.PostClickD(1);
			wait.ms(100);
			combatCapabilityWindow.SendKeys("","!"+fullaim, "Enter");
			
			// ідетнифікація
			string friendly = "дружній";
			var identificationWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-HO"].Find(3);
			identificationWindow.PostClick(1);
			wait.ms(100);
			identificationWindow.SendKeys("Ctrl+A", "!"+friendly, "Enter");
			
			// достовірність
			var reliabilityWindow = w.Elm["web:RADIOBUTTON", "A", "@data-testid=reliability-key-A"].Find(3);
			reliabilityWindow.PostClick(1);
			wait.ms(100);
			var certaintyWindow = w.Elm["web:RADIOBUTTON", "2", "@data-testid=reliability-key-2"].Find(3);
			certaintyWindow.PostClick(1);
			
			// тип джерела
			string flyeye = "повітр";
			var typeOfSourceWindow = w.Elm["web:GROUPING", prop: "@data-testid=AD", flags: EFFlags.HiddenToo].Find(1);
			typeOfSourceWindow.PostClick(2);
			wait.ms(100);
			typeOfSourceWindow.SendKeys("Ctrl+A", "!"+flyeye, "Enter");
			wait.ms(100);
			// завйваження штабу ід
			string idPurchaseText = idTargetJbd;
			var idPurchaseWindow = w.Elm["web:TEXT", prop: "@data-testid=G", flags: EFFlags.HiddenToo].Find(1);
			idPurchaseWindow.PostClick();
			wait.ms(100);
			idPurchaseWindow.SendKeys("Ctrl+A", "!"+idPurchaseText, "Enter");
			
			// коментар
			string commentContents = dateJbd + " " + timeJbd + " - встановлена за допомогою ударного коптера " + crewTeamJbd;
			var commentWindow = w.Elm["web:TEXT", prop: "@data-testid=comment-editing__textarea"].Find(1);
			commentWindow.PostClick();
			commentWindow.SendKeys("Ctrl+A", "!"+commentContents);
			
			// кнопка коментаря
			/*
			var commentAsseptButton = w.Elm["web:BUTTON", prop: "@data-testid=comment-editing__button-save"].Find(1);
			wait.ms(200);
			commentAsseptButton.ScrollTo();
			wait.ms(200);
			commentAsseptButton.MouseClick();
			*/
		
		}
		static string datePlasFourteen(string date) {
			// Перетворюємо рядок дати у DateTime
			DateTime originalDate = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
			// Додаємо Х днів
			DateTime newDate = originalDate.AddDays(60);
			// Перетворюємо нову дату назад у рядок
			//string newDateString = newDate.ToString("dd.MM.yyyy");
			string newDateString = newDate.ToString("dd.MM.yyyy");
			return newDateString;
		}
	}
}
