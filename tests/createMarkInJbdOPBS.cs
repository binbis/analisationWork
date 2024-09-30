/* 30,09,2024_v1.7.4
* id обрізаються, щоб поміститись в рядок 
* функція додавання до дати дні(60) підходить для мін
* 200 та 300 рахуються та вписуються самі
* відкриття апаки за ід повідомлення 1-3 секунди
* координата в коментар для укриття
*/
using System.Windows;
using System.Windows.Controls;

namespace CSLight {
	class Program {
		static void Main() {
			opt.key.KeySpeed = 20;
			opt.key.TextSpeed = 20;
			
			keys.send("Shift+Space*2"); //виділяємо весь рядок
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
			string targetClassJbd = parts[7]; // Міна/Вантажівка/...
			string idTargetJbd = TrimString(parts[9], 19); // Міна 270724043
			string mgrsCoords = parts[18]; // 37U CP 76420 45222
			string establishedJbd = parts[24]; // Встановлено/Уражено/Промах/...
			string twoHundredth = parts[25]; // 200
			string threeHundredth = parts[26]; // 300
			string combatLogId = parts[33]; // 1725666514064
			// шлях до папки з ід повідомленням
			string pathTo_combatLogId = @"\\SNG-8-sh\CombatLog\Donbas_Combat_Log";
			// перетворення дати до формату дельти
			string dateDeltaFormat = dateJbd.Replace('.', '/');
			// основне вікно
			clipboard.clear();
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			goToMain();
			wait.ms(400);
			deltaLayerWindow(targetClassJbd, commentJbd);
			wait.ms(200);
			deltaMarkName(targetClassJbd, dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
			wait.ms(200);
			deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
			wait.ms(200);
			deltaNumberOfnumberWindow(twoHundredth, threeHundredth);
			wait.ms(200);
			deltaCombatCapabilityWindow(targetClassJbd, establishedJbd, commentJbd);
			wait.ms(200);
			deltaIdentificationWindow(targetClassJbd, establishedJbd, commentJbd);
			wait.ms(200);
			deltaReliabilityWindow();
			wait.ms(200);
			deltaFlyeye();
			wait.ms(200);
			deltaIdPurchaseText(idTargetJbd);
			wait.ms(200);
			deltaMobilityLine(targetClassJbd);
			wait.ms(200);
			deltaCommentContents(targetClassJbd, dateJbd, timeJbd, crewTeamJbd, establishedJbd, commentJbd, mgrsCoords);
			wait.ms(1500);
			deltaAdditionalFields(idTargetJbd, targetClassJbd);
			wait.ms(1500);
			deltaGeografPlace(targetClassJbd, establishedJbd, commentJbd, mgrsCoords);
			wait.ms(200);
			
			if (combatLogId.Length > 6) {
				deltaImportFiles(combatLogId, pathTo_combatLogId);
			} else {
				goToMain();
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
		static void deltaLayerWindow(string targetClassJbd, string commentJbd) {
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
					if (permanentStorage[i].Contains(targetClassJbd)) {
						layerWindow.SendKeys("Ctrl+A", "!Пост", "Enter");
						return;
					}
				}
				for (int i = 0; i < antennaCamera.Length; i++) {
					if (antennaCamera[i].Contains(targetClassJbd)) {
						layerWindow.SendKeys("Ctrl+A", "!антени", "Enter");
						return;
					}
				}
				switch (targetClassJbd) {
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
					if (commentJbd.ToLower().Contains("рус") || commentJbd.ToLower().Contains("рух")) {
						layerWindow.SendKeys("Ctrl+A", "!06", "Enter");
					} else if (commentJbd.ToLower().Contains("виходи") || commentJbd.ToLower().Contains("вогнева позиція")) {
						layerWindow.SendKeys("Ctrl+A", "!05", "Enter");
					} else {
						layerWindow.SendKeys("Ctrl+A", "!04", "Enter");
					}
					break;
				}
			}
			//..
		}
		// поле назва
		static void deltaMarkName(string targetClassJbd, string dateJbd, string establishedJbd, string commentJbd, string twoHundredth, string threeHundredth) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле назва
			var nameOfMarkWindow = w.Elm["web:TEXT", prop: new("@data-testid=T")].Find();
			if (nameOfMarkWindow != null) {
				string markName = string.Empty;
				//. формування, перевірка
				switch (targetClassJbd) {
				//. Міна
				case "Міна":
					if (establishedJbd.Contains("Авар. скид") || establishedJbd.Contains("Розміновано") || establishedJbd.Contains("Підтв. ураж.") || establishedJbd.Contains("Тільки розрив")) {
						markName = "ПТМ-3 (" + dateJbd + ")";
					} else {
						markName = "ПТМ-3 до (" + datePlasDays(dateJbd) + ")";
					}
					break;
				//..
				//. Шипи - район загородження
				case "Загородження":
					markName = "Шипи (" + dateJbd + ")";
					break;
				//..
				//. "Укриття
				case "Укриття":
					if (establishedJbd.ToLower().Contains("знищ")) {
						markName = targetClassJbd + " ОС (знищ.)";
					} else if (establishedJbd.ToLower().Contains("ураж")) {
						markName = targetClassJbd + " ОС (ураж.)";
					} else if (establishedJbd.Contains("Виявлено") || establishedJbd.Contains("Підтверджено")) {
						if (commentJbd.ToLower().Contains("знищ")) {
							markName = targetClassJbd + " ОС (знищ.)";
						} else if (commentJbd.ToLower().Contains("ураж")) {
							markName = targetClassJbd + " ОС (ураж.)";
						} else {
							markName = targetClassJbd + " ОС";
						}
					} else {
						markName = targetClassJbd + " ОС";
					}
					break;
				//..
				//. Скупчення ОС
				case "Скупчення ОС":
					if (twoHundredth.Length > 0) {
						markName = twoHundredth + " - 200";
					}
					if (threeHundredth.Length > 0) {
						markName = threeHundredth + " - 300";
					}
					if (twoHundredth.Length > 0 && threeHundredth.Length > 0) {
						markName = twoHundredth + " - 200, " + threeHundredth + " - 300";
					}
					break;
				//..
				default:
					if (establishedJbd.Contains("Знищ") || establishedJbd.Contains("знищ")) {
						markName = targetClassJbd + " (знищ.)";
					} else if (establishedJbd.Contains("Ураж") || establishedJbd.Contains("ураж")) {
						markName = targetClassJbd + " (ураж.)";
					} else if (establishedJbd.Contains("Виявлено") || establishedJbd.Contains("Підтверджено")) {
						if (commentJbd.ToLower().Contains("знищ")) {
							markName = targetClassJbd + " (знищ.)";
						} else if (commentJbd.ToLower().Contains("ураж")) {
							markName = targetClassJbd + " (ураж.)";
						} else if (commentJbd.ToLower().Contains("в рус") || commentJbd.ToLower().Contains("рух")) {
							markName = targetClassJbd + " (в русі)";
						} else if (commentJbd.ToLower().Contains("схов")) {
							markName = targetClassJbd + " (схов.)";
						} else if (commentJbd.ToLower().Contains("стоїт")) {
							markName = targetClassJbd + " (стоїть)";
						} else {
							markName = targetClassJbd;
						}
					} else {
						markName = targetClassJbd;
					}
					break;
				}
				//..
				nameOfMarkWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + markName);
			}
			
		}
		// поле дата / час
		static void deltaDateLTimeWindow(string dateDeltaFormat, string timeJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле дата / час
			var dateDeltaWindow = w.Elm["web:TEXT", prop: "@data-testid=W"].Find();
			if (dateDeltaWindow != null) {
				dateDeltaWindow.PostClick();
				keys.sendL("Ctrl+A", "!" + dateDeltaFormat);
			}
			wait.ms(400);
			var timeDeltaWindow = w.Elm["web:TEXT", prop: "@data-testid=W-time-input"].Find();
			if (dateDeltaWindow != null) {
				timeDeltaWindow.PostClick();
				keys.sendL("Ctrl+A", "!" + timeJbd, "Enter");
				
			}
		}
		// поле кількість
		static void deltaNumberOfnumberWindow(string twoHundredth, string threeHundredth) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле кількість
			var numberOfnumberWindow = w.Elm["web:SPINBUTTON", prop: new("@data-testid=C", "@type=number")].Find();
			if (numberOfnumberWindow != null) {
				int counts = 1;
				if (twoHundredth.Length > 0 || threeHundredth.Length > 0) {
					counts = (twoHundredth.ToInt() + threeHundredth.ToInt());
				}
				numberOfnumberWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + counts);
			}
		}
		// поле боєздатність
		static void deltaCombatCapabilityWindow(string targetClassJbd, string establishedJbd, string commentJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле боєздатність
			var combatCapabilityWindow = w.Elm["web:GROUPING", prop: "@data-testid=operational-condition-select"].Find();
			//. перевірка
			if (combatCapabilityWindow != null) {
				string fullaim = string.Empty;
				switch (targetClassJbd) {
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
					if (establishedJbd.ToLower().Contains("знищ")) {
						fullaim = "небо";
					} else if (establishedJbd.ToLower().Contains("ураж")) {
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
						if (commentJbd.ToLower().Contains("знищ")) {
							fullaim = "небо";
						} else if (commentJbd.ToLower().Contains("ураж")) {
							fullaim = "част";
						} else {
							fullaim = "повніс";
						}
					}
					break;
				}
				//..
				combatCapabilityWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + fullaim, "Enter");
			}
		}
		// ідетнифікація
		static void deltaIdentificationWindow(string targetClassJbd, string establishedJbd, string commentJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// ідетнифікація
			var identificationWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-HO"].Find();
			if (identificationWindow != null) {
				string friendly = string.Empty;
				switch (targetClassJbd) {
				case "Міна":
					friendly = "дружній";
					break;
				case "Укриття":
					if (establishedJbd.ToLower().Contains("знищ") || commentJbd.ToLower().Contains("знищ")) {
						friendly = "відом";
					} else {
						friendly = "воро";
					}
					break;
				default:
					friendly = "воро";
					break;
				}
				identificationWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + friendly, "Enter");
			}
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
				keys.sendL("Ctrl+A", "!" + flyeye, "Enter");
			}
		}
		// зауваження штабу - ід
		static void deltaIdPurchaseText(string idTargetJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// завйваження штабу ід
			var idPurchaseWindow = w.Elm["web:TEXT", prop: "@data-testid=G", flags: EFFlags.HiddenToo].Find();
			if (idPurchaseWindow != null) {
				idPurchaseWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + idTargetJbd);
			}
		}
		// мобільність
		static void deltaMobilityLine(string targetClassJbd) {
			// Обмеженої прохідності
			string[] limitedAccess = { "Авто", "Вантажівка", "Паливозаправник" };
			string obmezheno = "обмежено";
			// Позашляховик
			string[] pozashlyakhovyk = { "Мотоцикл", "ББМ / МТ-ЛБ", "БМП", "РЕБ (техніка)", "БТР", "Військ. баггі" };
			string suv = "позашлях";
			// Гусеничний - колісний
			string[] caterpillar = { "Танк", "ЗРК", "РСЗВ", "САУ", "КШМ", "Інж. техніка" };
			string husenychnyy = "комбінов";
			//На буксирі
			string[] towTruck = { "Гармата", "Гаубиця" };
			string buksyri = "буксир";
			
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			w.Elm["web:GROUPING", prop: "@data-testid=select-ADR", navig: "pr"].Find(-1);
			var mobileLine = w.Elm["web:GROUPING", prop: "@data-testid=select-ADR"].Find(-1);
			if (mobileLine != null) {
				var checking = w.Elm["web:GROUPING", prop: "@data-testid=select-ADR", navig: "pr child"].Find(-1);
				if (checking.Name != "Мобільність") {
					return;
				}
				wait.ms(500);
				// Обмеженої прохідності
				for (int i = 0; i < limitedAccess.Length; i++) {
					if (limitedAccess.Contains(targetClassJbd)) {
						mobileLine.PostClick(scroll: 250);
						keys.sendL("Ctrl+A", "!" + obmezheno, "Enter");
						return;
					}
				}
				// Позашляховик
				for (int i = 0; i < pozashlyakhovyk.Length; i++) {
					if (pozashlyakhovyk.Contains(targetClassJbd)) {
						mobileLine.PostClick(scroll: 250);
						keys.sendL("Ctrl+A", "!" + suv, "Enter");
						return;
					}
				}
				// Гусеничний
				for (int i = 0; i < caterpillar.Length; i++) {
					if (caterpillar.Contains(targetClassJbd)) {
						mobileLine.PostClick(scroll: 250);
						keys.sendL("Ctrl+A", "!" + husenychnyy, "Enter");
						return;
					}
				}
				// На буксирі
				for (int i = 0; i < towTruck.Length; i++) {
					if (towTruck.Contains(targetClassJbd)) {
						mobileLine.PostClick(scroll: 250);
						keys.sendL("Ctrl+A", "!" + buksyri, "Enter");
						return;
					}
				}
			}
		}
		// коментар
		static void deltaCommentContents(string targetClassJbd, string dateJbd, string timeJbd, string crewTeamJbd, string establishedJbd, string commentJbd, string mgrsCoords) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			// коментар
			var commentWindow = w.Elm["web:TEXT", prop: "@data-testid=comment-editing__textarea"].Find(-1);
			if (commentWindow != null) {
				string commentContents = $"{dateJbd} {timeJbd} - ";
				//. перевірка
				switch (targetClassJbd) {
				//. Міна
				case "Міна":
					if (establishedJbd.Contains("Авар. скид") || establishedJbd.Contains("Подавлено")) {
						commentContents += "аварійно сикнуто з ударного коптера " + crewTeamJbd;
					} else if (establishedJbd.Contains("Розміновано")) {
						commentContents += commentJbd + ",розміновано, спостерігали з " + crewTeamJbd;
					} else if (establishedJbd.Contains("Тільки розрив")) {
						commentContents += "тільки розрив, спостерігали з " + crewTeamJbd;
					} else if (establishedJbd.Contains("Підтв. ураж.")) {
						commentContents += "підрив на міні, кори ( id ), спостерігали з " + crewTeamJbd;
					} else {
						commentContents += " встановлено за допомогою ударного коптера " + crewTeamJbd;
					}
					break;
				//..
				//. Укриття
				case "Укриття":
					commentContents = $"{dateJbd} {timeJbd} - (  {mgrsCoords}  ) - ";
					if (establishedJbd.ToLower().Contains("знищ")) {
						commentContents += establishedJbd.ToLower() + " за допомогою " + crewTeamJbd;
					} else if (establishedJbd.ToLower().Contains("ураж")) {
						commentContents += establishedJbd.ToLower() + " за допомогою " + crewTeamJbd;
					} else if (establishedJbd.Contains("Підтверджено") || establishedJbd.Contains("Спростовано")) {
						if (commentJbd.ToLower().Contains("знищ") || commentJbd.ToLower().Contains("ураж")) {
							commentContents += commentJbd + ", спостерігав " + crewTeamJbd;
						} else {
							commentContents += commentJbd + ", спостерігав " + crewTeamJbd;
						}
					} else if (establishedJbd.Contains("Виявлено")) {
						if (commentJbd.ToLower().Contains("знищ")) {
							commentContents += establishedJbd.ToLower() + " знищ." + targetClassJbd.ToLower() + ", спостерігав " + crewTeamJbd;
						} else if (commentJbd.Contains("ураж")) {
							commentContents += establishedJbd.ToLower() + " ураж." + targetClassJbd.ToLower() + ", спостерігав " + crewTeamJbd;
						} else {
							commentContents += commentJbd + ", спостерігав " + crewTeamJbd;
						}
					} else if (establishedJbd.Contains("Не зрозуміло")) {
						commentContents += "спроба ураження, " + crewTeamJbd;
					}
					break;
				//..
				default:
					if (establishedJbd.Contains("Виявлено")) {
						commentContents += commentJbd + ", спостерігали з " + crewTeamJbd;
					} else if (establishedJbd.Contains("Не зрозуміло")) {
						commentContents += "спроба ураження, " + crewTeamJbd;
					} else {
						commentContents += commentJbd + " " + establishedJbd.ToLower() + " за допомогою " + crewTeamJbd;
					}
					break;
				}
				//..
				commentWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + commentContents);
				// кнопка коментаря
				wait.ms(400);
				var commentAsseptButton = w.Elm["web:BUTTON", prop: "@data-testid=comment-editing__button-save"].Find(1);
				commentAsseptButton.PostClick(scroll: 250);
			}
			
		}
		// додаткові поля
		static void deltaAdditionalFields(string idTargetJbd, string targetClassJbd) {
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// додаткові поля
			var additionalFields = w.Elm["web:GROUPING", prop: new("desc=Додаткові поля", "@title=Додаткові поля")].Find();
			additionalFields.PostClick(scroll: 250);
			//примітки штабу туту біда поле назва та примітки мають одниковість тест ід
			var notesWindow = w.Elm["web:TEXT", prop: "@name=Примітки штабу"].Find(-1);
			if (notesWindow != null) {
				notesWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + idTargetJbd, "Enter");
			};
			
		}
		// Георафічне розташування
		static void deltaGeografPlace(string targetClassJbd, string establishedJbd, string commentJbd, string mgrsCoords) {
			//основне вікно
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// Георафічне розташування
			var geografPlaceWindow = w.Elm["web:GROUPING", prop: "@title=Географічне розташування"].Find();
			geografPlaceWindow.PostClick(scroll: 250);
			// Колір заливки
			var deltaColorFills = w.Elm["web:STATICTEXT", "Колір заливки"].Find(-1);
			if (deltaColorFills != null) {
				switch (targetClassJbd) {
				//. укриття
				case "Укриття":
					if (establishedJbd.ToLower().Contains("знищ") || establishedJbd.ToLower().Contains("ураж")) {
						// колір жовтий - знищ
						var placeColorYellowButton = w.Elm["web:BUTTON", prop: "@title=#ffeb3b"].Find(1);
						placeColorYellowButton.PostClick(scroll: 250);
						wait.ms(400);
						// відсоток прозрачності
						var transpatentColorRange = w.Elm["web:SLIDER", prop: "@data-testid=slider"].Find(1);
						transpatentColorRange.PostClick(scroll: 250);
						transpatentColorRange.SendKeys("Left*5");
						wait.ms(400);
					} else if (establishedJbd.Contains("Виявлено") || establishedJbd.Contains("Підтверджено") || establishedJbd.Contains("Спростовано")) {
						if (commentJbd.ToLower().Contains("знищ") || commentJbd.ToLower().Contains("ураж")) {
							// колір жовтий - знищ
							var placeColorYellowButton = w.Elm["web:BUTTON", prop: "@title=#ffeb3b"].Find(1);
							placeColorYellowButton.PostClick(scroll: 250);
							wait.ms(400);
							// відсоток прозрачності
							var transpatentColorRange = w.Elm["web:SLIDER", prop: "@data-testid=slider"].Find(1);
							transpatentColorRange.PostClick(scroll: 250);
							transpatentColorRange.SendKeys("Left*5");
							wait.ms(400);
						} else {
							//колір червоний - ворож
							var placeColorRedButton = w.Elm["web:BUTTON", prop: "@title=#f44336"].Find(1);
							placeColorRedButton.PostClick(scroll: 250);
							wait.ms(400);
							// відсоток прозрачності
							var transpatentColorRange = w.Elm["web:SLIDER", prop: "@data-testid=slider"].Find(1);
							transpatentColorRange.PostClick(scroll: 250);
							transpatentColorRange.SendKeys("Left*5");
							wait.ms(400);
						}
					} else {
						//колір червоний - ворож
						var placeColorRedButton = w.Elm["web:BUTTON", prop: "@title=#f44336"].Find(1);
						placeColorRedButton.PostClick(scroll: 250);
						wait.ms(400);
						// відсоток прозрачності
						var transpatentColorRange = w.Elm["web:SLIDER", prop: "@data-testid=slider"].Find(1);
						transpatentColorRange.PostClick(scroll: 250);
						transpatentColorRange.SendKeys("Left*5");
						wait.ms(400);
					}
					break;
				//..
				default:
					break;
				}
			}
		}
		// пошук файлів за ід для прикріплення
		static void deltaImportFiles(string combatLogId, string pathTo_combatLogId) {
			// основне вікно
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// кнопка прикріплення
			var deltaStickWindow = w.Elm["web:GROUPING", prop: new("desc=Прикріплення", "@title=Прикріплення")].Find(1);
			deltaStickWindow.PostClick();
			wait.ms(400);
			if (combatLogId.Length > 6) {
				// злови помилку
				Process.Start("explorer.exe", Path.Combine(pathTo_combatLogId, combatLogId));
			}
		}
		static void goToMain() {
			// основне вікно
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// повернення на основне вікно
			wait.ms(400);
			var mainFilds = w.Elm["web:GROUPING", prop: new("desc=Основні поля", "@title=Основні поля")].Find(1);
			mainFilds.PostClick();
			wait.ms(400);
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
