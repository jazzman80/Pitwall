using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Pitwall/Settings")]

public class Settings : ScriptableObject
{
    #region Fields

    [Header("Performance")]
    [SerializeField] private float maxAccelerationPerformance;
    [SerializeField] private float minAccelerationPerformance;

    [SerializeField] private float maxBrakingPerformance;
    [SerializeField] private float minBrakingPerformance;

    [SerializeField] private float maxCorneringPerformance;
    [SerializeField] private float minCorneringPerformance;

    [SerializeField] private float maxSpeedPerformance;
    [SerializeField] private float minSpeedPerformance;

    [Header("Car Component Impact")]
    [SerializeField] float engineToAccImpact;

    #endregion

    #region Properties

    public float MaxAccelerationPerformance => maxAccelerationPerformance;
    public float MinAccelerationPerformance => minAccelerationPerformance;

    public float MaxBrakingPerformance => maxBrakingPerformance;
    public float MinBrakingPerformance => minBrakingPerformance;

    public float MaxCorneringPerformance => maxCorneringPerformance;
    public float MinCorneringPerformance => minCorneringPerformance;

    public float MaxSpeedPerformance => maxSpeedPerformance;
    public float MinSpeedPerformance => minSpeedPerformance;


    public float EngineToAccImpact => engineToAccImpact / 100;

    #endregion
}
