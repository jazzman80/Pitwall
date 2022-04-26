using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car Data", menuName = "Pitwall/Car Data")]

public class CarData : ScriptableObject
{
    #region Fields

    [Header("Globals")]
    [SerializeField] Settings settings;
    [SerializeField] DriverData driverData;
    [SerializeField] TeamData teamData;

    [Header("Car Components")]
    [SerializeField] float engineStat;

    #endregion

    #region Properties

    public DriverData DriverData => driverData;
    public TeamData TeamData => teamData;

    public float EngineStat => engineStat;

    #endregion

    #region Methods



    #endregion

}
