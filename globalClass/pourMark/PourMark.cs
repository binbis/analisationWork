/*/ c .\ExtraFieldsContainer.cs; c .\MainFieldsTab.cs; c .\GeoPositionContainer.cs; /*/
/// <summary>
/// Поля вікна редагування мітки
/// </summary>
public class PourMark {
	
	/// <summary>
	/// елементи(поля) з вікна редагування у вкладці "Основні поля"
	/// </summary>
	public MainFieldsTab MainFieldsTab = new MainFieldsTab();
	/// <summary>
	/// елементи(поля) з вікна редагування у вкладці "Додаткові поля"
	/// </summary>
	public ExtraFieldsContainer ExtraFieldsContainer = new ExtraFieldsContainer();
	/// <summary>
	/// елементи(поля) з вікна редагування у вкладці "Георафічне розташування"
	/// </summary>
	public GeoPositionContainer GeoPositionContainer = new GeoPositionContainer();
	
}
