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
	public string Date {
		set { date = value; }
		get { return date; }
	}
	/// <summary>
	/// 00:40
	/// </summary>
	private string time;
	public string Time {
		set { time = value; }
		get { return time; }
	}
	/// <summary>
	/// опис подій
	/// </summary>
	private string decrtiptionComment;
	public string DecrtiptionComment {
		set { decrtiptionComment = value; }
		get { return decrtiptionComment; }
	}
	/// <summary>
	/// 5
	/// </summary>
	private string flightNumber;
	public string FlightNumber {
		set { flightNumber = value; }
		get { return flightNumber; }
	}
	/// <summary>
	/// R-18-1 (Мавка)
	/// </summary>
	private string crewTeam;
	public string CrewTeam {
		set { crewTeam = value; }
		get { return crewTeam; }
	}
	/// <summary>
	/// Дорозвідка / Мінування / Ураження ..
	/// </summary>
	private string goal;
	public string Goal {
		set { goal = value; }
		get { return goal; }
	}
	/// <summary>
	/// Міна, Авто, Антена.. 
	/// </summary>
	private string target;
	public string Target {
		set { target = value; }
		get { return target; }
	}
	/// <summary>
	/// Міна 270724043
	/// </summary>
	private string targetId;
	public string TargetId {
		set { targetId = value; }
		get { return targetId; }
	}
	/// <summary>
	/// 37U CP 76420 45222
	/// </summary>
	private string coordinates;
	public string Coordinates {
		set { coordinates = value; }
		get { return coordinates; }
	}
	/// <summary>
	/// Шевченко, Новооленівка
	/// </summary>
	private string locality;
	public string Locality {
		set { locality = value; }
		get { return locality; }
	}
	/// <summary>
	/// Куля 1100, Фаберже
	/// </summary>
	private string ammunition;
	public string Ammunition {
		set { ammunition = value; }
		get { return ammunition; }
	}
	/// <summary>
	/// 3, 4
	/// </summary>
	private string numberAmmunition;
	public string NumberAmmunition {
		set { numberAmmunition = value; }
		get { return numberAmmunition; }
	}
	/// <summary>
	/// Виявлено, Підтв. ураж. ..
	/// </summary>
	private string status;
	public string Status {
		set { status = value; }
		get { return status; }
	}
	/// <summary>
	/// 200
	/// </summary>
	private string dNumber;
	public string DNumber {
		set { dNumber = value; }
		get { return dNumber; }
	}
	/// <summary>
	/// 300
	/// </summary>
	private string wNumber;
	public string WNumber {
		set { wNumber = value; }
		get { return wNumber; }
	}
	/// <summary>
	/// Злий Доктор ....
	/// </summary>
	private string initiationFee;
	public string InitiationFee {
		set { initiationFee = value; }
		get { return initiationFee; }
	}
	/// <summary>
	/// 1734874827482 ...
	/// </summary>
	private string messageId;
	public string MessageId {
		set { messageId = value; }
		get { return messageId; }
	}
	/// <summary>
	/// F7 ....
	/// </summary>
	private string weapon;
	public string Weapon {
		set { weapon = value; }
		get { return weapon; }
	}
	/// <summary>
	/// дописує F7 до фпв
	/// </summary>
	/// <param name="crew"></param>
	private string getWeaponByCrew(string crew) {
		Regex regex = new Regex(@"\(([^)]+)\)");
		MatchCollection matches = regex.Matches(crew);
		foreach (Match match in matches) {
			if (match.Groups[1].Value.ToUpper().Contains("FPV")) {
				return "FPV - F7";
			} else {
				return crew;
			}
		}
		return crew;
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
		decrtiptionComment = rowList[2];
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
		this.decrtiptionComment = getPreparationByComment(rowList[2]);
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


