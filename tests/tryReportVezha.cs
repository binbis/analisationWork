/*
в вежі є функціонал запихнути в буфер обміну картинку та текст
редагування тексту з буфера обміну в наш формат, картинка видаляється

*/

class Program {
	static void Main() {
		// Отримання даних з буфера обміну
		//var clipboardDataCur = clipboardData.contains();
        //Image image = clipboardData.getImage();
        string input = clipboardData.getText();

        // Regular expression pattern to match lines to be removed
        string pattern = @"^(Надіслано від|Запис|WGS84|УСК-|Посилання).*$";
        string result = Regex.Replace(input, pattern, "", RegexOptions.Multiline);

        // Remove empty lines
        result = Regex.Replace(result, @"^\s*$\n|\r", "", RegexOptions.Multiline);

         // Prepend "Дата/час виявлення - " to the timestamp line
        //result = Regex.Replace(result, @"^(.*\d{2}\.\d{2}\.\d{4} \d{2}:\d{2}:\d{2})$", "Дата/час виявлення - $1", RegexOptions.Multiline);
		result = Regex.Replace(result, @"^(.*\d{2}\.\d{2}\.\d{4} \d{2}:\d{2}:\d{2})$", "Дата/час виявлення - $1", RegexOptions.Multiline);

        // Replace "MGRS" with "Координати:" and add "Екіпаж" and "Засіб" on new lines
        //result = Regex.Replace(result, @"^MGRS:(.*)$", "Координати:$1\nЕкіпаж: \nЗасіб: ", RegexOptions.Multiline);
		// Replace "MGRS" with "Координати:"
		result = Regex.Replace(result, @"^MGRS:(.*)$", "Координати:$1", RegexOptions.Multiline);

        // Очищення буфера обміну
        //clipboard.clear();
		
		clipboard.text = result;
	}
}

