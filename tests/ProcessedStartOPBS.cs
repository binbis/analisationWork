/*12.09.2024_0.0.1*/
using System.Windows;
using System.Windows.Controls;

class Program {
	static void Main() {
		var b = new wpfBuilder("Window").WinSize(400);
		b.R.Add(out Label _, "Поле ім'я");
		b.R.Add(out ComboBox recomendName).Editable().Items("Zero|One|Two").Select(2);
		// 
		b.R.Add(out Label _, "шматок коментару");
		b.R.Add(out ComboBox recomendComment).Editable().Items("Zero|One|Two").Select(1);
		// 
		b.R.AddOkCancel();
		b.End();
		// show dialog. Exit if closed not with the OK button.
		if (!b.ShowDialog()) return;
		string nameDeltaFill = recomendName.Text;
		string commentDeltaFill = recomendComment.Text;
		string nameAttachmentMessage = string.Empty;
		string clipData_time = DateTime.Now.ToString("dd/MM/yyyy");
		
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
		wait.ms(250);
		nameAttachmentMessage = nameWithAttachments(nameAttachmentMessage);
		wait.ms(250);
		goToMain();
		wait.ms(250);
		deltaMarkName(nameDeltaFill);
		wait.ms(250);
		clipData_time = dateTimeDeltaCombine(clipData_time);
		wait.ms(250);
		deltaNumberOfnumberWindow();
		wait.ms(250);
		deltaReliabilityWindow();
		wait.ms(250);
		deltaFlyeye();
		wait.ms(250);
		commentDeltaAreaFill(clipData_time, commentDeltaFill, nameAttachmentMessage);
		
	}
	// поле назва
	static void deltaMarkName(string nameDeltaFill) {
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// поле назва
		var nameOfMarkWindow = w.Elm["web:TEXT", prop: new("@data-testid=T")].Find();
		if (nameOfMarkWindow != null) {
			nameOfMarkWindow.PostClick(scroll: 250);
			nameOfMarkWindow.SendKeys("!" + nameDeltaFill);
		}
	}
	//
	static string nameWithAttachments(string nameAttachmentMessage) {
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// вкладка - прикріплення
		var deltaAttachmentWindow = w.Elm["web:GROUPING", prop: new("desc=Прикріплення", "@title=Прикріплення")].Find(1);
		deltaAttachmentWindow.PostClick(scroll: 250);
		// знаходжу елемент, відповідний за назву того хто прикріпив прикруплення
		var firstAttachmentMessage = w.Elm["web:GROUPING", prop: "@data-testid=uploaded-attachments-list-item", navig: "child2 last"].Find(-1);
		if (firstAttachmentMessage != null) {
			return nameAttachmentMessage = firstAttachmentMessage.Name;
		} else {
			return "";
		}
	}
	// поле кількість
	static void deltaNumberOfnumberWindow() {
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// поле кількість
		var numberOfnumberWindow = w.Elm["web:SPINBUTTON", prop: new("@data-testid=C", "@type=number")].Find();
		if (numberOfnumberWindow != null) {
			int counts = 1;
			numberOfnumberWindow.PostClick(scroll: 250);
			numberOfnumberWindow.SendKeys("Ctrl+A", "!" + counts);
		}
	}
	//
	static string dateTimeDeltaCombine(string clipData_time) {
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// дата
		var copyDeltaDate = w.Elm["web:TEXT", prop: "@data-testid=W"].Find(1);
		if (copyDeltaDate.Value.Length != 0) {
			clipData_time = copyDeltaDate.Value;
		} else {
			copyDeltaDate.PostClick(250);
			copyDeltaDate.SendKeys("!" + clipData_time);
		}
		clipData_time = clipData_time.Replace("/", ".");
		// час
		var copyDeltaTime = w.Elm["web:TEXT", "гг:хх", "@data-testid=W-time-input"].Find(1);
		return clipData_time += " " + copyDeltaTime.Value;
	}
	// достовірність
	static void deltaReliabilityWindow() {
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// достовірність
		var reliabilityWindow = w.Elm["web:RADIOBUTTON", "A", "@data-testid=reliability-key-A"].Find();
		if (reliabilityWindow != null) {
			reliabilityWindow.PostClick(scroll: 250);
		}
		var certaintyWindow = w.Elm["web:RADIOBUTTON", "2", "@data-testid=reliability-key-2"].Find();
		if (certaintyWindow != null) {
			certaintyWindow.PostClick(scroll: 250);
		};
	}
	// тип джерела
	static void deltaFlyeye() {
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// тип джерела
		string flyeye = "пові";
		var typeOfSourceWindow = w.Elm["web:GROUPING", prop: "@data-testid=AD"].Find();
		if (typeOfSourceWindow != null) {
			typeOfSourceWindow.PostClick(scroll: 250);
			typeOfSourceWindow.SendKeys("!" + flyeye, "Tab");
		}
	}
	static void commentDeltaAreaFill(string clipData_time, string commentDeltaFill, string nameAttachmentMessage) {
		string commentNew = string.Empty;
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		
		// поле - коментар
		var deltaCommentWindow = w.Elm["web:TEXT", prop: "@data-testid=comment-editing__textarea"].Find(1);
		deltaCommentWindow.ScrollTo();
		wait.ms(250);
		deltaCommentWindow.PostClick(scroll: 250);
		
		// формую комент
		commentNew += clipData_time + " - " + commentDeltaFill + " " + nameAttachmentMessage;
		deltaCommentWindow.SendKeys("Ctrl+A", "!" + commentNew);
		// Assept кнопка коменту
		var asseptButtonComment = w.Elm["web:BUTTON", prop: "@data-testid=comment-editing__button-save"].Find(1);
		asseptButtonComment.PostClick(scroll: 250);
	}
	static void goToMain() {
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
		// повернення на основне вікно
		wait.ms(250);
		var mainFilds = w.Elm["web:GROUPING", prop: new("desc=Основні поля", "@title=Основні поля")].Find(1);
		mainFilds.PostClick();
		wait.ms(250);
	}
};