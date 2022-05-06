using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceReview : MonoBehaviour
{
    GameplayTracking gameplayTracking;

    int performanceTier = 0; //A - F rating, 'F' being zero, 'A' being 7 
    string notes;

    public List<string> ExceptionalStatements;
    public List<string> PositiveStatements;
    public List<string> NegativeStatements;
    public List<string> ExceptionallyNegativeStatements;

    public List<string> StoryStatements;

    public void GenerateWeeklyReport(int dailyCaseAverage, int PunishmentAverage)
    {
        //rank each element out of 7, then average each element
        
        
    }

    void UpdateUI()
    {

    }
}
