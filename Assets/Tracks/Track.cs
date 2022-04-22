using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class Track : MonoBehaviour
{
    #region Fields

    [SerializeField] private float maxSpeed;
    [SerializeField] private PathCreator circuit;
    [SerializeField] private RoadMeshCreator meshCreator;

    #endregion

    #region Properties

    public float MaxSpeed => maxSpeed;
    public PathCreator Circuit => circuit;

    #endregion

    #region Lifecycle

    private void Start()
    {
        meshCreator.TriggerUpdate();
    }

    #endregion
}
