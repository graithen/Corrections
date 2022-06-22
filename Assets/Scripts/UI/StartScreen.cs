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
    private TutorialManager tutorialManager;

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
    [SerializeField]
    private GameObject gameScreen;

    [SerializeField]
    private Image titleScreen;
    [SerializeField]
    private Image titleLogo;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private Image disclaimer;
    [SerializeField]
    private TextMeshProUGUI disclaimerTitle;
    [SerializeField]
    private TextMeshProUGUI disclaimerBody;

    private void Start()
    {
        dateTimeSystem.Pause(); //Pause Time at Start
        StartCoroutine(coTitleDisclaimer());
    }

    public void StartGame()
    {
        if(!DetailsFilled()) //Display Enter Name Message!
        {
            warningText.gameObject.SetActive(true);
        }
        else //Start Game!
        {
            PlayerPrefs.SetString("PlayerFName", fNameInputField.text);
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
        gameScreen.SetActive(true);

        gameNameText.gameObject.SetActive(false);
        welcomeText.gameObject.SetActive(false);
        fNameInputField.gameObject.SetActive(false);
        sNameInputField.gameObject.SetActive(false);
        ageInputField.gameObject.SetActive(false);
        warningText.gameObject.SetActive(false);

        while(backgroundImage.color.a > 0)
        {
            backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, backgroundImage.color.a - Time.deltaTime);
            yield return null;
        }

        dateTimeSystem.Pause(); //Unpase Time when Clicked Play
        //Debug.Log(PlayerPrefs.GetString("FName: " + "PlayerFName"));
        //Debug.Log(PlayerPrefs.GetString("SName: " + "PlayerSName"));
        //Debug.Log(PlayerPrefs.GetString("Age: " + "PlayerAge"));
        yield return new WaitForSeconds(0.5f);

        tutorialManager.EnableTutorialCanvas(true);
        backgroundImage.gameObject.SetActive(false);
    }

    private IEnumerator coTitleDisclaimer()
    {
        yield return new WaitForSeconds(4f);
        titleLogo.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        while(titleScreen.color.a > 0)
        {
            titleScreen.color = new Color(titleScreen.color.r, titleScreen.color.g, titleScreen.color.b, titleScreen.color.a - Time.deltaTime);
            yield return null;
        }
        titleScreen.gameObject.SetActive(false);

        yield return new WaitForSeconds(5f);
        disclaimerTitle.gameObject.SetActive(false);
        disclaimerBody.gameObject.SetActive(false);
        while(disclaimer.color.a > 0)
        {
            disclaimer.color = new Color(disclaimer.color.r, disclaimer.color.g, disclaimer.color.b, disclaimer.color.a - Time.deltaTime);
            yield return null;
        }
        disclaimer.gameObject.SetActive(false);
    }
}
