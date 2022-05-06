using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceReview : MonoBehaviour
{
    GameplayTracking gameplayTracking;

    public int Grade; //A - F rating, 'F' being zero, 'A' being 7 
    string notes;

    int suspicionRating;

    public List<string> ExceptionalStatements;
    public List<string> PositiveStatements;
    public List<string> NegativeStatements;
    public List<string> ExceptionallyNegativeStatements;

    public List<string> StoryStatements;

    public void GenerateWeeklyReport(int DailyCaseAverage, int SusValue)
    {
        //rank each element out of 7, then average each element
        CalculateSuspicionRating(SusValue);

        //suspicionRating;




    }

    void UpdateUI()
    {

    }

    void CalculateSuspicionRating(int SusValue)
    {
        if (SusValue <= 15)
            suspicionRating = 7;
        else if (SusValue > 15 && SusValue <= 30)
            suspicionRating = 6;
        else if (SusValue > 30 && SusValue <= 45)
            suspicionRating = 5;
        else if (SusValue > 45 && SusValue <= 60)
            suspicionRating = 4;
        else if (SusValue > 60 && SusValue <= 75)
            suspicionRating = 3;
        else if (SusValue > 75 && SusValue <= 90)
            suspicionRating = 2;
        else if (SusValue > 90)
            suspicionRating = 1;
    }
}
