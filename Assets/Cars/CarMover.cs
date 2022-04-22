using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// moves car along track

public class CarMover : MonoBehaviour
{
    #region Fields

    [Header("Globals")]
    [SerializeField] private Car car;

    [Header("Move")]
    [SerializeField] private double speed;
    [SerializeField] private double acceleration;
    [SerializeField] private State state;

    [Header("Cornering")]
    [SerializeField] private float nextTurnPosition;
    [SerializeField] private float nextTurnSpeed;

    #endregion

    #region States

    private enum State
    {
        acceleration,
        braking,
        constantSpeed
    }

    #endregion

    #region Properties

    private double Speed { get => speed; set => speed = (value > 0) ? value : 0; }
    private double BrakingDistance => ((Speed * Speed) - (nextTurnSpeed * nextTurnSpeed)) / (2 * car.brakingPerformance);
    private double NextTurnDistance => nextTurnPosition - LapDistanceCovered;
    private double LapDistanceCovered => car.totalDistanceCovered % car.Track.Circuit.path.length;


    #endregion

    #region Lifecycle

    private void Start()
    {
        nextTurnPosition = 10000;
        nextTurnSpeed = 0;

        car.EntersTurn.AddListener(OnTurnEnter);
        car.ExitsTurn.AddListener(TransitionToAccelerationState);
    }

    private void FixedUpdate()
    {
        UpdatePosition();
        TransitionToBrakingState();
        TransitionToConstantSpeedState();
    }

    #endregion

    #region Methods

    private void UpdatePosition()
    {
        // update acceleration
        switch (state)
        {
            case State.acceleration:
                acceleration = car.accelerationPerformance * ((car.Track.MaxSpeed * car.maxSpeedPerformance - Speed)
                    / car.Track.MaxSpeed * car.maxSpeedPerformance);
                break;
            case State.braking:
                acceleration = -car.brakingPerformance;
                break;
            case State.constantSpeed:
                acceleration = 0;
                break;
        }

        // update speed
        Speed += acceleration * Time.fixedDeltaTime;

        // update total distance
        car.totalDistanceCovered += Speed * Time.fixedDeltaTime;

        // set position
        car.WorldCoordinates = car.Track.Circuit.path.GetPointAtDistance((float)car.totalDistanceCovered);
    }

    private void TransitionToBrakingState()
    {
        if (NextTurnDistance <= BrakingDistance && state == State.acceleration) state = State.braking;
    }

    private void TransitionToConstantSpeedState()
    {
        if (Speed <= nextTurnSpeed && state == State.braking)
        {
            state = State.constantSpeed;
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
        nextTurnSpeed = nextTurn.MaxSpeed * car.corneringPerformance;
    }

    #endregion
}
