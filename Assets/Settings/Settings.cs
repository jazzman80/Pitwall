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
    [SerializeField] private float maxCorneringPerformance;
    [SerializeField] private float minCorneringPerformance;

    #endregion

    #region Properties

    public float MaxAccelerationPerformance => maxAccelerationPerformance;
    public float MinAccelerationPerformance => minAccelerationPerformance;
    public float MaxBrakingPerformance => maxBrakingPerformance;
    public float MinBrakingPerformance => minBrakingPerformance;
    public float MaxCorneringPerformance => maxCorneringPerformance;
    public float MinCorneringPerformance => minCorneringPerformance;

    #endregion
}
