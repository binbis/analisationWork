/*/ c \analisationWork\globalClass\Bisbin.cs; /*/

/* 30.10.2024_0.0.3
* на основі обраної назви виставляється боєздатність
* якщо дата відсутня ставить з прикріплення(якщо відсутнє поточну) 
* якщо є прикріплення додасть перше ім'я в кінець коментару
*/
using System.Windows;
using System.Windows.Controls;

class Program {
	static void Main() {
		opt.key.KeySpeed = 45;
		opt.key.TextSpeed = 35;
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
			"Гаубиця (схов.)","Гаубиця (знищ.)","Гаубиця (ураж.)","Гаубиця ? (ураж.)","Гаубиця ? (знищ.)",
			"ББМ (схов.)","ББМ ? (знищ.)","ББМ ? (ураж.)","ББМ (знищ.)","ББМ (ураж.)",
			"Танк (схов.)","Танк ? (знищ.)","Танк (знищ.)","Танк (ураж.)",
			"Урал (схов.)","Урал (ураж.)","Урал (знищ.)",
			"МТ-ЛБ (схов.)","МТ-ЛБ (ураж.)","МТ-ЛБ (знищ.)",
			"БМП (схов.)","БМП (ураж.)","БМП (знищ.)","БМП ? (знищ.)","БМП ? (ураж.)",
			"БТР (схов.)","БТР (ураж.)","БТР (знищ.)","БТР ? (знищ.)","БТР ? (ураж.)",
			"ВАТ (схов.)","ВАТ (знищ.)","ВАТ (ураж.)",
			"Буханка (схов.)","Буханка (знищ.)","Буханка (ураж.)",
			"Укриття (схов.)","Укриття (ураж.)","Укриття (знищ.)",
			"Склад майна","Склад майна (ураж.)","Склад майна (знищ.)",
			"Склад БК","Склад БК (ураж.)","Склад БК (знищ.)",
			"Склад ПММ","Склад ПММ (ураж.)","Склад ПММ (знищ.)",
			"Польовий склад майна","Польовий склад майна (знищ.)",
			"Польовий склад БК","Польовий склад БК (знищ.)",
			"Польовий склад ПММ",
			"Мережеве обладнання",
			"Камера","Камера (знищ.)",
			"Антена","Антена (знищ.)",
			"РЕБ (окопні)","РЕБ (окопні) (ураж.)","РЕБ (окопні) (знищ.)",
			"Міна","Загородження",
			"Бліндаж","Бліндаж (ураж.)","Бліндаж (знищ.)",
			"Т. вильоту дронів","Т. вильоту дронів (ураж.)","Т. вильоту дронів (знищ.)",
			"Скупчення ОС",
			"Міномет","Міномет (ураж.)","Міномет (знищ.)",
		};
		
		var b = new wpfBuilder("Window").WinSize(400);
		b.R.Add("Назва", out ComboBox recomendName).Items(examlpeNames).Editable().Select(9);
		b.R.Add("Комент", out ComboBox recomendComment).Items(examlpeComment).Select(3);
		b.R.AddOkCancel();
		b.Window.Topmost = true;
		b.End();
		// show dialog. Exit if closed not with the OK button.
		if (!b.ShowDialog()) return;
		
