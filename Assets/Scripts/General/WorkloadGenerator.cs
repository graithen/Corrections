using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkloadGenerator : MonoBehaviour
{
    [SerializeField]
    private StoryManager storyManager;

    public bool useChapters;

    [SerializeField]
    private CharacterGeneration CharGen;
    public Dictionary<int, (string, string, int, bool)> CharacterPool;

    public List<string> Infractions1, Infractions2, Infractions3, Infractions4, Infractions5;

    public List<string> ch1Infractions1, ch1Infractions2, ch1Infractions3, ch1Infractions4, ch1Infractions5;
    public List<string> ch2Infractions1, ch2Infractions2, ch2Infractions3, ch2Infractions4, ch2Infractions5;
    public List<string> ch3Infractions1, ch3Infractions2, ch3Infractions3, ch3Infractions4, ch3Infractions5;
    public List<string> ch4Infractions1, ch4Infractions2, ch4Infractions3, ch4Infractions4, ch4Infractions5;

    private int CharacterID = 0;

    private List<CaseData> DailyCaseList = new List<CaseData>();
    [SerializeField]
    private int CaseListLength = 10;

    [Header("Infraction Limits")]
    [SerializeField]
    private int ch1MaxPossibleInfractions = 3;
    [SerializeField]
    private int ch2MaxPossibleInfractions = 5;
    [SerializeField]
    private int ch3MaxPossibleInfractions = 7;
    [SerializeField]
    private int ch4MaxPossibleInfractions = 10;
    private int maxPossibleInfractions;

    private void Start()
    {
        CharGen.InitializeData();

        CharGen.NumberOfCharacters = 10000;
        CharacterPool = CharGen.GenerateCharacters();
    }

    public List<CaseData> GenerateWorkload(bool Story)
    {
        for (int i = 0; i < CaseListLength; i++)
        {
            DailyCaseList.Add(GenerateCaseInfo());
        }

        if(Story)
        {
            DailyCaseList.Add(GenerateStoryCaseInfo());
        }

        //algorithm to randomly sort list

        return DailyCaseList;
    }

    private CaseData GenerateCaseInfo()
    {
        CaseData newCase = new CaseData();

        (string, string, int, bool) Character;
        CharacterPool.TryGetValue(CharacterID, out Character);

        newCase.SecondName = Character.Item1;
        newCase.FirstName = Character.Item2;
        newCase.Age = Character.Item3;

        if (Character.Item4)
        {
            newCase.Gender = "Male";
        }
        else
        {
            newCase.Gender = "Female";
        }

        newCase.InfractionDetails = GenerateInfraction(useChapters);
        newCase.InfractionLevel = CalculateInfractionLevel(newCase.InfractionDetails);

        CharacterID++;
        return newCase;
    }

    private CaseData GenerateStoryCaseInfo()
    {
        CaseData newCase = new CaseData();

        (string, string, int, bool) Character;
        CharacterPool.TryGetValue(CharacterID, out Character);

        newCase.SecondName = Character.Item1;
        newCase.FirstName = Character.Item2;
        newCase.Age = Character.Item3;

        if (Character.Item4)
        {
            newCase.Gender = "Male";
        }
        else
        {
            newCase.Gender = "Female";
        }

        //Instead of Generating Infractions Assign Based on Some Story Data?

        CharacterID++;
        return newCase;
    }

    private List<string> GenerateInfraction(bool chapters)
    {
        switch(storyManager.currentChapter)
        {
            case StoryManager.Chapter.One:
                maxPossibleInfractions = ch1MaxPossibleInfractions;
                break;
            case StoryManager.Chapter.Two:
                maxPossibleInfractions = ch2MaxPossibleInfractions;
                break;
            case StoryManager.Chapter.Three:
                maxPossibleInfractions = ch3MaxPossibleInfractions;
                break;
            case StoryManager.Chapter.Four:
                maxPossibleInfractions = ch4MaxPossibleInfractions;
                break;
        }

        int infractionNumber = Random.Range(1, maxPossibleInfractions);
        List<string> infractions = new List<string>();
        string infraction = "";

        if(chapters)
        {
            if(storyManager.currentChapter == StoryManager.Chapter.One)
            {
                for (int i = 0; i < infractionNumber; i++)
                {
                    infraction = PickInfraction(ch1Infractions1, ch1Infractions2, ch1Infractions3, ch1Infractions4, ch1Infractions5);

                    if (infractions.Contains(infraction))
                    {
                        break;
                    }
                    else
                    {
                        infractions.Add(infraction);
                    }
                }
            }
            else if(storyManager.currentChapter == StoryManager.Chapter.Two)
            {
                for (int i = 0; i < infractionNumber; i++)
                {
                    infraction = PickInfraction(ch2Infractions1, ch2Infractions2, ch2Infractions3, ch2Infractions4, ch2Infractions5);

                    if (infractions.Contains(infraction))
                    {
                        break;
                    }
                    else
                    {
                        infractions.Add(infraction);
                    }
                }
            }
            else if (storyManager.currentChapter == StoryManager.Chapter.Three)
            {
                for (int i = 0; i < infractionNumber; i++)
                {
                    infraction = PickInfraction(ch3Infractions1, ch3Infractions2, ch3Infractions3, ch3Infractions4, ch3Infractions5);

                    if (infractions.Contains(infraction))
                    {
                        break;
                    }
                    else
                    {
                        infractions.Add(infraction);
                    }
                }
            }
            else if (storyManager.currentChapter == StoryManager.Chapter.Four)
            {
                for (int i = 0; i < infractionNumber; i++)
                {
                    infraction = PickInfraction(ch4Infractions1, ch4Infractions2, ch4Infractions3, ch4Infractions4, ch4Infractions5);

                    if (infractions.Contains(infraction))
                    {
                        break;
                    }
                    else
                    {
                        infractions.Add(infraction);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < infractionNumber; i++)
            {
                infraction = PickInfraction(Infractions1, Infractions2, Infractions3, Infractions4, Infractions5);

                if (infractions.Contains(infraction))
                {
                    break;
                }
                else
                {
                    infractions.Add(infraction);
                }
            }
        }  

        return infractions;
    }

    private string PickInfraction(List<string> list1, List<string> list2, List<string> list3, List<string> list4, List<string> list5)
    {
        string infractionPicked = "";

        int chance = 0;
        chance = Random.Range(0, 100);

        if (chance > 60)
        {
            infractionPicked = Infractions1[Random.Range(0, list1.Count)];
        }
        else if (chance >= 45 && chance < 60)
        {
            infractionPicked = Infractions2[Random.Range(0, list2.Count)];
        }
        else if (chance >= 20 && chance < 45)
        {
            infractionPicked = Infractions3[Random.Range(0, list3.Count)];
        }
        else if (chance >= 5 && chance < 20)
        {
            infractionPicked = Infractions4[Random.Range(0, list4.Count)];
        }
        else if (chance < 5)
        {
            infractionPicked = Infractions5[Random.Range(0, list5.Count)];
        }

        return infractionPicked;
    }

    private int CalculateInfractionLevel(List<string> infraction)
    {
        int level = 0;

        if(ElementInListAIsInListB(infraction,Infractions1))
        {
            level = 1;
        }
        if (ElementInListAIsInListB(infraction, Infractions2))
        {
            level = 2;
        }
        if (ElementInListAIsInListB(infraction, Infractions3))
        {
            level = 3;
        }
        if (ElementInListAIsInListB(infraction, Infractions4))
        {
            level = 4;
        }
        if (ElementInListAIsInListB(infraction, Infractions5))
        {
            level = 5;
        }

        return level;
    }

    private bool ElementInListAIsInListB(List<string> A, List<string> B)
    {
        bool outcome = false;

        for (int i = 0; i < A.Count; i++)
        {
            for (int j = 0; j < B.Count; j++)
            {
                if(A[i].Equals(B[j]))
                {
                    outcome = true;
                    break;
                }
            }
        }

        return outcome;
    }
}

    
