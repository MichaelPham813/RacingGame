/// <summary>
/// ROOM Data for a PLAYER
/// </summary>
public class WolfAndSheep_Room_Data
{
    /// <summary>
    /// Display Name
    /// </summary>
    public string _Name = "";

    /// <summary>
    /// Display Type in ROOM
    /// </summary>
    public string _Type = "";

    /// <summary>
    /// Check Ready
    /// </summary>
    public string _Ready = "NotReady";

    public WolfAndSheep_Room_Data()
    {
        this._Name = "";
        this._Type = "";
        this._Ready = "";
    }

    /// <summary>
    /// ROOM Data for a PLAYER
    /// </summary>
    public WolfAndSheep_Room_Data(string _Name, string _Type, string _Ready)
    {
        this._Name = _Name;
        this._Type = _Type;
        this._Ready = _Ready;
    }
}
