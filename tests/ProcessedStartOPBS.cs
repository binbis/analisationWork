/*12.09.2024_0.0.1*/
using System.Windows;
using System.Windows.Controls;

class Program {
	static void Main() {
		opt.key.KeySpeed = 25;
		opt.key.TextSpeed = 20;
		string[] examlpeComment = {
			" спостерігали постріл артилерії. Спостерігали з дрону",
			" спостерігали постріл артилерії. Спостерігали з крила",
			" спостерігали з дрону",
			" спостерігали з крила",
			" виявлено за допомогою супутникових знімків",
			" за оперативними даними",
			" уражено за допомогою FPV. Спостерігали з крила",
			" уражено за допомогою FPV. Спостерігали з дрону",
			" знищено за допомогою FPV. Спостерігали з дрону",
			" знищено ударним коптером. Спостерігали з дрону",
			" уражено ударним коптером. Спостерігали з дрону",
			" по гарматі відпрацювала артилерія. Стан невідомий. Спостерігали з крила",
			" Desertcross",
			" уражений скидом з мавіка. Спостерігали з дрону",
			" знищений скидом з мавіка. Спостерігали з дрону",
			" укриття для техніки. Спостерігали з дрону",
			" укриття для техніки. Спостерігали з крила",
		};
		string[] examlpeNames = {
			"Пікап (схов.)",
			"Позашляховик (схов.)",
			"САУ (схов.)",
			"Гаубиця (схов.)",
			"Гаубиця (знищ.)",
			"Гаубиця (ураж.)",
			"Гаубиця ? (ураж.)",
			"Гаубиця ? (знищ.)",
			"ББМ (схов.)",
			"ББМ ? (знищ.)",
			"ББМ ? (ураж.)",
			"ББМ (знищ.)",
			"ББМ (ураж.)",
			"Танк ? (знищ.)",
			"Танк (знищ.)",
			"Танк (ураж.)",
			"Урал (ураж.)",
			"Урал (знищ.)",
			"МТ-ЛБ (ураж.)",
			"МТ-ЛБ (знищ.)",
			"БМП (ураж.)",
			"БМП (знищ.)",
			"БМП ? (знищ.)",
			"БМП ? (ураж.)",
			"БТР (ураж.)",
			"БТР (знищ.)",
			"БТР ? (знищ.)",
			"БТР ? (ураж.)",
			"ВАТ (знищ.)",
			"ВАТ (ураж.)",
			"Буханка (знищ.)",
			"Буханка (ураж.)",
		};
		
		var b = new wpfBuilder("Window").WinSize(400);
		b.R.Add("Назва", out ComboBox recomendName).Items(examlpeNames).Select(9);
		b.R.Add("Комент", out ComboBox recomendComment).Items(examlpeComment).Select(3);
		b.R.AddOkCancel();
		b.Window.Topmost = true;
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
		clipboard.clear();
	}
	// поле назва
	static void deltaMarkName(string nameDeltaFill) {
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// поле назва
		var nameOfMarkWindow = w.Elm["web:TEXT", prop: new("@data-testid=T")].Find();
		if (nameOfMarkWindow != null) {
			nameOfMarkWindow.PostClick(scroll: 250);
			nameOfMarkWindow.SendKeys("Ctrl+A", "!" + nameDeltaFill);
		}
	}
	// повертає ім'я з прикріплення якщо вони
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
	// повертає сьоднішню або ту яка вже є дату
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
	// поле коментар
	static void commentDeltaAreaFill(string clipData_time, string commentDeltaFill, string nameAttachmentMessage) {
		string commentNew = string.Empty;
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// поле - коментар
		var deltaCommentWindow = w.Elm["web:TEXT", prop: "@data-testid=comment-editing__textarea"].Find(1);
		deltaCommentWindow.PostClick(scroll: 250);
		
		// формую комент
		commentNew += $"{clipData_time} - {commentDeltaFill} - {nameAttachmentMessage}";
		deltaCommentWindow.SendKeys("Ctrl+A", "!" + commentNew);
		// кнопка коментаря
		var commentAsseptButton = w.Elm["web:BUTTON", prop: "@data-testid=comment-editing__button-save"].Find(1);
		commentAsseptButton.PostClick(scroll: 250);
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