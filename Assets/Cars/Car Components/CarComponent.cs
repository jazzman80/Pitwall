using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarComponent : MonoBehaviour
{
    #region Fields

    [SerializeField] float efficiencyStat;

    #endregion

    #region Properties

    public float EfficiencyStat => efficiencyStat;

    #endregion

    #region Methods

    public void Construct(float engineStat)
    {
        this.efficiencyStat = engineStat;
    }

    #endregion
}
