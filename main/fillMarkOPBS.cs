/*/ c \analisationWork-main\globalClass\Bisbin.cs; /*/

/* 05.12.2024 2.1

* id обрізаються, щоб поміститись в рядок 
* функція додавання до дати дні(x) підходить для мін
* 200 та 300 рахуються
* координата в коментар для укриття
* до fpv додається тип борту f7
* слово "виходи, вогнева позиція" 

*/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CSLight {
	
	class Program {
		static void Main() {
			opt.key.KeySpeed = 20;
			opt.key.TextSpeed = 20;
			
			keys.send("Ctrl+C"); //копіюємо код
			wait.ms(100);
			string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
			// вікно діалогу
			var b = new wpfBuilder("Window").WinSize(450);
			//Brush
			b.Font(size: 17, bold: true);
			b.Brush(Brushes.DarkGray);
			// insider
			b.R.AddButton("1. Заповнення мітки", 1).Brush(Brushes.LightCoral);
			b.R.AddButton("2. Створення РЕБ та РЕР мітки", 2).Brush(Brushes.LightCoral);
			b.R.AddButton("3. Файл імпорта для 777 міток", 3).Brush(Brushes.LightGoldenrodYellow);
			b.R.AddButton("4. Файл імпорта з МІНАМИ", 4).Brush(Brushes.LightGoldenrodYellow);
			b.R.AddButton("5. Файл імпорта - обізнаність ворога й всяке", 5).Brush(Brushes.LightGreen);
			b.R.AddOkCancel().Font(size: 14, bold: false);
			b.Window.Topmost = true;
			b.End();
			// show dialog. Exit if closed not with the OK button.
			if (!b.ShowDialog()) return;
			
			if (b.ResultButton == 1) {
				fillMarkWithJBD(clipboardData);
				return;
			}
			if (b.ResultButton == 2) {
				createREBandRER(clipboardData);
				return;
			}
			if (b.ResultButton == 3) {
				createWhoWork(clipboardData);
				return;
			}
			if (b.ResultButton == 4) {
				createImportFileToMine(clipboardData);
				return;
			}
			if (b.ResultButton == 5) {
				Console.WriteLine("Рано, ще не порацював концепцію під усі обізнаності");
				return;
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
			//. ставимо мітку
			var createButton = w.Elm["LISTITEM", "Створити об'єкт", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(1);
			createButton.PostClick(scroll: 250);
			wait.ms(2000);
			// обираємо мітку
			var categorySearch = w.Elm["TEXT", "Пошук об'єктів", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(1);
			categorySearch.PostClick();
			keys.sendL("Ctrl+A", "!" + bplaName);
			wait.ms(3000);
			var bplaMark = w.Elm["TEXT", "Пошук об'єктів", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2 child"].Find(1);
			bplaMark.PostClick();
			wait.ms(2000);
			//..
			//. шар
			var layerWindow = w.Elm["STATICTEXT", "Шар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1);
			layerWindow.PostClick(scroll: 300);
			keys.sendL("Ctrl+A", "!" + layerName, "Enter");
			//..
			wait.ms(400);
			//. назва
			var nameOfMarkWindow = w.Elm["STATICTEXT", "Назва", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next1"].Find(-1);
			nameOfMarkWindow.PostClick();
			keys.sendL("Ctrl+A", "!" + name, "Enter");
			//..
			wait.ms(400);
			//. час виявлення
			deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
			//..
			wait.ms(400);
			//. боєздатність
			var combatCapabilityWindow = w.Elm["STATICTEXT", "Боєздатність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1);
			combatCapabilityWindow.PostClick(scroll: 250);
			keys.sendL("Ctrl+A", "!" + capability, "Enter*2");
			//..
			wait.ms(400);
			//. ідентифікація
			var identificationWindow = w.Elm["STATICTEXT", "Ідентифікація", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1);
			identificationWindow.PostClick(scroll: 250);
			keys.sendL("Ctrl+A", "!" + identyfication, "Enter");
			//..
			wait.ms(400);
			//. коментар
			var commentWindow = w.Elm["STATICTEXT", "Новий коментар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1);
			commentWindow.PostClick(scroll: 250);
			keys.sendL("Ctrl+A", "!" + comment);
			wait.ms(400);
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
				if (elements[3].Length > 5) {
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
					} else if (elements[1].Contains("РЛС") || (elements[1].Contains("РЕБ"))) { // реб рлс
						sidc = "10031000001505040000";
					} else { // інші
						sidc = "10010100001103000000";
						outlineСolor = "#ff9327";
					}
					
					string name = $"{elements[1]} Т.в. ({elements[2]})";
					
					//координати обробка
					var wgsCoord = Bisbin.ConvertMGRSToWGS84(elements[3]);
					
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
					feature.AppendLine($"    \"coordinates\": [{wgsCoord}]").Replace("(", "").Replace(")", "");
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
		// файл з мінами
		static void createImportFileToMine(string clipboardData) {
			
			/*
				для птм, тм Протитанкова міна (ПТМ) - 10011500002103000000
				дружня - 10031500002103000000
					повність боездатна - 10031520002103000000
					частково боездатна - 10031530002103000000
					не боездатна - 10031540002103000000
				для ппм Міна-пастка - 10011500002003000000
					повність боездатна - 10011520002003000000
					частково боездатна - 10011530002003000000
					не боездатна - 10011540002003000000
				
			*/
			
			string[] parts = clipboardData.Split('\n'); // Розділяємо рядок на частини
			var features = new List<Object>(); //
			string plassEror = string.Empty; // для подальшої перевірки
			
			foreach (string item in parts) {
				string[] elements = item.Split('\t'); // ділимо рядок на елементи
				
				// Присвоюємо змінним відповідні значення
				string dateJbd = elements[0]; // 27.07.2024
				string timeJbd = elements[1]; //00:40
				string commentJbd = elements[2].Replace("\n", " "); //коментар (для ідентифікації скоріш за все)
				string numberOFlying = elements[3]; // 5
				string crewTeamJbd = Bisbin.TrimAfterDot(elements[4].Replace("\n\t", "")); // R-18-1 (Мавка)
				string whatDidJbd = elements[5]; // Дорозвідка / Мінування ..
				string targetClassJbd = elements[7]; // Міна/Вантажівка/...
				string idTargetJbd = Bisbin.TrimNTwonyString(elements[9], 19); // Міна 270724043
				string mgrsCoords = elements[18]; // 37U CP 76420 45222
				string nameOfBch = elements[22]; // ПТМ-3 ТМ-62
				string establishedJbd = elements[24]; // Встановлено/Уражено/Промах/...
				string twoHundredth = elements[25]; // 200
				string threeHundredth = elements[26]; // 300
				
				// захист від дурачка
				if (whatDidJbd.Contains("Мінування")) {
					// підготовка значень для полів
					string name = Bisbin.createMineName(nameOfBch, targetClassJbd, dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
					string sidc = string.Empty;
					string states = "Розміновано Підтв. ураж. Тільки розрив Авар. скид";
					if (states.Contains(establishedJbd)) {
						if (name.Contains("ППМ")) {
							sidc = "10011540002003000000";
						} else { sidc = "10031540002103000000"; }
					} else if (establishedJbd.Contains("Встановлено")) {
						if (name.Contains("ППМ")) {
							sidc = "10011520002003000000";
						} else { sidc = "10031520002103000000"; }
					} else if (establishedJbd.Contains("Спростовано")) {
						return;
					} else {
						if (name.Contains("ППМ")) {
							sidc = "10011530002003000000";
						} else { sidc = "10031530002103000000"; }
					}
					
					
					string commentar = Bisbin.createComment(targetClassJbd, dateJbd, timeJbd, crewTeamJbd, establishedJbd, commentJbd, mgrsCoords);
					string dateTimeNow = $"{dateJbd.Split(".")[2]}-{dateJbd.Split(".")[1]}-{dateJbd.Split(".")[0]}T{timeJbd}:22"; // поточний час yyyy-MM-ddTHH:mm:ss
					//координати обробка
					var wgsCoord = Bisbin.ConvertMGRSToWGS84(mgrsCoords);
					
					// Формуємо JSON для однієї мітки (Feature) вручну
					var feature = new StringBuilder();
					feature.AppendLine("{");
					feature.AppendLine("  \"type\": \"Feature\",");
					feature.AppendLine("  \"properties\": {");
					feature.AppendLine($"	\"sidc\": \"{sidc}\","); // номер мітки (мітка) 
					feature.AppendLine($"	\"name\": \"{name}\","); // назва
					feature.AppendLine($"	\"reliability_credibility\": \"A2\","); // достовірність
					feature.AppendLine($"	\"platform_type\": \"AIRREC\",");
					feature.AppendLine($"	\"staff_comments\": \"{idTargetJbd}\","); // ід
					feature.AppendLine($"	\"observation_datetime\": \"{dateTimeNow}\","); // дата-час
					feature.AppendLine($"	\"quantity\": \"1\","); // кількість
					feature.AppendLine($"	\"comments\": [");
					feature.AppendLine($" 		\"{commentar}\"");
					feature.AppendLine($"	]");
					feature.AppendLine("  },");
					feature.AppendLine("  \"geometry\": {");
					feature.AppendLine("    \"type\": \"Point\",");
					feature.AppendLine($"    \"coordinates\": [{wgsCoord}]").Replace("(", "").Replace(")", "");
					feature.AppendLine("  }");
					feature.AppendLine("}");
					
					features.Add(feature.ToString());
				}
				
			}
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
			string fileName = $"mines_marks - {DateTime.Now.ToString("dd mmss")}.geojson"; // Формування назви файлу
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
			var layerWindow = w.Elm["STATICTEXT", "Шар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1);
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
		// відповідна назва
		static string deltaMarkName(string nameOfBch, string targetClassJbd, string dateJbd, string establishedJbd, string commentJbd, string twoHundredth, string threeHundredth) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			string nameIs = string.Empty;
			// поле назва
			var nameOfMarkWindow = w.Elm["STATICTEXT", "Назва", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next1"].Find(-1);
			if (nameOfMarkWindow != null) {
				string markName = string.Empty;
				string nameOfMark = nameOfMarkWindow.Value;
				int indexLoss = nameOfMark.IndexOf(' ');
				string states = "Розміновано Підтв. ураж. Тільки розрив";
				string minesNameLarge = "ПТМ-3 ТМ-62"; // 90 днів
				string minesNameLess = ""; // заготовка
				
				switch (targetClassJbd) {
				//. Міна
				case "Міна":
					if (establishedJbd.Contains("Спростовано")) {
						return "";
					}
					if (establishedJbd.Contains("Авар. скид")) {
						markName = $"{nameOfBch} ({dateJbd})";
					} else if (states.Contains(establishedJbd)) {
						markName = $"{nameOfMark.Substring(0, indexLoss)} ({dateJbd})";
					} else {
						if (minesNameLarge.Contains(nameOfBch)) {
							markName = $"{nameOfBch} до ({Bisbin.datePlasDays(dateJbd, 90)})";
						} else if (nameOfBch.Contains("ППМ")) {
							markName = $"{nameOfBch} до ({Bisbin.datePlasDays(dateJbd, 8)})";
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
					if (twoHundredth.Length == 0 || threeHundredth.Length == 0) {
						markName = targetClassJbd;
					}
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
				nameIs = markName;
			}
			return nameIs;
			
		}
		// поле дата / час
		static void deltaDateLTimeWindow(string dateDeltaFormat, string timeJbd) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");// дата
			var dateDeltaWindow = w.Elm["TEXT", "дд/мм/рррр", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(1);
			if (dateDeltaWindow != null) {
				dateDeltaWindow.PostClick();
				keys.sendL("Ctrl+A", "!" + dateDeltaFormat, "Enter");
			}
			wait.ms(500);
			// час
			//var timeDeltaWindow = w.Elm["TEXT", "гг:хх", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(1);
			//if (timeDeltaWindow != null) {
			//timeDeltaWindow.PostClick();
			keys.sendL("!" + timeJbd, "Enter");
			//}
		}
		// поле кількість
		static void deltaNumberOfnumberWindow(string twoHundredth, string threeHundredth) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле кількість
			var numberOfnumberWindow = w.Elm["STATICTEXT", "Кількість", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1);
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
			var combatCapabilityWindow = w.Elm["STATICTEXT", "Боєздатність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1);
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
						// для ос не зроз ....
					} else if (!establishedJbd.ToLower().Contains("знищ") || !establishedJbd.ToLower().Contains("ураж")) {
						fullaim = "не о";
						combatCapabilityWindow.PostClick(scroll: 250);
						keys.sendL("Ctrl+A", "!" + fullaim, "Enter");
						return;
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
			var identificationWindow = w.Elm["STATICTEXT", "Ідентифікація", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1);
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
			var idPurchaseWindow = w.Elm["STATICTEXT", "Зауваження штабу", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next"].Find(-1);
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
			var mobileLine = w.Elm["STATICTEXT", "Мобільність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1);
			if (mobileLine != null) {
				var checking = w.Elm["STATICTEXT", "Мобільність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);
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
			// коментар
			var commentWindow = w.Elm["STATICTEXT", "Новий коментар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1);
			if (commentWindow != null) {
				string commentContents = Bisbin.createComment(targetClassJbd, dateJbd, timeJbd, crewTeamJbd, establishedJbd, commentJbd, mgrsCoords);
				commentWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + commentContents);
			}
			wait.ms(500);
			//var commentWindowAcceptButton = w.Elm["STATICTEXT", "Новий коментар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next4"].Find(-1);
			//commentWindowAcceptButton.PostClick(scroll: 250);
		}
		// додаткові поля
		static void deltaAdditionalFields(string idTargetJbd, string targetClassJbd) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// додаткові поля
			Bisbin.goToAdditionalField();
			// зауваження штабу поле
			var idPurchaseWindow = w.Elm["STATICTEXT", "Зауваження штабу", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next"].Find(-1);
			if (idPurchaseWindow != null) {
				idPurchaseWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + idTargetJbd);
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
			var deltaColorFills = w.Elm["STATICTEXT", "Колір заливки", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);
			if (deltaColorFills != null) {
				switch (targetClassJbd) {
				//. укриття
				case "Укриття":
					if (establishedJbd.ToLower().Contains("знищ") || establishedJbd.ToLower().Contains("ураж")) {
						// колір жовтий
						var placeColorYellowButton = w.Elm["BUTTON", "#ffeb3b", "class=Chrome_RenderWidgetHostHWND", EFFlags.HiddenToo | EFFlags.UIA].Find(1);
						placeColorYellowButton.PostClick(scroll: 500);
						wait.ms(500);
						// відсоток прозрачності
						var transpatentColorRange = w.Elm["web:SLIDER"].Find(-1);
						transpatentColorRange.PostClick(scroll: 500);
						transpatentColorRange.SendKeys("Left*5");
						wait.ms(500);
					} else if (states.Contains(establishedJbd)) {
						if (commentJbd.ToLower().Contains("знищ") || commentJbd.ToLower().Contains("ураж")) {
							// колір жовтий
							var placeColorYellowButton = w.Elm["BUTTON", "#ffeb3b", "class=Chrome_RenderWidgetHostHWND", EFFlags.HiddenToo | EFFlags.UIA].Find(1);
							placeColorYellowButton.PostClick(scroll: 500);
							wait.ms(500);
							// відсоток прозрачності
							var transpatentColorRange = w.Elm["web:SLIDER"].Find(-1);
							transpatentColorRange.PostClick(scroll: 500);
							transpatentColorRange.SendKeys("Left*5");
							wait.ms(500);
						} else {
							//колір червоний - ворож
							var placeColorRedButton = w.Elm["BUTTON", "#f44336", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);
							placeColorRedButton.PostClick(scroll: 500);
							wait.ms(500);
							// відсоток прозрачності
							var transpatentColorRange = w.Elm["web:SLIDER"].Find(-1);
							transpatentColorRange.PostClick(scroll: 500);
							transpatentColorRange.SendKeys("Left*5");
							wait.ms(500);
						}
					} else {
						//колір червоний - ворож
						var placeColorRedButton = w.Elm["BUTTON", "#f44336", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);
						placeColorRedButton.PostClick(scroll: 500);
						wait.ms(500);
						// відсоток прозрачності
						var transpatentColorRange = w.Elm["web:SLIDER"].Find(-1);
						transpatentColorRange.PostClick(scroll: 500);
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
