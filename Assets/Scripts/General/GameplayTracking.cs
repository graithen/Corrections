using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameplayTracking : MonoBehaviour
{
    public bool triggerSuspicionEvents = false;

    [Header("Custom Components")]
    [SerializeField]
    private StoryManager storyManager;
    [SerializeField]
    private EmailManager emailManager;

    [Header("UI")]
    [SerializeField]
    private GameObject endScreen;
    [SerializeField]
    private GameObject warCrimes;
    [SerializeField]
    private GameObject rapSheet;
    [SerializeField]
    private TextMeshProUGUI playerNameTextWarCrimes;
    [SerializeField]
    private Text victimListTextWarCrimes;
    [SerializeField]
    private TextMeshProUGUI playerNameTextRapSheet;
    [SerializeField]
    private Text crimesListRapSheet;

    [Header("Values")]
    public int suspicionRating = 0;
    private int suspicionLevel = 0;

    private int prevSuspicionEventIndex = -1;
    private int suspicionEventIndex = 0;

    private List<string> victimList = new List<string>();
    public List<string> VictimStatus = new List<string> { "Beaten to death by guards", "Died of starvation"};
    public List<string> crimesList = new List<string>();

    [Header("Performance Stats")]
    public int completedDays = 0;
    [SerializeField]
    private int weeklyCompletedCases = 0;
    [SerializeField]
    private int weeklyCorrectlyMarkedCases = 0;
    [SerializeField]
    private float averageCompletedCases = 0;
    private int daysElapsedInWeek = 0;

    [Header("Expected Case Clearance")]
    [SerializeField]
    private int ch1ExpectedClearance = 7;
    [SerializeField]
    private int ch2ExpectedClearance = 10;
    [SerializeField]
    private int ch3ExpectedClearance = 15;
    [SerializeField]
    private int ch4ExpectedClearance = 25;
    private int expectedCaseClearance;

    [Header("Suspicion Emails")]
    public List<EmailData> suspicionEmails;

    public void ProcessVictim(string Name, int Punishment, int ExpectedPunishment)
    {
        if(Punishment == ExpectedPunishment)
        {
            weeklyCorrectlyMarkedCases++;
        }
        else if(Punishment < ExpectedPunishment)
        {
            int punishmentDifference = ExpectedPunishment - Punishment;
            suspicionRating += punishmentDifference;

            if (suspicionRating > 40 && suspicionLevel < 1)
            {
                suspicionLevel++;
            }
            else if (suspicionRating > 60 && suspicionLevel < 2)
            {
                suspicionLevel++;
            }
            else if (suspicionRating > 80 && suspicionLevel < 3)
            {
                suspicionLevel++;
            }
        }

        //Debug.Log(Name + ": " + Punishment + " / " + ExpectedPunishment);
        //Debug.Log("Sus Rating = " + suspicionRating);

        weeklyCompletedCases++;

        if(triggerSuspicionEvents)
        {
            SuspicionEvents();
        }
     
        if (storyManager.currentChapter == StoryManager.Chapter.Three || storyManager.currentChapter == StoryManager.Chapter.Four)
        {
            if (Punishment > 2)
            {
                victimList.Add(Name);
            }
        }    
    }

    private void DetermineExpectedClearance()
    {
        if(storyManager.currentChapter == StoryManager.Chapter.One)
        {
            expectedCaseClearance = ch1ExpectedClearance;
        }
        else if(storyManager.currentChapter == StoryManager.Chapter.Two)
        {
            expectedCaseClearance = ch2ExpectedClearance;
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Three)
        {
            expectedCaseClearance = ch3ExpectedClearance;
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Four)
        {
            expectedCaseClearance = ch4ExpectedClearance;
        }
    }

    private void DetermineSuspicionEventIndex()
    {
        if(suspicionRating > 10 && suspicionEventIndex <= 20)
        {
            suspicionEventIndex = 0;
        }
        else if(suspicionRating > 20 && suspicionEventIndex <= 35)
        {
            suspicionEventIndex = 1;
        }
        else if (suspicionRating > 35 && suspicionEventIndex <= 55)
        {
            suspicionEventIndex = 2;
        }
        else if (suspicionRating > 55 && suspicionEventIndex <= 80)
        {
            suspicionEventIndex = 3;
        }
        else if (suspicionRating > 80 && suspicionEventIndex <= 100)
        {
            suspicionEventIndex = 4;
        }
    }

    private void SuspicionEvents()
    {
        DetermineSuspicionEventIndex();
        if(prevSuspicionEventIndex < suspicionEventIndex)
        {
            SendEmailsToEmailManager(suspicionEmails[suspicionEventIndex]);
            prevSuspicionEventIndex = suspicionEventIndex;
        }

    }

    public void DailyUpdate()
    {
        completedDays++;
        daysElapsedInWeek++;
        CheckMeetingQuota();

        if(completedDays >= storyManager.finalGameDay)
        {
            ProcessGameEnd(true);
        }
    }

    public float CalculateWeeklyCaseValues()
    {
        float ratio = (weeklyCompletedCases - weeklyCorrectlyMarkedCases) / weeklyCompletedCases;
        return ratio;
    }

    public float CalculateAverageCompletedCases()
    {
        DetermineExpectedClearance();

        averageCompletedCases = weeklyCompletedCases / daysElapsedInWeek;
        float ratio = averageCompletedCases / expectedCaseClearance;
        return ratio;
    }

    public void ResetWeeklyValues()
    {
        daysElapsedInWeek = 0;
        weeklyCompletedCases = 0;
        weeklyCorrectlyMarkedCases = 0;
        averageCompletedCases = 0;
    }

    public void CheckMeetingQuota()
    {
        DetermineExpectedClearance();

        if(averageCompletedCases < expectedCaseClearance/2)
        {
            suspicionLevel++;
        }
        if(averageCompletedCases > expectedCaseClearance*2)
        {
            suspicionLevel--;
        }
    }

    public void ProcessGameEnd(bool finalDayEnding)
    {
        endScreen.SetActive(true);


        if (finalDayEnding)
        {
            warCrimes.SetActive(true);
            playerNameTextWarCrimes.text = PlayerPrefs.GetString("PlayerFName") + " " + PlayerPrefs.GetString("PlayerSName");

            string victimNames = "";
            foreach(string name in victimList)
            {
                string details = name + ": " + VictimStatus[Random.Range(0, VictimStatus.Count)];
                victimNames += details + "\n";
            }
            victimListTextWarCrimes.text = victimNames;
        }
        else
        {
            rapSheet.SetActive(true);
            playerNameTextRapSheet.text = PlayerPrefs.GetString("PlayerFName") + " " + PlayerPrefs.GetString("PlayerSName");

            string crimes = "";
            for (int i = 0; i < 5; i++)
            {
                int temp = Random.Range(0, crimesList.Count);
                crimes += crimesList[temp] + "\n";
                crimesList.Remove(crimesList[temp]);
            }
            crimesListRapSheet.text = crimes;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SendEmailsToEmailManager(EmailData email)
    {
        if (email != null)
        {
            emailManager.todaysEmails.Add(email);
        }
    }
}
