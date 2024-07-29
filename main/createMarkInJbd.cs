/* 16,07,2024_v1.2
* 
*/

using System.Windows.Forms;

namespace CSLight {
	class Program {
		static void Main() {
			opt.mouse.MoveSpeed = opt.key.KeySpeed = opt.key.TextSpeed = 20;
			//string markComment = " - встановлена за допомогою ударного коптера ";
			//чистим буфер
			Clipboard.Clear();
			//виділяємо весь рядок
			//keys.send("Shift+Space Space");
			//копіюємо код
			keys.send("Ctrl+C");
			// зчитуємо буфер обміну
			string clipboardData = Clipboard.GetText();
			
			// Розділяємо рядок на частини
			string[] parts = clipboardData.Split('\t');

			// Присвоюємо змінним відповідні значення
			string date = parts[0];
			string time = parts[1];
			string crewTeam = parts[4];
			string whatDid = parts[5];
			string targetClass = parts[7];
			string idTarget = parts[9];
			string established = parts[25]; // Встановлено/Нерозрив/Промах/Авар. скид
			
			//приводжу дату до формату дельти
			string dateDeltaFormat = date.Replace('.','/');
			
			string dateForMines = datePlasFourteen(date);
			
			// Знаходить та активує вікно якщо воно звернуте 
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			
			// поле шар
			var layerWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-layer", flags: EFFlags.HiddenToo].Find(1);
			layerWindow.MouseClick();
			layerWindow.SendKeys("!11");
			var selectLayerWindow = w.Elm["web:LISTITEM", "(11) Маршрути, міни, загородження \"Птахи Мадяра\""].Find(1).MouseClick();
			
			// поле назва
			//string markName = "ПТМ-3 (до 10.08)";
			string markName = "ПТМ-3 (до "+dateForMines+")";
			var nameOfMarkWindow = w.Elm["web:TEXT", prop: "@autocomplete=off", flags: EFFlags.HiddenToo].Find(1);
			nameOfMarkWindow.MouseClick();
			nameOfMarkWindow.SendKeys("Ctrl+A","!"+markName);
			
			// поле дата виявлення
			var detectionDateWindow = w.Elm["web:TEXT", "дд/мм/рррр", flags: EFFlags.HiddenToo].Find(1);
			detectionDateWindow.MouseClick();
			detectionDateWindow.SendKeys("Ctrl+A","!"+dateDeltaFormat);
			
			// поле час виявлення
			var detectionTimeWindow = w.Elm["web:TEXT", "гг:хх", flags: EFFlags.HiddenToo].Find(1);
			detectionTimeWindow.MouseClick();
			detectionTimeWindow.SendKeys("!"+time, "Esc");
			
			// поле кількість
			var numberOfnumberWindow = w.Elm["web:SPINBUTTON", prop: "@autocomplete=off", flags: EFFlags.HiddenToo].Find(1);
			numberOfnumberWindow.MouseClick();
			numberOfnumberWindow.SendKeys("Ctrl+A", "!1");
			
			// поле боєздатність
			string fullaim = "Повністю бо";
			var combatCapabilityWindow = w.Elm["web:GROUPING", prop: "@data-testid=operational-condition-select", flags: EFFlags.HiddenToo].Find(1);
			combatCapabilityWindow.MouseClick();
			combatCapabilityWindow.SendKeys("Ctrl+A", "!"+fullaim, "Enter");
			
			// ідетнифікація
			string friendly = "дружній";
			var identificationWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-HO", flags: EFFlags.HiddenToo].Find(1);
			identificationWindow.MouseClick();
			identificationWindow.SendKeys("Ctrl+A", "!"+friendly, "Enter");
			
			// достовірність
			var reliabilityWindow = w.Elm["web:RADIOBUTTON", "A", "@data-testid=reliability-key-A", flags: EFFlags.HiddenToo].Find(1).MouseClick();
			var certaintyWindow = w.Elm["web:RADIOBUTTON", "2", "@data-testid=reliability-key-2", flags: EFFlags.HiddenToo].Find(1).MouseClick();
			
			// тип джерела
			string flyeye = "повітр";
			var typeOfSourceWindow = w.Elm["web:STATICTEXT", "Не обрано", flags: EFFlags.HiddenToo].Find(1);
			typeOfSourceWindow.MouseClick();
			typeOfSourceWindow.MouseClick();
			typeOfSourceWindow.SendKeys("!"+flyeye, "Enter");
			
			// завйваження штабу ід
			var idPurchaseWindow = w.Elm["web:TEXT", prop: "@data-testid=G", flags: EFFlags.HiddenToo].Find(1);
			idPurchaseWindow.MouseClick();
			idPurchaseWindow.SendKeys("Ctrl+A", "!"+idTarget, "Enter");
			
			// коментар
			string commentContents = date + ' ' + time + " - встановлена за допомогою ударного коптера " + crewTeam;
			var commentWindow = w.Elm["web:TEXT", "Введіть значення", "@data-testid=comment-editing__textarea", EFFlags.HiddenToo].Find(1);
			commentWindow.MouseClick();
			commentWindow.SendKeys("Ctrl+A", "!"+commentContents);
			//w.Elm["web:BUTTON", prop: "@data-testid=comment-editing__button-save", flags: EFFlags.HiddenToo].Find(1).MouseClick();
			
		}
		static string datePlasFourteen(string date) {
			// Перетворюємо рядок дати у DateTime
			DateTime originalDate = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
			// Додаємо 14 днів
			DateTime newDate = originalDate.AddDays(14);
			// Перетворюємо нову дату назад у рядок
			//string newDateString = newDate.ToString("dd.MM.yyyy");
			string newDateString = newDate.ToString("dd.MM");
			return newDateString;
		}
	}
}
