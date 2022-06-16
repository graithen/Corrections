using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreakingNewsBanner : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public float scrollSpeed = 10;
    public float lifeTimer = 5;

    float width;
    float scrollPosition;
    Vector3 startPos;

    private void OnEnable()
    {
        width = textBox.preferredWidth;
        startPos = textBox.GetComponent<RectTransform>().position;
        scrollPosition = 0;

        StartCoroutine(coAutoDisable());
    }

    private void Update()
    {
        if (textBox.havePropertiesChanged)
        {
            width = textBox.preferredWidth;
        }

        textBox.GetComponent<RectTransform>().position = new Vector3(-scrollPosition % width, startPos.y, startPos.z);
        scrollPosition += scrollSpeed * Time.deltaTime;
    }

    private IEnumerator coAutoDisable()
    {
        yield return new WaitForSeconds(lifeTimer);
        gameObject.SetActive(false);
    }
}
