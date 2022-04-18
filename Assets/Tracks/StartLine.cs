using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class StartLine : MonoBehaviour
{
    #region Fields

    [SerializeField] private Track track;

    #endregion

    #region Lifecycle

    private void Start()
    {
        transform.position = track.Circuit.path.GetPointAtDistance(1.0f);
    }

    #endregion
}
