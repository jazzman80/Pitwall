using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class Track : MonoBehaviour
{
    #region Fields

    [Header("Globals")]
    [SerializeField] PathCreator circuit;
    [SerializeField] RoadMeshCreator roadMesh;
    [SerializeField] float maxSpeed;

    #endregion

    #region Properties

    public VertexPath Path => circuit.path;
    public float MaxSpeed => maxSpeed;
    public float Length => circuit.path.length;

    #endregion

    #region Lifecycle

    private void Start()
    {
        // show road mesh on start
        roadMesh.TriggerUpdate();
    }

    #endregion
}
