using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayEndScreen : MonoBehaviour
{
    [SerializeField]
    private GameplayTracking gameplayTracking;

    [SerializeField]
    private TextMeshProUGUI dayEndText;

    private void OnEnable()
    {
        dayEndText.text = "End of Day " + gameplayTracking.completedDays;
    }
}
