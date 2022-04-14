using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Pitwall/Settings")]

public class Settings : ScriptableObject
{
    #region Fields

    [SerializeField] private float maxAccelerationPerformance;
    [SerializeField] private float minAccelerationPerformance;
    [SerializeField] private float maxBrakingPerformance;
    [SerializeField] private float minBrakingPerformance;

    #endregion

    #region Properties

    public float MaxAccelerationPerformance => maxAccelerationPerformance;
    public float MinAccelerationPerformance => minAccelerationPerformance;
    public float MaxBrakingPerformance => maxBrakingPerformance;
    public float MinBrakingPerformance => minBrakingPerformance;

    #endregion
}
