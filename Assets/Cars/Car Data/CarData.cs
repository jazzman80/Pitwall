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
    [SerializeField] float engineEfficiencyStat;
    [SerializeField] float gearBoxEfficiencyStat;
    [SerializeField] float brakesEfficiencyStat;
    [SerializeField] float suspensionEfficiencyStat;
    [SerializeField] float aerodynamicsEfficiencyStat;
    [SerializeField] float weightEfficiencyStat;

    [Header("Tires")]
    [SerializeField] List<TireSet> tireSets;

    #endregion

    #region Properties

    public DriverData DriverData => driverData;
    public TeamData TeamData => teamData;

    public float EngineEfficiencyStat => engineEfficiencyStat;
    public float GearBoxEfficiencyStat => gearBoxEfficiencyStat;
    public float BrakesEfficiencyStat => brakesEfficiencyStat;
    public float SuspensionEfficiencyStat => suspensionEfficiencyStat;
    public float AerodynamicsEfficiencyStat => aerodynamicsEfficiencyStat;
    public float WeightEfficiencyStat => weightEfficiencyStat;

    public List<TireSet> TireSets => tireSets;


    #endregion

    #region Methods



    #endregion

}
