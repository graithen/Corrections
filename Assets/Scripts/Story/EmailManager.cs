using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailManager : MonoBehaviour
{
    //Look at List of Dated Emails and Send
    //Process Story Emails and Send
    [SerializeField]
    private GameObject emailButton;
    [SerializeField]
    private Transform emailButtonHolder;
    [SerializeField]
    private GameObject newEmailNotif;

    [SerializeField]
    private DateTimeSystem dateTimeSystem;
    [SerializeField]
    private GameplayTracking gameplayTracking;

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
                    UpdateEmailUI(todaysEmails[i]);
                    if(!emailButtonHolder.gameObject.activeInHierarchy)
                    {
                        newEmailNotif.SetActive(true);
                    }
                }
            }
        }
    }

    private void UpdateEmailUI(EmailData email)
    {
        GameObject newEmail = Instantiate(emailButton,emailButtonHolder);
        newEmail.GetComponent<EmailButton>().InitButton(email);
        newEmail.transform.SetAsFirstSibling();
    }
}
