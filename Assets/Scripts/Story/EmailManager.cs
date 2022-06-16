using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmailManager : MonoBehaviour
{
    public bool generateDailyEmails;

    [Header("UI")]
    [SerializeField]
    private GameObject emailButton;
    [SerializeField]
    private Transform emailButtonHolder;
    [SerializeField]
    private GameObject newEmailNotif;
    [SerializeField]
    private TextMeshProUGUI senderText;
    [SerializeField]
    private TextMeshProUGUI subjectText;
    [SerializeField]
    private TextMeshProUGUI bodyText;
    [SerializeField]
    private GameObject noEmailsText;

    [Header("Custom Components")]
    [SerializeField]
    private DateTimeSystem dateTimeSystem;
    [SerializeField]
    private GameplayTracking gameplayTracking;
    [SerializeField]
    private StoryManager storyManager;

    [Header("Data")]
    [SerializeField]
    private List<EmailData> allEmails;
    [HideInInspector]
    public List<EmailData> todaysEmails = new List<EmailData>();

    private void OnEnable()
    {
        UpdateNoEmailsText();
    }

    public void ConstructTodaysEmails() //Call every Day
    {
        todaysEmails.Clear();

        if(generateDailyEmails)
        {
            for (int i = 0; i < allEmails.Count; i++)
            {
                if (allEmails[i].sendDay == gameplayTracking.completedDays + 1)
                {
                    todaysEmails.Add(allEmails[i]);
                }
            }
        }
        
        storyManager.CheckForStoryEmails();
    }

    public void SendEmail() //Call every Minute
    {
        if(todaysEmails.Count > 0)
        {
            for (int i = 0; i < todaysEmails.Count; i++)
            {
                if(dateTimeSystem.TimeGet == dateTimeSystem.IntToStringTime(todaysEmails[i].sendHour, todaysEmails[i].sendMin))
                {
                    NewEmailButton(todaysEmails[i]);
                    todaysEmails.Remove(todaysEmails[i]);

                    if (!emailButtonHolder.gameObject.activeInHierarchy)
                    {
                        newEmailNotif.SetActive(true);
                    }
                }
            }
        }

        UpdateNoEmailsText();
    }

    private void NewEmailButton(EmailData email)
    {
        GameObject newEmail = Instantiate(emailButton,emailButtonHolder);
        newEmail.GetComponent<EmailButton>().InitButton(email, this);
        newEmail.transform.SetAsFirstSibling();
        emailButtonHolder.GetComponent<RectTransform>().sizeDelta += new Vector2(0, emailButton.GetComponent<RectTransform>().sizeDelta.y + 8);
    }

    public void DisplayEmail(EmailData email)
    {
        senderText.gameObject.SetActive(true);
        subjectText.gameObject.SetActive(true);
        bodyText.gameObject.SetActive(true);

        senderText.text = email.sender;
        subjectText.text = email.subject;
        bodyText.text = email.body;
    }

    private void UpdateNoEmailsText()
    {
        if(emailButtonHolder.childCount > 0)
        {
            noEmailsText.SetActive(false);
        }
        else
        {
            noEmailsText.SetActive(true);
        }
    }

    public void SendRemainingEmailsForToday()
    {
        if(todaysEmails.Count > 0)
        {
            for (int i = 0; i < todaysEmails.Count; i++)
            {
                NewEmailButton(todaysEmails[i]);

                if (!emailButtonHolder.gameObject.activeInHierarchy)
                {
                    newEmailNotif.SetActive(true);
                }
            }
        }
    }
}
