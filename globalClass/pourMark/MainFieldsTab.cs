/*/ c \analisationWork\globalClass\ElementNavigator.cs; /*/
/// <summary>
/// елементи(поля) з вікна редагування у вкладці "Основні поля"
/// </summary>
public class MainFieldsTab {
	ElementNavigator ElementNavigator = new ElementNavigator();
	/// <summary>
	/// поле шар у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldLayer() { return ElementNavigator.DeltaWindow().Elm["STATICTEXT", "Шар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1); }
	/// <summary>
	/// поле назва у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldName() { return ElementNavigator.DeltaWindow().Elm["STATICTEXT", "Назва", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next1"].Find(-1); }
	/// <summary>
	/// поле дата у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldDate() { return ElementNavigator.DeltaWindow().Elm["TEXT", "дд/мм/рррр", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1); }
	/// <summary>
	/// поле кількість у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldCounts() { return ElementNavigator.DeltaWindow().Elm["STATICTEXT", "Кількість", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1); }
	/// <summary>
	/// поле боєздатність у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldCapability() { return ElementNavigator.DeltaWindow().Elm["STATICTEXT", "Боєздатність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1); }
	/// <summary>
	/// поле ідентифікація у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldIdentification() { return ElementNavigator.DeltaWindow().Elm["STATICTEXT", "Ідентифікація", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1); }
	/// <summary>
	/// поле зауваження штабу у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldPurchase() { return ElementNavigator.DeltaWindow().Elm["STATICTEXT", "Зауваження штабу", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next"].Find(-1); }
	/// <summary>
	/// поле мобільність у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldMobility() { return ElementNavigator.DeltaWindow().Elm["STATICTEXT", "Мобільність", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1); }
	/// <summary>
	/// поле Новий коментар у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldNewComment() { return ElementNavigator.DeltaWindow().Elm["STATICTEXT", "Новий коментар", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next2"].Find(-1); }
	/// <summary>
	/// поле Тип джерела у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaFieldSourceType() { return ElementNavigator.DeltaWindow().Elm["STATICTEXT", "Тип джерела", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA, navig: "next3"].Find(-1); }
	/// <summary>
	/// кнопка достовірність у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaButtonCertainty_2() { return ElementNavigator.DeltaWindow().Elm["RADIOBUTTON", "2", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1); }
	/// <summary>
	/// кнопка надійність у вкладці "Основні поля"
	/// </summary>
	public Au.elm DeltaButtonReliability_A() { return ElementNavigator.DeltaWindow().Elm["RADIOBUTTON", "A", "class=Chrome_RenderWidgetHostHWND", EFFlags.UIA].Find(-1); }
	
	
}
