using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Race Data", menuName = "Pitwall/Race Data")]

public class RaceData : ScriptableObject
{
    #region Fields

    [SerializeField] Track track;
    [SerializeField] List<CarData> startingGridList;

    #endregion

    #region Properties

    public Track Track => track;
    public List<CarData> StartingGridList => startingGridList;

    #endregion
}
