using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class CarTimer : MonoBehaviour
{
    #region Fields

    [SerializeField] private Car car;
    [SerializeField] private List<double> lapTimes;
    [SerializeField] private int currentLapNumber;
    [SerializeField] private double timeStamp;
    [SerializeField] private double pace;
    [SerializeField] private string paceString;
 
    #endregion

    #region LifeCycle

    private void Start()
    {
        timeStamp = 0;
        currentLapNumber = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Start Line")) OnLapStart();
    }

    #endregion

    #region Methods

    private void OnLapStart()
    {
        lapTimes.Add(Time.timeAsDouble - timeStamp);

        if(currentLapNumber > 1)
        {
            pace = lapTimes[currentLapNumber] - lapTimes[currentLapNumber - 1];
            paceString = pace.ToString("0.000", CultureInfo.InvariantCulture);
        }

        currentLapNumber++;
        timeStamp = Time.timeAsDouble;

    }

    #endregion
}
