using UnityEngine;

[CreateAssetMenu(fileName = "Tire Set", menuName = "Pitwall/Tire Set")]

public class TireSet : ScriptableObject
{
    #region Fields

    [SerializeField] float efficiencyStat;
    [SerializeField] float wear;

    #endregion

    #region Properties

    public float EfficiencyStat => efficiencyStat;
    public float Wear => wear;

    #endregion
}