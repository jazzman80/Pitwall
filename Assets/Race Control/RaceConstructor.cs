using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceConstructor : MonoBehaviour
{
    #region Fields

    [SerializeField] private RaceControl raceControl;
    [SerializeField] private Track trackPrefab;
    [SerializeField] private Car carPrefab;

    #endregion

    #region LifeCycle

    private void Start()
    {
        ConstructRace();
    }

    #endregion

    #region Methods

    private void ConstructRace()
    {
        // create track
        Track newTrack = Instantiate(trackPrefab);

        // create cars
        foreach(CarData carData in raceControl.startingGridList)
        {
            Car newCar = Instantiate(carPrefab);
            newCar.Construct(newTrack, carData, raceControl.startingGridList.IndexOf(carData));
        }
    }

    #endregion
}
