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
        float rating = (gameplayTracking.CalculateWeeklyCaseValues() + gameplayTracking.CalculateAverageCompletedCases()) * (100 - gameplayTracking.suspicionRating);
        Debug.Log("Rating = " + rating);
        
        if (rating <= 40) //F
        {
            Grade = 1;
        }
        else if (rating > 40 && rating <= 50) //E
        {
            Grade = 2;
        }
        else if (rating > 50 && rating <= 60) //D
        {
            Grade = 3;
        }
        else if (rating > 60 && rating <= 80) //C
        {
            Grade = 4;
        }
        else if (rating > 80 && rating <= 90) //B
        {
            Grade = 5;
        }
        else if (rating > 90) //A
        {
            Grade = 6;
        }
    }

    private void UpdateUI()
    {
        employeeNameText.text = PlayerPrefs.GetString("PlayerFName") + " " + PlayerPrefs.GetString("PlayerSName");

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

        gameplayTracking.ResetWeeklyValues();
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
}
