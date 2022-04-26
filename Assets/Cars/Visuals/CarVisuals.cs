using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarVisuals : MonoBehaviour
{
    #region Fields

    [SerializeField] SpriteRenderer circle;
    [SerializeField] SpriteRenderer ring;

    #endregion

    #region Methods

    public void SetVisuals(int carPosition, Color teamColor)
    {
        circle.sortingOrder = (carPosition * 2) + 10;
        ring.sortingOrder = (carPosition * 2) + 11;

        circle.color = teamColor;
    }

    #endregion

}
