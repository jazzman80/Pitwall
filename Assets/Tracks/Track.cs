using UnityEngine;
using PathCreation;

public class Track : MonoBehaviour
{
    #region Fields

    [Header("Globals")]
    [SerializeField] PathCreator circuit;
    [SerializeField] float maxSpeed;

    #endregion

    #region Properties

    public VertexPath Path => circuit.path;
    public float MaxSpeed => maxSpeed;
    public float Length => circuit.path.length;

    #endregion
}
