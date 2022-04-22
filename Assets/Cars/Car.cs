using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    #region Fields

    [Header("Globals")]
    [SerializeField] Settings settings;
    private CarData carData;
    private Track track;

    [Header("Race")]
    public int position;

    [Header("Move")]
    public double totalDistanceCovered;

    [Header("Performance")]
    [SerializeField] public float accelerationPerformance;
    [SerializeField] public float brakingPerformance;
    [SerializeField] public float corneringPerformance;
    [SerializeField] public float maxSpeedPerformance;

    #endregion

    #region Events

    public UnityEvent<Collider2D> EntersTurn;
    public UnityEvent ExitsTurn;
    public UnityEvent StartLineCross;

    #endregion

    #region Properties

    public Track Track => track;
    public Vector3 WorldCoordinates { get => transform.position; set => transform.position = value; }

    #endregion

    #region Unified stats
    private float AccelerationStat
    {

        get
        {
            return 100 * (accelerationPerformance - settings.MinAccelerationPerformance) /
                (settings.MaxAccelerationPerformance - settings.MinAccelerationPerformance);
        }

        set
        {
            accelerationPerformance = ((value / 100) * (settings.MaxAccelerationPerformance
                - settings.MinAccelerationPerformance))
                + settings.MinAccelerationPerformance;
        }
    }

    private float BrakingStat
    {

        get
        {
            return 100 * (brakingPerformance - settings.MinBrakingPerformance) /
                 (settings.MaxBrakingPerformance - settings.MinBrakingPerformance);
        }

        set
        {
            brakingPerformance = ((value / 100) * (settings.MaxBrakingPerformance
                - settings.MinBrakingPerformance))
                + settings.MinBrakingPerformance;
        }
    }

    private float CorneringStat
    {

        get
        {
            return 100 * (corneringPerformance - settings.MinCorneringPerformance) /
                 (settings.MaxCorneringPerformance - settings.MinCorneringPerformance);
        }

        set
        {
            corneringPerformance = ((value / 100) * (settings.MaxCorneringPerformance
                - settings.MinCorneringPerformance))
                + settings.MinCorneringPerformance;
        }
    }

    private float MaxSpeedStat
    {

        get
        {
            return 100 * (maxSpeedPerformance - settings.MinSpeedPerformance) /
                 (settings.MaxSpeedPerformance - settings.MinSpeedPerformance);
        }

        set
        {
            maxSpeedPerformance = ((value / 100) * (settings.MaxSpeedPerformance
                - settings.MinSpeedPerformance))
                + settings.MinSpeedPerformance;
        }
    }

    #endregion

    #region LifeCycle

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Turn Enter":
                EntersTurn.Invoke(collision);
                break;
            case "Turn Exit":
                ExitsTurn.Invoke();
                break;
            case "Start Line":
                StartLineCross.Invoke();
                break;
        }

    }

    #endregion

    #region Methods

    public void Construct(Track track, CarData carData, int position)
    {
        this.track = track;
        this.carData = carData;

        AccelerationStat = carData.accelerationStat;
        BrakingStat = carData.brakingStat;
        CorneringStat = carData.corneringStat;
        MaxSpeedStat = carData.maxSpeedStat;

        // set starting position on track
        this.position = position;
        totalDistanceCovered = -(10 * position);
    }

    #endregion
}
