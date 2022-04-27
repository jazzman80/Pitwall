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
    [SerializeField] float gearBoxToAccImpact;
    [SerializeField] float weightToAccImpact;

    [SerializeField] float brakesToBraImpact;
    [SerializeField] float weightToBraImpact;
    [SerializeField] float tiresToBraImpact;

    [SerializeField] float aeroToSpeedImpact;
    [SerializeField] float gearBoxToSpeedImpact;
    [SerializeField] float suspensionToSpeedImpact;

    [SerializeField] float suspensionToCorImpact;
    [SerializeField] float aeroToCorImpact;
    [SerializeField] float tiresToCorImpact;
 
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

    // car component to car stats impacts
    public float EngineToAccImpact => engineToAccImpact / 100;
    public float GearBoxToAccImpact => gearBoxToAccImpact / 100;
    public float WeightToAccImpact => weightToAccImpact / 100;

    public float BrakesToBraImpact => brakesToBraImpact;
    public float WeightToBraImpact => weightToBraImpact;
    public float TiresToBraImpact => tiresToBraImpact;

    public float AeroToSpeedImpact => aeroToSpeedImpact;
    public float GearBoxToSpeedImpact => gearBoxToSpeedImpact;
    public float SuspensionToSpeedImpact => suspensionToSpeedImpact;

    public float SuspensionToCorImpact => suspensionToCorImpact;
    public float AeroToCorImpact => aeroToCorImpact;
    public float TiresToCorImpact => tiresToCorImpact;

    #endregion
}
