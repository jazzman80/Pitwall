using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Car : MonoBehaviour
{
    #region Fields

    [Header("Globals")]
    [SerializeField] Settings settings;
    [SerializeField] Track track;

    [Header("Move")]
    [SerializeField] private float totalDistanceCovered;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private State state;

    [Header("Cornering")]
    [SerializeField] private float nextTurnPosition = 10000;
    [SerializeField] private float nextTurnSpeed;

    [Header("Performances")]
    [SerializeField] private float accelerationPerformance;
    [SerializeField] private float brakingPerformance;
    [SerializeField] private float corneringPerformance;
    [SerializeField] private float maxSpeedPerformance;

    [Header("Initial Stats")]
    [SerializeField] private float initialAccelerationStat;
    [SerializeField] private float initialBrakingStat;
    [SerializeField] private float initialCorneringStat;
    [SerializeField] private float initialMaxSpeedStat;

    #endregion

    #region Properties

    private float Speed { get => speed; set => speed = (value > 0) ? value : 0; }
    private float LapDistanceCovered => totalDistanceCovered % track.Circuit.path.length;
    private float BrakingDistance => ((Speed * Speed) - (nextTurnSpeed * nextTurnSpeed)) / (2 * brakingPerformance);
    private float NextTurnDistance => nextTurnPosition - LapDistanceCovered;

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
        AccelerationStat = initialAccelerationStat;
        BrakingStat = initialBrakingStat;
        CorneringStat = initialCorneringStat;
        MaxSpeedStat = initialMaxSpeedStat;

        nextTurnPosition = 10000;
        nextTurnSpeed = 0;

        maxSpeed = track.MaxSpeed;
    }

    private void Update()
    {
        UpdatePosition();
        TransitionToBrakingState();
        TransitionToConstantSpeedState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Turn Enter")) OnTurnEnter(collision);
        else if (collision.CompareTag("Turn Exit")) TransitionToAccelerationState();
    }

    #endregion

    #region Methods

    private void UpdatePosition()
    {
        // update acceleration
        switch (state)
        {
            case State.acceleration:
                acceleration = accelerationPerformance * ((maxSpeed * maxSpeedPerformance - Speed) / maxSpeed * maxSpeedPerformance);
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
        transform.position = track.Circuit.path.GetPointAtDistance(totalDistanceCovered);
    }

    private void TransitionToBrakingState()
    {
        if (NextTurnDistance <= BrakingDistance && state == State.acceleration) state = State.braking;
    }

    private void TransitionToConstantSpeedState()
    {
        if (Speed < nextTurnSpeed && state == State.braking)
        {
            state = State.constantSpeed;
            Speed = nextTurnSpeed;
        }
    }

    private void TransitionToAccelerationState()
    {
        nextTurnPosition = 10000;
        nextTurnSpeed = 0;
        state = State.acceleration;
    }

    private void OnTurnEnter(Collider2D collision)
    {
        Turn nextTurn = collision.gameObject.GetComponent<Turn>();
        nextTurnPosition = nextTurn.Position;
        nextTurnSpeed = nextTurn.MaxSpeed * corneringPerformance;
    }

    #endregion
}
