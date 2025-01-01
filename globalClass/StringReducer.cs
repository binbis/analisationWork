/// <summary>
/// функції з різними варіантами обрізання рядків
/// </summary>
public class StringReducer {
	
	/// <summary>
	/// усе після N символу буде обрізано
	/// </summary>
	public string TrimAllAfterN(string str, int maxLength) {
		if (str.Length > maxLength) {
			return str.Substring(str.Length - maxLength);
		}
		return str;
	}
	/// <summary>
	/// усе після першої крапки буде обрізано
	/// </summary>
	public string TrimAfterFirstDot(string str) {
		int dotIndex = str.IndexOf('.');
		if (dotIndex != -1) {
			return str.Substring(0, dotIndex);
		}
		return str;
	}
	/// <summary>
	/// усе після першого ")" буде брізано
	/// </summary>
	public string TrimAfterFirstClosingParenthes(string str) {
		int index = str.LastIndexOf(')');
		if (index != -1) {
			return str.Substring(0, index + 1);
		}
		return str;
	}
	/// <summary>
	/// після першого "(" до рядка буде додано "FPV f7)"
	/// </summary>
	public string addTypeForBoard(string str) {
		int index = str.LastIndexOf('(');
		if (index != -1) {
			return str.Substring(0, index + 1) + "FPV F7)";
		}
		return str;
	}
	
}
