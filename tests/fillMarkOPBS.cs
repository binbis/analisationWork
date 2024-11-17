/*/ c \analisationWork\globalClass\Bisbin.cs; /*/

/* 17.11.2024 2.0

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
				return;
			}
			if (itemSelect.Text.Contains("2.")) {
				createREBandRER(clipboardData);
				return;
			}
			if (itemSelect.Text.Contains("3.")) {
				createWhoWork(clipboardData);
				return;
			}
			if (itemSelect.Text.Contains("4.")) {
				createImportFileToMine(clipboardData);
				return;
			}
			if (itemSelect.Text.Contains("5.")) {
				Console.WriteLine("Рано, поки в роботі міни");
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
			//. перехід по корам
			//var searchWindow = w.Elm["web:COMBOBOX", prop: new("@aria-label=Пошук", "@placeholder=Знайти адресу або координату")].Find(1);
			//searchWindow.PostClick();
			//keys.sendL("Ctrl+A", "!" + mgsrCoord, "Enter");
			//.. 
			//wait.ms(2000);
			//. ставимо мітку
			var createButton = w.Elm["LISTITEM", "Створити об'єкт", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(1);/*image:WkJNGzUEAMSIsflI5ojmXk2TRNOJhsz1qTbWF6kLdR4K605/aykfC+x4oVvz1gItkeBrCSSYQsKxRnA2J0mKsQ/fA2kLGZUGUIYu5GQClqOVOl/InZ7CohTDnsugWAZG399A+Rl+oxoUCg3JwwNsbzbQB/1orFaIfMTL9noNb6sJQNBeOjzYWV5cnOPGDZ/JRbawcPPP7kxfpeo/FNmx//qDKRku7XCVnM8Hccd8cVGFd+JN7bxdpFGEvCA0X1ycJPY7jIs3lB0s2/X+BQA=*/
			createButton.PostClick(scroll: 250);
			wait.ms(2000);
			// обираємо мітку
			var categorySearch = w.Elm["TEXT", "Пошук об'єктів", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(1);/*image:WkJNGzUEAMQnxr3v4zqsbWlukJR2nSGugRk22/GTboRLRxkHQZhQCdRdNlB8phLT9IbbLpdENMPTz4mMqOWEBs4QdQKWKxul/651t5sQG+SH+IpejPe2IjYmDQutIeiZWEZtaR76g/JRHxeA1dV5NNR1Y3OyGVNVHYiNqEVzdjkynfr4GwfXg3q93v26LEgSKLfXbyE7b7//R9mkWelcOZ1ep3qYHro/cnZzG0f/YJ9fdz9uOKcYlCvnorJP2dSvigovr4oiKsoQsQ02OejSk3I5AQ==*/
			categorySearch.PostClick();
			keys.sendL("Ctrl+A", "!" + bplaName);
			wait.ms(3000);
			var bplaMark = w.Elm["TEXT", "Пошук об'єктів", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2 child"].Find(1);/*image:WkJNG/0DAATCdr9tIAMZR0nadV0+yBPioBnHzx5LEFFqEx+47G4dIJS1p76RFxqTHAVHdFvao/NuQY/n8zkd*/
			bplaMark.PostClick();
			wait.ms(2000);
			//..
			//. шар
			var layerWindow = w.Elm["STATICTEXT", "Шар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
			layerWindow.PostClick(scroll: 300);
			keys.sendL("Ctrl+A", "!" + layerName, "Enter");
			//..
			wait.ms(400);
			//. назва
			var nameOfMarkWindow = w.Elm["STATICTEXT", "Назва", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next1"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
			nameOfMarkWindow.PostClick(scroll: 300);
			keys.sendL("Ctrl+A", "!" + name, "Enter");
			//..
			wait.ms(400);
			//. час виявлення
			deltaDateLTimeWindow(dateDeltaFormat, timeJbd);
			//..
			wait.ms(400);
			//. боєздатність
			var combatCapabilityWindow = w.Elm["STATICTEXT", "Боєздатність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
			combatCapabilityWindow.PostClick(scroll: 250);
			keys.sendL("Ctrl+A", "!" + capability, "Enter*2");
			//..
			wait.ms(400);
			//. ідентифікація
			var identificationWindow = /*image:WkJNG30IAAQib/e/D18VodkEkm7zSdEkLQAnHA8Ygyzg5AB+cDzYGWMMX6sVNHG+E01UHq2CISWzbtvW6NwdpP5G+JwaAA==*/w.Elm["STATICTEXT", "Ідентифікація", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
			identificationWindow.PostClick(scroll: 250);
			keys.sendL("Ctrl+A", "!" + identyfication, "Enter");
			//..
			wait.ms(400);
			//. коментар
			var commentWindow = w.Elm["STATICTEXT", "Новий коментар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
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
				Протитанкова міна (ПТМ) - 10011500002103000000
				дружня - 10031500002103000000
					повність боездатна - 10031520002103000000
					частково боездатна - 10031530002103000000
					не боездатна - 10031540002103000000
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
					// підготовка значнь для полів
					string sidc = string.Empty;
					string states = "Розміновано Підтв. ураж. Тільки розрив Авар. скид";
					if (states.Contains(establishedJbd)) {
						sidc = "10031540002103000000";
					} else if (establishedJbd.Contains("Встановлено")) {
						sidc = "10031520002103000000";
					} else if (establishedJbd.Contains("Спростовано")) {
						return;
					} else {
						sidc = "10031530002103000000";
					}
					
					string name = Bisbin.createMineName(nameOfBch, targetClassJbd, dateJbd, establishedJbd, commentJbd, twoHundredth, threeHundredth);
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
			var layerWindow = w.Elm["STATICTEXT", "Шар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
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
		static string deltaMarkName(string nameOfBch, string targetClassJbd, string dateJbd, string establishedJbd, string commentJbd, string twoHundredth, string threeHundredth) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			string nameIs = string.Empty;
			// поле назва
			var nameOfMarkWindow = w.Elm["STATICTEXT", "Назва", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next1"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
			if (nameOfMarkWindow != null) {
				string markName = string.Empty;
				string nameOfMark = nameOfMarkWindow.Value;
				int indexLoss = nameOfMark.IndexOf(' ');
				string states = "Розміновано Підтв. ураж. Тільки розрив";
				
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
				nameIs = markName;
			}
			return nameIs;
			
		}
		// поле дата / час
		static void deltaDateLTimeWindow(string dateDeltaFormat, string timeJbd) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// дата
			var dateDeltaWindow = w.Elm["TEXT", "дд/мм/рррр", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(1);/*image:WkJNGzUEAOSP/dVbTN+/aZ2JtM6uBBp4YAbbhXapUGWnH/svdsCJHI4JJZIlESZwwcC4gC1wjjmTWfDhXaOTYiGRB3TIQGsoGEcS1wu1iAM2kxbOBxwsttcQS1fA53HCcjSG+8sltJxa6JlUHYdhuL08A1uko59uskI55AR7YQHRI5e78TcQvEBsWhOx8vH58fBFbZCE8sGfGy8UrAyk3Uzk/znzA/HbSzD7kHXEj+OpAA==*/
			if (dateDeltaWindow != null) {
				dateDeltaWindow.PostClick();
				keys.sendL("Ctrl+A", "!" + dateDeltaFormat);
			}
			wait.ms(500);
			// час
			var timeDeltaWindow = w.Elm["TEXT", "гг:хх", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(1);/*image:WkJNGzUEAOSP/dVbTN+/aZ2JtM6uBBp4YAbbhXapUGWnH/svdsCJHI4JJZIlESZwwcC4gC1wjjmTWfDhXaOTYiGRB3TIQGsoGEcS1wu1iAM2kxbOBxwsttcQS1fA53HCcjSG+8sltJxa6JlUHYdhuL08A1uko59uskI55AR7YQHRI5e78TcQvEBsWhOx8vH58fBFbZCE8sGfGy8UrAyk3Uzk/znzA/HbSzD7kHXEj+OpAA==*/
			if (timeDeltaWindow != null) {
				timeDeltaWindow.PostClick();
				keys.sendL("Ctrl+A", "!" + timeJbd, "Enter");
				
			}
		}
		// поле кількість
		static void deltaNumberOfnumberWindow(string twoHundredth, string threeHundredth) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// поле кількість
			var numberOfnumberWindow = w.Elm["STATICTEXT", "Кількість", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
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
			var combatCapabilityWindow = w.Elm["STATICTEXT", "Боєздатність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
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
			var identificationWindow = /*image:WkJNG30IAAQib/e/D18VodkEkm7zSdEkLQAnHA8Ygyzg5AB+cDzYGWMMX6sVNHG+E01UHq2CISWzbtvW6NwdpP5G+JwaAA==*/w.Elm["STATICTEXT", "Ідентифікація", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
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
			var idPurchaseWindow = w.Elm["STATICTEXT", "Зауваження штабу", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
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
			var mobileLine = w.Elm["STATICTEXT", "Мобільність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
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
			// коментар
			var commentWindow = w.Elm["STATICTEXT", "Новий коментар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
			if (commentWindow != null) {
				string commentContents = Bisbin.createComment(targetClassJbd, dateJbd, timeJbd, crewTeamJbd, establishedJbd, commentJbd, mgrsCoords);
				commentWindow.PostClick(scroll: 250);
				keys.sendL("Ctrl+A", "!" + commentContents);
			}
			wait.ms(500);
			var commentWindowAcceptButton = w.Elm["STATICTEXT", "Новий коментар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next4"].Find(1);/*image:WkJNGwkEAMTnudy/Me1A9ta1az9PLNdFEJZwUJPjY3+WIEwoC6h5DoFKW+AhuyYJNrI/ijRsO+FSIgeBscuZvuiKCJ/7jNv1gPf3j9+DuZTfQNfP2qeYua4D*/
			commentWindowAcceptButton.PostClick(scroll: 250);
		}
		// додаткові поля
		static void deltaAdditionalFields(string idTargetJbd, string targetClassJbd) {
			// основна вкладка
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1");
			// додаткові поля
			Bisbin.goToAdditionalField();
			// зауваження штабу поле
			var idPurchaseWindow = w.Elm["STATICTEXT", "Зауваження штабу", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next"].Find(-1);/*image:WkJNGzUEAOSvd6ablD5dTRQijSoFgLzZOYVUXiuAmvOqaE80u3oOyC1bRZr/jhxElckwVXrufsZu2eHP8+PP8dnw47ACxBILfDiQMUZi1PtH4xFt7r6ZDIIyVmF73s2owiA0gZbQmGwAWjaT3j5/PWrBRLRoA0GTVs9F9xU3o1n3LEWTehajO8hsNGXtEjTTXol6H3sUTVuXRcs2r8O7Zymac99KZPQsRNqyGciBKOqjEAsaAoEGPd/wZPmvuPOjlAhBeXf0zMZqFEMI3LYAjxO/8F28+yB8Lf5D6PmAVivR8/tjAb5rNx/5tTb6K6+vXSh3trYRunBYlKIoBhBwr5uVABJ2JqnX12bALgqE5PPPrK9GMQUIQz7yw5uusJa2IfBk0NnvYB841UoUVcphRdoWhxYAyHQog9JL2Zh8WKFVUXopDTxogh/aiUU7wJahhKheEk61Unb+N3VMpqJzL5gSmlmNqSMo3Q3gA2+2jlla8kxj4IOg6wVfen4/AN2qQHPNHm0DMMaYy1w/d422cX8kK1udipNybEqScojKfh5Mg2nbLiMKxDEAMADDlbalccUH82kAQ7vXBTr7492wH8oFFsrfDZ0QzFVfbDkmJeFPO7To+VyhI4LbOjvkZ4/gtSeJpYkwqSlK60ObVJRjnXj34UdT+OxHy9SyQ3ViAZctNds6C1z1fK7giFIaytTPpppJ/PEqsUQo8dQ0l6dSJ3A2O9W4dYwkdZP72tqT2dEWhem94/ozWl1KgYr87rCVNK3qlqYPOpNNhnZR93K3HwGviTyHIAMwMklbqo51f+1JzusIscz1i21g3s/lYRvyQLE0Qohv93AlcJvdG9Z1r/o+0BTbMoyha1uGFYvaeNXMYr82zhut7RxGN0YxkMM2hK51MzfwrT2ujg2daIQxzW4NExv37yeU5vS1W7soM/3E7OPYLV7WXMw0W8mFQG409Hczme6t1ycv3jNTUQ1m6AS7NeVDLuhGoO8yl7HrEP62tPHawpm4lxCiktroX8ul+L+/sJAHpYqUMhy+eOHs947hrEIUQnjKCuHLTt66NpFSpoH3/XtvnHtvjqKoxlq8jni+7xBo1j5MD8ZtxMxwzxuDXlNUrBPMuOXdy6Mk0m5bX3sA7VtdHZ+bY41o2kiMMybYQDfK/dgjhwYM9n7NQLeGv7lwzmJNVwgu9usqBNehgxA=*/
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
			var deltaColorFills = w.Elm["STATICTEXT", "Колір заливки", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);/*image:WkJNGzUEAOT+dbVBOhEJciYB36AfgtkPli3OJkjwfxkkJRK8hKKkPCn3BU/SDBo8G8RKKq/u77dZ/0uIRhpEi/Qyc0uk5WjWxOJWd4Nve3dPh5oaguQQv3VXbpo/EG63B1RCAabG+8HHefzD3z9VZy4Cw1oHS1s2wvwdK0F0bIPC3jWwsrgBVrVugnW4HVbv3yITh/RNy8Cw18NiKWB1cav+c2AbtA2wvaIuUwNzq2MeIyEiusy2lLsqCOBqGb0dhp0amO7yX8XoeDUowh9oi0zX8dN5diRxh/D0NUa8ZD7rMfk75iXkY2q52Xx86jZGzEGfTxB1VhC5uYWPX5OWuXZHGkdSdspabazdrlFwnkjR47L/iHwlM91GkLtO1FZYq6DsKOEnhCYq5sVng4D4BxpNnv5PfcJ81L7FQ4rS3iTiifxT24kqTvfduNmnru7yDltVVQtj9v2hIIBdfXSi4lRwOlBXC06HRV6MDrWbQl2kWzcEAJ3+W6FEEASnrhYcX7Jr+0QPGpPdDgIAimpUc9eFglrJzLpk1jSt4kSi1grpSASnggv2I0qfJ9bGpECdvxgnK2sjLkoZ+1qtdoKU2UWi21CgcBEbZnXakhFdjSfzFsA82mKSEsG5QJg+P9polON2l4kpCeTKDFJCiAuDtUL4MsEsHLuxzMyx4ybEHjZ4IUxjbuOzUjilyT2/iJ1E3hIiQqQ4Fh+ZVCp1hXmqG6nOrmSJ2PLLVBeZQlMm4v7PkydB+Dwr9S4ZVNiBWExYEtHCIy1HWrq6X2dhqvcKjUiUkx7MnPLc51PGYsuIrKu7ZfIIzpxh03WJMJW4iM+z8oifLY0QIqKTNSn5QfJEp1bnadOUsVOmlF6hc47E6PZJ8vAUIbbkmZEfzl85I3T7SAXWhKqYPnOPB364WWh0oOV46JikBanBiTCJiLEs0IwQj/WSseqWM6iWwsnsd5ow9hBJnfOjrkFhnlu/igFgUPeUPFYu3/GQ0DFVIMxnEGL+CqWUAFAakZdjRYqER+gMpEIPjXk1HcQOAA==*/
			if (deltaColorFills != null) {
				switch (targetClassJbd) {
				//. укриття
				case "Укриття":
					if (establishedJbd.ToLower().Contains("знищ") || establishedJbd.ToLower().Contains("ураж")) {
						// колір жовтий
						var placeColorYellowButton = w.Elm["BUTTON", "#ffeb3b", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);/*image:WkJNGzUEAOT+dbVBOhEJciYB36AfgtkPli3OJkjwfxkkJRK8hKKkPCn3BU/SDBo8G8RKKq/u77dZ/0uIRhpEi/Qyc0uk5WjWxOJWd4Nve3dPh5oaguQQv3VXbpo/EG63B1RCAabG+8HHefzD3z9VZy4Cw1oHS1s2wvwdK0F0bIPC3jWwsrgBVrVugnW4HVbv3yITh/RNy8Cw18NiKWB1cav+c2AbtA2wvaIuUwNzq2MeIyEiusy2lLsqCOBqGb0dhp0amO7yX8XoeDUowh9oi0zX8dN5diRxh/D0NUa8ZD7rMfk75iXkY2q52Xx86jZGzEGfTxB1VhC5uYWPX5OWuXZHGkdSdspabazdrlFwnkjR47L/iHwlM91GkLtO1FZYq6DsKOEnhCYq5sVng4D4BxpNnv5PfcJ81L7FQ4rS3iTiifxT24kqTvfduNmnru7yDltVVQtj9v2hIIBdfXSi4lRwOlBXC06HRV6MDrWbQl2kWzcEAJ3+W6FEEASnrhYcX7Jr+0QPGpPdDgIAimpUc9eFglrJzLpk1jSt4kSi1grpSASnggv2I0qfJ9bGpECdvxgnK2sjLkoZ+1qtdoKU2UWi21CgcBEbZnXakhFdjSfzFsA82mKSEsG5QJg+P9polON2l4kpCeTKDFJCiAuDtUL4MsEsHLuxzMyx4ybEHjZ4IUxjbuOzUjilyT2/iJ1E3hIiQqQ4Fh+ZVCp1hXmqG6nOrmSJ2PLLVBeZQlMm4v7PkydB+Dwr9S4ZVNiBWExYEtHCIy1HWrq6X2dhqvcKjUiUkx7MnPLc51PGYsuIrKu7ZfIIzpxh03WJMJW4iM+z8oifLY0QIqKTNSn5QfJEp1bnadOUsVOmlF6hc47E6PZJ8vAUIbbkmZEfzl85I3T7SAXWhKqYPnOPB364WWh0oOV46JikBanBiTCJiLEs0IwQj/WSseqWM6iWwsnsd5ow9hBJnfOjrkFhnlu/igFgUPeUPFYu3/GQ0DFVIMxnEGL+CqWUAFAakZdjRYqER+gMpEIPjXk1HcQOAA==*/
						placeColorYellowButton.PostClick(scroll: 250);
						wait.ms(500);
						// відсоток прозрачності
						var transpatentColorRange = w.Elm["web:SLIDER"].Find(1);/*image:WkJNGzUEAMQn9ldvQTXxAKVNl+jkLmeh/blIJCtEwu895QOJRTBEZxrvHqAV6cZu/K8UcCxRCdzV5eMlrSjMuvisYCq6U4fMFEn6goTFvkAYf3+7SYvIiC70trcgzudASNwDYoOSsbm2iqrifIRFzyE6tBy311fIiI/GzPgoghPe8PT4iNDYXfjGX2KPEqTMukuVZRGLBlLdHV8inM2lRUfOXXvAl939EGiDsr5DIOw/SR5z2R6EPih0r27/JWTQ3SA7dFZ6Lt2GHhGhV2zYpfCfsUGnVdSJTe7glZWMM+CdHcYdzckEkf9GuMlanyNzRjUxnWOcvrPxRzi3suSPX+I019bIshobqdGpPcNpHQ==*/
						transpatentColorRange.PostClick(scroll: 250);
						transpatentColorRange.SendKeys("Left*5");
						wait.ms(500);
					} else if (states.Contains(establishedJbd)) {
						if (commentJbd.ToLower().Contains("знищ") || commentJbd.ToLower().Contains("ураж")) {
							// колір жовтий
							var placeColorYellowButton = w.Elm["BUTTON", "#ffeb3b", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);/*image:WkJNGzUEAOT+dbVBOhEJciYB36AfgtkPli3OJkjwfxkkJRK8hKKkPCn3BU/SDBo8G8RKKq/u77dZ/0uIRhpEi/Qyc0uk5WjWxOJWd4Nve3dPh5oaguQQv3VXbpo/EG63B1RCAabG+8HHefzD3z9VZy4Cw1oHS1s2wvwdK0F0bIPC3jWwsrgBVrVugnW4HVbv3yITh/RNy8Cw18NiKWB1cav+c2AbtA2wvaIuUwNzq2MeIyEiusy2lLsqCOBqGb0dhp0amO7yX8XoeDUowh9oi0zX8dN5diRxh/D0NUa8ZD7rMfk75iXkY2q52Xx86jZGzEGfTxB1VhC5uYWPX5OWuXZHGkdSdspabazdrlFwnkjR47L/iHwlM91GkLtO1FZYq6DsKOEnhCYq5sVng4D4BxpNnv5PfcJ81L7FQ4rS3iTiifxT24kqTvfduNmnru7yDltVVQtj9v2hIIBdfXSi4lRwOlBXC06HRV6MDrWbQl2kWzcEAJ3+W6FEEASnrhYcX7Jr+0QPGpPdDgIAimpUc9eFglrJzLpk1jSt4kSi1grpSASnggv2I0qfJ9bGpECdvxgnK2sjLkoZ+1qtdoKU2UWi21CgcBEbZnXakhFdjSfzFsA82mKSEsG5QJg+P9polON2l4kpCeTKDFJCiAuDtUL4MsEsHLuxzMyx4ybEHjZ4IUxjbuOzUjilyT2/iJ1E3hIiQqQ4Fh+ZVCp1hXmqG6nOrmSJ2PLLVBeZQlMm4v7PkydB+Dwr9S4ZVNiBWExYEtHCIy1HWrq6X2dhqvcKjUiUkx7MnPLc51PGYsuIrKu7ZfIIzpxh03WJMJW4iM+z8oifLY0QIqKTNSn5QfJEp1bnadOUsVOmlF6hc47E6PZJ8vAUIbbkmZEfzl85I3T7SAXWhKqYPnOPB364WWh0oOV46JikBanBiTCJiLEs0IwQj/WSseqWM6iWwsnsd5ow9hBJnfOjrkFhnlu/igFgUPeUPFYu3/GQ0DFVIMxnEGL+CqWUAFAakZdjRYqER+gMpEIPjXk1HcQOAA==*/
							placeColorYellowButton.PostClick(scroll: 250);
							wait.ms(500);
							// відсоток прозрачності
							var transpatentColorRange = w.Elm["web:SLIDER"].Find(1);/*image:WkJNGzUEAMQn9ldvQTXxAKVNl+jkLmeh/blIJCtEwu895QOJRTBEZxrvHqAV6cZu/K8UcCxRCdzV5eMlrSjMuvisYCq6U4fMFEn6goTFvkAYf3+7SYvIiC70trcgzudASNwDYoOSsbm2iqrifIRFzyE6tBy311fIiI/GzPgoghPe8PT4iNDYXfjGX2KPEqTMukuVZRGLBlLdHV8inM2lRUfOXXvAl939EGiDsr5DIOw/SR5z2R6EPih0r27/JWTQ3SA7dFZ6Lt2GHhGhV2zYpfCfsUGnVdSJTe7glZWMM+CdHcYdzckEkf9GuMlanyNzRjUxnWOcvrPxRzi3suSPX+I019bIshobqdGpPcNpHQ==*/
							transpatentColorRange.PostClick(scroll: 250);
							transpatentColorRange.SendKeys("Left*5");
							wait.ms(500);
						} else {
							//колір червоний - ворож
							var placeColorRedButton = w.Elm["BUTTON", "#f44336", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);/*image:WkJNGzUEAOT+dbVBOhEJciYB36AfgtkPli3OJkjwfxkkJRK8hKKkPCn3BU/SDBo8G8RKKq/u77dZ/0uIRhpEi/Qyc0uk5WjWxOJWd4Nve3dPh5oaguQQv3VXbpo/EG63B1RCAabG+8HHefzD3z9VZy4Cw1oHS1s2wvwdK0F0bIPC3jWwsrgBVrVugnW4HVbv3yITh/RNy8Cw18NiKWB1cav+c2AbtA2wvaIuUwNzq2MeIyEiusy2lLsqCOBqGb0dhp0amO7yX8XoeDUowh9oi0zX8dN5diRxh/D0NUa8ZD7rMfk75iXkY2q52Xx86jZGzEGfTxB1VhC5uYWPX5OWuXZHGkdSdspabazdrlFwnkjR47L/iHwlM91GkLtO1FZYq6DsKOEnhCYq5sVng4D4BxpNnv5PfcJ81L7FQ4rS3iTiifxT24kqTvfduNmnru7yDltVVQtj9v2hIIBdfXSi4lRwOlBXC06HRV6MDrWbQl2kWzcEAJ3+W6FEEASnrhYcX7Jr+0QPGpPdDgIAimpUc9eFglrJzLpk1jSt4kSi1grpSASnggv2I0qfJ9bGpECdvxgnK2sjLkoZ+1qtdoKU2UWi21CgcBEbZnXakhFdjSfzFsA82mKSEsG5QJg+P9polON2l4kpCeTKDFJCiAuDtUL4MsEsHLuxzMyx4ybEHjZ4IUxjbuOzUjilyT2/iJ1E3hIiQqQ4Fh+ZVCp1hXmqG6nOrmSJ2PLLVBeZQlMm4v7PkydB+Dwr9S4ZVNiBWExYEtHCIy1HWrq6X2dhqvcKjUiUkx7MnPLc51PGYsuIrKu7ZfIIzpxh03WJMJW4iM+z8oifLY0QIqKTNSn5QfJEp1bnadOUsVOmlF6hc47E6PZJ8vAUIbbkmZEfzl85I3T7SAXWhKqYPnOPB364WWh0oOV46JikBanBiTCJiLEs0IwQj/WSseqWM6iWwsnsd5ow9hBJnfOjrkFhnlu/igFgUPeUPFYu3/GQ0DFVIMxnEGL+CqWUAFAakZdjRYqER+gMpEIPjXk1HcQOAA==*/
							placeColorRedButton.PostClick(scroll: 250);
							wait.ms(500);
							// відсоток прозрачності
							var transpatentColorRange = w.Elm["web:SLIDER"].Find(1);/*image:WkJNGzUEAMQn9ldvQTXxAKVNl+jkLmeh/blIJCtEwu895QOJRTBEZxrvHqAV6cZu/K8UcCxRCdzV5eMlrSjMuvisYCq6U4fMFEn6goTFvkAYf3+7SYvIiC70trcgzudASNwDYoOSsbm2iqrifIRFzyE6tBy311fIiI/GzPgoghPe8PT4iNDYXfjGX2KPEqTMukuVZRGLBlLdHV8inM2lRUfOXXvAl939EGiDsr5DIOw/SR5z2R6EPih0r27/JWTQ3SA7dFZ6Lt2GHhGhV2zYpfCfsUGnVdSJTe7glZWMM+CdHcYdzckEkf9GuMlanyNzRjUxnWOcvrPxRzi3suSPX+I019bIshobqdGpPcNpHQ==*/
							transpatentColorRange.PostClick(scroll: 250);
							transpatentColorRange.SendKeys("Left*5");
							wait.ms(500);
						}
					} else {
						//колір червоний - ворож
						var placeColorRedButton = w.Elm["BUTTON", "#f44336", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1);/*image:WkJNGzUEAOT+dbVBOhEJciYB36AfgtkPli3OJkjwfxkkJRK8hKKkPCn3BU/SDBo8G8RKKq/u77dZ/0uIRhpEi/Qyc0uk5WjWxOJWd4Nve3dPh5oaguQQv3VXbpo/EG63B1RCAabG+8HHefzD3z9VZy4Cw1oHS1s2wvwdK0F0bIPC3jWwsrgBVrVugnW4HVbv3yITh/RNy8Cw18NiKWB1cav+c2AbtA2wvaIuUwNzq2MeIyEiusy2lLsqCOBqGb0dhp0amO7yX8XoeDUowh9oi0zX8dN5diRxh/D0NUa8ZD7rMfk75iXkY2q52Xx86jZGzEGfTxB1VhC5uYWPX5OWuXZHGkdSdspabazdrlFwnkjR47L/iHwlM91GkLtO1FZYq6DsKOEnhCYq5sVng4D4BxpNnv5PfcJ81L7FQ4rS3iTiifxT24kqTvfduNmnru7yDltVVQtj9v2hIIBdfXSi4lRwOlBXC06HRV6MDrWbQl2kWzcEAJ3+W6FEEASnrhYcX7Jr+0QPGpPdDgIAimpUc9eFglrJzLpk1jSt4kSi1grpSASnggv2I0qfJ9bGpECdvxgnK2sjLkoZ+1qtdoKU2UWi21CgcBEbZnXakhFdjSfzFsA82mKSEsG5QJg+P9polON2l4kpCeTKDFJCiAuDtUL4MsEsHLuxzMyx4ybEHjZ4IUxjbuOzUjilyT2/iJ1E3hIiQqQ4Fh+ZVCp1hXmqG6nOrmSJ2PLLVBeZQlMm4v7PkydB+Dwr9S4ZVNiBWExYEtHCIy1HWrq6X2dhqvcKjUiUkx7MnPLc51PGYsuIrKu7ZfIIzpxh03WJMJW4iM+z8oifLY0QIqKTNSn5QfJEp1bnadOUsVOmlF6hc47E6PZJ8vAUIbbkmZEfzl85I3T7SAXWhKqYPnOPB364WWh0oOV46JikBanBiTCJiLEs0IwQj/WSseqWM6iWwsnsd5ow9hBJnfOjrkFhnlu/igFgUPeUPFYu3/GQ0DFVIMxnEGL+CqWUAFAakZdjRYqER+gMpEIPjXk1HcQOAA==*/
						placeColorRedButton.PostClick(scroll: 250);
						wait.ms(500);
						// відсоток прозрачності
						var transpatentColorRange = w.Elm["web:SLIDER"].Find(1);/*image:WkJNGzUEAMQn9ldvQTXxAKVNl+jkLmeh/blIJCtEwu895QOJRTBEZxrvHqAV6cZu/K8UcCxRCdzV5eMlrSjMuvisYCq6U4fMFEn6goTFvkAYf3+7SYvIiC70trcgzudASNwDYoOSsbm2iqrifIRFzyE6tBy311fIiI/GzPgoghPe8PT4iNDYXfjGX2KPEqTMukuVZRGLBlLdHV8inM2lRUfOXXvAl939EGiDsr5DIOw/SR5z2R6EPih0r27/JWTQ3SA7dFZ6Lt2GHhGhV2zYpfCfsUGnVdSJTe7glZWMM+CdHcYdzckEkf9GuMlanyNzRjUxnWOcvrPxRzi3suSPX+I019bIshobqdGpPcNpHQ==*/
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
