using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmailManager : MonoBehaviour
{
    //Look at List of Dated Emails and Send
    //Process Story Emails and Send
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

    [Header("Custom Components")]
    [SerializeField]
    private DateTimeSystem dateTimeSystem;
    [SerializeField]
    private GameplayTracking gameplayTracking;

    [Header("Data")]
    [SerializeField]
    private List<EmailData> allEmails;
    private List<EmailData> todaysEmails = new List<EmailData>();

    public void ConstructTodaysEmails() //Call every Day
    {
        todaysEmails.Clear();

        for (int i = 0; i < allEmails.Count; i++)
        {
            if(allEmails[i].sendDay == gameplayTracking.completedDays + 1)
            {
                todaysEmails.Add(allEmails[i]);
            }
        }
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

                    if (!emailButtonHolder.gameObject.activeInHierarchy)
                    {
                        newEmailNotif.SetActive(true);
                    }
                }
            }
        }
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
}
