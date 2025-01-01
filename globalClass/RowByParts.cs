/*/ c .\Bisbin.cs; /*/

/// <summary>
/// Row is devided by parts
/// </summary>
public class RowByParts {
	/// <summary>
	/// різноманітні змінні
	/// </summary>
	private Bisbin Bisbin = new Bisbin();
	/// <summary>
	/// 27.07.2024
	/// </summary>
	private string date;
	/// <summary>
	/// 00:40
	/// </summary>
	private string time;
	/// <summary>
	/// опис подій
	/// </summary>
	private string descrtiptionComment;
	/// <summary>
	/// 5
	/// </summary>
	private string flightNumber;
	/// <summary>
	/// R-18-1 (Мавка)
	/// </summary>
	private string crewTeam;
	/// <summary>
	/// Дорозвідка / Мінування / Ураження ..
	/// </summary>
	private string goal;
	/// <summary>
	/// Міна, Авто, Антена.. 
	/// </summary>
	private string target;
	/// <summary>
	/// Міна 270724043
	/// </summary>
	private string targetId;
	/// <summary>
	/// 37U CP 76420 45222
	/// </summary>
	private string coordinates;
	/// <summary>
	/// Шевченко, Новооленівка
	/// </summary>
	private string locality;
	/// <summary>
	/// Куля 1100, Фаберже
	/// </summary>
	private string ammunition;
	/// <summary>
	/// 3, 4
	/// </summary>
	private string numberAmmunition;
	/// <summary>
	/// Виявлено, Підтв. ураж. ..
	/// </summary>
	private string status;
	/// <summary>
	/// 200
	/// </summary>
	private string dNumber;
	/// <summary>
	/// 300
	/// </summary>
	private string wNumber;
	/// <summary>
	/// Злий Доктор ....
	/// </summary>
	private string initiationFee;
	/// <summary>
	/// 1734874827482 ...
	/// </summary>
	private string messageId;
	
	public string Date { get { return date; } set { date = value; } }
	public string Time { get { return time; } set { time = value; } }
	public string DescrtiptionComment { get { return descrtiptionComment; } set { descrtiptionComment = value; } }
	public string FlightNumber { get { return flightNumber; } set { flightNumber = value; } }
	public string CrewTeam { get { return crewTeam; } set { crewTeam = value; } }
	public string Goal { get { return goal; } set { goal = value; } }
	public string Target { get { return target; } set { target = value; } }
	public string TargetId { get { return targetId; } set { targetId = value; } }
	public string Coordinates { get { return coordinates; } set { coordinates = value; } }
	public string Locality { get { return locality; } set { locality = value; } }
	public string Ammunition { get { return ammunition; } set { ammunition = value; } }
	public string NumberAmmunition { get { return numberAmmunition; } set { numberAmmunition = value; } }
	public string Status { get { return status; } set { status = value; } }
	public string DNumber { get { return dNumber; } set { dNumber = value; } }
	public string WNumber { get { return wNumber; } set { wNumber = value; } }
	public string InitiationFee { get { return initiationFee; } set { initiationFee = value; } }
	public string MessageId { get { return messageId; } set { messageId = value; } }
	
	/// <summary>
	/// дописує F7 до фпв
	/// </summary>
	/// <param name="crew"></param>
	private string getWeaponByCrew(string crew) {
		if (crew.Contains("FPV")) {
			return Bisbin.StringReducer.addTypeForBoard(crew);
		} else {
			return crew;
		}
		
		//return crew;
	}
	/// <summary>
	/// скоротити рядок до 19 елементів
	/// </summary>
	/// <param name="targetId"></param>
	private string getReduceBy_idTarget(string targetId) {
		return Bisbin.StringReducer.TrimAllAfterN(targetId, 19);
	}
	/// <summary>
	/// отримати підготовлений рядок для назви екіпажу
	/// </summary>
	private string getPreparationByTeam(string crewTeam) {
		return Bisbin.StringReducer.TrimAfterFirstDot(crewTeam.Replace("\n", "").Trim());
	}
	/// <summary>
	/// отримати підготовлений коментар
	/// </summary>
	private string getPreparationByComment(string decrtiptionComment) {
		return decrtiptionComment.Replace("\n", " ").Trim();
	}
	
	public RowByParts() { }
	
	/// <summary>
	/// користувацький конструктор розбиття рядка на змінні
	/// </summary>
	/// <param name="rowList"></param>
	public RowByParts(string[] rowList) {
		date = rowList[0];
		time = rowList[1];
		descrtiptionComment = rowList[2];
		flightNumber = rowList[3];
		crewTeam = rowList[4];
		goal = rowList[5];
		target = rowList[7];
		targetId = rowList[9];
		coordinates = rowList[18];
		locality = rowList[19];
		ammunition = rowList[22];
		numberAmmunition = rowList[23];
		status = rowList[24];
		dNumber = rowList[25];
		wNumber = rowList[26];
		initiationFee = rowList[27];
		messageId = rowList[34];
		getWeaponByCrew(crewTeam);
	}
	/// <summary>
	/// функція отримання даних для реб рер мітки
	/// </summary>
	/// <param name="rowList"></param>
	public void RowByParts_ReR(string[] rowList) {
		this.date = rowList[4];
		this.time = rowList[5];
		this.coordinates = rowList[8];
	}
	/// <summary>
	/// функція як кор. конструктор але з підготовленими данними
	/// </summary>
	/// <param name="rowList"></param>
	public void RowByParts_Prepare(string[] rowList) {
		this.date = rowList[0];
		this.time = rowList[1];
		this.descrtiptionComment = getPreparationByComment(rowList[2]);
		this.flightNumber = rowList[3];
		this.crewTeam = getWeaponByCrew(getPreparationByTeam(rowList[4]));
		this.goal = rowList[5];
		this.target = rowList[7];
		this.targetId = getReduceBy_idTarget(rowList[9]);
		this.coordinates = rowList[18];
		this.locality = rowList[19];
		this.ammunition = rowList[22];
		this.numberAmmunition = rowList[23];
		this.status = rowList[24];
		this.dNumber = rowList[25];
		this.wNumber = rowList[26];
		this.initiationFee = rowList[27];
		this.messageId = rowList[34];
	}
}


