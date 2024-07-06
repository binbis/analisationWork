/**
06,07,2024_v2.2b
0. відкрий дельту в окремій вкладці та вікні, (!не міняй вкладку!), можеш звернути це вікно
1. виділяєш текст де є координати mgrs
2. жмеш hotkey
3. відкриває дельту(де б вона не була та який розмір вікна б не мала) 
4. вставяє оброблені координати до пошуку та натискає кнопку пошуку

ХОЧУ:
*. ще не придумав як нормально заміняти укр-ру букри на англ. можливо потрібно змінити концет
* щоб не переносився курсор
*/
//using System;
using System.Windows.Forms;
//using System.Text.RegularExpressions;
namespace CSLight
{
    class Program
    {
        [STAThread]
        static void Main()
        {
			opt.mouse.MoveSpeed = opt.key.KeySpeed = opt.key.TextSpeed = 10;
			//string clipText = "Дата/час виявлення - 24.06.2024 19:07 Координати - 37U Cr 52470 81419 Екіпаж - Домаха 1; Ціль - 2 квадроцикла Дод.коментарі - до КПВВ";
			//string clipText = "Дата/час події: 28.06.24 21:20 Координати: 37U CR  21146  71695 Засіб: А1СМ ФУРІЯ Назва екіпажу: степАн № вильоту: 1 Ціль: АТ - ротація - зупинка за корами";
			//string clipText = "Дата/час події: 28.06.24 21:20 Координати: 37U СР  21146  71695 Засіб: А1СМ ФУРІЯ Назва екіпажу: степАн № вильоту: 1 Ціль: АТ - ротація - зупинка за корами";
			//string clipText = "37U Cr  18478   70552";
			string clipText = "37UcR5550578317";
			string patternFirstEnglishBefore = @".{0,2}[a-zA-Z]"; // шаблон (1) 1 англ + 2 символи попереду 
			string patternFirstEnglishAfter = @"[a-zA-Z].{0,12}"; // шаблон (2) 1 англ + 15 символів після (з запасом)
			string firstEnglishBefore = string.Empty;
			string firstEnglishAfter = string.Empty;
			

			// 
			if (clipText.Length > 20) {
				// приводимо до онго регістру
				clipText = clipText.ToUpper();
				// Видаляємо все окрім цифр та англ букв, переводим в один регістр
				clipText = Regex.Replace(clipText, @"[^a-zA-Z0-9]", "");
				// виявляємо 1 шаблон
				firstEnglishBefore = PatternExtract(clipText,patternFirstEnglishBefore);
				// виявляємо 2 шаблон
				firstEnglishAfter = PatternExtract(clipText,patternFirstEnglishAfter);
				// Формуємо результат: прибераємо дублі після конкатенації + додаємо пробіли 
				clipText = InsertSpaces(RemoveDuplicates(firstEnglishBefore + firstEnglishAfter));
			}
			if(clipText.Length <= 18) {
				// Видаляємо все окрім цифр та англ букв, переводим в один регістр
				clipText = Regex.Replace(clipText, @"[^a-zA-Z0-9]", "").Trim().ToUpper();
				clipText = "***" + clipText + "***";
				// виявляємо 1 шаблон
				firstEnglishBefore = PatternExtract(clipText,patternFirstEnglishBefore);
				// виявляємо 2 шаблон
				firstEnglishAfter = PatternExtract(clipText,patternFirstEnglishAfter);
				// Формуємо результат: прибераємо дублі після конкатенації + додаємо пробіли 
				clipText = InsertSpaces(RemoveDuplicates(firstEnglishBefore + firstEnglishAfter));
			}
			
			Console.WriteLine(clipText);
		}
		// Функція для видалення дублікатів зі строки
		static string RemoveDuplicates(string input) {
			string result = "";
			foreach (char c in input)
			{
				if (char.IsLetter(c) && result.IndexOf(c) != -1)
				{
					continue; // Пропускаємо англійські букви, які вже є в результаті
				}
				result += c;
			}
			return result;
		}
		// отримаємо вміст шаблону з рядку
		static string PatternExtract(string copyText, string pattern) {
			Match match = Regex.Match(copyText, pattern);
            if (match.Success)
            {
                return match.Value;
            }
			 return string.Empty; // or return null, or any default value you prefer
		}
		//додаємо пробіли
		static string InsertSpaces(string input)
		{
			/*
			if (input.Length < 15)
			{
				return input; // If the input length is less than 15, no need to insert spaces
			}
			*/
			// Insert spaces at the specified positions
			input = input.Insert(input.Length - 5, " ");
			input = input.Insert(input.Length - 11, " ");
			input = input.Insert(input.Length - 14, " ");

			return input;
		}
		
		static string Transliterate(string text)
		{
			Dictionary<char, string> translitMap = new Dictionary<char, string>
			{
				{'А', "A"}, {'Б', "B"}, {'В', "V"}, {'Г', "H"}, {'Ґ', "G"},
				{'Д', "D"}, {'Е', "E"}, {'Є', "Ye"}, {'Ж', "Zh"}, {'З', "Z"},
				{'И', "Y"}, {'І', "I"}, {'Ї', "Yi"}, {'Й', "Y"}, {'К', "K"},
				{'Л', "L"}, {'М', "M"}, {'Н', "N"}, {'О', "O"}, {'П', "P"},
				{'Р', "R"}, {'С', "S"}, {'Т', "T"}, {'У', "U"}, {'Ф', "F"},
				{'Х', "Kh"}, {'Ц', "Ts"}, {'Ч', "Ch"}, {'Ш', "Sh"}, {'Щ', "Shch"},
				{'Ю', "Yu"}, {'Я', "Ya"},
				{'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "h"}, {'ґ', "g"},
				{'д', "d"}, {'е', "e"}, {'є', "ye"}, {'ж', "zh"}, {'з', "z"},
				{'и', "y"}, {'і', "i"}, {'ї', "yi"}, {'й', "y"}, {'к', "k"},
				{'л', "l"}, {'м', "m"}, {'н', "n"}, {'о', "o"}, {'п', "p"},
				{'р', "r"}, {'с', "s"}, {'т', "t"}, {'у', "u"}, {'ф', "f"},
				{'х', "kh"}, {'ц', "ts"}, {'ч', "ch"}, {'ш', "sh"}, {'щ', "shch"},
				{'ю', "yu"}, {'я', "ya"}
			};

			StringBuilder result = new StringBuilder();

			foreach (char c in text)
			{
				if (translitMap.ContainsKey(c))
				{
					result.Append(translitMap[c]);
				}
				else
				{
					result.Append(c);
				}
			}

			return result.ToString();
		}
	}
}