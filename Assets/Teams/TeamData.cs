using UnityEngine;

[CreateAssetMenu(fileName = "Team Data", menuName = "Pitwall/Team Data")]

public class TeamData : ScriptableObject
{
    #region Fields

    [SerializeField] string longName;
    [SerializeField] Color primaryColor;

    #endregion

    #region Properties

    public string LongName => longName;
    public Color PrimaryColor => primaryColor;

    #endregion
}
