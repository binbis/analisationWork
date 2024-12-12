/*/ c \analisationWork\globalClass\ElementNavigator.cs; /*/
/// <summary>
/// елементи(поля) з вікна редагування у вкладці "Додаткові поля"
/// </summary>
public class ExtraFieldsContainer {
	ElementNavigator ElementNavigator = new ElementNavigator();
	/// <summary>
	/// поле зауваження штабу у вкладці "Додаткові поля"
	/// </summary>
	public Au.elm DeltaFieldPurchase() { return ElementNavigator.DeltaWindow().Elm["STATICTEXT", "Зауваження штабу", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next"].Find(-1); }
	
	
}
