using UnityEngine;

public class Engine : MonoBehaviour
{
    #region Fields

    [SerializeField] float engineStat;

    #endregion

    #region Properties

    public float EngineStat => engineStat;

    #endregion

    #region Methods

    public void Construct(float engineStat)
    {
        this.engineStat = engineStat;
    }

    #endregion
}
