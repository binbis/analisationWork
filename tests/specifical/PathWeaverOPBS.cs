/*/ c \analisationWork\globalClass\Bisbin.cs; /*/
/* 05.12.24 створювання папки
на основі жбд створує папку в папці з ієрархією
folde -> нава екіпажу -> ід цілі
та відкриває її

потім зможе щей перейменовувати, до ід цілі номер вильоту приписувати інший вже до існуючого

*/
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CSLight {
	
	class Program {
		static void Main() {
			opt.key.KeySpeed = 20;
			opt.key.TextSpeed = 20;
			
			string rememberPath = @"C:\Users\User-PM\Documents\LibreAutomate\Main\files\analisationWork\customTemp\pathes.txt"; // шлях до файлу зі збереженим шляхом
			string pathWithRemebers = File.ReadAllText(rememberPath);
			
			keys.send("Ctrl+C"); //копіюємо код
			wait.ms(100);
			string clipboardData = clipboard.copy(); // зчитуємо буфер обміну
			
			//string pathToMainFolder = @"C:\Users\User-PM\Desktop\еtest"; // потім буде записуватися до файлу txt та читатися звідтиля
			string[] parts = clipboardData.Split('\t'); // Розділяємо рядок на частини
			string flyNumber = parts[3]; // номер вильоту
			string crewName = parts[4]; // імя екіпажу
			string aidId = parts[9]; // ід цілі
			
			string partFolderByCrew = $"{crewName}"; // заготовка екіпажа
			string partFolderByFlyNumberAndId = $"({flyNumber}) {aidId}"; // заготовка ід
			
			string pathToFolderByCrew = pathname.combine(pathWithRemebers,partFolderByCrew); // збирає шляхи папки екіпаж
			bool f = filesystem.createDirectory(pathToFolderByCrew); // пробую створити папку
			//перевірка
			if (!f) {
				Console.WriteLine($"папка {crewName} вже існує");
			}
			wait.ms(400);
			string pathToFolderById = pathname.combine(pathToFolderByCrew,partFolderByFlyNumberAndId); // збирає шляхи папки ід
			f = filesystem.createDirectory(pathToFolderById); // пробую створити папку
				//перевірка
			if (!f) {
				Console.WriteLine($"папка {partFolderByFlyNumberAndId} в {partFolderByCrew} папці вже існує");
			}
			//Console.WriteLine($"{}");
			Process.Start("explorer.exe", pathToFolderById); // відкриваємо папку
		}
		
	}
}


/* інтерфейс який ми заслуговуємо

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

var bMain = new wpfBuilder("Window").WinSize(600);
var b = bMain;
b.Row(-1).Add(out TabControl tc).Height(250..);
b.R.AddOkCancel(apply: "_Apply").Font(size: 14, bold: false);
b.Window.Topmost = true;

wpfBuilder _Page(string name, WBPanelType panelType = WBPanelType.Grid) {
	var tp = new TabItem { Header = name };
	tc.Items.Add(tp);
	return new wpfBuilder(tp, panelType);
}

var b1 = b = _Page("Скрипти");
//Brush
b.Font(size: 17, bold: true);
b.Brush(Brushes.DarkGray);
// insider
b.R.AddButton("1. Готове речення в буфер обміну", 2).Brush(Brushes.LightCoral);
b.R.AddButton("2. Створти папку екіпаж -> ід", 5).Brush(Brushes.LightCyan);
b.R.AddButton("3. Перейменування в папці Бамбас", 1).Brush(Brushes.LightSalmon);
b.R.AddButton("4. Перейменування в папці Уголь", 3).Brush(Brushes.LightGoldenrodYellow);
b.R.AddButton("5. Перейменування в папці Уголь-Суджа", 4).Brush(Brushes.LightGoldenrodYellow);
b.End();

var b2 = b = _Page("Технічне налаштування");
//Brush
b.Font(size: 16, bold: true);
b.Brush(Brushes.DarkGray);
// insider
b2.R.Add(out Label _, "Шлях до папки де буде створено папки в папках").AlignContent(HorizontalAlignment.Center);
b.R.Add(out TextBox text1).Font(size: 14, bold: false).Size(400, 40).AddButton("Запам'ятати", 99).Brush(Brushes.LightCoral).Width(125);
b.End();



b = bMain.End();
if (!b.ShowDialog()) return;

