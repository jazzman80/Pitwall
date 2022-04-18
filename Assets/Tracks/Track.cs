using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Track : MonoBehaviour
{
    #region Fields

    [SerializeField] private float maxSpeed;
    [SerializeField] private PathCreator circuit;

    #endregion

    #region Properties

    public float MaxSpeed => maxSpeed;
    public PathCreator Circuit => circuit;

    #endregion
}
