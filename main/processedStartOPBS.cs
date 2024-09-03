string commentNew = string.Empty;
string nameAttachmentMessage = string.Empty;
string clipData_time = DateTime.Now.ToString("dd/MM/yyyy");

// основне вікно
var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
// вкладка - прикріплення
var deltaAttachmentWindow = w.Elm["web:GROUPING", prop: new("desc=Прикріплення", "@title=Прикріплення")].Find(1);
deltaAttachmentWindow.PostClick(scroll: 250);
// знаходжу елемент, відповідний за назву того хто прикріпив прикруплення
var firstAttachmentMessage = w.Elm["web:GROUPING", prop: "@data-testid=uploaded-attachments-list-item", navig: "child2 last"].Find(-1);
nameAttachmentMessage = firstAttachmentMessage.Name;
// Основні поля - вкладка
var deltaMainFildsWindow = w.Elm["web:GROUPING", prop: "@title=Основні поля"].Find(1);
deltaMainFildsWindow.PostClick(scroll: 250);
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
clipData_time += " " + copyDeltaTime.Value;
// н-д 
var button_reali_cert = w.Elm["web:BUTTON", "Надійність / достовірність", "@type=button"].Find(1);
button_reali_cert.ScrollTo();
wait.ms(400);

var reliabilityWindow = w.Elm["web:RADIOBUTTON", "A", "@data-testid=reliability-key-A"].Find();
// надійність 
if (reliabilityWindow != null) {
	reliabilityWindow.PostClick(scroll: 250);
}
wait.ms(200);
//достовірність
var certaintyWindow = w.Elm["web:RADIOBUTTON", "2", "@data-testid=reliability-key-2"].Find();
if (certaintyWindow != null) {
	certaintyWindow.PostClick(scroll: 250);
};

// поле - коментар
var deltaCommentWindow = w.Elm["web:TEXT", prop: "@data-testid=comment-editing__textarea"].Find(1);
deltaCommentWindow.ScrollTo();
wait.ms(250);
deltaCommentWindow.PostClick(scroll: 250);

// формую комент
commentNew += clipData_time + " - спостерігав з дрону " + nameAttachmentMessage;
deltaCommentWindow.SendKeys("Ctrl+A", "!" + commentNew);
// Assept кнопка коменту
var asseptButtonComment = w.Elm["web:BUTTON", prop: "@data-testid=comment-editing__button-save"].Find(1);
asseptButtonComment.PostClick(scroll: 250);
