/*/ c \analisationWork\globalClass\ElementNavigator.cs; /*/
/// <summary>
/// елементи(поля) з вікна редагування у вкладці "Георафічне розташування"
/// </summary>
public class GeoPositionContainer {
	ElementNavigator ElementNavigator = new ElementNavigator();
	/// <summary>
	/// рендж відсоток прозрачності у вкладці "Додаткові поля"
	/// </summary>
	public Au.elm DeltaFieldTransparentPercentage() { return ElementNavigator.deltaWindow.Elm["web:SLIDER"].Find(-1); }
	
	
	
}
