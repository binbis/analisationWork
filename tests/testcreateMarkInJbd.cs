/* 16,07,2024_v1
* 
*/

using System.Windows.Forms;

namespace CSLight {
	class Program {
		static void Main() {
			opt.mouse.MoveSpeed = opt.key.KeySpeed = opt.key.TextSpeed = 20;
			//string markComment = " - встановлена за допомогою ударного коптера ";
			
			// Знаходить та активує вікно якщо воно звернуте 
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			
			// поле шар
			var layerWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-layer", flags: EFFlags.HiddenToo].Find(1);
			layerWindow.MouseClick();
			layerWindow.SendKeys("!11");
			var selectLayerWindow = w.Elm["web:LISTITEM", "(11) Маршрути, міни, загородження \"Птахи Мадяра\""].Find(1).MouseClick();
			
			// поле назва
			string markName = "ПТМ-3 (до 30.07)";
			var nameOfMarkWindow = w.Elm["web:TEXT", prop: "@autocomplete=off", flags: EFFlags.HiddenToo].Find(1);
			nameOfMarkWindow.MouseClick();
			nameOfMarkWindow.SendKeys("Ctrl+A","!"+markName);
			
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
			
		}
	}
}
