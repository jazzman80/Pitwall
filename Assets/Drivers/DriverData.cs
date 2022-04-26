using UnityEngine;

[CreateAssetMenu(fileName = "Driver Data", menuName = "Pitwall/Driver Data")]

public class DriverData : ScriptableObject
{
    #region Fields

    [SerializeField] string shortName;

    #endregion

    #region Properties

    public string ShortName => shortName;

    #endregion
}
