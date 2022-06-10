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
    private TMP_InputField fNameInputField;
    [SerializeField]
    private TMP_InputField sNameInputField;
    [SerializeField]
    private TMP_InputField ageInputField;
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
        if(DetailsFilled()) //Display Enter Name Message!
        {
            warningText.gameObject.SetActive(true);
        }
        else //Start Game!
        {
            PlayerPrefs.SetString("PlayerName", fNameInputField.text);
            PlayerPrefs.SetString("PlayerSName", sNameInputField.text);
            PlayerPrefs.SetString("PlayerAge", ageInputField.text);

            startButton.gameObject.SetActive(false);
            StartCoroutine(coStartAnim());
        }
    }

    private bool DetailsFilled()
    {
        if(string.IsNullOrEmpty(fNameInputField.text) || string.IsNullOrEmpty(sNameInputField.text) || string.IsNullOrEmpty(ageInputField.text))
        {
            return false;
        }

        return true;
    }

    private IEnumerator coStartAnim()
    {
        gameNameText.gameObject.SetActive(false);
        welcomeText.gameObject.SetActive(false);
        fNameInputField.gameObject.SetActive(false);
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
