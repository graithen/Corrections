using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PerformanceReview : MonoBehaviour
{
    [Header("Custom Components")]
    [SerializeField]
    private GameplayTracking gameplayTracking;
    [SerializeField]
    private StoryManager storyManager;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI employeeNameText;
    [SerializeField]
    private TextMeshProUGUI gradeText;
    [SerializeField]
    private TextMeshProUGUI performanceReviewText;
    [SerializeField]
    private GameObject underConstructionText;
    [SerializeField]
    private GameObject newReviewNotif;

    private int Grade; //A - F rating, 'F' being 1, 'A' being  
    private int suspicionLevel;

    private List<string> reviewStatements;

    public List<string> ch1ReviewStatements;
    public List<string> ch2ReviewStatements;
    public List<string> ch3ReviewStatements;
    public List<string> ch4ReviewStatements;

    public void GenerateWeeklyReport()
    {
        underConstructionText.gameObject.SetActive(false);

        employeeNameText.gameObject.SetActive(true);
        gradeText.gameObject.SetActive(true);
        performanceReviewText.gameObject.SetActive(true);

        CalculateGrade();
        UpdateUI();
    }

    private void CalculateGrade()
    {
        //rank each element out of 6, then average each element?

        int rating = gameplayTracking.AverageCompletedCases + CalculateSuspicionRating(gameplayTracking.suspicionRating); //max DCA = 5 & max SR = 7 ===> max = 12

        if (rating <= 2)
        {
            Grade = 1;
        }
        else if (rating > 2 && rating <= 4)
        {
            Grade = 2;
        }
        else if (rating > 2 && rating <= 4)
        {
            Grade = 3;
        }
        else if (rating > 4 && rating <= 6)
        {
            Grade = 4;
        }
        else if (rating > 6 && rating <= 8)
        {
            Grade = 5;
        }
        else if (rating > 8)
        {
            Grade = 6;
        }
    }

    private void UpdateUI()
    {
        employeeNameText.text = PlayerPrefs.GetString("PlayerName");

        //Debug.Log(Grade);
        switch (Grade)
        {
            case 1:
                gradeText.text = "F";
                gameplayTracking.ProcessGameEnd(false);
                break;
            case 2:
                gradeText.text = "E";
                break;
            case 3:
                gradeText.text = "D";
                break;
            case 4:
                gradeText.text = "C";
                break;
            case 5:
                gradeText.text = "B";
                break;
            case 6:
                gradeText.text = "A";
                break;
        }

        performanceReviewText.text = PickPerformanceReviewText();
        newReviewNotif.SetActive(true);
    }

    private string PickPerformanceReviewText()
    {
        string review = "";

        if (storyManager.currentChapter == StoryManager.Chapter.One)
        {
            reviewStatements = ch1ReviewStatements;
        }
        else if(storyManager.currentChapter == StoryManager.Chapter.Two)
        {
            reviewStatements = ch2ReviewStatements;
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Three)
        {
            reviewStatements = ch3ReviewStatements;
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Four)
        {
            reviewStatements = ch4ReviewStatements;
        }

        review = reviewStatements[Grade - 1];

        return review;
    }

    private int CalculateSuspicionRating(int SusValue)
    {
        if (SusValue <= 15)
            suspicionLevel = 7;
        else if (SusValue > 15 && SusValue <= 30)
            suspicionLevel = 6;
        else if (SusValue > 30 && SusValue <= 45)
            suspicionLevel = 5;
        else if (SusValue > 45 && SusValue <= 60)
            suspicionLevel = 4;
        else if (SusValue > 60 && SusValue <= 75)
            suspicionLevel = 3;
        else if (SusValue > 75 && SusValue <= 90)
            suspicionLevel = 2;
        else if (SusValue > 90)
            suspicionLevel = 1;

        return suspicionLevel;
    }
}
