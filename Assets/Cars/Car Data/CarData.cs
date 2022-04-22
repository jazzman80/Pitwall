using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car Data", menuName = "Pitwall/Car Data")]

public class CarData : ScriptableObject
{
    #region Fields

    public float accelerationStat;
    public float brakingStat;
    public float corneringStat;
    public float maxSpeedStat;

    #endregion
}
