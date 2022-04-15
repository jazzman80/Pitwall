using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    [SerializeField] private float position;
    [SerializeField] private float maxSpeed;

    public float Position => position;
    public float MaxSpeed => maxSpeed;
}
