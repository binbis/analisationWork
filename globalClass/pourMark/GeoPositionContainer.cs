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
	/// <summary>
	/// колір заливки, спадкове меню у вкладці "Додаткові поля"
	/// </summary>
	public Au.elm DeltaDropDownFillColor() { return ElementNavigator.deltaWindow.Elm["STATICTEXT", "Колір заливки", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1); }
	/// <summary>
	/// колір жовтий у вкладці "Додаткові поля"
	/// </summary>
	public Au.elm DeltaButtonYellow() { return ElementNavigator.deltaWindow.Elm["BUTTON", "#ffeb3b", "class=Chrome_RenderWidgetHostHWND", EFFlags.HiddenToo | EFFlags.UIA].Find(1); }
	/// <summary>
	/// колір червоний у вкладці "Додаткові поля"
	/// </summary>
	public Au.elm DeltaButtonRed() { return ElementNavigator.deltaWindow.Elm["BUTTON", "#f44336", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1); }
	
}
