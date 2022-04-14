using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Car : MonoBehaviour
{
    #region Fields

    [Header("Globals")]
    [SerializeField] Settings settings;

    [Header("Move")]
    [SerializeField] private PathCreator circuit;
    [SerializeField] private float totalDistanceCovered;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private State state;

    [Header("Stats")]
    [SerializeField] private float accelerationPerformance;
    [SerializeField] private float brakingPerformance;
    [SerializeField] private float maxSpeed;

    #endregion

    #region Properties

    private float Speed { get => speed; set => speed = (value > 0) ? value : 0; }

    // unified stats
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
            // ERROR
            brakingPerformance = ((value / 100) * (settings.MaxBrakingPerformance
                - settings.MinBrakingPerformance))
                + settings.MinBrakingPerformance;
        }
    }

    #endregion

    #region States

    private enum State
    {
        acceleration,
        braking,
        constantSpeed
    }

    #endregion

    #region LifeCycle

    private void Start()
    {
        AccelerationStat = 100;
        BrakingStat = 100;
    }

    private void Update()
    {
        UpdatePosition();
    }

    #endregion

    #region Methods

    private void UpdatePosition()
    {
        // update acceleration
        switch (state)
        {
            case State.acceleration:
                acceleration = accelerationPerformance * ((maxSpeed - Speed) / maxSpeed);
                break;
            case State.braking:
                acceleration = -brakingPerformance;
                break;
            case State.constantSpeed:
                acceleration = 0;
                break;
        }

        // update speed
        Speed += acceleration * Time.deltaTime;

        // update total distance
        totalDistanceCovered += Speed * Time.deltaTime;

        // set position
        transform.position = circuit.path.GetPointAtDistance(totalDistanceCovered);
    }

    #endregion
}
