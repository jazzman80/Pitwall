using UnityEngine;

[CreateAssetMenu(fileName = "Tire Data", menuName = "Pitwall/Tire Data")]

public class TireData : ScriptableObject
{
    #region Fields

    [SerializeField] float efficiencyStat;

    #endregion

    #region Properties

    public float EfficiencyStat => efficiencyStat;

    #endregion
}