		string nameDeltaFill = recomendName.Text;
		string commentDeltaFill = recomendComment.Text;
		string nameAttachmentMessage = string.Empty;
		string dateTimeAttachmentMessage = string.Empty;
		string clipData_time = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
		clipboard.clear();
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
		wait.ms(900);
		nameAttachmentMessage = getNameWithAttachments();
		dateTimeAttachmentMessage = getDateTimeWithAttachments();
		wait.ms(900);
		Bisbin.goToMainField();
		wait.ms(900);
		deltaLayerWindow(nameDeltaFill);
		wait.ms(900);
		deltaMarkName(nameDeltaFill);
		wait.ms(900);
		clipData_time = dateTimeDeltaCombine(clipData_time, dateTimeAttachmentMessage);
		wait.ms(900);
		deltaNumberOfnumberWindow();
		wait.ms(900);
		deltaCombatCapabilityWindow(nameDeltaFill);
		wait.ms(900);
		Bisbin.reliabilityWindow();
		wait.ms(900);
		Bisbin.flyEye();
		wait.ms(900);
		commentDeltaAreaFill(clipData_time, commentDeltaFill, nameAttachmentMessage);
		
	}
	static void deltaLayerWindow(string nameDeltaFill) {
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// (01) постійні схов. і укриття
		string[] permanentStorage = {
					"Укриття","Склад майна","Склад БК","Склад ПММ",
					"Польовий склад майна","Польовий склад БК","Польовий склад ПММ"
				};
		// (02) антени, камери...
		string[] antennaCamera = {
					"Мережеве обладнання","Камера","Антена","РЕБ (окопні)"
				};
		// поле шар
		var layerWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-layer"].Find(3);
		if (layerWindow != null) {
			layerWindow.PostClick(scroll: 250);
			//. перевірка, запис
			for (int i = 0; i < permanentStorage.Length; i++) {
				if (permanentStorage[i].Contains(nameDeltaFill)) {
					layerWindow.SendKeys("Ctrl+A", "!Пост", "Enter");
					return;
				}
			}
			for (int i = 0; i < antennaCamera.Length; i++) {
				if (antennaCamera[i].Contains(nameDeltaFill)) {
					layerWindow.SendKeys("Ctrl+A", "!антени", "Enter");
					return;
				}
			}
			switch (nameDeltaFill) {
			case "Міна":
				layerWindow.SendKeys("Ctrl+A", "!11", "Enter");
				break;
			case "Загородження":
				layerWindow.SendKeys("Ctrl+A", "!11", "Enter");
				break;
			case "Бліндаж":
				layerWindow.SendKeys("Ctrl+A", "!07", "Enter");
				break;
			case "Т. вильоту дронів":
				layerWindow.SendKeys("Ctrl+A", "!08", "Enter");
				break;
			case "Скупчення ОС":
				layerWindow.SendKeys("Ctrl+A", "!10", "Enter");
				break;
			case "Міномет":
				layerWindow.SendKeys("Ctrl+A", "!09", "Enter");
				break;
			default:
				if (nameDeltaFill.ToLower().Contains("рус")) {
					layerWindow.SendKeys("Ctrl+A", "!06", "Enter");
				} else {
					layerWindow.SendKeys("Ctrl+A", "!04", "Enter");
				}
				break;
			}
		}
		//..
	}
	// поле назва
	static void deltaMarkName(string nameDeltaFill) {
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// поле назва
		var nameOfMarkWindow = w.Elm["web:TEXT", prop: new("@data-testid=T")].Find();
		if (nameOfMarkWindow != null) {
			nameOfMarkWindow.PostClick(scroll: 250);
			keys.sendL("Ctrl+A", "!" + nameDeltaFill);
		}
	}
	// повертає ім'я з прикріплення якщо є
	static string getNameWithAttachments() {
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// вкладка - прикріплення
		var deltaAttachmentWindow = w.Elm["web:GROUPING", prop: new("desc=Прикріплення", "@title=Прикріплення")].Find(1);
		deltaAttachmentWindow.PostClick(scroll: 250);
		// знаходжу елемент, відповідний за назву того хто прикріпив прикруплення
		var lastAttachmentMessage = w.Elm["web:GROUPING", prop: "@data-testid=zoneContainer", navig: "parent last child2 last"].Find(-1);
		if (lastAttachmentMessage != null) {
			return lastAttachmentMessage.Name;
		} else {
			return "";
		}
	}
	// повертає дату та час з прикріплення якщо є
	static string getDateTimeWithAttachments() {
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// знаходжу елемент, відповідний за дату припріплення
		var deltaDateTimeAttachmentWindow = w.Elm["web:GROUPING", prop: "@data-testid=zoneContainer", navig: "parent last child2 child6"].Find(-1);
		if (deltaDateTimeAttachmentWindow != null) {
			return deltaDateTimeAttachmentWindow.Name.Replace(" | ", "");
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
			keys.sendL("Ctrl+A", "!" + counts);
		}
	}
	// поле боєздатність
	static void deltaCombatCapabilityWindow(string nameDeltaFill) {
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		string fullaim = string.Empty;
		// поле боєздатність
		var combatCapabilityWindow = w.Elm["web:GROUPING", prop: "@data-testid=operational-condition-select"].Find();
		if (combatCapabilityWindow != null) {
			if (nameDeltaFill.ToLower().Contains("знищ")) {
				fullaim = "небо";
			} else if (nameDeltaFill.ToLower().Contains("ураж")) {
				fullaim = "част";
			} else {
				fullaim = "повніс";
			}
			combatCapabilityWindow.PostClick(scroll: 250);
			keys.sendL("Ctrl+A", "!" + fullaim, "Enter");
		}
	}
	// вписує сьоднішню дату час або бере ту яка вже є 
	static string dateTimeDeltaCombine(string clipData_time, string dateTimeAttachmentMessage) {
		string dateAttachment = string.Empty;
		string timeAttachment = string.Empty;
		string dateLineWtime = clipData_time.Split(" ")[0];
		string timeLineWtime = clipData_time.Split(" ")[1];
		if (dateTimeAttachmentMessage != "") {
			// Перетворюємо рядок на об'єкт DateTime
			DateTime parsedDate = DateTime.ParseExact(dateTimeAttachmentMessage.Split(" ")[0], "dd/MM/yy", CultureInfo.InvariantCulture);
			// Форматуємо дату у потрібний формат
			dateAttachment = parsedDate.ToString("dd/MM/yyyy");
			timeAttachment = dateTimeAttachmentMessage.Split(" ")[1];
		}
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// дата
		var copyDeltaDate = w.Elm["web:TEXT", prop: "@data-testid=W"].Find(-1);
		if (copyDeltaDate.Value.Length != 0) {
			dateLineWtime = copyDeltaDate.Value;
		} else {
			if (dateAttachment.Length > 0) {
				copyDeltaDate.PostClick(scroll: 250);
				wait.ms(800);
				keys.sendL("Ctrl+A", "!" + dateAttachment, "Enter");
				dateLineWtime = dateAttachment;
			} else {
				copyDeltaDate.PostClick(scroll: 250);
				wait.ms(800);
				keys.sendL("Ctrl+A", "!" + dateLineWtime, "Enter");
			}
		}
		wait.ms(500);
		// час
		var copyDeltaTime = w.Elm["web:TEXT", prop: "@data-testid=W-time-input"].Find(-1);
		if (copyDeltaTime.Value != "00:00") {
			timeLineWtime = copyDeltaTime.Value;
		} else {
			if (timeAttachment.Length > 0) {
				copyDeltaTime.PostClick(scroll: 250);
				wait.ms(800);
				keys.sendL("Ctrl+A", "!" + timeAttachment, "Enter");
				timeLineWtime = timeAttachment;
			} else {
				copyDeltaTime.PostClick(scroll: 250);
				wait.ms(800);
				keys.sendL("Ctrl+A", "!" + timeLineWtime, "Enter");
			}
		}
		return $"{dateLineWtime.Replace("/", ".")}  {timeLineWtime}";
	}
	// достовірність
	static void deltaReliabilityWindow() {
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// достовірність
		var reliabilityWindow = w.Elm["web:RADIOBUTTON", "A", "@data-testid=reliability-key-A"].Find();
		if (reliabilityWindow != null) {
			reliabilityWindow.PostClick(scroll: 250);
		}
		wait.ms(400);
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
			keys.sendL("!" + flyeye, "Tab");
		}
	}
	// поле коментар
	static void commentDeltaAreaFill(string clipData_time, string commentDeltaFill, string nameAttachmentMessage) {
		// формую комент
		string commentNew = string.Empty;
		commentNew += $"{clipData_time} -{commentDeltaFill} {nameAttachmentMessage}";
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// поле - коментар
		var deltaCommentWindow = w.Elm["web:TEXT", prop: "@data-testid=comment-editing__textarea"].Find(1);
		deltaCommentWindow.PostClick(scroll: 250);
		keys.sendL("Ctrl+A", "!" + commentNew);
	}
	static void goToMain() {
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
		// повернення на основне вікно
		wait.ms(900);
		var mainFilds = w.Elm["web:GROUPING", prop: new("desc=Основні поля", "@title=Основні поля")].Find(1);
		mainFilds.PostClick(scroll:250);
		wait.ms(900);
	}
};
