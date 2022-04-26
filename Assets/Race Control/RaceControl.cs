using System.Collections.Generic;
using UnityEngine;

public class RaceControl : MonoBehaviour
{
    #region Fields

    [SerializeField] RaceData raceData;
    [SerializeField] Car carPrefab;
    [SerializeField] List<Car> carList;

    #endregion

    #region Properties

    public List<Car> CarList => carList;

    #endregion

    #region Lifecycle

    private void Start()
    {
        Track newTrack = Instantiate(raceData.Track);
        CreateCars(newTrack);
    }

    #endregion

    #region Methods

    private void CreateCars(Track track)
    {
        for (int i = 0; i < raceData.StartingGridList.Count; i++)
        {
            Car newCar = Instantiate(carPrefab);
            newCar.Construct(raceData.StartingGridList[i], track, i);
            carList.Add(newCar);
        }
    }

    #endregion
}
