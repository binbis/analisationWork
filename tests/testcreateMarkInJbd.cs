/* 16,07,2024_v1
* в тестовому режимі
*/

using System.Windows.Forms;

namespace CSLight {
	class Program {
		static void Main() {
			opt.mouse.MoveSpeed = opt.key.KeySpeed = opt.key.TextSpeed = 20;
			
			// Знаходить та активує вікно якщо воно звернуте 
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			
			// поле шар
			var layerWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-layer"].Find(3);
			layerWindow.WebInvoke();
			if (true) {
				layerWindow = layerWindow.Navigate("parrent");
				layerWindow.SendKeys("!11","Enter");
			} else {
				
			}
			//layerWindow.PostClick();
			layerWindow.SendKeys("Ctrl+A", "!11", "Enter");
			/*
			// поле назва
			string markName = "ПТМ-3 (до 30.07)";
			var nameOfMarkWindow = w.Elm["web:TEXT", prop: new("@data-testid=T")].Find(2);
			nameOfMarkWindow.PostClick();
			nameOfMarkWindow.SendKeys("Ctrl+A","!"+markName);
			
			// поле кількість
			var numberOfnumberWindow = w.Elm["web:SPINBUTTON", prop: new("@data-testid=C", "@type=number")].Find(2);
			numberOfnumberWindow.PostClick();
			numberOfnumberWindow.SendKeys("!1");
			
			// поле боєздатність
			string fullaim = "Повністю бо";
			var combatCapabilityWindow = w.Elm["web:GROUPING", prop: "@data-testid=operational-condition-select"].Find(1);
			combatCapabilityWindow.PostClick();
			combatCapabilityWindow.SendKeys("!"+fullaim, "Enter");
			
			// ідетнифікація
			string friendly = "дружній";
			var identificationWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-HO"].Find(1);
			identificationWindow.PostClick();
			identificationWindow.SendKeys("Ctrl+A", "!"+friendly, "Enter");
			
			// достовірність
			var reliabilityWindow = w.Elm["web:RADIOBUTTON", "A", "@data-testid=reliability-key-A"].Find(1);
			reliabilityWindow.PostClick();
			var certaintyWindow = w.Elm["web:RADIOBUTTON", "2", "@data-testid=reliability-key-2"].Find(1);
			certaintyWindow.PostClick();
			/*
			// тип джерела
			string flyeye = "повітр";
			var typeOfSourceWindow = w.Elm["web:GROUPING", prop: "@data-testid=AD", flags: EFFlags.HiddenToo].Find(1);
			typeOfSourceWindow.MouseClick();
			typeOfSourceWindow.SendKeys("Ctrl+A", "!"+flyeye, "Enter");
			
			// завйваження штабу ід
			string idPurchaseText = "id";
			var idPurchaseWindow = w.Elm["web:TEXT", prop: "@data-testid=G", flags: EFFlags.HiddenToo].Find(1);
			idPurchaseWindow.MouseClick();
			idPurchaseWindow.SendKeys("Ctrl+A", "!"+idPurchaseText, "Enter");
			
			// коментар
			string commentContents = " - встановлена за допомогою ударного коптера ";
			var commentWindow = w.Elm["web:TEXT", "Введіть значення", "@data-testid=comment-editing__textarea", EFFlags.HiddenToo].Find(1);
			commentWindow.MouseClick();
			commentWindow.SendKeys("Ctrl+A", "!"+commentContents);
			*/
		}
	}
}
