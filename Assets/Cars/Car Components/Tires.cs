using UnityEngine;

public class Tires : MonoBehaviour
{
    #region Fields
    [SerializeField] Settings settings;
    [SerializeField] float efficiencyStat;
    [SerializeField] float wearStat;
    float tireWearRatio;

    #endregion

    #region Properties

    public float EfficiencyStat => efficiencyStat;

    #endregion

    #region Methods

    public void Construct(TireSet tireSet)
    {
        this.efficiencyStat = tireSet.EfficiencyStat;
        this.wearStat = tireSet.Wear;
        this.tireWearRatio = settings.TireWearRatio;
    }

    public void UpdateEfficiency(float coveredDistance)
    {
        efficiencyStat -= coveredDistance * wearStat * tireWearRatio;
        if (efficiencyStat < 0) efficiencyStat = 0;
    }

    #endregion
}
