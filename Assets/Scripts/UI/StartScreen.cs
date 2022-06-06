using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    private DateTimeSystem dateTimeSystem;

    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private TextMeshProUGUI gameNameText;
    [SerializeField]
    private TextMeshProUGUI welcomeText;
    [SerializeField]
    private TMP_InputField nameInputField;
    [SerializeField]
    private TextMeshProUGUI warningText;
    [SerializeField]
    private Button startButton;

    private void Start()
    {
        dateTimeSystem.Pause(); //Pause Time at Start
    }

    public void StartGame()
    {
        if(string.IsNullOrEmpty(nameInputField.text)) //Display Enter Name Message!
        {
            warningText.gameObject.SetActive(true);
        }
        else //Start Game!
        {
            PlayerPrefs.SetString("PlayerName", nameInputField.text);
            startButton.gameObject.SetActive(false);
            StartCoroutine(coStartAnim());
        }
    }

    private IEnumerator coStartAnim()
    {
        gameNameText.gameObject.SetActive(false);
        welcomeText.gameObject.SetActive(false);
        nameInputField.gameObject.SetActive(false);
        warningText.gameObject.SetActive(false);

        while(backgroundImage.color.a > 0)
        {
            backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, backgroundImage.color.a - Time.deltaTime);
            yield return null;
        }

        dateTimeSystem.Pause(); //Unpase Time when Clicked Play
        //Debug.Log(PlayerPrefs.GetString("PlayerName"));
        backgroundImage.gameObject.SetActive(false);
    }
}
