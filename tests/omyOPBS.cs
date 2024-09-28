/** 28.09.2024
реб рер
*/

using System.Windows.Forms;

class Program {
	static void Main() {
		opt.key.KeySpeed = 35;
		opt.key.TextSpeed = 30;
		
		keys.send("Shift+Space*2"); //виділяємо весь рядок
		wait.ms(100);
		keys.send("Ctrl+C"); //копіюємо код
		wait.ms(100);
		string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
		string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
		
		// Присвоюємо змінним відповідні значення
		string dateJbd = parts[4]; // 27.07.2024
		// перетворення дати до формату дельти
		string dateDeltaFormat = dateJbd.Replace('.', '/');
		string timeJbd = parts[5]; // 00:40
		string layerName = "схована техніка";
		string name = "FPV (Подавлено)";
		string capability = "небо";
		string identyfication = "ворож";
		string comment = $"{dateJbd} {timeJbd} - подавлено та знищено засобами роти РЕБ 414 ОПБС";
		
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
		
		//. шар
		var layerWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-layer"].Find(-1);
		layerWindow.PostClick(scroll: 300);
		keys.sendL("Ctrl+A", "!" + layerName, "Enter");
		//..
		wait.ms(400);
		//. назва
		var nameWindow = w.Elm["web:TEXT", prop: "@data-testid=T"].Find(-1);
		nameWindow.PostClick(scroll: 300);
		keys.sendL("Ctrl+A", "!" + name, "Enter");
		//..
		wait.ms(400);
		//. час виявлення
		// дата
		var dateWindow = w.Elm["web:TEXT", prop: "@data-testid=W"].Find(-1);
		dateWindow.PostClick();
		keys.sendL("Ctrl+A", "!" + dateDeltaFormat);
		wait.ms(400);
		// час
		var dtimeWindow = w.Elm["web:TEXT", prop: "@data-testid=W-time-input"].Find(-1);
		dtimeWindow.PostClick();
		keys.sendL("Ctrl+A", "!" + timeJbd, "Enter");
		//..
		wait.ms(400);
		//. боєздатність
		var capabilityWindow = w.Elm["web:GROUPING", prop: "@data-testid=operational-condition-select"].Find(-1);
		capabilityWindow.PostClick(scroll: 250);
		keys.sendL("Ctrl+A", "!" + capability, "Enter*2");
		//..
		wait.ms(400);
		//. ідентифікація
		var identyficationWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-HO"].Find(-1);
		identyficationWindow.PostClick(scroll: 250);
		keys.sendL("Ctrl+A", "!" + identyfication, "Enter");
		//..
		wait.ms(400);
		//. коментар
		var commentWindow = w.Elm["web:TEXT", prop: "@data-testid=comment-editing__textarea"].Find(-1);
		commentWindow.PostClick(scroll: 250);
		keys.sendL("Ctrl+A", "!" + comment);
		wait.ms(400);
		var acceptButton = w.Elm["web:BUTTON", prop: new("@data-testid=comment-editing__button-save", "@type=button")].Find(-1);
		acceptButton.PostClick(scroll: 250);
		//..
		
	}
}