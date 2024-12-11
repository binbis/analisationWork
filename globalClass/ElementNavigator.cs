/// <summary>
/// вікна й елементи з цих вікон
/// </summary>
public class ElementNavigator {
	/// <summary>
	/// вікно дельти
	/// </summary>
	public wnd deltaWindow = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
	
	/// <summary>
	/// вкладка "Основні поля" в вікні редагування мітки
	/// </summary>
	public Au.elm DeltaMainFilds() {return deltaWindow.Elm["web:GROUPING", prop: "desc=Основні поля"].Find(-1);}
	/// <summary>
	/// вкладка "Додаткові поля" в вікні редагування мітки
	/// </summary>
	public Au.elm DeltaAdditionalFields() {return deltaWindow.Elm["web:GROUPING", prop: "desc=Додаткові поля"].Find(-1);}
	/// <summary>
	/// вкладка "Географічне розташування" в вікні редагування мітки
	/// </summary>
	public Au.elm DeltaGeograficPlace() {return deltaWindow.Elm["web:GROUPING", prop: "desc=Географічне розташування"].Find(-1);}
	/// <summary>
	/// вкладка "Прикріплення" в вікні редагування мітки
	/// </summary>
	public Au.elm DeltaAttachmentFields() { return deltaWindow.Elm["web:GROUPING", prop: "desc=Прикріплення"].Find(-1); }

}

