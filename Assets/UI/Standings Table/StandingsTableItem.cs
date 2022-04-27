using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StandingsTableItem : MonoBehaviour
{
    #region Fields

    [Header("Globals")]
    [SerializeField] RaceControl raceControl;

    [Header("Fields")]
    [SerializeField] int index;
    [SerializeField] Image teamBox;
    [SerializeField] TextMeshProUGUI driverName;

    #endregion

    #region Lifecycle

    private void Start()
    {
        InvokeRepeating("UpdateItem", 0f, 3f);
    }

    #endregion

    #region Methods

    private void UpdateItem()
    {
        teamBox.color = raceControl.CarList[index].CarData.TeamData.PrimaryColor;
        driverName.text = raceControl.CarList[index].CarData.DriverData.ShortName;
    }

    #endregion
}
