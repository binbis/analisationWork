/*/ c \analisationWork\globalClass\Bisbin.cs; /*/

/* 15.11.2024 2.0

* id обрізаються, щоб поміститись в рядок 
* функція додавання до дати дні(x) підходить для мін
* 200 та 300 рахуються
* координата в коментар для укриття
* до fpv додається тип борту f7
* слово "виходи, вогнева позиція" 

*/

using System.Windows;
using System.Windows.Controls;

namespace CSLight {
	
	class Program {
		static void Main() {
			opt.key.KeySpeed = 20;
			opt.key.TextSpeed = 20;
			
			//keys.send("Shift+Space*2"); //виділяємо весь рядок
			//wait.ms(100);
			keys.send("Ctrl+C"); //копіюємо код
			wait.ms(100);
			string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
			string[] examlpelesItem = {
										"1. Заповнення мітки",
										"2. Створення РЕБ та РЕР мітки",
										"3. Створення файлу 777 міток",
										"4. міни ска",
										"5. обізнаність ворога й всяке",
									};
			// вікно діалогу
			var b = new wpfBuilder("Window").WinSize(400);
			b.R.Add("Назва", out ComboBox itemSelect).Items(examlpelesItem);
			b.R.AddOkCancel();
			b.Window.Topmost = true;
			b.End();
			// show dialog. Exit if closed not with the OK button.
			if (!b.ShowDialog()) return;
			
			if (itemSelect.Text.Contains("1.")) {
				fillMarkWithJBD(clipboardData);
			}
			if (itemSelect.Text.Contains("2.")) {
				createREBandRER(clipboardData);
			}
			if (itemSelect.Text.Contains("3.")) {
				createWhoWork(clipboardData);
			}
		}
		// тіло для заповнення мітки
		static void fillMarkWithJBD(string clipboardData) {
			
			string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
			// Присвоюємо змінним відповідні значення
			string dateJbd = parts[0]; // 27.07.2024
			string timeJbd = parts[1]; //00:40
			string commentJbd = parts[2].Replace("\n", " "); //коментар (для ідентифікації скоріш за все)
			string numberOFlying = parts[3]; // 5
			string crewTeamJbd = Bisbin.TrimAfterDot(parts[4].Replace("\n\t", "")); // R-18-1 (Мавка)
			string whatDidJbd = parts[5]; // Дорозвідка / Мінування ..
			string targetClassJbd = parts[7]; // Міна/Вантажівка/...
			string idTargetJbd = Bisbin.TrimNTwonyString(parts[9], 19); // Міна 270724043
			string mgrsCoords = parts[18]; // 37U CP 76420 45222
			string nameOfBch = parts[22]; // ПТМ-3 ТМ-62
			string establishedJbd = parts[24]; // Встановлено/Уражено/Промах/...
			string twoHundredth = parts[25]; // 200
			string threeHundredth = parts[26]; // 300
			// перетворення дати до формату дельти
			string dateDeltaFormat = dateJbd.Replace('.', '/');
			// додавно для подашої верифікації
			if (crewTeamJbd.Contains("FPV")) {
				crewTeamJbd = Bisbin.addTypeForBoard(crewTeamJbd);
			}
			clipboard.clear();
			Bisbin.goToMainField();
			wait.ms(500);
			deltaLayerWindow(targetClassJbd, commentJbd);
			wait.ms(500);
			deltaMarkName(nameOfBch, targetClassJbd, dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
			wait.ms(500);
			deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
			wait.ms(500);
			deltaNumberOfnumberWindow(twoHundredth, threeHundredth);
			wait.ms(500);
			deltaCombatCapabilityWindow(targetClassJbd, establishedJbd, commentJbd);
			wait.ms(500);
			deltaIdentificationWindow(targetClassJbd, establishedJbd, commentJbd);
			wait.ms(500);
			Bisbin.reliabilityWindow();
			wait.ms(500);
			Bisbin.flyEye();
			wait.ms(500);
			deltaIdPurchaseText(idTargetJbd);
			wait.ms(500);
			deltaMobilityLine(targetClassJbd);
			wait.ms(500);
			deltaCommentContents(targetClassJbd, dateJbd, timeJbd, crewTeamJbd, establishedJbd, commentJbd, mgrsCoords);
			wait.ms(500);
			deltaAdditionalFields(idTargetJbd, targetClassJbd);
			wait.ms(500);
			deltaGeografPlace(targetClassJbd, establishedJbd, commentJbd);
			wait.ms(500);
			Bisbin.goToAttachmentFiles();
		}
		// тіло для створення мітки з подавленням від РЕБ та РЕР
		static void createREBandRER(string clipboardData) {
			string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
			string dateJbd = parts[4]; // 27.07.2024
			string dateDeltaFormat = dateJbd.Replace('.', '/'); // перетворення дати до формату дельти
			string timeJbd = parts[5]; // 00:40
			string mgrsX = parts[8];
			string mgrsY = parts[9];
			string mgsrCoord = $"{mgrsX}{mgrsY}";
			string layerName = "Крила, FPV, повітр";
			string name = "FPV (Подавлено)";
			string capability = "небо";
			string identyfication = "ворож";
			string comment = $"{dateJbd} {timeJbd} - подавлено та знищено засобами роти РЕБ 414 ОПБС";
			string bplaName = "вертикального зльоту";
			
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			//. перехід по корам
			var searchWindow = w.Elm["web:COMBOBOX", prop: new("@aria-label=Пошук", "@placeholder=Знайти адресу або координату")].Find(1);
			searchWindow.PostClick();
			keys.sendL("Ctrl+A", "!" + mgsrCoord, "Enter");
			//.. 
			wait.ms(2000);
			//. ставимо мітку
			var createButton = w.Elm["web:LISTITEM", prop: "@data-testid=create-object"].Find(1);
			createButton.PostClick(scroll: 250);
			wait.ms(2000);
			// обираємо мітку
			var categorySearch = w.Elm["web:TEXT", "Пошук об'єктів", "@placeholder=Пошук об'єктів"].Find(1);/*image:WkJNG30IAAQib/e/D18VodkEU3Qz/YNqUgY6OJ7L1Q+3gJNILEgpxhjD12oFzY7vxBOVS2vBUCWLbk9njc47QWXpevusAg==*/
			categorySearch.PostClick();
			keys.sendL("Ctrl+A", "!" + bplaName);
			wait.ms(3000);
			var bplaMark = w.Elm["web:LISTITEM", "Військовий повітряний засіб БПЛА вертикального зльоту / посадки (VT-UAV)"].Find(1);
			bplaMark.PostClick();
			wait.ms(2000);
			//..
			//. шар
			var layerWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-layer"].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZR0nadV0+yBPiwAnHA8YgotQSDhyP7wChrDn1jbTQmGQVHNZtv0fn3SL8Lp/TAg==*/
			layerWindow.PostClick(scroll: 300);
			keys.sendL("Ctrl+A", "!" + layerName, "Enter");
			//..
			wait.ms(400);
			//. назва
			var nameOfMarkWindow = w.Elm["web:TEXT", prop: "@data-testid=T"].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZJ1HadVdc0HlCGjjheOyxBBGllpBf6R0glLXrfCMPFDo5BUd0e9rZKu9u6HE8Pg==*/
			nameOfMarkWindow.PostClick(scroll: 300);
			keys.sendL("Ctrl+A", "!" + name, "Enter");
			//..
			wait.ms(400);
			//. час виявлення
			deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
			//..
			wait.ms(400);
			//. боєздатність
			var combatCapabilityWindow = w.Elm["web:GROUPING", prop: "@data-testid=operational-condition-select"].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZJ1HadVdc0HlCGjjheOyxBBGldiG6XOEdIJS163wjDxQ6OQqO6Pb0bJV3N/Q4Pp/VAA==*/
			combatCapabilityWindow.PostClick(scroll: 250);
			keys.sendL("Ctrl+A", "!" + capability, "Enter*2");
			//..
			wait.ms(400);
			//. ідентифікація
			var identificationWindow = /*image:WkJNG30IAAQib/e/D18VodkEkm7zSdEkLQAnHA8Ygyzg5AB+cDzYGWMMX6sVNHG+E01UHq2CISWzbtvW6NwdpP5G+JwaAA==*/w.Elm["STATICTEXT", "Ідентифікація", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZJ1HadVdc0HlCGjjheOyxBBGldiE7HP0dIJS163wjDxQ6OQqO6Pb0bJV3N/Q4Pp/VAA==*/
			identificationWindow.PostClick(scroll: 250);
			keys.sendL("Ctrl+A", "!" + identyfication, "Enter");
			//..
			wait.ms(400);
			//. коментар
			var commentWindow = w.Elm["web:TEXT", "Введіть значення", "@data-testid=comment-editing__textarea"].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZJ1HadVdc0HlCGjjheOyxBBGllpBf6R0glLXrfCMPFDo5BUd0e7rZKu9u6HFcPg==*/
			commentWindow.PostClick(scroll: 250);
			keys.sendL("Ctrl+A", "!" + comment);
			wait.ms(400);
			var commentAsseptButton = w.Elm["web:BUTTON", prop: "@data-testid=comment-editing__button-save"].Find(-1);/*image:WkJNGxUEAMSHv+a7a722w1tUYzRY+vgABzifyAY2mkdiJH5bHj97WVkYSXdJMJzQRAaJ6Ax2w4Hb0Hl5RQRFq1zXWaQGwIkszElhuTPE+ViliA87u4v7eonTfApCCG4LG//dFu/NCkWXnu+Go8hnbbsxrLmtULTt7kpKLWg+STdJNFLi0yRd3QA=*/
			commentAsseptButton.PostClick(scroll: 250);
			//..
			
		}
		// створення файлу для імпорта з Чергування - 777
		static void createWhoWork(string clipboardData) {
			
			string[] parts = clipboardData.Split('\n'); // Розділяємо рядок на частини
			string dateTimeNow = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"); // поточний час
			var features = new List<Object>(); //
			string plassEror = string.Empty; // для подальшої перевірки
			
			foreach (string item in parts) {
				string[] elements = item.Split('\t'); // ділимо рядок на елементи
				if (elements.Length < 10) continue; // Пропускаємо, якщо елементів недостатньо
				if (elements[10].Length > 5) {
					// Парсимо елементи з буфера обміну
					string sidc = string.Empty;
					string outlineСolor = ""; // колір обведення
					
					if (elements[1].Contains("група")) { // якщо це евак
						sidc = "10032500003211000000";
					} else if (elements[1].Contains("Маві")) { // якщо це мавік
						sidc = "10030120001104000000";
						outlineСolor = "#4dc04d";
					} else if (elements[1].Contains("(FPV)")) { //  або фпв
						sidc = "10030120001104000000";
						outlineСolor = "#3bd5e7";
					} else if (Regex.IsMatch(elements[1], @"\bП\d{2}\b")) { // якщо ти бомбер або дартс
						sidc = "10030120001104000000";
						outlineСolor = "#597380";
					} else { // інші
						sidc = "10010120001103000000";
						outlineСolor = "#ff9327";
					}
					
					string name = $"{elements[1]} Т.в. ({elements[2]})";
					
					//координати обробка
					var wgsCoord = Bisbin.ConvertMGRSToWGS84(elements[10]);
					
					// Формуємо JSON для однієї мітки (Feature) вручну
					var feature = new StringBuilder();
					feature.AppendLine("{");
					feature.AppendLine("  \"type\": \"Feature\",");
					feature.AppendLine("  \"properties\": {");
					feature.AppendLine($"    \"sidc\": \"{sidc}\",");
					feature.AppendLine($"    \"name\": \"{name}\",");
					feature.AppendLine($"    \"observation_datetime\": \"{dateTimeNow}\",");
					feature.AppendLine($"    	\"outline-color\": \"{outlineСolor}\"");
					feature.AppendLine("  },");
					feature.AppendLine("  \"geometry\": {");
					feature.AppendLine("    \"type\": \"Point\",");
					feature.AppendLine($"    \"coordinates\": [{wgsCoord}]").Replace("(","").Replace(")","");
					feature.AppendLine("  }");
					feature.AppendLine("}");
					
					features.Add(feature.ToString());
				} else {
					Console.WriteLine($"речення {elements[1]} Т.в. ({elements[2]}) не містить mgrs координат");
					plassEror += $"\r речення {elements[1]} Т.в. ({elements[2]}) не містить mgrs координат";
				}
			};
			
			// Формуємо повний JSON для FeatureCollection
			var geoJson = new StringBuilder();
			geoJson.AppendLine("{");
			geoJson.AppendLine("  \"type\": \"FeatureCollection\",");
			geoJson.AppendLine("  \"features\": [");
			geoJson.AppendLine(string.Join(",\n", features));
			geoJson.AppendLine("  ]");
			geoJson.AppendLine("}");
			
			// Шлях до робочого столу користувача
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string fileName = $"{DateTime.Now.ToString("dd mmss")} - layer 777.geojson"; // Формування назви файлу
			string finalComment = string.Empty; // для подальшої перевірки
			try {
				// Повний шлях до файлу, який ми хочемо створити
				string filePath = Path.Combine(desktopPath, fileName);
				
				// Записуємо рядок у файл
				File.WriteAllText(filePath, geoJson.ToString());
				
				finalComment = $"Файл {fileName} успішно створено на робочому столі.";
			}
			catch (Exception ex) {
				Console.WriteLine($"Виникла помилка при створенні файлу: {ex.Message}");
				finalComment = $"Виникла помилка при створенні файлу: {ex.Message}";
			}
			
			// вікно діалогу
			var b = new wpfBuilder("Window").WinSize(650);
			b.R.Add(out Label _, plassEror);
			b.R.Add(out Label _, finalComment);
			b.R.AddOkCancel();
			b.Window.Topmost = true;
			b.End();
			// show dialog. Exit if closed not with the OK button.
			if (!b.ShowDialog()) return;
			
		}
		// обрати відповідний шар
		static void deltaLayerWindow(string targetClassJbd, string commentJbd) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// (01) постійні схов. і укриття
			string permanentStorage = "Укриття Склад майна Склад БК Склад ПММ Польовий склад майна Польовий склад БК Польовий склад ПММ";
			// (02) антени, камери...
			string antennaCamera = "Мережеве обладнання Камера Антена РЕБ (окопні)";
			
			// поле шар
			var layerWindow = w.Elm["web:GROUPING", prop: "@data-testid=select-layer"].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZR0nadV0+yBPiwAnHA8YgotQSDhyP7wChrDn1jbTQmGQVHNZtv0fn3SL8Lp/TAg==*/
			if (layerWindow != null) {
				layerWindow.PostClick(scroll: 250);
				//. перевірка, запис
				if (permanentStorage.Contains(targetClassJbd)) {
					keys.sendL("Ctrl+A", "!постійні схов.", "Enter");
					return;
				}
				if (antennaCamera.Contains(targetClassJbd)) {
					keys.sendL("Ctrl+A", "!антени, камери", "Enter");
					return;
				}
				switch (targetClassJbd) {
				case "Міна":
					keys.sendL("Ctrl+A", "!маршрути, міни, загородж", "Enter");
					break;
				case "Загородження":
					keys.sendL("Ctrl+A", "!маршрути, міни, загородж", "Enter");
					break;
				case "Бліндаж":
					keys.sendL("Ctrl+A", "!траншеї і бліндажі", "Enter");
					break;
				case "Т. вильоту дронів":
					keys.sendL("Ctrl+A", "!т. вильоту дронів", "Enter");
					break;
				case "ОС РОВ":
					keys.sendL("Ctrl+A", "!особовий склад РОВ", "Enter");
					break;
				case "Міномет":
					keys.sendL("Ctrl+A", "!окопна зброя (+ міномети)", "Enter");
					break;
				default:
					if (commentJbd.ToLower().Contains("рус") || commentJbd.ToLower().Contains("рух")) {
						keys.sendL("Ctrl+A", "!техніка в русі ", "Enter");
					} else if (commentJbd.ToLower().Contains("виходи") || commentJbd.ToLower().Contains("вогнева позиція")) {
						keys.sendL("Ctrl+A", "!вогневі позиції ", "Enter");
					} else {
						keys.sendL("Ctrl+A", "!схована техніка ", "Enter");
					}
					break;
				}
				//..
			}
			
		}
		// відповідна назва
		static void deltaMarkName(string nameOfBch, string targetClassJbd, string dateJbd, string establishedJbd, string commentJbd, string twoHundredth, string threeHundredth) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле назва
			var nameOfMarkWindow = w.Elm["web:TEXT", prop: "@data-testid=T"].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZJ1HadVdc0HlCGjjheOyxBBGllpBf6R0glLXrfCMPFDo5BUd0e9rZKu9u6HE8Pg==*/
			if (nameOfMarkWindow != null) {
				string markName = string.Empty;
				string nameOfMark = nameOfMarkWindow.Value;
				int indexLoss = nameOfMark.IndexOf(' ');
				string states = "Розміновано Підтв. ураж. Тільки розрив";
				
				switch (targetClassJbd) {
				//. Міна
				case "Міна":
					if (establishedJbd.Contains("Спростовано")) {
						return;
					}
					if (establishedJbd.Contains("Авар. скид")) {
						markName = $"{nameOfBch} ({dateJbd})";
					} else if (states.Contains(establishedJbd)) {
						markName = $"{nameOfMark.Substring(0, indexLoss)} ({dateJbd})";
					} else {
						if (nameOfBch.Contains("ПТМ-3")) {
							markName = $"{nameOfBch} до ({Bisbin.datePlasDays(dateJbd, 90)})";
						} else {
							markName = $"{nameOfBch} до ()";
						}
					}
					
					break;
				//..
				//. "Укриття
				case "Укриття":
					if (establishedJbd.ToLower().Contains("знищ")) {
						markName = targetClassJbd + " ОС (знищ.)";
					} else if (establishedJbd.ToLower().Contains("ураж")) {
						markName = targetClassJbd + " ОС (ураж.)";
					} else if (establishedJbd.Contains("Виявлено") || establishedJbd.Contains("Підтверджено") || establishedJbd.Contains("Не зрозуміло")) {
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
				case "ОС РОВ":
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
					//.
					if (establishedJbd.ToLower().Contains("знищ")) {
						markName = targetClassJbd + " (знищ.)";
					} else if (establishedJbd.ToLower().Contains("ураж")) {
						markName = targetClassJbd + " (ураж.)";
					} else if (establishedJbd.Contains("Виявлено") || establishedJbd.Contains("Підтверджено") || establishedJbd.Contains("Не зрозуміло")) {
						if (commentJbd.ToLower().Contains("знищ")) {
							markName = targetClassJbd + " (знищ.)";
						} else if (commentJbd.ToLower().Contains("ураж")) {
							markName = targetClassJbd + " (ураж.)";
						} else if (commentJbd.ToLower().Contains("в рус") || commentJbd.ToLower().Contains("рух")) {
							markName = targetClassJbd + " (в русі)";
						} else if (commentJbd.ToLower().Contains("схов")) {
							markName = targetClassJbd + " (схов.)";
						} else if (commentJbd.ToLower().Contains("стої")) {
							markName = targetClassJbd + " (стоїть)";
						} else if (commentJbd.ToLower().Contains("виходи") || commentJbd.ToLower().Contains("вогнева позиція")) {
							markName = targetClassJbd + " (вогнева позиція)";
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
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// дата
			var dateDeltaWindow = w.Elm["web:TEXT", "дд/мм/рррр", "@data-testid=W"].Find(-1);/*image:WkJNGzUEAOR/xuU/vE5kIp0zqM4GYGZaXGJorithdZY7vWM/wAd8wAcJwoQSaV5mbWmEAWlFdLLdJ0mwEVSDSYmGSkVkU0VWQcCylTHzRi8TQMhmhOOOk+PtM4iWRtByWOHqeAb1ah8SsQhcnh7BTrsA5YwPQrYALIdjqxYa8HB/C61jX3fjJCaxOAkoGGq1oG1Fbvt3e/EU9JZJ0qQqtx5EO04qkWnzPhWJH8N+4oegy9rjO71fxvZj8zm53QCFlfT3Dw==*/
			if (dateDeltaWindow != null) {
				dateDeltaWindow.PostClick();
				keys.sendL("Ctrl+A", "!" + dateDeltaFormat);
			}
			wait.ms(500);
			// час
			var timeDeltaWindow = w.Elm["web:TEXT", "гг:хх", "@data-testid=W-time-input"].Find(-1);/*image:WkJNGzUEAMSKNd3A+MSGW8UKARWDY3KoQnSZia1iBYTqm4lr8gd9YzXICz5FG1aCLkUizyf2IiuBsQxhNxvCNp2Xl0RUdETr0ARPLiptATgA/oBkZGFzU8mbvxvi5oqV6SGkR4fi2liMrq5OMFcGFCiOsVaW7MbuJwrytXj5PvKJUuLxd7gKi0GH959vVBeUoDN71zgTGbCX4e/TGVg7iyMDs3b566Xm/YkyDRuB7/OFj2E/sV5Q5rpsE1GmZXC8ZzlBCTYGWfbWSTMy3OGhVRCZLbC2JRiGjZ2t/4T1QnvS1W3fwXuTZjyuYihhoXl+Crw3HfheJd96A7o+jsKn6/lxqjh7h293zB/9aRI=*/
			if (dateDeltaWindow != null) {
				timeDeltaWindow.PostClick();
				keys.sendL("Ctrl+A", "!" + timeJbd, "Enter");
				
			}
		}
		// поле кількість
		static void deltaNumberOfnumberWindow(string twoHundredth, string threeHundredth) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле кількість
			var numberOfnumberWindow = w.Elm["web:SPINBUTTON", prop: new("@data-testid=C", "@type=number")].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZJ1HadVdc0HlCGjjheOyxBBElfJIL+VXeAUJZu8438kChk6vgiG5Pn63y7oYex+czAg==*/
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
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле боєздатність
			var combatCapabilityWindow = w.Elm["web:GROUPING", prop: "@data-testid=operational-condition-select"].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZJ1HadVdc0HlCGjjheOyxBBGldiG6XOEdIJS163wjDxQ6OQqO6Pb0bJV3N/Q4Pp/VAA==*/
			//. перевірка
			if (combatCapabilityWindow != null) {
				string fullaim = string.Empty;
				string states = "Розміновано Підтв. ураж. Тільки розрив Авар. скид";
				switch (targetClassJbd) {
				//. Якщо ти міна
				case "Міна":
					if (states.Contains(establishedJbd)) {
						fullaim = "небо";
					} else if (establishedJbd.Contains("Встановлено")) {
						fullaim = "повніс";
					} else if (establishedJbd.Contains("Спростовано")) {
						return;
					} else {
						fullaim = "част";
					}
					break;
				//..
				default:
					//.
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
					//..
				}
				//..
				combatCapabilityWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + fullaim, "Enter");
			}
		}
		// ідентифікація 
		static void deltaIdentificationWindow(string targetClassJbd, string establishedJbd, string commentJbd) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// ідетнифікація поле
			var identificationWindow = /*image:WkJNG30IAAQib/e/D18VodkEkm7zSdEkLQAnHA8Ygyzg5AB+cDzYGWMMX6sVNHG+E01UHq2CISWzbtvW6NwdpP5G+JwaAA==*/w.Elm["STATICTEXT", "Ідентифікація", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZJ1HadVdc0HlCGjjheOyxBBGldiE7HP0dIJS163wjDxQ6OQqO6Pb0bJV3N/Q4Pp/VAA==*/
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
		// зауваження штабу - ід
		static void deltaIdPurchaseText(string idTargetJbd) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// зауваження штабу поле
			var idPurchaseWindow = w.Elm["web:TEXT", prop: "@data-testid=G", flags: EFFlags.HiddenToo].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZJ1HadVdc0HlCGjjheMAYRJRawoHb+R0glDXrfCMNFDpZBYd1289WeXcTfg+f0wA=*/
			if (idPurchaseWindow != null) {
				idPurchaseWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + idTargetJbd);
			}
		}
		// мобільність
		static void deltaMobilityLine(string targetClassJbd) {
			// Обмеженої прохідності
			string limitedAccess = "Мотоцикл Вантажівка Паливозаправник";
			string obmezheno = "обмежено";
			// Позашляховик
			string pozashlyakhovyk = "Авто БТР Військ. баггі";
			string suv = "позашлях";
			// Гусеничний - колісний
			string caterpillar = "ЗРК РСЗВ САУ КШМ Інж. техніка";
			string husenychnyy = "комбінов";
			//На буксирі
			string towTruck = "Гармата Гаубиця";
			string buksyri = "буксир";
			// Гусинечний
			string gusankaList = "Танк БМП МТ-ЛБ БМД";
			string gusanka = "Гусеничн";
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле мобільності
			var mobileLine = w.Elm["STATICTEXT", "Мобільність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1);/*image:WkJNG30IAAQib/e/D18VodkEkm4jE1SbVIETjgeMQRZwcgA/uB30jDGGr9UKmrm/E3VkLqWCISmybttGq7yzCP1N4HPqAA==*/
			if (mobileLine != null) {
				var checking = w.Elm["STATICTEXT", "Мобільність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);/*image:WkJNG30IAAQib/e/D18VodkEkm4jE1SbVIETjgeMQRZwcgA/uB30jDGGr9UKmrm/E3VkLqWCISmybttGq7yzCP1N4HPqAA==*/
				if (checking.Name != "Мобільність") {
					return;
				}
				// Обмеженої прохідності
				if (limitedAccess.Contains(targetClassJbd)) {
					mobileLine.PostClick(scroll: 250);
					keys.sendL("Ctrl+A", "!" + obmezheno, "Enter");
					return;
				}
				// Позашляховик
				if (pozashlyakhovyk.Contains(targetClassJbd)) {
					mobileLine.PostClick(scroll: 250);
					keys.sendL("Ctrl+A", "!" + suv, "Enter");
					return;
				}
				// Гусеничний - колісний
				if (caterpillar.Contains(targetClassJbd)) {
					mobileLine.PostClick(scroll: 250);
					keys.sendL("Ctrl+A", "!" + husenychnyy, "Enter");
					return;
				}
				// На буксирі
				if (towTruck.Contains(targetClassJbd)) {
					mobileLine.PostClick(scroll: 250);
					keys.sendL("Ctrl+A", "!" + buksyri, "Enter");
					return;
				}
				// Гусинечний
				if (gusankaList.Contains(targetClassJbd)) {
					mobileLine.PostClick(scroll: 250);
					keys.sendL("Ctrl+A", "!" + gusanka, "Enter");
					return;
				}
			}
		}
		// коментар
		static void deltaCommentContents(string targetClassJbd, string dateJbd, string timeJbd, string crewTeamJbd, string establishedJbd, string commentJbd, string mgrsCoords) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			string commentContents = $"{dateJbd} {timeJbd} - ";
			// коментар
			var commentWindow = w.Elm["web:TEXT", "Введіть значення", "@data-testid=comment-editing__textarea"].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZJ1HadVdc0HlCGjjheOyxBBGllpBf6R0glLXrfCMPFDo5BUd0e7rZKu9u6HFcPg==*/
			if (commentWindow != null) {
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
					} else if (establishedJbd.Contains("Підтв. ураж.")) {
						commentContents += $"підрив на міні, **кори**, спостерігали з {crewTeamJbd}";
						commentWindow.PostClick(scroll: 250);
						keys.sendL("Ctrl+A", "!" + commentContents);
						script.end();
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
				commentWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + commentContents);
				wait.ms(500);
				// кнопка коментаря
				var commentAsseptButton = w.Elm["web:BUTTON", prop: "@data-testid=comment-editing__button-save"].Find(-1);/*image:WkJNGxUEAMSHv+a7a722w1tUYzRY+vgABzifyAY2mkdiJH5bHj97WVkYSXdJMJzQRAaJ6Ax2w4Hb0Hl5RQRFq1zXWaQGwIkszElhuTPE+ViliA87u4v7eonTfApCCG4LG//dFu/NCkWXnu+Go8hnbbsxrLmtULTt7kpKLWg+STdJNFLi0yRd3QA=*/
				commentAsseptButton.PostClick(scroll: 250);
			}
			
		}
		// додаткові поля
		static void deltaAdditionalFields(string idTargetJbd, string targetClassJbd) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// додаткові поля
			Bisbin.goToAdditionalField();
			//примітки штабу - (поле назва та примітки мають одниковість тест ід)
			var notesWindow = w.Elm["web:TEXT", prop: new("@data-testid=string-field__input", "@name=Зауваження штабу")].Find(-1);/*image:WkJNG/0DAATCdr9tIAMZJ1HadVdc0HlCGjjheOyxBBGldiG6XOEdIJS163wjDxQ6OQqO6Pb0bJV3N/Q4Pp/VAA==*/
			if (notesWindow != null) {
				notesWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + idTargetJbd, "Enter");
			}
		}
		// Георафічне розташування
		static void deltaGeografPlace(string targetClassJbd, string establishedJbd, string commentJbd) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// Георафічне розташування
			Bisbin.goToGeograficalPlace();
			string states = "Виявлено Підтверджено Спростовано Не зрозуміло";
			// Колір заливки
			var deltaColorFills = w.Elm["web:STATICTEXT", "Колір заливки"].Find(-1);
			if (deltaColorFills != null) {
				switch (targetClassJbd) {
				//. укриття
				case "Укриття":
					if (establishedJbd.ToLower().Contains("знищ") || establishedJbd.ToLower().Contains("ураж")) {
						// колір жовтий
						var placeColorYellowButton = w.Elm["web:BUTTON", prop: "@title=#ffeb3b"].Find(1);
						placeColorYellowButton.PostClick(scroll: 250);
						wait.ms(500);
						// відсоток прозрачності
						var transpatentColorRange = w.Elm["web:SLIDER", prop: "@data-testid=slider"].Find(1);
						transpatentColorRange.PostClick(scroll: 250);
						transpatentColorRange.SendKeys("Left*5");
						wait.ms(500);
					} else if (states.Contains(establishedJbd)) {
						if (commentJbd.ToLower().Contains("знищ") || commentJbd.ToLower().Contains("ураж")) {
							// колір жовтий
							var placeColorYellowButton = w.Elm["web:BUTTON", prop: "@title=#ffeb3b"].Find(1);
							placeColorYellowButton.PostClick(scroll: 250);
							wait.ms(500);
							// відсоток прозрачності
							var transpatentColorRange = w.Elm["web:SLIDER", prop: "@data-testid=slider"].Find(1);
							transpatentColorRange.PostClick(scroll: 250);
							transpatentColorRange.SendKeys("Left*5");
							wait.ms(500);
						} else {
							//колір червоний - ворож
							var placeColorRedButton = w.Elm["web:BUTTON", prop: "@title=#f44336"].Find(1);
							placeColorRedButton.PostClick(scroll: 250);
							wait.ms(500);
							// відсоток прозрачності
							var transpatentColorRange = w.Elm["web:SLIDER", prop: "@data-testid=slider"].Find(1);
							transpatentColorRange.PostClick(scroll: 250);
							transpatentColorRange.SendKeys("Left*5");
							wait.ms(500);
						}
					} else {
						//колір червоний - ворож
						var placeColorRedButton = w.Elm["web:BUTTON", prop: "@title=#f44336"].Find(1);
						placeColorRedButton.PostClick(scroll: 250);
						wait.ms(500);
						// відсоток прозрачності
						var transpatentColorRange = w.Elm["web:SLIDER", prop: "@data-testid=slider"].Find(1);
						transpatentColorRange.PostClick(scroll: 250);
						transpatentColorRange.SendKeys("Left*5");
						wait.ms(500);
					}
					break;
				//..
				default:
					break;
				}
			}
		}
		
	}
}
