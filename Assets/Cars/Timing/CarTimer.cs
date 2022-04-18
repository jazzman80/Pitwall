using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTimer : MonoBehaviour
{
    #region Fields

    [SerializeField] private float currentLapTime;
    [SerializeField] private float lastLapTime;
    [SerializeField] private float timeStamp;
    [SerializeField] private float pace;
 
    #endregion

    #region LifeCycle

    private void Start()
    {
        timeStamp = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Start Line")) OnLapStart();
    }

    #endregion

    #region Methods

    private void OnLapStart()
    {
        currentLapTime = Time.time - timeStamp;
        pace = currentLapTime - lastLapTime;
        lastLapTime = currentLapTime;
        timeStamp = Time.time;
    }

    #endregion
}
