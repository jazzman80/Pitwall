using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    #region Fields

    [Header("Globals")]
    [SerializeField] Settings settings;
    [SerializeField] Track track;
    //[SerializeField] RaceData raceData;
    [SerializeField] CarData carData;
    [SerializeField] CarVisuals carVisuals;

    [Header("Performance")]
    [SerializeField] float accelerationPerformance;
    [SerializeField] float brakingPerformance;
    [SerializeField] float corneringPerformance;
    [SerializeField] float speedPerformance;

    [Header("Performance Ranges")]
    [SerializeField] private float maxAccelerationPerformance;
    [SerializeField] private float minAccelerationPerformance;

    [SerializeField] private float maxBrakingPerformance;
    [SerializeField] private float minBrakingPerformance;

    [SerializeField] private float maxCorneringPerformance;
    [SerializeField] private float minCorneringPerformance;

    [SerializeField] private float maxSpeedPerformance;
    [SerializeField] private float minSpeedPerformance;

    [Header("Car Components")]
    [SerializeField] CarComponent engine;
    [SerializeField] CarComponent gearBox;
    [SerializeField] CarComponent brakes;
    [SerializeField] CarComponent suspension;
    [SerializeField] CarComponent aerodynamics;
    [SerializeField] CarComponent weight;

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

    [Header("Tires")]
    [SerializeField] Tires tires;

    [Header("Move")]
    [SerializeField] float acceleration;
    [SerializeField] float speed;
    float totalDistanceCovered;
    Turn nextTurn;
    float nextTurnSpeed;
    float nextTurnPosition;

    [Header("Car State")]
    [SerializeField] State state;
    private enum State
    {
        acceleration,
        braking,
        constantSpeed
    }

    [Header("Position")]
    [SerializeField] int position;

    #endregion

    #region Properties

    float NextTurnDistance => nextTurnPosition - LapDistanceCovered;
    float BrakingDistance => ((speed * speed) - (nextTurnSpeed * nextTurnSpeed)) /
        (2 * brakingPerformance);

    float LapDistanceCovered => totalDistanceCovered % track.Length;
    public CarData CarData => carData;
    public float TotalDistanceCovered => totalDistanceCovered;

    #endregion

    #region Lifecycle

    private void Start()
    {
        nextTurnSpeed = 0;
        nextTurnPosition = 10000;

        LoadCarData();
    }

    private void FixedUpdate()
    {
        UpdatePerformance();
        CheckState();
        UpdateWorldCoordinates();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Turn Enter":
                nextTurn = collision.gameObject.GetComponent<Turn>();
                nextTurnPosition = nextTurn.Position;
                nextTurnSpeed = nextTurn.MaxSpeed * corneringPerformance;
                break;
            case "Turn Exit":
                state = State.acceleration;
                nextTurnPosition = 10000;
                nextTurnSpeed = 0;
                break;
        }
    }

    #endregion

    #region Methods

    // to avoid accessing the disk in each frame, we load the data at the start of the class
    private void LoadCarData()
    {
        engineToAccImpact = settings.EngineToAccImpact;
        gearBoxToAccImpact = settings.GearBoxToAccImpact;
        weightToAccImpact = settings.WeightToAccImpact;

        brakesToBraImpact = settings.BrakesToBraImpact;
        weightToBraImpact = settings.WeightToBraImpact;
        tiresToBraImpact = settings.TiresToBraImpact;

        aeroToSpeedImpact = settings.AeroToSpeedImpact;
        gearBoxToSpeedImpact = settings.GearBoxToSpeedImpact;
        suspensionToSpeedImpact = settings.SuspensionToSpeedImpact;

        suspensionToCorImpact = settings.SuspensionToCorImpact;
        aeroToCorImpact = settings.AeroToCorImpact;
        tiresToCorImpact = settings.TiresToCorImpact;

        maxAccelerationPerformance = settings.MaxAccelerationPerformance;
        minAccelerationPerformance = settings.MinAccelerationPerformance;

        maxBrakingPerformance = settings.MaxBrakingPerformance;
        minBrakingPerformance = settings.MinBrakingPerformance;

        maxCorneringPerformance = settings.MaxCorneringPerformance;
        minCorneringPerformance = settings.MinCorneringPerformance;

        maxSpeedPerformance = settings.MaxSpeedPerformance;
        minSpeedPerformance = settings.MinSpeedPerformance;
    }

    // set data at object start
    public void Construct(CarData carData, Track track, int position)
    {
        this.carData = carData;
        this.track = track;
        this.position = position;

        // set starting position
        totalDistanceCovered = -(position * 5);

        // set visuals layer
        carVisuals.SetVisuals(position, carData.TeamData.PrimaryColor);

        // set car components start stats
        engine.Construct(carData.EngineEfficiencyStat);
        gearBox.Construct(carData.GearBoxEfficiencyStat);
        brakes.Construct(carData.BrakesEfficiencyStat);
        suspension.Construct(carData.SuspensionEfficiencyStat);
        aerodynamics.Construct(carData.AerodynamicsEfficiencyStat);
        weight.Construct(carData.WeightEfficiencyStat);

        tires.Construct(carData.TireSets[0]);
    }

    // set car position in 2D space
    private void UpdateWorldCoordinates()
    {
        // update acceleration
        switch (state)
        {
            case State.acceleration:
                acceleration = accelerationPerformance *
                (track.MaxSpeed * maxSpeedPerformance - speed) /
                track.MaxSpeed * maxSpeedPerformance;
                break;

            case State.braking:
                acceleration = -brakingPerformance;
                break;

            case State.constantSpeed:
                acceleration = 0f;
                break;
        }

        // update speed
        speed += acceleration * Time.fixedDeltaTime;

        // clamp speed over zero
        if (speed < 0) speed = 0;

        // update total covered distance
        float distanceDelta = speed * Time.fixedDeltaTime;
        totalDistanceCovered += distanceDelta;
        tires.UpdateEfficiency(distanceDelta);

        // set position in 2D space
        transform.position = track.Path.GetPointAtDistance(totalDistanceCovered);
    }

    private void CheckState()
    {
        // set braking state
        if (NextTurnDistance <= BrakingDistance && state == State.acceleration) state = State.braking;

        // set constant speed state
        if (speed <= nextTurnSpeed && state == State.braking) state = State.constantSpeed;
    }

    private void UpdatePerformance()
    {
        float accelerationStat = engine.EfficiencyStat * engineToAccImpact
            + gearBox.EfficiencyStat * gearBoxToAccImpact
            + weight.EfficiencyStat * weightToAccImpact;

        accelerationPerformance = Pitwall.ConvertStatToPerformance(accelerationStat,
            minAccelerationPerformance, maxAccelerationPerformance);


        float brakingStat = brakes.EfficiencyStat * brakesToBraImpact
            + tires.EfficiencyStat * tiresToBraImpact
            + weight.EfficiencyStat * weightToBraImpact;

        brakingPerformance = Pitwall.ConvertStatToPerformance(brakingStat,
            minBrakingPerformance, maxBrakingPerformance);


        float corneringStat = tires.EfficiencyStat * tiresToCorImpact
            + suspension.EfficiencyStat * suspensionToCorImpact
            + aerodynamics.EfficiencyStat * aeroToCorImpact;

        corneringPerformance = Pitwall.ConvertStatToPerformance(corneringStat,
            minCorneringPerformance, maxCorneringPerformance);


        float maxSpeedStat = aerodynamics.EfficiencyStat * aeroToSpeedImpact
            + gearBox.EfficiencyStat * gearBoxToSpeedImpact
            + suspension.EfficiencyStat * suspensionToSpeedImpact;

        maxSpeedPerformance = Pitwall.ConvertStatToPerformance(maxSpeedStat,
            minSpeedPerformance, maxSpeedPerformance);
    }

    #endregion
}
