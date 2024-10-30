/*/ c \analisationWork\globalClass\Bisbin.cs; /*/

/* 29.10.2024 2.0

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
			
			keys.send("Shift+Space*2"); //виділяємо весь рядок
			wait.ms(100);
			keys.send("Ctrl+C"); //копіюємо код
			wait.ms(100);
			string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
			string[] examlpelesItem = {
										"1. Заповнення мітки",
										"2. Створення РЕБ та РЕР мітки",
										"3. Створення 777 мітки",
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
			capabilityWindow.PostClick(scroll: 250);
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
		// створення мітки з Чергування для оч для 777
		static void createWhoWork(string clipboardData) {
			string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
			
			string ecipashName = parts[0]; // екіпаж
			string flyDot = $"Т.в. \"{parts[1]}\""; // точка вильоту
			string mgrsCoord = parts[9]; // координати
			string finalName = $"{ecipashName} {flyDot}";
			
			string rebMark = "Створення перешкод";
			string damageMark = "БПЛА вертикального зльоту";
			string witness = "Безпілотний літак";
			
			string friendlyStatys = "дружній";
			bool forCall = false; // змінна для визначення фарбування
			int rangeNumber = 0;
			
			// вкладка
			var w = wnd.find(1, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			// поле для ввода координат
			var searchWindow = w.Elm["web:COMBOBOX", "Пошук", "@placeholder=Знайти адресу або координату"].Find(1);/*image:WkJNG7UIAORoHN5oRBJfB/vGbqVFY/5vprU1wUSXLYGe52tSUS8aW2TeWPoNUzjwPssdgp7rLpc4JMIwWdrO0kML0qDyqBeq3FIANchkeCXsc19HyUIBra2toGVtDldXV8ChwgG2wQHw+fkJmZmZcHt7C6I64rC+vg4vLy/AIMEA5g724JYQCwr6aiApqXLo6np3d+fqK/n/2dDQcWttm42NNbDWoH7oDadBzAZyKXSH2giDtcU6MRupyqU5z+dr3q62m/i1wvVS8k7frj6Sl/SNZzcJV9dNwCBs+D8nE10xq0Y0OeMXcv6f4VT+vxAyQIBMSGglDZkkV0kEbNBECE2EOI0w1Sh0UZ5dgSjCKs/GziFUIZWcfOq445jgYp8iBgSMcOQ/GbH8mj9KoUXF+XIBTO0aM8ozcKwGUOmEKp1DxxCaeCzONdJJXnyznMyL/YnQv/DL8xg4r4WIU/qYVxt+3TXPK1wA*/
			searchWindow.PostClick();
			keys.sendL("Ctrl+A", "!" + mgrsCoord, "Enter");
			wait.ms(3000);
			// кнопка створення мітки
			var createButton = w.Elm["web:LISTITEM", prop: "@data-testid=create-object"].Find(1);/*image:WkJNG7UIAGRBTjORbUfyBexPvLqJa+pHaeSMMjAF5p22aevOI5ISKAFsqBMIgrbWjiH0KpEor6ypGQ7hwxmKAvKDdqBwEQOwQZaHVoz82UcMmxYLQt0CoKVpCC4itECYgxleb88hLjoRjDXVgEQAH576y+A+yxfQaDRICfFAblolvG5MAKUkOexxXm5c5PZfCvIvfGloAMDZi29cem4Gl2PKpWQz9sgrknIEs0X+ah5KUiAr+ZTsfz3h56JOEUteHtF9NUVMhihmKcOS4oDfIndLrSAPX5XdJm4quTbqhXh00QMFvAUNvfC3NOJ3KMJsbW1OI1c4Pj6Yf1OufNCpyj53I9z1+jHkyqccytmbVzshdRSTbXwHqDFEuzkhV29YblyfNjNhXx+WvdeV57PXsh8XZuUGEYbk7yVM1nN9OB02FZhEE3shao3Hd8qOIuTaukbb6hFK4dByQSO0TnZ8fHx8rKPWWq6Ry0sQpYVTpfl9eEm5djEH35gaEo4w1G7uQIja4LHhH7Rd0ys01mF9ydO0e9ets6xPUdV/NrArixqtJxo36WqZmD0E*/
			createButton.PostClick();
			wait.ms(3000);
			// поле пошуку об'єктів
			var fieldSearshingMark = w.Elm["web:GROUPING", "Категорії", navig: "next child2"].Find(1);/*image:WkJNG30IAAQib/e/D18VodkEinU2/YNqUgZOOB4wBlnAyQH84HbQM8YYvlYraHZ8J5qoXFoFQ0pm3bat0XknSP194HOKAA==*/
			fieldSearshingMark.PostClick();
			if (ecipashName.Contains("РЕБ") || ecipashName.Contains("РЛС")) {
				rangeNumber = 45000;
				keys.sendL("Ctrl+A", "!" + rebMark, "Enter");
			} else if (ecipashName.Contains("Мавік") || ecipashName.Contains("FPV") || Regex.IsMatch(ecipashName, @"\bП\d{2}\b")) {
				rangeNumber = 11000;
				keys.sendL("Ctrl+A", "!" + damageMark, "Enter");
			} else {
				rangeNumber = 45000;
				forCall = true; // перемикання 
				keys.sendL("Ctrl+A", "!" + witness, "Enter");
			}
			wait.ms(3000);
			// обираємо 1 зі списку
			var firstMarkInList = w.Elm["web:GROUPING", "Категорії", navig: "next last child"].Find(1);/*image:WkJNG30IAAQib/e/D18VodkEU3QziiJpWgBOOJ5L6kEW8OUkfNUzxhi+Vito5/lO3JF5lBEMTVF0e/qMVrl7UVm4Rh8=*/
			firstMarkInList.PostClick();
			wait.ms(4000);
			
			// обравши мітку, залишилося її заповнити
			
			// повернення на основне вікно
			var mainFilds = w.Elm["web:GROUPING", prop: new("desc=Основні поля", "@title=Основні поля")].Find(-1);
			mainFilds.PostClick();
			wait.ms(200);
			// поле назва
			var nameMark = w.Elm["STATICTEXT", "Назва", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next"].Find(1);/*image:WkJNG30IAAQib/e/D18VodkEkm4jE1SbVIETjgeMQRZwcgA/OB34jDGGr9UKmrm/E3VkLqWCISmybttGq7yzCP3d4HPKAA==*/
			nameMark.PostClick(scroll: 500);
			keys.sendL("Ctrl+A", "!" + finalName, "Enter");
			wait.ms(200);
			// дата/час
			var dateField = w.Elm["TEXT", "дд/мм/рррр", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(1);/*image:WkJNG7UIAMQH9mdvcwKVFtK/7AqIjYmJrNcugSo7/exMwwsGOAuCLOAk8QgTDJyekIe4McbLk0kQl5vHJhNtmWCBLxyTBCoMo0jn7L5rwuVEgI87SrLS0d/RiviQANxcXqCtrgqz4yPITY5HdVEeVuamMTHYh56WRqRGh6OxshQjvV0YGjcdbuAKijvBP+3YJUTMw4nEe+48dz93idA4eddGtey3dIVotPxz7/aOT+Qr9uLqtwjFVv1BUWuUfBNdL/sl5r1buO87eSf7O1klRLrbwrk6RwJ3W14LyeCU5GuRoxDL9+M1EA==*/
			dateField.PostClick(scroll: 500);
			wait.ms(200);
			// кнопка поточного часу
			var dateAndTimeNowButton = w.Elm["BUTTON", "Час виявлення", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3 last"].Find(1);/*image:WkJNG7UIAMT/Z391B0zzBlpADWTaNHGbWdfvzG8Loq0jFJpC+WcHb/K2zkeOhokVWJ+uBfw5sHcswTwAWWYJjeN5b/03Yqd0iBi1qQX7anTUCASCWhBaSCCH3M8s0919z7jwQPS1NuD6/AQ5SbH4+/3BjMCoKchCSWYyvj7eIYoiEqNC8HR3g83lBRSkJqA8OxUpMWFo2lrnz2XgDdYmfeeRT2c+C9dosH7Onofy7Y851hMccEjqhxqpjHR8d+qgXd74L9qyj5xp9j+goFGCjrzK2ZlzVc6jYTJewC/nqnzcdbzkaCuEho6TftwqkDEjzyPMfuklpvTy7ssh1lE68griAbE8xQJy4XGjeNMTzGTGKlB5T9bDnqAtF6freMmIF0t39cw99Ac76aCPAg==*/
			dateAndTimeNowButton.PostClick(scroll: 500);
			wait.ms(200);
			// поле ідентифікації 
			var identyficationField = w.Elm["STATICTEXT", "Ідентифікація", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(1);/*image:WkJNG7UIAOTn5drLhzRB1M/U2E3NxP/e4m+b4HDrFrzsmxWdFvANEl/eDRLRQLiAE+kFFtE2/3BpCiEhGmq0TGcxsMI4IABOIJITASnIeOmktLPnTVNaFHJTEsDGQAtWF+Yg0M0RBrs74OL0GOrKiiEhLAjcrM3gYGcL2upr4O76CgyV5aAUZcHE8ABw7X8T4M4bc2BhBCc07tngvNBBC1nngJdvjYu80xo9ah+HDO9G4APQeJVn1bF0Nzo+PjQOQI803SG/ipwd67JiVUB75Z2QErUsgcad4gPwniTvwUZfXYDGqf2jWTqCuAgSjLSrjH3PMr2OUUyw5vgPqrbk0Jvj40m20hsaJ0UA/UWUboXGqhny+J6BLMg8NfPe5W0RV2sPEWQW3xouj8g7ZEvlL+rxh7/7reFxpGpL7nYI9BxNtBj3uKX/c8xCUwbRNBX7EzRlYBYwC1TMS1HQuv94DL9sFP0dk05Em2CjlunwjWk=*/
			identyficationField.PostClick(scroll: 500);
			keys.sendL("Ctrl+A", "!" + friendlyStatys, "Enter");
			wait.ms(200);
			
			// Георафічне розташування
			var geografPlaceWindow = w.Elm["web:GROUPING", prop: "@title=Географічне розташування"].Find();
			geografPlaceWindow.PostClick(scroll: 500);
			wait.ms(3000);
			// якщо ти не скидувач, фарбуйся
			if (forCall == true) {
				var colorBoxing = w.Elm["web:STATICTEXT", "Колір обведення", navig: "parent"].Find(1);/*image:WkJNG4EIAAQib/cjqIp1m00gCcknRZOEpMg60MHp53L1QxZwEokHTDHGGL5WK2h1fkeiyGzKCBadgjLbq9ZbZQ3l3UNtUdj8tfcB*/
				colorBoxing.PostClick(scroll: 500);
				wait.ms(1000);
				var selectColor = w.Elm["web:BUTTON", "#ff9327", new("@title=#ff9327", "@type=button")].Find(1);/*image:WkJNG7UIAMT/GFdvibTT6bquOsANrst/5wjH3XpQhKYDqNfk05/Kny15WYMwQHNSztMlMtbGglWhQNLzImyTgYdnRyBkVicYWeMjvV5ET8qJuWaADaVGIZLvfSvgskCxfYnj42M4OubRUlmC6NQTvAUNsMVOkJ/5h7MxBsnpP5yTvwgPHSHUNuLzA1lEA26kDFPJB3Rk46ekxijcr1JK7tRWklJyhvQEK94nSk6I3rxMFJeIvUrUjnrIMT0tdZfIDk7Qz+AmlNNLYRplZiXu0fAEtAQ45i11k8nWola4BuzoDS73a3reIS5pTqVgsV/8FQ0baw9jNcoMLhjHjPu+IS7ffd/oPKm++v4BIIPJUf4YHOy/zP5Y7a5mhn2ovSMA*/
				selectColor.PostClick(scroll: 500);
				wait.ms(200);
			}
			// кнопка встановлення сектору
			var sectorButton = w.Elm["web:RADIOBUTTON", "Сектор", "@name=zoneType"].Find(1);/*image:WkJNG7UIAOT+7XISl3CXnUNcgdgArFhJFbTv9gesrgr4YxdNOkhHN/XPXh84sICnUEDhg7Qs4G2BrmEWyFgA8MIJPbZAtrl5eYmHYNFV4howqQxpFgsGEAMmvgzIg55IS1dldrm6mJlCa2UexIf4w+ZEB8Tn1UNSgxT4kEDoHluHqu4J2N1ehuacIJhoDIX4kFooys2E6fllGOwhUDKOW/WOHIDeL0c7vxFzYMZVY6QGxxL7T1JJjytDSBVlB0/QCuN0yYmYEG7Shh28SS1kWBrh6AodsZWv44bE13FlWixBY46yiDSUQ9LGHNIIDqfZLhpziBr07q6EkirIDUolZyMULm/oZ/7A9jtEKK1KzaexPF0jHh09eXR0ZbOGcQ9hKUvrHMkdSy95MbmU4JtZhOoWOOTKvP3//7syjqz497kqRkJaNXiDLUvdBKTHNb981VL/DK4W*/
			sectorButton.PostClick(scroll: 500);
			wait.ms(200);
			// поле відстані
			var rangeMark = w.Elm["web:SPINBUTTON", prop: "@data-testid=first-range"].Find(1);/*image:WkJNG7UIAMQn9ldv13E93Yjruq46J4CJbJDjuTkDSH8Oanj8LmEYhicSDWYDnow8UUpMh7gxxsuTSRCfy7vCRFc+yRWS+pJAhSMR2Kzddy1Okwa9PZ2IBj04PzlGRlIc/n5/sbG8iIq8LFxfXuBwdxvjA32ory7H89MjCtOTQRAE5qfGMWbCebvxc7vCFG+P1MB/W3oC1daQvWWkjDXC0moqoeGY30YdN0VeFjBhTf6QsMfyoaQd881H1Jcu5uG4TZjpAQ==*/
			rangeMark.PostClick(scroll: 500);
			keys.sendL("Ctrl+A", "!" + rangeNumber, "Enter");
			wait.ms(200);
			// колір залівки
			var sectorFillColor = w.Elm["web:RADIOBUTTON", "Сектор", "@name=zoneType", navig: "next11"].Find(1);/*image:WkJNGzEGAAQib/djOiEKpiiE5JPZNCUomg3o4LQ5nCHIAk4ST9STQD3wkO2SJJmsdVWHXSEzSSVYyBcI072yt8K/hz2UyN6Q4xWxHfm2lQI=*/
			sectorFillColor.PostClick(scroll: 500);
			if (ecipashName.Contains("Мавік")) {
				var greenColorFill = w.Elm["web:BUTTON", "#4caf50"].Find(1);/*image:WkJNG1EGAGRhbkM1kd2SmDUR/yFBIkTL4pFMP/w3t3xhrNY7+DBjtBpEkIVssVR5KLjQFqqhNlOzCOWtI0QkJGbrv90Tz5BhTDVMbhGTbjggAnXBZ2acvSq3SQPFfRYwDINo1grjXw86lxpkOwGY/yeQLLlg8G1BYR6BNcQL/w7t4S6H+AWh1idC05lqQWjIXk3kkfuVmdvyaiH0XgTR40lqyt/VjT+I6zOVPgTZEnEaN5IOJnMlorkipewEiY3kgtQGqTXydowWEG0hWkW0z3gJ8Sbjdebv+IQb*/
				greenColorFill.PostClick(scroll: 500);
			}else if (ecipashName.Contains("FPV")) {
				var lightBlueColorFill = w.Elm["web:BUTTON", "#00bcd4"].Find(1);/*image:WkJNG1UGAMSIMfczSfO4ZB6SWjJzzCKJI4omIlSodOD7ueWf2w6u1ZUwmE0RsVssVZ4IP1hWQrNHif+dnclkkhLed5p9ISp6KwlZuENyEECGecAxsec+r8moxt2lBsWQGiGXBX/fBpQTYXy96LBsqvH/a0Q3q8bPpx6nYzVGD/phSEYjsLYaQ+A8yN5srIeTNnL2YSdWTgwciS2TE/ljK+1EuqOQ2bui4h69WSeSH4Rp3suteSoicysV80ic5sHzxM2QvZGzIHeD3DXydkgWkGwxWUWyj9QSU5tIrV//OwA=*/
				lightBlueColorFill.PostClick(scroll: 500);
			}else {
				var yelowTwoColorFill = w.Elm["web:BUTTON", "#ffc107"].Find(1);/*image:WkJNG2UGAMSAcXlPhgiyMWNbAxiqnV2/DQlSI9J8vpt8bP2jXkjptLF8C81/g3oRF0EWmMZBbbP7qSkcMghNm0LxPCpmUIdQiIiQtQSQQVojJOHvvQ2atfANTmJ1dRW+RBacy29wnz/DMXcIT3kzzxa34Nz4h6epBd7sEjjS93A/cNXmJVwdU1huouj/YdzG+g73lM9no2KuDHIyMoHU0JAKB7H0ea1HyhoHZfbjcVFR0AaZe08u9qQ5z2K35NhRiH3mRXBH5qAxJwTXwMYFOIM0DsMO/OH4PBijkBk+cABh7IELc9DwodxNiYWSG0quKW/HqAVKbVFqlVH7lF6i9OaK2dDrazD+jg8A*/
				yelowTwoColorFill.PostClick(scroll: 500);
			}
			wait.ms(200);
			// відсотки залівки колір залівки
			var rangeSectorFillColor = w.Elm["web:SLIDER", prop: "@data-testid=opacity-slider"].Find(1);/*image:WkJNG2UGAMTnGZf/2OgIJSUIcfOd24S6t+jXl+GEKCUAbVFTN/lHqW/MPrDAu2wtoLE5kDXBckghpDSoG7p+MUmSEo0rmqfC9Q6ACoSAIieADKI2Njaz9xyHTsNa0Yav9TLaczYc3T9jqKcOToMeU8MjmN09gTMxjUl717//X/zvLeC8042My4yftw3k474EmBXeRF4L1sjL34My5C0Bla+KGHIxjqW4MgM+XaFWupoarcu9vb3dPdmrdZcm0yS7p7hlkt9M8msx8lfk9VPlTeFDNpu1BaZs2kvDaIiesuzUEQMA*/
			rangeSectorFillColor.PostClick();
			rangeSectorFillColor.SendKeys("Left*5");
			wait.ms(200);
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
					keys.sendL("Ctrl+A", "!Пост", "Enter");
					return;
				}
				if (antennaCamera.Contains(targetClassJbd)) {
					keys.sendL("Ctrl+A", "!антени", "Enter");
					return;
				}
				switch (targetClassJbd) {
				case "Міна":
					keys.sendL("Ctrl+A", "!11", "Enter");
					break;
				case "Загородження":
					keys.sendL("Ctrl+A", "!11", "Enter");
					break;
				case "Бліндаж":
					keys.sendL("Ctrl+A", "!07", "Enter");
					break;
				case "Т. вильоту дронів":
					keys.sendL("Ctrl+A", "!08", "Enter");
					break;
				case "ОС РОВ":
					keys.sendL("Ctrl+A", "!10", "Enter");
					break;
				case "Міномет":
					keys.sendL("Ctrl+A", "!09", "Enter");
					break;
				default:
					if (commentJbd.ToLower().Contains("рус") || commentJbd.ToLower().Contains("рух")) {
						keys.sendL("Ctrl+A", "!06", "Enter");
					} else if (commentJbd.ToLower().Contains("виходи") || commentJbd.ToLower().Contains("вогнева позиція")) {
						keys.sendL("Ctrl+A", "!05", "Enter");
					} else {
						keys.sendL("Ctrl+A", "!04", "Enter");
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
				string states = "Розміновано Підтв. ураж. Тільки розрив";
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
			string pozashlyakhovyk = "Авто БМП (техніка) БТР Військ. баггі";
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
