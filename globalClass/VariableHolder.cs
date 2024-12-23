/// <summary>
/// різноманітні змінні
/// </summary>
public class VariableHolder {
	/// <summary>
	/// список мін для техніки
	/// </summary>
	public string bchHeavyMines = "ПТМ-3 ТМ-62";
	/// <summary>
	/// список протипіхотки
	/// </summary>
	public string bchTropsMines = "К2 ППМ";
	/// <summary>
	/// Розміновано, Підтв. ураж., Тільки розрив, Авар. скид
	/// </summary>
	public string states = "Розміновано Підтв. ураж. Тільки розрив Авар. скид Нерозрив";
	/// <summary>
	/// список мін для техніки
	/// </summary>
	public string BchHeavyMines {
		//set { bchHeavyMines = value; }
		get { return bchHeavyMines; }
	}
	/// <summary>
	/// список протипіхотки
	/// </summary>
	public string BchTropsMines {
		//set { bchTropsMines = value; }
		get { return bchTropsMines; }
	}
	/// <summary>
	/// Розміновано, Підтв. ураж., Тільки розрив, Авар. скид
	/// </summary>
	public string States {
		//set { states = value; }
		get { return states; }
	}
	
	
	
}
