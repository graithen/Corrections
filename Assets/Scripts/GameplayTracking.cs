using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayTracking : MonoBehaviour
{
    private int suspicionRating = 0;
    private int suspicionModifier = 0;

    private int suspicionEventIndex = 0;

    private List<string> victimList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessVictim(string Name, int Punishment, int ExpectedPunishment)
    {
        if(Punishment < ExpectedPunishment)
        {
            int punishmentDifference = ExpectedPunishment - Punishment;
            suspicionRating += punishmentDifference;

            if (suspicionRating > 40 && suspicionModifier < 1)
            {
                suspicionModifier++;
            }
            else if (suspicionRating > 60 && suspicionModifier < 2)
            {
                suspicionModifier++;
            }
            else if (suspicionRating > 80 && suspicionModifier < 3)
            {
                suspicionModifier++;
            }
        }

        Debug.Log(Name + ": " + Punishment + " / " + ExpectedPunishment);
        Debug.Log("Sus Rating = " + suspicionRating);

        SuspicionEvents();
        victimList.Add(Name);
    }

    void SuspicionEvents()
    {
        switch (suspicionEventIndex)
        {
            case 10:
                //friendly warning
                break;

            case 45:
                //Stern warning
                break;

            case 50:
                //under investigation
                break;

            case 90:
                //house raided
                break;

            case 100:
                //trigger end game loss
                break;
        }
    }
}
