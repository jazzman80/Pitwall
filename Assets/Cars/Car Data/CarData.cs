using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car Data", menuName = "Pitwall/Car Data")]

public class CarData : ScriptableObject
{
    #region Fields

    [Header("Globals")]
    [SerializeField] Settings settings;

    [Header("Stats")]
    [SerializeField] float accelerationStat;
    [SerializeField] float brakingStat;
    [SerializeField] float corneringStat;
    [SerializeField] float maxSpeedStat;

    #endregion

    #region Properties

    public float AccelerationPerformance =>
        Pitwall.ConvertStatToPerformance(accelerationStat,
        settings.MinAccelerationPerformance, settings.MaxAccelerationPerformance);

    public float BrakingPerformance =>
        Pitwall.ConvertStatToPerformance(brakingStat,
        settings.MinBrakingPerformance, settings.MaxBrakingPerformance);

    public float MaxSpeedPerformance =>
        Pitwall.ConvertStatToPerformance(maxSpeedStat,
        settings.MinSpeedPerformance, settings.MaxSpeedPerformance);

    public float CorneringPerformance =>
        Pitwall.ConvertStatToPerformance(corneringStat,
        settings.MinCorneringPerformance, settings.MaxCorneringPerformance);

    #endregion

    #region Methods



    #endregion

}
