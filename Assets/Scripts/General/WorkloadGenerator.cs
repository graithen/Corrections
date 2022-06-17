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
    public Dictionary<int, (string, string, int, bool, string)> CharacterPool;

    public List<string> Infractions1, Infractions2, Infractions3, Infractions4, Infractions5;

    public List<string> ch1Infractions1, ch1Infractions2, ch1Infractions3, ch1Infractions4, ch1Infractions5;
    public List<string> ch2Infractions1, ch2Infractions2, ch2Infractions3, ch2Infractions4, ch2Infractions5;
    public List<string> ch3Infractions1, ch3Infractions2, ch3Infractions3, ch3Infractions4, ch3Infractions5;
    public List<string> ch4Infractions1, ch4Infractions2, ch4Infractions3, ch4Infractions4, ch4Infractions5;

    private int CharacterID = 0;

    private List<CaseData> DailyCaseList = new List<CaseData>();

    [Header("Daily Case Numbers")]
    [SerializeField]
    private int ch1CaseCount = 10;
    [SerializeField]
    private int ch2CaseCount = 15;
    [SerializeField]
    private int ch3CaseCount = 25;
    [SerializeField]
    private int ch4CaseCount = 35;
    private int CaseListLength;

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
        DetermineCaseListLength();

        for (int i = 0; i < CaseListLength; i++)
        {
            DailyCaseList.Add(GenerateCaseInfo());
        }

        if(Story)
        {
            DailyCaseList.Add(GenerateStoryCaseInfo());
        }

        ChangeMutator();

        return DailyCaseList;
    }

    private void DetermineCaseListLength()
    {
        if (storyManager.currentChapter == StoryManager.Chapter.One)
        {
            CaseListLength = ch1CaseCount;
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Two)
        {
            CaseListLength = ch2CaseCount;
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Three)
        {
            CaseListLength = ch3CaseCount;
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Four)
        {
            CaseListLength = ch4CaseCount;
        }
    }

    private CaseData GenerateCaseInfo()
    {
        CaseData newCase = new CaseData();

        (string, string, int, bool, string) Character;
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
        newCase.Occupation = Character.Item5;

        newCase.InfractionDetails = GenerateInfraction(useChapters);
        newCase.InfractionLevel = CalculateInfractionLevel(newCase.InfractionDetails);

        CharacterID++;
        return newCase;
    }

    private CaseData GenerateStoryCaseInfo()
    {
        CaseData newCase = new CaseData();

        (string, string, int, bool, string) Character;
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

    private void ChangeMutator()
    {

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

        if (storyManager.currentChapter == StoryManager.Chapter.One)
        {
            if (chance > 60)
            {
                infractionPicked = list1[Random.Range(0, list1.Count)];
            }
            else if (chance >= 45 && chance < 60)
            {
                infractionPicked = list2[Random.Range(0, list2.Count)];
            }
            else if (chance >= 20 && chance < 45)
            {
                infractionPicked = list3[Random.Range(0, list3.Count)];
            }
            else if (chance >= 5 && chance < 20)
            {
                infractionPicked = list4[Random.Range(0, list4.Count)];
            }
            else if (chance < 5)
            {
                infractionPicked = list5[Random.Range(0, list5.Count)];
            }
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Two)
        {
            if (chance > 70)
            {
                infractionPicked = list1[Random.Range(0, list1.Count)];
            }
            else if (chance >= 45 && chance < 70)
            {
                infractionPicked = list2[Random.Range(0, list2.Count)];
            }
            else if (chance >= 20 && chance < 45)
            {
                infractionPicked = list3[Random.Range(0, list3.Count)];
            }
            else if (chance >= 5 && chance < 20)
            {
                infractionPicked = list4[Random.Range(0, list4.Count)];
            }
            else if (chance < 5)
            {
                infractionPicked = list5[Random.Range(0, list5.Count)];
            }
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Three)
        {
            if (chance > 80)
            {
                infractionPicked = list1[Random.Range(0, list1.Count)];
            }
            else if (chance >= 55 && chance < 80)
            {
                infractionPicked = list2[Random.Range(0, list2.Count)];
            }
            else if (chance >= 25 && chance < 55)
            {
                infractionPicked = list3[Random.Range(0, list3.Count)];
            }
            else if (chance >= 10 && chance < 25)
            {
                infractionPicked = list4[Random.Range(0, list4.Count)];
            }
            else if (chance < 10)
            {
                infractionPicked = list5[Random.Range(0, list5.Count)];
            }
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Four)
        {
            if (chance > 85)
            {
                infractionPicked = list1[Random.Range(0, list1.Count)];
            }
            else if (chance >= 55 && chance < 85)
            {
                infractionPicked = list2[Random.Range(0, list2.Count)];
            }
            else if (chance >= 30 && chance < 55)
            {
                infractionPicked = list3[Random.Range(0, list3.Count)];
            }
            else if (chance >= 15 && chance < 30)
            {
                infractionPicked = list4[Random.Range(0, list4.Count)];
            }
            else if (chance < 15)
            {
                infractionPicked = list5[Random.Range(0, list5.Count)];
            }
        }

        return infractionPicked;
    }


    private int CalculateInfractionLevel(List<string> infraction)
    {
        int level = 0;

        if (useChapters)
        {
            if (storyManager.currentChapter == StoryManager.Chapter.One)
            {
                level = ProcessLevel(infraction, ch1Infractions1, ch1Infractions2, ch1Infractions3, ch1Infractions4, ch1Infractions5);
            }
            else if(storyManager.currentChapter == StoryManager.Chapter.Two)
            {
                level = ProcessLevel(infraction, ch2Infractions1, ch2Infractions2, ch2Infractions3, ch2Infractions4, ch2Infractions5);
            }
            else if (storyManager.currentChapter == StoryManager.Chapter.Three)
            {
                level = ProcessLevel(infraction, ch3Infractions1, ch3Infractions2, ch3Infractions3, ch3Infractions4, ch3Infractions5);
            }
            else if (storyManager.currentChapter == StoryManager.Chapter.Four)
            {
                level = ProcessLevel(infraction, ch4Infractions1, ch4Infractions2, ch4Infractions3, ch4Infractions4, ch4Infractions5);
            }
        }
        else
        {
            level = ProcessLevel(infraction, Infractions1, Infractions2, Infractions3, Infractions4, Infractions5);
        }
    
        return level;
    }

    private int ProcessLevel(List<string> infraction,List<string> list1, List<string> list2, List<string> list3, List<string> list4, List<string> list5)
    {
        int level = 0;

        if (ElementInListAIsInListB(infraction, Infractions1))
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

    
