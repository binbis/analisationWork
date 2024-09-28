/** 28.09.2024
реб рер жбд
переходить по координатам
обирає мітку(створює)
заповнює
прикріпити відео - це вже самі
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
		string mgrsX = parts[8];
		string mgrsY = parts[9];
		string mgsrCoord = $"{mgrsX}{mgrsY}";
		string layerName = "схована техніка";
		string name = "FPV (Подавлено)";
		string capability = "небо";
		string identyfication = "ворож";
		string comment = $"{dateJbd} {timeJbd} - подавлено та знищено засобами роти РЕБ 414 ОПБС";
		string bplaName = "вертикального зльоту";
		
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
		
		//. перехід по корам
		var searchWindow = w.Elm["web:COMBOBOX", prop: new("@aria-label=Пошук", "@placeholder=Знайти адресу або координату")].Find(1);
		searchWindow.PostClick();
		keys.sendL("Ctrl+A", "!" + mgsrCoord, "Enter");
		//.. 
		wait.ms(6000);
		//. ставимо мітку
		var createButton =  w.Elm["web:LISTITEM", prop: "@data-testid=create-object"].Find(1);
		createButton.PostClick(scroll: 250);
		wait.ms(6000);
		// обираємо мітку
		var categorySearch = w.Elm["web:GROUPING", prop: "@data-testid=map-page", navig: "child2 child2 child2"].Find(1);
		categorySearch.PostClick();
		keys.sendL("Ctrl+A", "!" + bplaName);
		wait.ms(800);
		var bplaMark = w.Elm["web:LISTITEM", "Військовий повітряний засіб БПЛА вертикального зльоту / посадки (VT-UAV)"].Find(1);
		bplaMark.PostClick();
		wait.ms(8000);
		//..
		
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