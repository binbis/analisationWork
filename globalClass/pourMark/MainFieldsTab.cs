/*/ c \analisationWork\globalClass\ElementNavigator.cs; /*/
/// <summary>
/// елементи(поля) з вікна редагування у вкладці "Основні поля"
/// </summary>
public class MainFieldsTab {
	ElementNavigator ElementNavigator = new ElementNavigator();
	/// <summary>
	/// поле шар у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldLayer() { return ElementNavigator.deltaWindow.Elm["STATICTEXT", "Шар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1); }
	/// <summary>
	/// поле назва у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldName() { return ElementNavigator.deltaWindow.Elm["STATICTEXT", "Назва", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next1"].Find(-1); }
	/// <summary>
	/// поле дата у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldDate() { return ElementNavigator.deltaWindow.Elm["TEXT", "дд/мм/рррр", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1); }
	/// <summary>
	/// поле кількість у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldCounts() { return ElementNavigator.deltaWindow.Elm["STATICTEXT", "Кількість", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1); }
	/// <summary>
	/// поле боєздатність у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldCapability() { return ElementNavigator.deltaWindow.Elm["STATICTEXT", "Боєздатність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1); }
	/// <summary>
	/// поле ідентифікація у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldIdentification() { return ElementNavigator.deltaWindow.Elm["STATICTEXT", "Ідентифікація", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1); }
	/// <summary>
	/// поле зауваження штабу у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldPurchase() { return ElementNavigator.deltaWindow.Elm["STATICTEXT", "Зауваження штабу", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next"].Find(-1); }
	/// <summary>
	/// поле мобільність у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldMobility() { return ElementNavigator.deltaWindow.Elm["STATICTEXT", "Мобільність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1); }
	/// <summary>
	/// поле Новий коментар у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldNewComment() { return ElementNavigator.deltaWindow.Elm["STATICTEXT", "Новий коментар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1); }
	
}
