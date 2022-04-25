using UnityEngine;

public class Car : MonoBehaviour
{
    #region Fields

    [Header("Globals")]
    [SerializeField] Track track;
    [SerializeField] CarData data;

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


    #endregion

    #region Properties

    float NextTurnDistance => nextTurnPosition - LapDistanceCovered;
    float BrakingDistance => ((speed * speed) - (nextTurnSpeed * nextTurnSpeed)) /
        (2 * data.BrakingPerformance);

    float LapDistanceCovered => totalDistanceCovered % track.Length;

    #endregion

    #region Lifecycle

    private void Start()
    {
        nextTurnSpeed = 0;
        nextTurnPosition = 10000;
    }

    private void FixedUpdate()
    {
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
                nextTurnSpeed = nextTurn.MaxSpeed * data.CorneringPerformance;
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

    // set car position in 2D space
    private void UpdateWorldCoordinates()
    {
        // update acceleration
        switch (state)
        {
            case State.acceleration:
                acceleration = data.AccelerationPerformance *
                (track.MaxSpeed * data.MaxSpeedPerformance - speed) /
                track.MaxSpeed * data.MaxSpeedPerformance;
                break;

            case State.braking:
                acceleration = -data.BrakingPerformance;
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
        totalDistanceCovered += speed * Time.fixedDeltaTime;

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

    #endregion
}
