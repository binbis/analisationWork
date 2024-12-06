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

