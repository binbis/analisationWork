/* 04,08,2024_v1.5
* в тестовому режимі
- міна, залежно від статусу заповнюється та оновлюється(назва, дата\час, боєздатність, коментар)
- укриття, залежно від заповнення, заповнюється та оновлюється "без ід"(назва,  дата\час, ідентифікатор, коментар)[якщо виявлено, бере комент, ураження-знищення бере статус, ]
- техніка, ніби як працює, теба тестувати в бою

- САУ
- Антена
- Бліндаж
- Т. вильоту дронів
- Скупчення ОС
- Вор. розвід. крило / Вор. FPV-крило
ще є така помилка для массива (вона фантомна)
Index was outside the bounds of the array. це от тут parts[]
*/

namespace CSLight {
	class Program {
		static void Main() {
			opt.key.KeySpeed = 30;
			opt.key.TextSpeed = 20;
			//виділяємо весь рядок
			keys.send("Shift+Space*2");
			//копіюємо код
			keys.send("Ctrl+C");
			// зчитуємо буфер обміну
			string clipboardData = clipboard.copy();
			
			// Розділяємо рядок на частини
			string[] parts = clipboardData.Split('\t');
			
			// Присвоюємо змінним відповідні значення
			string dateJbd = parts[0]; // 27.07.2024
			string timeJbd = parts[1]; //00:40
			string commentJbd = parts[2].Replace("\n"," ").ToLower(); //коментар (для ідентифікації скоріш за все)
			string crewTeamJbd = parts[4].Replace("\n\t"," "); // R-18-1 (Мавка)
			string whatDidJbd = parts[5]; // Мінування (можливо його видалю)
			string targetClassJbd = parts[7]; // Міна/Вантажівка/Військ. баггі/Скупчення ОС/Укриття
			string idTargetJbd = parts[9]; // Міна 270724043
			// Встановлено/Уражено/Промах/Авар. скид/Повторно уражено
			string establishedJbd = parts[24];
			//Console.WriteLine(establishedJbd);
			//Console.WriteLine(targetClassJbd);
			//звести дату до формату дельти
			string dateDeltaFormat = dateJbd.Replace('.','/');
			// для мін на 11 шар
			string targetMinePTM = "Міна";
			// уриття типу район зосередження на 01 нар
			string areaConcentration = "Укриття";
			// массив техніки на 04 або 06 шари
			string[] machineryArray = {
				"ББМ / МТ-ЛБ","Авто","Вантажівка","Танк","Гармата",
				"Мотоцикл","Військ. баггі","Гаубиця","РСЗВ","БТР",
				"БМП",
			};
			//. якщо ти техніка
			for (int i = 0; i < machineryArray.Length; i++) {
				if (targetClassJbd.Contains(machineryArray[i])) {
					deltaLayerWindow(machineryArray[i], commentJbd);
					deltaMarkName(machineryArray[i], dateJbd, establishedJbd, commentJbd);
					deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
					deltaNumberOfnumberWindow();
					deltaCombatCapabilityWindow(machineryArray[i], establishedJbd, commentJbd);
					deltaIdentificationWindow(machineryArray[i], establishedJbd);
					deltaReliabilityWindow();
					deltaFlyeye();
					deltaIdPurchaseText(idTargetJbd);
					deltaCommentContents(machineryArray[i], dateJbd, timeJbd, crewTeamJbd, establishedJbd, targetClassJbd, commentJbd);
				}
			}
			//..
			//. якщо ти міна
			if (targetClassJbd.Contains(targetMinePTM)) {
				deltaLayerWindow(targetMinePTM, commentJbd);
				deltaMarkName(targetMinePTM, dateJbd,establishedJbd,commentJbd);
				deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
				deltaNumberOfnumberWindow();
				deltaCombatCapabilityWindow(targetMinePTM, establishedJbd, commentJbd);
				deltaIdentificationWindow(targetMinePTM, establishedJbd);
				deltaReliabilityWindow();
				deltaFlyeye();
				deltaIdPurchaseText(idTargetJbd);
				deltaCommentContents(targetMinePTM, dateJbd, timeJbd, crewTeamJbd, establishedJbd, targetClassJbd, commentJbd);
			//..
			//. якщо ти укриття
			} else if (targetClassJbd.Contains(areaConcentration)) {
				deltaLayerWindow(areaConcentration,commentJbd);
				deltaMarkName(areaConcentration, dateJbd, establishedJbd, commentJbd);
				deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
				deltaIdentificationWindow(areaConcentration, establishedJbd);
				deltaReliabilityWindow();
				deltaFlyeye();
				deltaCommentContents(areaConcentration, dateJbd, timeJbd, crewTeamJbd, establishedJbd, targetClassJbd, commentJbd);
				//..
			}else {
				//Console.WriteLine("нічого спільного не зміг знайти");
			}
			
			// Знаходить та активує вікно якщо воно звернуте 
			//var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
		}
		static string datePlasFourteen(string date) {
			// Перетворюємо рядок дати у DateTime
			DateTime originalDate = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
			// Додаємо Х днів
			DateTime newDate = originalDate.AddDays(60);
			// Перетворюємо нову дату назад у рядок
			//string newDateString = newDate.ToString("dd.MM.yyyy");
			string newDateString = newDate.ToString("dd.MM.yyyy");
			return newDateString;
		}
		// поле шар
		static void deltaLayerWindow(string whoAreYou, string commentJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			// поле шар
			var layerWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-layer"].Find(3);
			wait.ms(50);
			layerWindow.ScrollTo();
			wait.ms(100);
			layerWindow.PostClick(2);
			wait.ms(100);
			if (whoAreYou.Contains("Міна")) {
				layerWindow.SendKeys("Ctrl+A","!11","Enter");
			} else if (whoAreYou.Contains("Укриття")) {
				layerWindow.SendKeys("Ctrl+A","!01","Enter");
			} else {
				if (commentJbd.Contains("в рус") || commentJbd.Contains("рух")) {
					layerWindow.SendKeys("Ctrl+A","!06","Enter");
				}else {
					layerWindow.SendKeys("Ctrl+A","!04","Enter");
				}
			}
			//layerWindow.SendKeys("Ctrl+A","!000","Enter");
		}
		// поле назва
		static void deltaMarkName(string whoAreYou, string dateJbd, string establishedJbd, string commentJbd){
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле назва
			string markName = string.Empty;
			if (establishedJbd.Contains("Знищ") || establishedJbd.Contains("знищ")) {
				if (whoAreYou.Contains("Укриття")) {
					markName = whoAreYou + " ОС (знищ.)";
				} else {
					markName = whoAreYou + " (знищ.)";
				}
			}else if (establishedJbd.Contains("Ураж") || establishedJbd.Contains("ураж")) {
				if (whoAreYou.Contains("Укриття")) {
					markName = whoAreYou + " ОС (ураж.)";
				} else {
					markName = whoAreYou + " (ураж.)";
				}
			}else if (whoAreYou.Contains("Міна")) {
				if (establishedJbd.Contains("Авар. скид") || establishedJbd.Contains("Розміновано")) {
					markName = "ПТМ-3 (" + dateJbd + ")";
				}else {
					markName = "ПТМ-3 до ("+ datePlasFourteen(dateJbd)+")";
				}
			}else {
				if (whoAreYou.Contains("Укриття")) {
					markName = whoAreYou + " ОС ";
				}else {
					markName = whoAreYou;
				}
			}
			
			if (commentJbd.Contains("в рус") || commentJbd.Contains("рух")) {
				markName = whoAreYou + " (в русі)";
			}else if (commentJbd.Contains("схов")) {
				markName = whoAreYou + " (схов.)";
			}else if (commentJbd.Contains("знищ")) {
				if (whoAreYou.Contains("Укриття")) {
					markName = whoAreYou + " ОС (знищ.)";
				} else {
					markName = whoAreYou + " (знищ.)";
				}
			}else if (commentJbd.Contains("ураж")) {
				if (whoAreYou.Contains("Укриття")) {
					markName = whoAreYou + " ОС (ураж.)";
				} else {
					markName = whoAreYou + " (ураж.)";
				}
			}
			var nameOfMarkWindow = w.Elm["web:TEXT", prop: new("@data-testid=T")].Find(3);
			nameOfMarkWindow.PostClick(2);
			wait.ms(100);
			nameOfMarkWindow.SendKeys("Ctrl+A","!"+markName);
		}
		// поле дата / час
		static void deltaDateLTimeWindow(string dateDeltaFormat, string timeJbd){
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле дата / час
			var dateDeltaWindow = w.Elm["web:TEXT", prop: "@data-testid=W"].Find(3);
			dateDeltaWindow.PostClick(2);
			wait.ms(100);
			dateDeltaWindow.SendKeys("Ctrl+A","!"+ dateDeltaFormat);
			wait.ms(100);
			var timeDeltaWindow = w.Elm["web:TEXT", prop: "@data-testid=W-time-input"].Find(1);
			timeDeltaWindow.PostClick(2);
			wait.ms(200);
			timeDeltaWindow.SendKeys("Ctrl+A", "!"+timeJbd, "Enter*2");
			wait.ms(100);
		}
		// поле кількість
		static void deltaNumberOfnumberWindow(){
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле кількість
			var numberOfnumberWindow = w.Elm["web:SPINBUTTON", prop: new("@data-testid=C", "@type=number")].Find(3);
			numberOfnumberWindow.PostClick(1);
			wait.ms(100);
			numberOfnumberWindow.SendKeys("Ctrl+A", "!1");
		}
		// поле боєздатність
		static void deltaCombatCapabilityWindow(string whoAreYou, string establishedJbd, string commentJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле боєздатність
			string fullaim = string.Empty;
			if (whoAreYou.Contains("Міна")) {
				if (establishedJbd.Contains("Авар. скид") || establishedJbd.Contains("Розміновано")) {
					fullaim = "небо";
				} else if (establishedJbd.Contains("Встановлено")) {
					fullaim = "повніс";
				} else {
					fullaim = "част";
				}
			}else if (establishedJbd.Contains("Ураж")) {
				fullaim = "част";
			}else if (establishedJbd.Contains("Знищ")) {
				fullaim = "небо";
			}else if (establishedJbd.Contains("Виявлено")) {
				if (commentJbd.Contains("Ураж") || commentJbd.Contains("ураж")) {
					fullaim = "част";
				}
				if (commentJbd.Contains("Знищ") || commentJbd.Contains("знищ")) {
					fullaim = "небо";
				}else {
					fullaim = "повніс";
				}
			}else {
				fullaim = "повніс";
			}
			var combatCapabilityWindow = w.Elm["web:GROUPING", prop: "@data-testid=operational-condition-select"].Find(3);
			combatCapabilityWindow.PostClick(2);
			wait.ms(100);
			combatCapabilityWindow.SendKeys("Ctrl+A","!"+fullaim, "Enter");
		}
		// ідетнифікація
		static void deltaIdentificationWindow(string whoAreYou, string establishedJbd){
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// ідетнифікація
			string friendly = string.Empty;
			if (whoAreYou.Contains("Міна")) {
				friendly = "дружній";
			} else if (whoAreYou.Contains("Укриття")) {
				if (establishedJbd.Contains("Знищ")) {
					friendly = "відом";
				} else {
					friendly = "воро";
				}
			}else {
				friendly = "воро";
			};
			var identificationWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-HO"].Find(3);
			identificationWindow.PostClick(1);
			wait.ms(100);
			identificationWindow.SendKeys("Ctrl+A", "!"+friendly, "Enter");
		}
		// достовірність
		static void deltaReliabilityWindow(){
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// достовірність
			var reliabilityWindow = w.Elm["web:RADIOBUTTON", "A", "@data-testid=reliability-key-A"].Find(3);
			reliabilityWindow.PostClick(1);
			wait.ms(100);
			var certaintyWindow = w.Elm["web:RADIOBUTTON", "2", "@data-testid=reliability-key-2"].Find(3);
			certaintyWindow.PostClick(1);
		}
		// тип джерела
		static void deltaFlyeye(){
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// тип джерела
			string flyeye = "повітр";
			var typeOfSourceWindow = w.Elm["web:GROUPING", prop: "@data-testid=AD"].Find(100);
			typeOfSourceWindow.ScrollTo();
			wait.ms(500);
			typeOfSourceWindow.Select();
			wait.ms(100);
			typeOfSourceWindow.PostClick(2);
			wait.ms(100);
			typeOfSourceWindow.SendKeys("Ctrl+A", "!"+flyeye, "Enter");
		}
		// завйваження штабу ід
		static void deltaIdPurchaseText(string idTargetJbd){
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// завйваження штабу ід
			string idPurchaseText = idTargetJbd;
			var idPurchaseWindow = w.Elm["web:TEXT", prop: "@data-testid=G", flags: EFFlags.HiddenToo].Find(1);
			idPurchaseWindow.PostClick();
			wait.ms(100);
			idPurchaseWindow.SendKeys("Ctrl+A", "!"+idPurchaseText, "Enter");
		}
		// коментар
		static void deltaCommentContents(string whoAreYou, string dateJbd, string timeJbd, string crewTeamJbd, string establishedJbd, string targetClassJbd, string commentJbd){
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			string commentContents = dateJbd + " " + timeJbd + " - ";
			// коментар
			if (crewTeamJbd.Contains("FPV")) {
				commentContents += establishedJbd.ToLower() + " за допомогою " + crewTeamJbd;
			} else if (establishedJbd.Contains("Виявлено")) {
				commentContents += commentJbd + ", спостерігали з " + crewTeamJbd;
			} else if (establishedJbd.Contains("Підтверджено")) {
				commentContents += "дорозвідка, спостерігали з " + crewTeamJbd;
			}else if (whoAreYou.Contains("Міна")) {
				if (establishedJbd.Contains("Авар. скид") || establishedJbd.Contains("Подавлено")) {
					commentContents += "аварійно сикнуто з ударного коптера " + crewTeamJbd;
				} else if (establishedJbd.Contains("Розміновано")) {
					commentContents += "розміновано спостерігали з " + crewTeamJbd;
				} else {
					commentContents += establishedJbd.ToLower() + " за допомогою ударного коптера " + crewTeamJbd;
				}
			} else {
				commentContents += establishedJbd.ToLower() + " за допомогою ударного коптера " + crewTeamJbd;
			}
			
			var commentWindow = w.Elm["web:TEXT", prop: "@data-testid=comment-editing__textarea"].Find(1);
			commentWindow.PostClick();
			commentWindow.SendKeys("Ctrl+A", "!"+commentContents);
			
			// кнопка коментаря
			var commentAsseptButton = w.Elm["web:BUTTON", prop: "@data-testid=comment-editing__button-save"].Find(1);
			wait.ms(200);
			commentAsseptButton.ScrollTo();
			wait.ms(200);
			commentAsseptButton.PostClick(2);
		}
	}
}
