/*/ 
c \analisationWork\globalClass\Bisbin.cs; 
c \analisationWork\globalClass\RowByParts.cs; 
/*/

/* 28.12.2024 2.3

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
			
			// вікно діалогу
			var b = new wpfBuilder("Window").WinSize(450);
			//Brush
			b.Font(size: 17, bold: true);
			b.Brush(Brushes.DarkGray);
			// insider
			b.R.AddButton("1. Заповнення мітки", 1).Brush(Brushes.LightCoral);
			b.R.AddButton("2. Файлa імпорта РЕБ та РЕР мітки", 2).Brush(Brushes.LightGoldenrodYellow);
			b.R.AddButton("3. Файлa імпорта 777 мітки", 3).Brush(Brushes.LightGoldenrodYellow);
			b.R.AddButton("4. Файл імпорта МІНАМИ", 4).Brush(Brushes.LightGoldenrodYellow);
			b.R.AddButton("5. Файл імпорта - обізнаність ворога й всяке", 5).Brush(Brushes.LightGreen);
			b.R.AddOkCancel().Font(size: 14, bold: false);
			b.Window.Topmost = true;
			b.End();
			// show dialog. Exit if closed not with the OK button.
			if (!b.ShowDialog()) return;
			
			keys.send("Ctrl+C"); //копіюємо код
			wait.ms(100);
			string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
			
			Bisbin Bisbin = new Bisbin();
			
			if (b.ResultButton == 1) {
				fillMarkWithJBD(clipboardData, Bisbin);
				return;
			}
			if (b.ResultButton == 2) {
				createREBandRER(clipboardData, Bisbin);
				return;
			}
			if (b.ResultButton == 3) {
				createWhoWork(clipboardData, Bisbin);
				return;
			}
			if (b.ResultButton == 4) {
				createImportFileToMine(clipboardData, Bisbin);
				return;
			}
			if (b.ResultButton == 5) {
				Console.WriteLine("Рано, ще не порацював концепцію під усі обізнаності");
				return;
			}
		}
		// тіло для заповнення мітки
		static void fillMarkWithJBD(string clipboardData, Bisbin Bisbin) {
			string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
			RowByParts RowByParts = new RowByParts();
			RowByParts.RowByParts_Prepare(parts);
			
			clipboard.clear();
			goToMainField(Bisbin);
			wait.ms(500);
			deltaLayerWindow(RowByParts, Bisbin);
			wait.ms(500);
			deltaMarkName(RowByParts, Bisbin);
			wait.ms(500);
			deltaDateLTimeWindow(RowByParts, Bisbin);
			wait.ms(500);
			deltaNumberOfnumberWindow(RowByParts, Bisbin);
			wait.ms(500);
			deltaCombatCapabilityWindow(RowByParts, Bisbin);
			wait.ms(500);
			deltaIdentificationWindow(RowByParts, Bisbin);
			wait.ms(500);
			reliabilityWindow(Bisbin);
			wait.ms(500);
			flyEye(Bisbin);
			wait.ms(500);
			deltaIdPurchaseText(RowByParts, Bisbin);
			wait.ms(500);
			deltaMobilityLine(RowByParts, Bisbin);
			wait.ms(500);
			deltaCommentContents(RowByParts, Bisbin);
			wait.ms(500);
			deltaAdditionalFields(RowByParts, Bisbin);
			wait.ms(500);
			deltaGeografPlace(RowByParts, Bisbin);
			wait.ms(500);
			
			goToAttachmentFiles(Bisbin);
		}
		// тіло для створення мітки з подавленням від РЕБ та РЕР
		static void createREBandRER(string clipboardData, Bisbin Bisbin) {
			string[] parts = clipboardData.Split('\r'); // Розділяємо рядок на частини
			var features = new List<Object>(); //
			string plassEror = string.Empty; // для подальшої перевірки
			foreach (string item in parts) {
				string[] elenemts = item.Replace("\n", "").Split("\t"); // отримую рядок поділений на елементи
				RowByParts RowByParts = new RowByParts();
				RowByParts.RowByParts_ReR(elenemts);
				
				// номер мітки з деякими параметрами
				string sidc = "10060140001104000000";
				// Визначаємо формат вхідного рядка
				string inputFormat = "dd.MM.yyyy";
				// Визначаємо формат вихідного рядка
				string outputFormat = "yyyy-MM-dd";
				// Парсимо дату з вхідного рядка
				DateTime dateTime = DateTime.ParseExact(RowByParts.Date, inputFormat, CultureInfo.InvariantCulture);
				// Перетворюємо дату у формат ISO
				string dateForImport = dateTime.ToString(outputFormat);
				string name = "FPV (Подавлено)"; // назва
				string target_id = "414 ОПБС"; // 
				string comment = $"{RowByParts.Date.Replace('.', '/')} {RowByParts.Time} - подавлено та знищено засобами роти РЕБ 414 ОПБС"; // коментар
				
				// Формуємо JSON для однієї мітки (Feature) вручну
				var feature = new StringBuilder();
				feature.AppendLine("{");
				feature.AppendLine("  \"type\": \"Feature\",");
				feature.AppendLine("  \"properties\": {");
				feature.AppendLine($"    \"sidc\": \"{sidc}\",");
				feature.AppendLine($"    \"name\": \"{name}\",");
				feature.AppendLine($"    \"observation_datetime\": \"{dateForImport}T{RowByParts.Time}:22\","); // yyyy-MM-ddTHH:mm:ss
				feature.AppendLine($"	 \"staff_comments\": \"{target_id}\",");
				feature.AppendLine($"	\"comments\": [");
				feature.AppendLine($"	\"{comment}\"");
				feature.AppendLine($"	]");
				feature.AppendLine("  },");
				feature.AppendLine("  \"geometry\": {");
				feature.AppendLine("    \"type\": \"Point\",");
				feature.AppendLine($"    \"coordinates\": [{Bisbin.ConvertMGRSToWGS84(Bisbin.getCorrectCoord(RowByParts.Coordinates))}]").Replace("(", "").Replace(")", "");
				feature.AppendLine("  }");
				feature.AppendLine("}");
				
				features.Add(feature.ToString());
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
			string fileName = $"{DateTime.Now.ToString("dd mmss")} - rep-rep.geojson"; // Формування назви файлу
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
		// створення файлу для імпорта з Чергування - 777
		static void createWhoWork(string clipboardData, Bisbin Bisbin) {
			
			string[] parts = clipboardData.Split('\r'); // Розділяємо рядок на частини
			string dateTimeNow = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"); // поточний час
			var features = new List<Object>(); //
			string plassEror = string.Empty; // для подальшої перевірки
			
			foreach (string item in parts) {
				string[] elements = item.Replace("\n", "").Split("\t"); // ділимо рядок на елементи
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
					var wgsCoord = Bisbin.ConvertMGRSToWGS84(Bisbin.getCorrectCoord(elements[3]));
					
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
		static void createImportFileToMine(string clipboardData, Bisbin Bisbin) {
			
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
			
			
			string[] parts = clipboardData.Split('\r'); // Розділяємо рядок на частини
			var features = new List<Object>(); //
			string plassEror = string.Empty; // для подальшої перевірки
			
			foreach (string item in parts) {
				string[] elements = item.Replace("\n", "").Split("\t"); // ділимо рядок на елементи
				
				// Присвоюємо змінним відповідні значення
				string dateJbd = elements[0]; // 27.07.2024
				string timeJbd = elements[1]; //00:40
				string comment = elements[2].Replace("\n", " "); //коментар (для ідентифікації скоріш за все)
				string numberOFlying = elements[3]; // 5
				string crewTeamJbd = Bisbin.StringReducer.TrimAfterFirstDot(elements[4].Replace("\n\t", "")); // R-18-1 (Мавка)
				string whatDidJbd = elements[5]; // Дорозвідка / Мінування ..
				string target = elements[7]; // Міна/Вантажівка/...
				string targetId = Bisbin.StringReducer.TrimAllAfterN(elements[9], 19); // Міна 270724043
				string mgrsCoords = elements[18]; // 37U CP 76420 45222
				string nameOfBch = elements[22]; // ПТМ-3 ТМ-62
				string status = elements[24]; // Встановлено/Уражено/Промах/...
				string DNumber = elements[25]; // 200
				string WNumber = elements[26]; // 300
				
				// захист від дурачка
				if (whatDidJbd.Contains("Мінування")) {
					// підготовка значень для полів
					string name = Bisbin.createMineName(nameOfBch, target, dateJbd, status, comment, DNumber, WNumber);
					string sidc = string.Empty;
					string states = "Розміновано Підтв. ураж. Тільки розрив Авар. скид";
					if (states.Contains(status)) {
						if (Bisbin.VariableHolder.bchTropsMines.Contains(nameOfBch)) {
							sidc = "10011540002003000000";
						} else { sidc = "10031540002103000000"; }
					} else if (status.Contains("Встановлено")) {
						if (Bisbin.VariableHolder.bchTropsMines.Contains(nameOfBch)) {
							sidc = "10011520002003000000";
						} else { sidc = "10031520002103000000"; }
					} else if (status.Contains("Спростовано")) {
						return;
					} else {
						if (Bisbin.VariableHolder.bchTropsMines.Contains(nameOfBch)) {
							sidc = "10011530002003000000";
						} else { sidc = "10031530002103000000"; }
					}
					
					
					string commentar = Bisbin.createComment(target, dateJbd.Replace('.', '/'), timeJbd, crewTeamJbd, status, comment, mgrsCoords);
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
					feature.AppendLine($"	\"staff_comments\": \"{targetId}\","); // ід
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
		// перша вкладка (Основні поля) мітки
		static void goToMainField(Bisbin Bisbin) {
			wait.ms(875);
			Bisbin.ElementNavigator.DeltaMainFilds().PostClick(scroll: 300);
		}
		// друга вкладка (Додаткові поля) мітки
		static void goToAdditionalField(Bisbin Bisbin) {
			wait.ms(875);
			Bisbin.ElementNavigator.DeltaAdditionalFields().PostClick(scroll: 300);
		}
		// третя вкладка (Географічне розташування) мітки
		static void goToGeograficalPlace(Bisbin Bisbin) {
			wait.ms(875);
			Bisbin.ElementNavigator.DeltaGeograficPlace().PostClick(scroll: 300);
		}
		// четверта вкладка (прикріплення) мітки
		static void goToAttachmentFiles(Bisbin Bisbin) {
			wait.ms(875);
			Bisbin.ElementNavigator.DeltaAttachmentFields().PostClick(scroll: 300);
		}
		// обрати відповідний шар
		static void deltaLayerWindow(RowByParts RowByParts, Bisbin Bisbin) {
			// (01) постійні схов. і укриття
			string permanentStorage = "Укриття Склад майна Склад БК Склад ПММ Польовий склад майна Польовий склад БК Польовий склад ПММ";
			// (02) антени, камери...
			string antennaCamera = "Мережеве обладнання Камера Антена РЕБ (окопні)";
			
			// поле шар
			var layerWindow = Bisbin.PourMark.MainFieldsTab.DeltaFieldLayer();
			layerWindow.PostClick(scroll: 250);
			//. перевірка, запис
			if (permanentStorage.Contains(RowByParts.Target)) {
				keys.sendL("Ctrl+A", "!постійні схов.", "Enter");
				return;
			}
			if (antennaCamera.Contains(RowByParts.Target)) {
				keys.sendL("Ctrl+A", "!антени, камери", "Enter");
				return;
			}
			switch (RowByParts.Target) {
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
				if (RowByParts.DecrtiptionComment.ToLower().Contains("рус") || RowByParts.DecrtiptionComment.ToLower().Contains("рух")) {
					keys.sendL("Ctrl+A", "!техніка в русі ", "Enter");
				} else if (RowByParts.DecrtiptionComment.ToLower().Contains("виходи") || RowByParts.DecrtiptionComment.ToLower().Contains("вогнева позиція")) {
					keys.sendL("Ctrl+A", "!вогневі позиції ", "Enter");
				} else {
					keys.sendL("Ctrl+A", "!схована техніка ", "Enter");
				}
				break;
			}
			//..
		}
		// відповідна назва
		static string deltaMarkName(RowByParts RowByParts, Bisbin Bisbin) {
			string nameIs = string.Empty;
			string statusOne = "Виявлено Підтверджено Не зрозуміло";
			
			var nameOfMarkWindow = Bisbin.PourMark.MainFieldsTab.DeltaFieldName();
			if (nameOfMarkWindow != null) {
				string markName = string.Empty;
				string nameOfMark = nameOfMarkWindow.Value;
				int indexLoss = nameOfMark.IndexOf(' ');
				
				switch (RowByParts.Target) {
				//. Міна
				case "Міна":
					if (RowByParts.Status.Contains("Спростовано")) {
						return "";
					}
					if (RowByParts.Status.Contains("Авар. скид")) {
						markName = $"{RowByParts.Ammunition} ({RowByParts.Date})";
					} else if (Bisbin.VariableHolder.states.Contains(RowByParts.Status)) {
						markName = $"{nameOfMark.Substring(0, indexLoss)} ({RowByParts.Date})";
					} else {
						if (Bisbin.VariableHolder.bchHeavyMines.Contains(RowByParts.Ammunition)) {
							markName = $"{RowByParts.Ammunition} до ({Bisbin.datePlasDays(RowByParts.Date, 90)})";
						} else if (Bisbin.VariableHolder.bchTropsMines.Contains(RowByParts.Ammunition)) {
							markName = $"{RowByParts.Ammunition} до ({Bisbin.datePlasDays(RowByParts.Date, 8)})";
						} else {
							markName = $"{RowByParts.Ammunition} до ()";
						}
					}
					
					break;
				//..
				//. "Укриття
				case "Укриття":
					if (RowByParts.Status.ToLower().Contains("знищ")) {
						markName = RowByParts.Target + " ОС (знищ.)";
					} else if (RowByParts.Status.ToLower().Contains("ураж")) {
						markName = RowByParts.Target + " ОС (ураж.)";
					} else if (statusOne.Contains(RowByParts.Status)) {
						if (RowByParts.DecrtiptionComment.ToLower().Contains("знищ")) {
							markName = RowByParts.Target + " ОС (знищ.)";
						} else if (RowByParts.DecrtiptionComment.ToLower().Contains("ураж")) {
							markName = RowByParts.Target + " ОС (ураж.)";
						} else {
							markName = RowByParts.Target + " ОС";
						}
					} else {
						markName = RowByParts.Target + " ОС";
					}
					break;
				//..
				//. Скупчення ОС
				case "ОС РОВ":
					if (RowByParts.DNumber.Length == 0 || RowByParts.WNumber.Length == 0) {
						markName = RowByParts.Target;
					}
					if (RowByParts.DNumber.Length > 0) {
						markName = RowByParts.DNumber + " - 200";
					}
					if (RowByParts.WNumber.Length > 0) {
						markName = RowByParts.WNumber + " - 300";
					}
					if (RowByParts.DNumber.Length > 0 && RowByParts.WNumber.Length > 0) {
						markName = RowByParts.DNumber + " - 200, " + RowByParts.WNumber + " - 300";
					}
					
					break;
				//..
				default:
					//.
					if (RowByParts.Status.ToLower().Contains("знищ")) {
						markName = RowByParts.Target + " (знищ.)";
					} else if (RowByParts.Status.ToLower().Contains("ураж")) {
						markName = RowByParts.Target + " (ураж.)";
					} else if (statusOne.Contains(RowByParts.Status)) {
						if (RowByParts.DecrtiptionComment.ToLower().Contains("знищ")) {
							markName = RowByParts.Target + " (знищ.)";
						} else if (RowByParts.DecrtiptionComment.ToLower().Contains("ураж")) {
							markName = RowByParts.Target + " (ураж.)";
						} else if (RowByParts.DecrtiptionComment.ToLower().Contains("в рус") || RowByParts.DecrtiptionComment.ToLower().Contains("рух")) {
							markName = RowByParts.Target + " (в русі)";
						} else if (RowByParts.DecrtiptionComment.ToLower().Contains("схов")) {
							markName = RowByParts.Target + " (схов.)";
						} else if (RowByParts.DecrtiptionComment.ToLower().Contains("стої")) {
							markName = RowByParts.Target + " (стоїть)";
						} else if (RowByParts.DecrtiptionComment.ToLower().Contains("виходи") || RowByParts.DecrtiptionComment.ToLower().Contains("вогнева позиція")) {
							markName = RowByParts.Target + " (вогнева позиція)";
						} else {
							markName = RowByParts.Target;
						}
					} else {
						markName = RowByParts.Target;
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
		static void deltaDateLTimeWindow(RowByParts RowByParts, Bisbin Bisbin) {
			var dateDeltaWindow = Bisbin.PourMark.MainFieldsTab.DeltaFieldDate();
			if (dateDeltaWindow != null) {
				dateDeltaWindow.PostClick();
				keys.sendL("Ctrl+A", "!" + RowByParts.Date.Replace('.', '/'), "Enter");
				wait.ms(500);
				keys.sendL("!" + RowByParts.Time, "Enter");
			}
		}
		// поле кількість
		static void deltaNumberOfnumberWindow(RowByParts RowByParts, Bisbin Bisbin) {
			var numberOfnumberWindow = Bisbin.PourMark.MainFieldsTab.DeltaFieldCounts();
			if (numberOfnumberWindow != null) {
				int counts = 1;
				if (RowByParts.DNumber.Length > 0 || RowByParts.WNumber.Length > 0) {
					counts = (RowByParts.DNumber.ToInt() + RowByParts.WNumber.ToInt());
				}
				numberOfnumberWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + counts);
			}
		}
		// поле боєздатність
		static void deltaCombatCapabilityWindow(RowByParts RowByParts, Bisbin Bisbin) {
			var combatCapabilityWindow = Bisbin.PourMark.MainFieldsTab.DeltaFieldCapability();
			//. перевірка
			if (combatCapabilityWindow != null) {
				string fullaim = string.Empty;
				switch (RowByParts.Target) {
				//. Якщо ти міна
				case "Міна":
					if (Bisbin.VariableHolder.states.Contains(RowByParts.Status)) {
						fullaim = "небо";
					} else if (RowByParts.Status.Contains("Встановлено")) {
						fullaim = "повніс";
					} else if (RowByParts.Status.Contains("Спростовано")) {
						return;
					} else {
						fullaim = "част";
					}
					break;
				//..
				default:
					//.
					if (RowByParts.Status.ToLower().Contains("знищ")) {
						fullaim = "небо";
					} else if (RowByParts.Status.ToLower().Contains("ураж")) {
						fullaim = "част";
					} else if (RowByParts.Status.Contains("Виявлено")) {
						if (RowByParts.DecrtiptionComment.ToLower().Contains("знищ")) {
							fullaim = "небо";
						} else if (RowByParts.DecrtiptionComment.ToLower().Contains("ураж")) {
							fullaim = "част";
						} else {
							fullaim = "повніс";
						}
						// для ос не зроз ....
					} else if (!RowByParts.Status.ToLower().Contains("знищ") || !RowByParts.Status.ToLower().Contains("ураж")) {
						fullaim = "не о";
						combatCapabilityWindow.PostClick(scroll: 250);
						keys.sendL("Ctrl+A", "!" + fullaim, "Enter");
						return;
					} else {
						if (RowByParts.DecrtiptionComment.ToLower().Contains("знищ")) {
							fullaim = "небо";
						} else if (RowByParts.DecrtiptionComment.ToLower().Contains("ураж")) {
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
		static void deltaIdentificationWindow(RowByParts RowByParts, Bisbin Bisbin) {
			var identificationWindow = Bisbin.PourMark.MainFieldsTab.DeltaFieldIdentification();
			if (identificationWindow != null) {
				string friendly = string.Empty;
				switch (RowByParts.Target) {
				case "Міна":
					if (Bisbin.VariableHolder.bchTropsMines.Contains(RowByParts.Ammunition) && RowByParts.Ammunition.Length >= 1) {
						friendly = "відом";
					} else {
						friendly = "дружній";
					}
					break;
				case "Укриття":
					if (RowByParts.Status.ToLower().Contains("знищ") || RowByParts.DecrtiptionComment.ToLower().Contains("знищ")) {
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
		// тип джерела
		static void flyEye(Bisbin Bisbin) {
			string flyeye = "пові";
			var typeOfSourceWindow = Bisbin.PourMark.MainFieldsTab.DeltaFieldSourceType();
			if (typeOfSourceWindow != null) {
				typeOfSourceWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + flyeye, "Enter");
			}
		}
		// достовірність
		static void reliabilityWindow(Bisbin Bisbin) {
			if (Bisbin.PourMark.MainFieldsTab.DeltaButtonReliability_A() != null) {
				Bisbin.PourMark.MainFieldsTab.DeltaButtonReliability_A().PostClick(scroll: 250);
			}
			wait.ms(500);
			if (Bisbin.PourMark.MainFieldsTab.DeltaButtonCertainty_2() != null) {
				Bisbin.PourMark.MainFieldsTab.DeltaButtonCertainty_2().PostClick(scroll: 250);
			}
		}
		// зауваження штабу - ід
		static void deltaIdPurchaseText(RowByParts RowByParts, Bisbin Bisbin) {
			var idPurchaseWindow = Bisbin.PourMark.MainFieldsTab.DeltaFieldPurchase();
			if (idPurchaseWindow != null) {
				idPurchaseWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + RowByParts.TargetId);
			}
		}
		// мобільність
		static void deltaMobilityLine(RowByParts RowByParts, Bisbin Bisbin) {
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
			
			// поле мобільності
			var mobileLine = Bisbin.PourMark.MainFieldsTab.DeltaFieldMobility();
			if (mobileLine != null) {
				var checking = Bisbin.ElementNavigator.DeltaWindow().Elm["STATICTEXT", "Мобільність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);
				if (checking.Name != "Мобільність") {
					return;
				}
				// Обмеженої прохідності
				if (limitedAccess.Contains(RowByParts.Target)) {
					mobileLine.PostClick(scroll: 250);
					keys.sendL("Ctrl+A", "!" + obmezheno, "Enter");
					return;
				}
				// Позашляховик
				if (pozashlyakhovyk.Contains(RowByParts.Target)) {
					mobileLine.PostClick(scroll: 250);
					keys.sendL("Ctrl+A", "!" + suv, "Enter");
					return;
				}
				// Гусеничний - колісний
				if (caterpillar.Contains(RowByParts.Target)) {
					mobileLine.PostClick(scroll: 250);
					keys.sendL("Ctrl+A", "!" + husenychnyy, "Enter");
					return;
				}
				// На буксирі
				if (towTruck.Contains(RowByParts.Target)) {
					mobileLine.PostClick(scroll: 250);
					keys.sendL("Ctrl+A", "!" + buksyri, "Enter");
					return;
				}
				// Гусинечний
				if (gusankaList.Contains(RowByParts.Target)) {
					mobileLine.PostClick(scroll: 250);
					keys.sendL("Ctrl+A", "!" + gusanka, "Enter");
					return;
				}
			}
		}
		// коментар
		static void deltaCommentContents(RowByParts RowByParts, Bisbin Bisbin) {
			var commentWindow = Bisbin.PourMark.MainFieldsTab.DeltaFieldNewComment();
			if (commentWindow != null) {
				string commentContents = Bisbin.createComment(RowByParts.Target, RowByParts.Date.Replace('.', '/'), RowByParts.Time, RowByParts.CrewTeam, RowByParts.Status, RowByParts.DecrtiptionComment, RowByParts.Coordinates);
				commentWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + commentContents);
			}
		}
		// додаткові поля
		static void deltaAdditionalFields(RowByParts RowByParts, Bisbin Bisbin) {
			Bisbin.ElementNavigator.DeltaAdditionalFields().PostClick();
			var idPurchaseWindow = Bisbin.PourMark.ExtraFieldsContainer.DeltaFieldPurchase();
			if (idPurchaseWindow != null) {
				idPurchaseWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + RowByParts.TargetId);
			}
		}
		// Георафічне розташування
		static void deltaGeografPlace(RowByParts RowByParts, Bisbin Bisbin) {
			// Георафічне розташування
			Bisbin.ElementNavigator.DeltaGeograficPlace().PostClick();
			Bisbin.PourMark.MainFieldsTab.DeltaFieldLayer().ScrollTo(); // це для того щоб піднятися на самий верх
			string states = "Виявлено Підтверджено Спростовано Не зрозуміло";
			// Колір заливки
			if (Bisbin.PourMark.GeoPositionContainer.DeltaDropDownFillColor() != null) {
				switch (RowByParts.Target) {
				//. укриття
				case "Укриття":
					if (RowByParts.Status.ToLower().Contains("знищ") || RowByParts.Status.ToLower().Contains("ураж")) {
						Bisbin.PourMark.GeoPositionContainer.DeltaButtonYellow().PostClick(scroll: 500);
						wait.ms(500);
						transpatentColorRange();
						wait.ms(500);
					} else if (states.Contains(RowByParts.Status)) {
						if (RowByParts.DecrtiptionComment.ToLower().Contains("знищ") || RowByParts.DecrtiptionComment.ToLower().Contains("ураж")) {
							Bisbin.PourMark.GeoPositionContainer.DeltaButtonYellow().PostClick(scroll: 500);
							wait.ms(500);
							transpatentColorRange();
							wait.ms(500);
						}
					} else {
						Bisbin.PourMark.GeoPositionContainer.DeltaButtonRed().PostClick(scroll: 500);
						wait.ms(500);
						transpatentColorRange();
						wait.ms(500);
					}
					break;
				//..
				default:
					break;
				}
			}
			void transpatentColorRange() {
				// відсоток прозрачності
				var transpatentColorRange = Bisbin.PourMark.GeoPositionContainer.DeltaFieldTransparentPercentage();
				transpatentColorRange.PostClick(scroll: 500);
				transpatentColorRange.SendKeys("Left*5");
			}
		}
	}
}
