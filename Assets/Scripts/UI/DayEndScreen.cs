using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayEndScreen : MonoBehaviour
{
    [Header("UI")]
    public Image dayEndScreen;
    public TextMeshProUGUI dayEndText;
    public Button dayEndButton;

    public void ActivateDayEndScreen()
    {
        StartCoroutine(coActivateDayEndScreen());
    }

    public void DeactivateDayEndScreen()
    {
        StartCoroutine(coDeactivateDayEndScreen());
    }

    private IEnumerator coActivateDayEndScreen()
    {
        dayEndScreen.gameObject.SetActive(true);

        while (dayEndScreen.color.a < 1)
        {
            dayEndScreen.color = new Color(dayEndScreen.color.r, dayEndScreen.color.g, dayEndScreen.color.b, dayEndScreen.color.a + Time.deltaTime);
            yield return null;
        }

        dayEndText.gameObject.SetActive(true);
        dayEndButton.gameObject.SetActive(true);
    }

    private IEnumerator coDeactivateDayEndScreen()
    {
        dayEndText.gameObject.SetActive(false);
        dayEndButton.gameObject.SetActive(false);

        while (dayEndScreen.color.a > 0)
        {
            dayEndScreen.color = new Color(dayEndScreen.color.r, dayEndScreen.color.g, dayEndScreen.color.b, dayEndScreen.color.a - Time.deltaTime);
            yield return null;
        }

        dayEndScreen.gameObject.SetActive(false);
    }
}
