/*/ nuget -\CoordinateSharp; /*/
using CoordinateSharp;

public class Bisbin {
	
	//public string states = "Розміновано Підтв. ураж. Тільки розрив";
	
	// додає вказану кількість днів до дати
	public static string datePlasDays(string date, int count) {
		// Перетворюємо рядок дати у DateTime
		DateTime originalDate = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
		// Додаємо Х днів
		DateTime newDate = originalDate.AddDays(count);
		// Перетворюємо нову дату назад у рядок
		string newDateString = newDate.ToString("dd.MM.yyyy");
		return newDateString;
	}
	// обрізати рядок до 19 символів починаючи з початку
	public static string TrimNTwonyString(string str, int maxLength) {
		if (str.Length > maxLength) {
			return str.Substring(str.Length - maxLength);
		}
		return str;
	}
	// обрізати рядок, усе після крапки
	public static string TrimAfterDot(string str) {
		int dotIndex = str.IndexOf('.');
		if (dotIndex != -1) {
			return str.Substring(0, dotIndex);
		}
		return str;
	}
	// обрізати назви екіпажів
	public static string GetCutsEcipash(string str) {
		int index = str.LastIndexOf(')');
		if (index != -1) {
			return str.Substring(0, index + 1);
		}
		return str;
	}
	// додавання типу до бортів
	public static string addTypeForBoard(string str) {
		int index = str.LastIndexOf('(');
		if (index != -1) {
			return str.Substring(0, index + 1) + "FPV f7)";
		}
		return str;
	}
	// перша вкладка (Основні поля) мітки
	public static void goToMainField() {
		// основне вікно
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
		wait.ms(875);
		var mainFilds = w.Elm["web:GROUPING", prop: "desc=Основні поля"].Find(1);/*image:WkJNGyEEAOR/xuU/tEt0uiTwdai7wSMi3Nbct1CWAJvt+G8yhuF0NgwTCbqbJJRIEIYJaESziE7mmiTYfDItfh4auiWBDx1YAwHHVcfsT2WSCuCs6ID7xQp8HG1A1GWFy3oCXnfm4f/0GJ7WevB9sgs3IzkYCZZs6ckgLCVyYUkZFVdMVbGU2mRxfkMzaLqJylmcAUvD/htb0dp2uv7jIFVc1sI2pCAD*/
		mainFilds.PostClick();
	}
	// друга вкладка (Додаткові поля) мітки
	public static void goToAdditionalField() {
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		wait.ms(875);
		var additionalButton = w.Elm["web:GROUPING", prop: "desc=Додаткові поля"].Find(1);/*image:WkJNGxkEAORkbB1iobhERiaJaBKrYtW0M9YZmeYdQiGpvajZjt/GMNzEws67bBAmlEiwLCyxiWOAFrgNGT5eEUE9NuQmKhpsfiCGDMyOgOVMI8xfBuuMGi69PHzMB7AruOF/tYDXsAvHThy+lyO49+tQJmEXFYg8AaGUndGuBbwmEfVGhPXiTSoRxU8jCsdYoGZwAxsXe+GFtowCZXUrWorFYtEfvwoYpVKpkYAA*/
		additionalButton.PostClick();
	}
	// третя вкладка (Географічне розташування) мітки
	public static void goToGeograficalPlace() {
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		wait.ms(875);
		var geografical = w.Elm["web:GROUPING", prop: "desc=Географічне розташування"].Find(1);/*image:WkJNGyEEAOR/xtkf2hKg5Ao5zt1V7HuLUF3bqd+FcgkwZOx48i6v4WEkQZhQIhGEw0QsocAt0I2xpyeDoOCD0+jhocEthSRSizUQMFzpmP6r1qukcFaywv2kCiG7CT6O1nBZi8LTsgvfJzu4GeXh//QYXrdjqFBlCuy171BBdTKuJdsjWIod42J7N84pG5KjnrE4HynYxDI2IUxLtkmYHKwKcYzGm2IqsRTYJ02JGXysWVTicdyq/sM=*/
		geografical.PostClick();
		
	}
	// четверта вкладка (прикріплення) мітки
	public static void goToAttachmentFiles() {
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		wait.ms(875);
		var attachment = w.Elm["web:GROUPING", prop: "desc=Прикріплення"].Find(1);/*image:WkJNGyEEAOSPcf4b2hKg5AopQ3WuYtb13fKa+xbK0gdotuOfcxgOJzAdBEF33QWYSDAchiVGCVkIm2NYTAZBMfzT6OGhIS0BEqjBaggYrgqm/6rrVVI4K1nhflaFkN0Er8sJfB9XcFmLwtO8Cx/7BfyfDnAzykKRKkWBffKdfFI/xk7nswF2I5cXSsfdHi4gjdopdeEwU+pHXDqF2xMUqKQG9xtZTjQf6pSMq1P7eTI2qd2O32Np3D8E*/
		attachment.PostClick();
	}
	//  тип джерела
	public static void flyEye() {
		// основна вкладка
		var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
		// тип джерела поле
		string flyeye = "пові";
		var typeOfSourceWindow = w.Elm["web:GROUPING", prop: "@data-testid=AD"].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZJ1HadVdc0HlCGjjheOyxBBElfJIL+ZXeAUJZu8438kChk6vgiG5PM1vl3Q09jstnBA==*/
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
		var reliabilityWindow = w.Elm["RADIOBUTTON", "A", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
		if (reliabilityWindow != null) {
			reliabilityWindow.PostClick(scroll: 250);
		}
		wait.ms(500);
		var certaintyWindow = w.Elm["RADIOBUTTON", "2", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
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
		// поле назва
		string markName = string.Empty;
		string bchMines = "ПТМ-3 ТМ-62";
		if (establishedJbd.Contains("Авар. скид")) {
			markName = $"{nameOfBch} ({dateJbd})";
		} else {
			if (bchMines.Contains(nameOfBch)) {
				markName = $"{nameOfBch} до ({Bisbin.datePlasDays(dateJbd, 90)})";
			}else if (nameOfBch.Contains("ППМ")) {
				markName = $"{nameOfBch} до ({Bisbin.datePlasDays(dateJbd, 8)})";
			} else {
				markName = $"{nameOfBch} до ()";
			}
		}
		return markName;
		
	}
}
