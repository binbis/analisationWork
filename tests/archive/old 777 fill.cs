string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
			
			string ecipashName = parts[1]; // екіпаж
			string flyDot = $"Т.в. \"{parts[2]}\""; // точка вильоту
			string mgrsCoord = parts[10]; // координати
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
			} else if (ecipashName.Contains("FPV")) {
				var lightBlueColorFill = w.Elm["web:BUTTON", "#00bcd4"].Find(1);/*image:WkJNG1UGAMSIMfczSfO4ZB6SWjJzzCKJI4omIlSodOD7ueWf2w6u1ZUwmE0RsVssVZ4IP1hWQrNHif+dnclkkhLed5p9ISp6KwlZuENyEECGecAxsec+r8moxt2lBsWQGiGXBX/fBpQTYXy96LBsqvH/a0Q3q8bPpx6nYzVGD/phSEYjsLYaQ+A8yN5srIeTNnL2YSdWTgwciS2TE/ljK+1EuqOQ2bui4h69WSeSH4Rp3suteSoicysV80ic5sHzxM2QvZGzIHeD3DXydkgWkGwxWUWyj9QSU5tIrV//OwA=*/
				lightBlueColorFill.PostClick(scroll: 500);
			} else {
				var yelowTwoColorFill = w.Elm["web:BUTTON", "#ffc107"].Find(1);/*image:WkJNG2UGAMSAcXlPhgiyMWNbAxiqnV2/DQlSI9J8vpt8bP2jXkjptLF8C81/g3oRF0EWmMZBbbP7qSkcMghNm0LxPCpmUIdQiIiQtQSQQVojJOHvvQ2atfANTmJ1dRW+RBacy29wnz/DMXcIT3kzzxa34Nz4h6epBd7sEjjS93A/cNXmJVwdU1huouj/YdzG+g73lM9no2KuDHIyMoHU0JAKB7H0ea1HyhoHZfbjcVFR0AaZe08u9qQ5z2K35NhRiH3mRXBH5qAxJwTXwMYFOIM0DsMO/OH4PBijkBk+cABh7IELc9DwodxNiYWSG0quKW/HqAVKbVFqlVH7lF6i9OaK2dDrazD+jg8A*/
				yelowTwoColorFill.PostClick(scroll: 500);
			}
			wait.ms(200);
			// відсотки залівки колір залівки
			var rangeSectorFillColor = w.Elm["web:SLIDER", prop: "@data-testid=opacity-slider"].Find(1);/*image:WkJNG2UGAMTnGZf/2OgIJSUIcfOd24S6t+jXl+GEKCUAbVFTN/lHqW/MPrDAu2wtoLE5kDXBckghpDSoG7p+MUmSEo0rmqfC9Q6ACoSAIieADKI2Njaz9xyHTsNa0Yav9TLaczYc3T9jqKcOToMeU8MjmN09gTMxjUl717//X/zvLeC8042My4yftw3k474EmBXeRF4L1sjL34My5C0Bla+KGHIxjqW4MgM+XaFWupoarcu9vb3dPdmrdZcm0yS7p7hlkt9M8msx8lfk9VPlTeFDNpu1BaZs2kvDaIiesuzUEQMA*/
			rangeSectorFillColor.PostClick();
			rangeSectorFillColor.SendKeys("Left*8");
			wait.ms(200);