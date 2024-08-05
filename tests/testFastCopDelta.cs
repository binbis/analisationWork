/**
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
			string clipText = "Дата/час події:	17:00 Координати:	37u cp 88258 44947 Засіб:	FPV - Камікадзе Назва екіпажу:	PM ADAM 1 № вильоту за зміну:	12 Ціль:	укриття ос Результат:	Не Уражено Дрон	Adam girls 7 Коментар: БЧ:     	Гаррі АКБ тип	6s2p Причина:	 подавлення відео";
			
			if (clipText.Length!=0) {
				clipText = clipText.ToUpper();
				clipText = Regex.Replace(clipText, "[^a-zA-Zа-яА-Я0-9]", "");
			}
			
			string pattern = @"(.{5})(\d{10})";
        
			Match match = Regex.Match(clipText, pattern);
			
			if (match.Success)
			{
				clipText = InsertSpaces(match.Groups[1].Value + match.Groups[2].Value);
			}
			
			//Console.WriteLine(clipText);
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
				{'Р', "P"}, {'С', "C"}, {'Т', "T"} 
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