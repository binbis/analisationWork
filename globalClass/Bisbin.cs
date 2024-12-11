/*/ 
nuget -\CoordinateSharp;

 c .\VariableHolder.cs;
 c .\ElementNavigator.cs;
 c .\StringReducer.cs;
 c .\pourMark\PourMark.cs;

 /*/
using CoordinateSharp;
/// <summary>
/// основний клас який має доступ до усього
/// </summary>
public class Bisbin {
	/// <summary>
	/// різноманітні змінні
	/// </summary>
	public VariableHolder VariableHolder = new VariableHolder();
	/// <summary>
	/// вікна й елементи з цих вікон
	/// </summary>
	public ElementNavigator ElementNavigator = new ElementNavigator();
	/// <summary>
	/// функції з різними варіантами обрізання рядків
	/// </summary>
	public StringReducer StringReducer = new StringReducer();
	/// <summary>
	/// Поля вікна редагування мітки
	/// </summary>
	public PourMark PourMark = new PourMark();
	
	// додає вказану кількість днів та повертає дату
	public static string datePlasDays(string date, int count) {
		// Перетворюємо рядок дати у DateTime
		DateTime originalDate = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
		// Додаємо Х днів
		DateTime newDate = originalDate.AddDays(count);
		// Перетворюємо нову дату назад у рядок
		string newDateString = newDate.ToString("dd.MM.yyyy");
		return newDateString;
	}
	
	//  тип джерела
	public static void flyEye() {
		// основна вкладка
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// тип джерела поле
		string flyeye = "пові";
		var typeOfSourceWindow = w.Elm["STATICTEXT", "Тип джерела", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1);
		if (typeOfSourceWindow != null) {
			typeOfSourceWindow.PostClick(scroll: 250);
			keys.sendL("Ctrl+A", "!" + flyeye, "Enter");
		}
	}
	// достовірність
	public static void reliabilityWindow() {
		// основна вкладка
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// достовірність поле
		var reliabilityWindow = w.Elm["RADIOBUTTON", "A", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);
		if (reliabilityWindow != null) {
			reliabilityWindow.PostClick(scroll: 250);
		}
		wait.ms(500);
		var certaintyWindow = w.Elm["RADIOBUTTON", "2", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);
		if (certaintyWindow != null) {
			certaintyWindow.PostClick(scroll: 250);
		}
	}
	// координати в wgs84
	public static (double Latitude, double Longitude) ConvertMGRSToWGS84(string mgrs) {
		// Створення координати з MGRS
		Coordinate coordinate = Coordinate.Parse(mgrs);
		
		// Отримання широти та довготи з об'єкта координати
		return (coordinate.Longitude.ToDouble(), coordinate.Latitude.ToDouble());
	}
	// формування коментару на основі данних
	public static string createComment(string targetClassJbd, string dateJbd, string timeJbd, string crewTeamJbd, string establishedJbd, string commentJbd, string mgrsCoords) {
		// основна вкладка
		string commentContents = $"{dateJbd} {timeJbd} - ";
		// коментар
		switch (targetClassJbd) {
		//. Міна
		case "Міна":
			if (establishedJbd.Contains("Авар. скид") || establishedJbd.Contains("Подавлено")) {
				commentContents += $"аварійно сикнуто з ударного коптера {crewTeamJbd}";
			} else if (establishedJbd.Contains("Розміновано")) {
				commentContents += $"розміновано, спостерігали з {crewTeamJbd}";
			} else if (establishedJbd.Contains("Спростовано")) {
				commentContents += $"міна на місці, сліди розриву відсутні, спостерігали з {crewTeamJbd}";
			} else if (establishedJbd.Contains("Тільки розрив")) {
				commentContents += $"тільки розрив, спостерігали з {crewTeamJbd}";
			} else if (establishedJbd.Contains("Нерозрив")) {
				commentContents += $"рух без підриву, спостерігали з {crewTeamJbd}";
			} else if (establishedJbd.Contains("Підтв. ураж.")) {
				commentContents += $"{commentJbd}, спостерігали з {crewTeamJbd}";
			} else {
				commentContents += $"встановлено за допомогою ударного коптера {crewTeamJbd}";
			}
			break;
		//..
		//. Укриття
		case "Укриття":
			commentContents += $"(  {mgrsCoords}  ) - ";
			if (establishedJbd.ToLower().Contains("знищ") || establishedJbd.ToLower().Contains("ураж")) {
				commentContents += $"{establishedJbd.ToLower()} за допомогою {crewTeamJbd}";
			} else if (establishedJbd.Contains("Підтверджено") || establishedJbd.Contains("Спростовано")) {
				if (commentJbd.ToLower().Contains("знищ") || commentJbd.ToLower().Contains("ураж")) {
					commentContents += $"{commentJbd}, спостерігав {crewTeamJbd}";
				} else {
					commentContents += $"{commentJbd}, спостерігав {crewTeamJbd}";
				}
			} else if (establishedJbd.Contains("Виявлено")) {
				if (commentJbd.ToLower().Contains("знищ")) {
					commentContents += $"{establishedJbd.ToLower()} знищ. {targetClassJbd.ToLower()}, спостерігав {crewTeamJbd}";
				} else if (commentJbd.Contains("ураж")) {
					commentContents += $"{establishedJbd.ToLower()} ураж. {targetClassJbd.ToLower()}, спостерігав {crewTeamJbd}";
				} else {
					commentContents += $"{commentJbd} , спостерігав {crewTeamJbd}";
				}
			} else if (establishedJbd.Contains("Не зрозуміло")) {
				commentContents += $"спроба ураження, {crewTeamJbd}";
			}
			break;
		//..
		default:
			//.
			if (establishedJbd.ToLower().Contains("знищ") || establishedJbd.ToLower().Contains("ураж")) {
				commentContents += $"{establishedJbd.ToLower()} за допомогою {crewTeamJbd}";
			} else if (establishedJbd.Contains("Виявлено")) {
				commentContents += $"{commentJbd} , спостерігав {crewTeamJbd}";
			} else if (establishedJbd.Contains("Не зрозуміло")) {
				commentContents += $"спроба ураження, {crewTeamJbd}";
			} else {
				commentContents += $"{commentJbd}, {establishedJbd.ToLower()} за допомогою {crewTeamJbd}";
			}
			//..
			break;
		}
		return commentContents;
		
	}
	// формування імені
	public static string createMineName(string nameOfBch, string targetClassJbd, string dateJbd, string establishedJbd, string commentJbd, string twoHundredth, string threeHundredth) {
		Bisbin instance = new Bisbin();
		// поле назва
		string markName = string.Empty;
		if (establishedJbd.Contains("Авар. скид")) {
			markName = $"{nameOfBch} ({dateJbd})";
		} else {
			if (instance.VariableHolder.bchHeavyMines.Contains(nameOfBch)) {
				markName = $"{nameOfBch} до ({Bisbin.datePlasDays(dateJbd, 90)})";
			} else if (instance.VariableHolder.bchTropsMines.Contains(nameOfBch)) {
				markName = $"{nameOfBch} до ({Bisbin.datePlasDays(dateJbd, 8)})";
			} else {
				markName = $"{nameOfBch} до ()";
			}
		}
		return markName;
		
	}
}
