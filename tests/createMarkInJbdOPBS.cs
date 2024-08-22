/* 22,08,2024_v1.7b
- міна, залежно від статусу заповнюється та оновлюється(назва, дата\час, боєздатність, коментар)
- укриття, залежно від заповнення, заповнюється та оновлюється "без ід"(назва,  дата\час, ідентифікатор, коментар)[якщо виявлено, бере комент, ураження-знищення бере статус, ]
- техніка, окремий масив зі таким самим інтерфейсом
- САУ, довелось зробити окремо, бо немає боєздатності, Скупчення ОС (200/300)
- Антена, запихнув як техніка, бо інтерфейс співпадає
- Т. вильоту дронів
- Бліндаж (підземне, наземне, укриття)
- Склад майна, Польовий склад БК, Склад БК (без кількості та розвідки)
- мережеве обладнання (генератор)
- камера відеоспостереження
- Міномет
- Вор. розвід. крило / Вор. FPV-крило / Розв. крило / Ударні крила
- Загородження (шипи)

- вонева позиція, ще невпевнений
*/

namespace CSLight {
	class Program {
		static void Main() {
			opt.key.KeySpeed = 35;
			opt.key.TextSpeed = 20;
			
			keys.send("Shift+Space*2");//виділяємо весь рядок
			wait.ms(100);
			keys.send("Ctrl+C"); //копіюємо код
			wait.ms(100);
			string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
			string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
			
			// Присвоюємо змінним відповідні значення
			string dateJbd = parts[0]; // 27.07.2024
			string timeJbd = parts[1]; //00:40
			string commentJbd = parts[2].Replace("\n", " "); //коментар (для ідентифікації скоріш за все)
			string crewTeamJbd = TrimAfterDot(parts[4].Replace("\n\t", " ")); // R-18-1 (Мавка)
			string whatDidJbd = parts[5]; // Мінування (можливо його видалю)
			string targetClassJbd = parts[7]; // Міна/Вантажівка/Військ. баггі/Скупчення ОС/Укриття
			string idTargetJbd = TrimString(parts[9], 19); // Міна 270724043
			string establishedJbd = parts[24]; // Встановлено/Уражено/Промах/Авар. скид/Повторно уражено
			string twoHundredth = parts[25]; // 200
			string threeHundredth = parts[26]; // 300
			
			//звести дату до формату дельти
			string dateDeltaFormat = dateJbd.Replace('.', '/');
			// мітка безпілотний літак на сховану техніку - на 04 шар
			string[] wings = { "Вор. розвід. крило", "Вор. FPV-крило", "Розв. крило", "Ударні крила" };
			// міна, уриття - типу район зосередження, Самохі́дна артилері́йська устано́вка;
			string targetMinePTM = "Міна", areaConcentration = "Укриття", dugout = "Бліндаж";
			// без хп (піхота)
			string[] infantry = { "САУ", "Скупчення ОС", "Камера" };
			// склади
			string[] storegas = { "Склад майна", "Польовий склад БК", "Склад БК" };
			//
			string flightOfDrones = "Т. вильоту дронів";
			string barrage = "Загородження";
			// массив техніки на 04 або 06 шари
			string[] machineryArray = {
				"ББМ / МТ-ЛБ","Авто","Вантажівка","Танк","Гармата",
				"Мотоцикл","Військ. баггі","Гаубиця","РСЗВ","БТР",
				"БМП","Антена","Мережеве обладнання", "Міномет"
			};
			//. якщо ти склад(без)
			for (int i = 0; i < storegas.Length; i++) {
				if (targetClassJbd.Contains(storegas[i])) {
					deltaLayerWindow(storegas[i], commentJbd);
					deltaMarkName(storegas[i], dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
					deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
					deltaCombatCapabilityWindow(storegas[i], establishedJbd, commentJbd);
					deltaIdentificationWindow(storegas[i], establishedJbd, commentJbd);
					deltaReliabilityWindow();
					deltaIdPurchaseText(idTargetJbd);
					deltaCommentContents(machineryArray[i], dateJbd, timeJbd, crewTeamJbd, establishedJbd, targetClassJbd, commentJbd);
				}
			}
			//..
			//. якщо ти техніка
			for (int i = 0; i < machineryArray.Length; i++) {
				if (targetClassJbd.Contains(machineryArray[i])) {
					deltaLayerWindow(machineryArray[i], commentJbd);
					deltaMarkName(machineryArray[i], dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
					deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
					deltaNumberOfnumberWindow();
					deltaCombatCapabilityWindow(machineryArray[i], establishedJbd, commentJbd);
					deltaIdentificationWindow(machineryArray[i], establishedJbd, commentJbd);
					deltaReliabilityWindow();
					deltaFlyeye();
					deltaIdPurchaseText(idTargetJbd);
					deltaCommentContents(machineryArray[i], dateJbd, timeJbd, crewTeamJbd, establishedJbd, targetClassJbd, commentJbd);
				}
			}
			//..
			//. якщо ти САУ або піхота
			for (int i = 0; i < infantry.Length; i++) {
				if (targetClassJbd.Contains(infantry[i])) {
					deltaLayerWindow(infantry[i], commentJbd);
					deltaMarkName(infantry[i], dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
					deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
					deltaNumberOfnumberWindow();
					deltaIdentificationWindow(infantry[i], establishedJbd, commentJbd);
					deltaReliabilityWindow();
					deltaFlyeye();
					deltaIdPurchaseText(idTargetJbd);
					deltaCommentContents(infantry[i], dateJbd, timeJbd, crewTeamJbd, establishedJbd, targetClassJbd, commentJbd);
				}
			}
			
			//..
			//. якщо ти крило
			for (int i = 0; i < wings.Length; i++) {
				if (targetClassJbd.Contains(wings[i])) {
					deltaLayerWindow(wings[i], commentJbd);
					deltaMarkName(wings[i], dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
					deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
					deltaCombatCapabilityWindow(wings[i], establishedJbd, commentJbd);
					deltaIdentificationWindow(wings[i], establishedJbd, commentJbd);
					deltaIdPurchaseText(idTargetJbd);
					deltaCommentContents(wings[i], dateJbd, timeJbd, crewTeamJbd, establishedJbd, targetClassJbd, commentJbd);
				}
			}
			//..
			//. якщо ти міна
			if (targetClassJbd.Contains(targetMinePTM)) {
				deltaLayerWindow(targetMinePTM, commentJbd);
				deltaMarkName(targetMinePTM, dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
				deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
				deltaNumberOfnumberWindow();
				deltaCombatCapabilityWindow(targetMinePTM, establishedJbd, commentJbd);
				deltaIdentificationWindow(targetMinePTM, establishedJbd, commentJbd);
				deltaReliabilityWindow();
				deltaFlyeye();
				deltaIdPurchaseText(idTargetJbd);
				deltaCommentContents(targetMinePTM, dateJbd, timeJbd, crewTeamJbd, establishedJbd, targetClassJbd, commentJbd);
				//..
				//. якщо ти укриття
			} else if (targetClassJbd.Contains(areaConcentration)) {
				deltaLayerWindow(areaConcentration, commentJbd);
				deltaMarkName(areaConcentration, dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
				deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
				deltaIdentificationWindow(areaConcentration, establishedJbd, commentJbd);
				deltaReliabilityWindow();
				deltaFlyeye();
				deltaCommentContents(areaConcentration, dateJbd, timeJbd, crewTeamJbd, establishedJbd, targetClassJbd, commentJbd);
				deltaAdditionalFields(idTargetJbd, areaConcentration);
				deltaGeografPlace(areaConcentration, establishedJbd, commentJbd);
				//..
				//. якщо ти бліндаж або з укриттів
			} else if (targetClassJbd.Contains(dugout)) {
				deltaLayerWindow(dugout, commentJbd);
				deltaMarkName(dugout, dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
				deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
				deltaIdentificationWindow(dugout, establishedJbd, commentJbd);
				deltaCommentContents(dugout, dateJbd, timeJbd, crewTeamJbd, establishedJbd, targetClassJbd, commentJbd);
				deltaAdditionalFields(idTargetJbd, dugout);
				//..
				//. якщо ти Т. вильоту дронів
			} else if (targetClassJbd.Contains(flightOfDrones)) {
				deltaLayerWindow(flightOfDrones, commentJbd);
				deltaMarkName(flightOfDrones, dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
				deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
				deltaIdentificationWindow(flightOfDrones, establishedJbd, commentJbd);
				deltaCommentContents(flightOfDrones, dateJbd, timeJbd, crewTeamJbd, establishedJbd, targetClassJbd, commentJbd);
				deltaAdditionalFields(idTargetJbd, flightOfDrones);
				//..
				//. якщо ти загородження (шипи)
			} else if (targetClassJbd.Contains(barrage)) {
				deltaLayerWindow(barrage, commentJbd);
				deltaMarkName(barrage, dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
				deltaIdentificationWindow(barrage, establishedJbd, commentJbd);
				deltaCommentContents(barrage, dateJbd, timeJbd, crewTeamJbd, establishedJbd, targetClassJbd, commentJbd);
				deltaAdditionalFields(idTargetJbd, barrage);
				//..	
			} else {
				Console.WriteLine("нічого спільного не зміг знайти");
			}
			
		}
		static string datePlasDays(string date) {
			// Перетворюємо рядок дати у DateTime
			DateTime originalDate = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
			// Додаємо Х днів
			DateTime newDate = originalDate.AddDays(60);
			// Перетворюємо нову дату назад у рядок
			string newDateString = newDate.ToString("dd.MM.yyyy");
			return newDateString;
		}
		// поле шар
		static void deltaLayerWindow(string whoAreYou, string commentJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			// поле шар
			var layerWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-layer"].Find(3);
			layerWindow.ScrollTo();
			wait.ms(200);
			layerWindow.PostClick(2);
			
			switch (whoAreYou) {
			case "Міна":
				layerWindow.SendKeys("Ctrl+A", "!11", "Enter");
				break;
			case "Загородження":
				layerWindow.SendKeys("Ctrl+A", "!11", "Enter");
				break;
			case "Укриття":
				layerWindow.SendKeys("Ctrl+A", "!Пост", "Enter");
				break;
			case "Мережеве обладнання":
				layerWindow.SendKeys("Ctrl+A", "!антени", "Enter");
				break;
			case "Камера":
				layerWindow.SendKeys("Ctrl+A", "!антени", "Enter");
				break;
			case "Антена":
				layerWindow.SendKeys("Ctrl+A", "!антени", "Enter");
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
				if (whoAreYou.Contains("Склад майна") || whoAreYou.Contains("Польовий склад БК") || whoAreYou.Contains("Склад БК")) {
					layerWindow.SendKeys("Ctrl+A", "!Пост", "Enter");
				} else if (commentJbd.Contains("рус") || commentJbd.Contains("рух")) {
					layerWindow.SendKeys("Ctrl+A", "!06", "Enter");
				} else if (commentJbd.Contains("виходи") || commentJbd.Contains("вогнева позиція")) {
					layerWindow.SendKeys("Ctrl+A", "!05", "Enter");
				} else {
					layerWindow.SendKeys("Ctrl+A", "!04", "Enter");
				}
				break;
			}
		}
		// поле назва
		static void deltaMarkName(string whoAreYou, string dateJbd, string establishedJbd, string commentJbd, string twoHundredth, string threeHundredth) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле назва
			string markName = string.Empty;
			switch (whoAreYou) {
			case "Міна":
				if (establishedJbd.Contains("Авар. скид") || establishedJbd.Contains("Розміновано") || establishedJbd.Contains("Підтв. ураж.") || establishedJbd.Contains("Тільки розрив")) {
					markName = "ПТМ-3 (" + dateJbd + ")";
				} else {
					markName = "ПТМ-3 до (" + datePlasDays(dateJbd) + ")";
				}
				break;
			//. Шипи - район загородження
			case "Загородження":
				markName = "Шипи (" + dateJbd + ")";
				break;
			//..
			//. "Укриття
			case "Укриття":
				if (establishedJbd.ToLower().Contains("знищ")) {
					markName = whoAreYou + " ОС (знищ.)";
				} else if (establishedJbd.ToLower().Contains("ураж")) {
					markName = whoAreYou + " ОС (ураж.)";
				} else if (establishedJbd.Contains("Виявлено") || establishedJbd.Contains("Підтверджено")) {
					if (commentJbd.ToLower().Contains("знищ")) {
						markName = whoAreYou + " ОС (знищ.)";
					} else if (commentJbd.ToLower().Contains("ураж")) {
						markName = whoAreYou + " ОС (ураж.)";
					} else {
						markName = whoAreYou + " ОС";
					}
				} else {
					
				}
				break;
			//..
			//. Бліндаж
			case "Бліндаж":
				if (establishedJbd.Contains("Знищ") || establishedJbd.Contains("знищ")) {
					markName = whoAreYou + " (знищ.)";
				} else if (establishedJbd.Contains("Ураж") || establishedJbd.Contains("ураж")) {
					markName = whoAreYou + " (ураж.)";
				} else if (establishedJbd.Contains("Виявлено") || establishedJbd.Contains("Підтверджено")) {
					if (commentJbd.ToLower().Contains("знищ")) {
						markName = whoAreYou + " (знищ.)";
					} else if (commentJbd.ToLower().Contains("ураж")) {
						markName = whoAreYou + " (ураж.)";
					} else {
						markName = whoAreYou;
					}
				}
				break;
			//..
			//. Т. вильоту дронів
			case "Т. вильоту дронів":
				if (establishedJbd.Contains("Знищ") || establishedJbd.Contains("знищ")) {
					markName = whoAreYou + " (знищ.)";
				} else if (establishedJbd.Contains("Ураж") || establishedJbd.Contains("ураж")) {
					markName = whoAreYou + " (ураж.)";
				} else if (establishedJbd.Contains("Виявлено") || establishedJbd.Contains("Підтверджено")) {
					if (commentJbd.ToLower().Contains("знищ")) {
						markName = whoAreYou + " (знищ.)";
					} else if (commentJbd.ToLower().Contains("ураж")) {
						markName = whoAreYou + " (ураж.)";
					} else {
						markName = whoAreYou;
					}
				}
				break;
			//..
			//. Скупчення ОС
			case "Скупчення ОС":
				if (twoHundredth.Length > 0) {
					markName = twoHundredth.ToInt() + " - 200";
				} else if (threeHundredth.Length > 0) {
					markName = threeHundredth.ToInt() + " - 300";
				} else {
					markName = whoAreYou;
				}
				break;
			//..
			default:
				if (establishedJbd.Contains("Знищ") || establishedJbd.Contains("знищ")) {
					markName = whoAreYou + " (знищ.)";
				} else if (establishedJbd.Contains("Ураж") || establishedJbd.Contains("ураж")) {
					markName = whoAreYou + " (ураж.)";
				} else if (establishedJbd.Contains("Виявлено") || establishedJbd.Contains("Підтверджено")) {
					if (commentJbd.ToLower().Contains("знищ")) {
						markName = whoAreYou + " (знищ.)";
					} else if (commentJbd.ToLower().Contains("ураж")) {
						markName = whoAreYou + " (ураж.)";
					} else if (commentJbd.ToLower().Contains("в рус") || commentJbd.ToLower().Contains("рух")) {
						markName = whoAreYou + " (в русі)";
					}
				} else {
					markName = whoAreYou;
				}
				break;
			}
			
			var nameOfMarkWindow = w.Elm["web:TEXT", prop: new("@data-testid=T")].Find(3);
			nameOfMarkWindow.PostClick(2);
			wait.ms(100);
			nameOfMarkWindow.SendKeys("Ctrl+A", "!" + markName);
		}
		// поле дата / час
		static void deltaDateLTimeWindow(string dateDeltaFormat, string timeJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле дата / час
			var dateDeltaWindow = w.Elm["web:TEXT", prop: "@data-testid=W"].Find(3);
			dateDeltaWindow.PostClick(2);
			wait.ms(100);
			dateDeltaWindow.SendKeys("Ctrl+A", "!" + dateDeltaFormat);
			wait.ms(200);
			var timeDeltaWindow = w.Elm["web:TEXT", prop: "@data-testid=W-time-input"].Find(1);
			timeDeltaWindow.PostClick(2);
			wait.ms(100);
			timeDeltaWindow.SendKeys("Ctrl+A", "!" + timeJbd, "Enter*2");
		}
		// поле кількість
		static void deltaNumberOfnumberWindow() {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле кількість
			var numberOfnumberWindow = w.Elm["web:SPINBUTTON", prop: new("@data-testid=C", "@type=number")].Find(3);
			numberOfnumberWindow.PostClick(1);
			numberOfnumberWindow.SendKeys("Ctrl+A", "!1");
			wait.ms(100);
		}
		// поле боєздатність
		static void deltaCombatCapabilityWindow(string whoAreYou, string establishedJbd, string commentJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле боєздатність
			string fullaim = string.Empty;
			switch (whoAreYou) {
			//. Якщо ти міна
			case "Міна":
				if (establishedJbd.Contains("Авар. скид") || establishedJbd.Contains("Розміновано") || establishedJbd.Contains("Підтв. ураж.") || establishedJbd.Contains("Тільки розрив")) {
					fullaim = "небо";
				} else if (establishedJbd.Contains("Встановлено")) {
					fullaim = "повніс";
				} else {
					fullaim = "част";
				}
				break;
			//..
			default:
				if (establishedJbd.Contains("Знищ") || establishedJbd.Contains("знищ")) {
					fullaim = "небо";
				} else if (establishedJbd.Contains("Ураж") || establishedJbd.Contains("ураж")) {
					fullaim = "част";
				} else if (establishedJbd.Contains("Виявлено")) {
					if (commentJbd.ToLower().Contains("знищ")) {
						fullaim = "небо";
					} else if (commentJbd.ToLower().Contains("ураж")) {
						fullaim = "част";
					} else {
						fullaim = "повніс";
					}
				} else {
					fullaim = "повніс";
				}
				break;
			}
			
			var combatCapabilityWindow = w.Elm["web:GROUPING", prop: "@data-testid=operational-condition-select"].Find(3);
			combatCapabilityWindow.ScrollTo();
			wait.ms(200);
			combatCapabilityWindow.PostClick(2);
			combatCapabilityWindow.SendKeys("Ctrl+A", "!" + fullaim, "Enter");
			wait.ms(100);
		}
		// ідетнифікація
		static void deltaIdentificationWindow(string whoAreYou, string establishedJbd, string commentJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// ідетнифікація
			string friendly = string.Empty;
			switch (whoAreYou) {
			case "Міна":
				friendly = "дружній";
				break;
			case "Укриття":
				if (establishedJbd.Contains("Знищ") || establishedJbd.Contains("знищ") || commentJbd.Contains("знищ")) {
					friendly = "відом";
				} else {
					friendly = "воро";
				}
				break;
			default:
				friendly = "воро";
				break;
			}
			var identificationWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-HO"].Find(3);
			identificationWindow.ScrollTo();
			wait.ms(200);
			identificationWindow.PostClick(1);
			identificationWindow.SendKeys("Ctrl+A", "!" + friendly, "Enter");
			wait.ms(100);
		}
		// достовірність
		static void deltaReliabilityWindow() {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// достовірність
			var reliabilityWindow = w.Elm["web:RADIOBUTTON", "A", "@data-testid=reliability-key-A"].Find(3);
			reliabilityWindow.PostClick(1);
			wait.ms(100);
			var certaintyWindow = w.Elm["web:RADIOBUTTON", "2", "@data-testid=reliability-key-2"].Find(3);
			certaintyWindow.PostClick(1);
		}
		// тип джерела
		static void deltaFlyeye() {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// тип джерела
			string flyeye = "пові";
			var typeOfSourceWindow = w.Elm["web:GROUPING", prop: "@data-testid=AD"].Find(1);
			typeOfSourceWindow.ScrollTo();
			wait.ms(500);
			typeOfSourceWindow.PostClick(2);
			wait.ms(200);
			typeOfSourceWindow.SendKeys("!" + flyeye, "Tab");
		}
		// завуваження штабу - ід
		static void deltaIdPurchaseText(string idTargetJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// завйваження штабу ід
			var idPurchaseWindow = w.Elm["web:TEXT", prop: "@data-testid=G", flags: EFFlags.HiddenToo].Find(1);
			idPurchaseWindow.PostClick();
			wait.ms(100);
			idPurchaseWindow.SendKeys("Ctrl+A", "!" + idTargetJbd, "Enter");
		}
		// коментар
		static void deltaCommentContents(string whoAreYou, string dateJbd, string timeJbd, string crewTeamJbd, string establishedJbd, string targetClassJbd, string commentJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			string commentContents = dateJbd + " " + timeJbd + " - ";
			switch (whoAreYou) {
			//. Міна
			case "Міна":
				if (establishedJbd.Contains("Авар. скид") || establishedJbd.Contains("Подавлено")) {
					commentContents += "аварійно сикнуто з ударного коптера " + crewTeamJbd;
				} else if (establishedJbd.Contains("Розміновано") || establishedJbd.Contains("Тільки розрив")) {
					commentContents += "розміновано спостерігали з " + crewTeamJbd;
				} else if (establishedJbd.Contains("Підтв. ураж.")) {
					commentContents += "підрив на міні, кори ( id ), спостерігали з " + crewTeamJbd;
				} else {
					commentContents += " встановлено за допомогою ударного коптера " + crewTeamJbd;
				}
				break;
			//..
			//. Укриття
			case "Укриття":
				if (establishedJbd.ToLower().Contains("знищ")) {
					commentContents += establishedJbd.ToLower() + " за допомогою " + crewTeamJbd;
				} else if (establishedJbd.ToLower().Contains("ураж")) {
					commentContents += establishedJbd.ToLower() + " за допомогою " + crewTeamJbd;
				} else if (establishedJbd.Contains("Підтверджено") || establishedJbd.Contains("Спростовано")) {
					if (commentJbd.ToLower().Contains("знищ") || commentJbd.ToLower().Contains("ураж")) {
						commentContents += commentJbd + ", спостергіав " + crewTeamJbd;
					}else {
						commentContents += commentJbd + ", спостергіав " + crewTeamJbd;
					}
				} else if (establishedJbd.Contains("Виявлено")) {
					if (commentJbd.ToLower().Contains("знищ")) {
						commentContents += establishedJbd.ToLower() + " знищ." + whoAreYou.ToLower() + ", спостерігав " + crewTeamJbd;
					} else if (commentJbd.Contains("ураж")) {
						commentContents += establishedJbd.ToLower() + " ураж." + whoAreYou.ToLower() + ", спостерігав " + crewTeamJbd;
					} else {
						commentContents += commentJbd + ", спостерігав " + crewTeamJbd;
					}
				}else if (establishedJbd.Contains("Не зрозуміло")) {
					commentContents += "спроба ураження, " + crewTeamJbd;
				}
				break;
			//..
			default:
				if (establishedJbd.Contains("Виявлено")) {
					commentContents += commentJbd + ", спостерігали з " + crewTeamJbd;
				}else if (establishedJbd.Contains("Не зрозуміло")) {
					commentContents += "спроба ураження, " + crewTeamJbd;
				} else {
					commentContents += commentJbd + " ," + establishedJbd.ToLower() + " за допомогою " + crewTeamJbd;
				}
				break;
			}
			// коментар
			var commentWindow = w.Elm["web:TEXT", prop: new("@data-testid=comment-editing__textarea", "@name=text")].Find(1);
			commentWindow.ScrollTo();
			wait.ms(200);
			//mouse.wheel(-5);
			wait.ms(100);
			commentWindow.PostClick();
			commentWindow.SendKeys("Ctrl+A", "!" + commentContents);
			wait.ms(100);
			
			// кнопка коментаря
			var commentAsseptButton = w.Elm["web:BUTTON", prop: "@data-testid=comment-editing__button-save"].Find(1);
			commentAsseptButton.ScrollTo();
			wait.ms(200);
			commentAsseptButton.PostClick(2);
		}
		// додаткові поля
		static void deltaAdditionalFields(string idTargetJbd, string whoAreYou) {
			// додаткові поля
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			var additionalFields = w.Elm["web:GROUPING", "Додаткові поля", "@title=Додаткові поля"].Find(1);
			additionalFields.PostClick(1);
			wait.ms(200);
			//примітки штабу
			var notesWindow = w.Elm["web:TEXT", prop: new("@data-testid=string-field__input", "@name=Примітки штабу")].Find(1);
			notesWindow.ScrollTo();
			wait.ms(400);
			notesWindow.PostClick(1);
			notesWindow.SendKeys("Ctrl+A", "!" + idTargetJbd, "Enter");
			wait.ms(200);
			
			if (!whoAreYou.Contains("Укриття")) {
				// повернення на основне вікно
				var mainFilds = w.Elm["web:GROUPING", prop: "@title=Основні поля"].Find(1);
				mainFilds.PostClick(1);
				wait.ms(200);
			}
			
		}
		// Георафічне розташування
		static void deltaGeografPlace(string whoAreYou, string establishedJbd, string commentJbd) {
			//основне вікно
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// Георафічне розташування
			var geografPlaceWindow = w.Elm["web:GROUPING", prop: "@title=Географічне розташування"].Find(1);
			geografPlaceWindow.ScrollTo();
			wait.ms(200);
			geografPlaceWindow.PostClick();
			wait.ms(200);
			
			switch (whoAreYou) {
			//. укриття
			case "Укриття":
				if (establishedJbd.ToLower().Contains("знищ") || establishedJbd.ToLower().Contains("ураж")) {
					// колір жовтий - знищ
					var placeColorYellowButton = w.Elm["web:BUTTON", "#ffeb3b", "@title=#ffeb3b"].Find(1);
					placeColorYellowButton.ScrollTo();
					wait.ms(300);
					placeColorYellowButton.PostClick();
					wait.ms(300);
					// відсоток прозрачності
					var transpatentColorRange = w.Elm["web:SLIDER", prop: "@data-testid=slider"].Find(1);
					transpatentColorRange.PostClick();
					transpatentColorRange.SendKeys("Left*5");
					wait.ms(300);
				} else if (establishedJbd.Contains("Виявлено")) {
					if (commentJbd.ToLower().Contains("знищ") || commentJbd.ToLower().Contains("ураж")) {
						// колір жовтий - знищ
						var placeColorYellowButton = w.Elm["web:BUTTON", "#ffeb3b", "@title=#ffeb3b"].Find(1);
						placeColorYellowButton.ScrollTo();
						wait.ms(300);
						placeColorYellowButton.PostClick();
						wait.ms(300);
						// відсоток прозрачності
						var transpatentColorRange = w.Elm["web:SLIDER", prop: "@data-testid=slider"].Find(1);
						transpatentColorRange.PostClick();
						transpatentColorRange.SendKeys("Left*5");
						wait.ms(300);
					}
				} else {
					//колір червоний - ворож
					var placeColorRedButton = w.Elm["web:BUTTON", "#f44336", "@title=#f44336"].Find(1);
					placeColorRedButton.ScrollTo();
					wait.ms(300);
					placeColorRedButton.PostClick();
					wait.ms(300);
					// відсоток прозрачності
					var transpatentColorRange = w.Elm["web:SLIDER", prop: "@data-testid=slider"].Find(1);
					transpatentColorRange.PostClick();
					transpatentColorRange.SendKeys("Left*5");
					wait.ms(300);
				}
				// повернення на основне вікно
				var mainFilds = w.Elm["web:GROUPING", prop: "@title=Основні поля"].Find(1);
				mainFilds.PostClick(1);
				wait.ms(200);
				break;
			//..
			default:
				
				break;
			}
			
			// колір голубий - дружній
			/* заготовка під майбутнє
			var placeColorBlueButton = w.Elm["web:BUTTON", "#00bcd4", "@title=#00bcd4"].Find(1);
			placeColorBlueButton.PostClick();
			wait.ms(200);
			*/
			
		}
		// main window
		static void deltaGeograficPlace() {
			
		}
		// обрізка до 19 символів в рядку
		static string TrimString(string str, int maxLength) {
			if (str.Length > maxLength) {
				return str.Substring(str.Length - maxLength);
			}
			return str;
		}
		// обрізати рядок усе після крапки
		static string TrimAfterDot(string str) {
			int dotIndex = str.IndexOf('.');
			if (dotIndex != -1) {
				return str.Substring(0, dotIndex);
			}
			return str;
		}
	}
}

