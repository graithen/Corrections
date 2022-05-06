using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceReview : MonoBehaviour
{
    [SerializeField]
    private GameplayTracking gameplayTracking;

    public int Grade; //A - F rating, 'F' being 1, 'A' being 7 
    string notes;

    int suspicionLevel;

    public List<string> ExceptionalStatements;
    public List<string> PositiveStatements;
    public List<string> NegativeStatements;
    public List<string> ExceptionallyNegativeStatements;

    public List<string> StoryStatements;

    public void GenerateWeeklyReport()
    {
        //rank each element out of 7, then average each element
        int grade = gameplayTracking.AverageCompletedCases + CalculateSuspicionRating(gameplayTracking.suspicionRating); //max DCA = 5 & max SR = 7 ===> max = 12

        //suspicionRating;
        if(grade <= 2)
        {
            Grade = 1;
        }
        else if(grade > 2 && grade <=4)
        {
            Grade = 2;
        }
        else if (grade > 2 && grade <= 4)
        {
            Grade = 3;
        }
        else if (grade > 4 && grade <= 6)
        {
            Grade = 4;
        }
        else if (grade > 6 && grade <= 8)
        {
            Grade = 5;
        }
        else if (grade > 8 && grade <= 10)
        {
            Grade = 6;
        }
        else if (grade > 10 && grade <= 12)
        {
            Grade = 7;
        }

        Debug.Log(Grade);
    }

    void UpdateUI()
    {

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
