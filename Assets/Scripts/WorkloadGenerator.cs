using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkloadGenerator : MonoBehaviour
{
    [SerializeField]
    private CharacterGeneration CharGen;
    public Dictionary<int, (string, string, int, bool)> CharacterPool;

    public List<string> Infractions1, Infractions2, Infractions3, Infractions4, Infractions5;

    private int CharacterID = 0;

    private List<CaseData> DailyCaseList = new List<CaseData>();
    [SerializeField]
    private int CaseListLength = 10;

    [SerializeField]
    private int maxPossibleInfractions = 10;

    // Start is called before the first frame update
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

        newCase.InfractionDetails = GenerateInfraction();
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

        CharacterID++;
        return newCase;
    }

    private List<string> GenerateInfraction()
    {
        int infractionNumber = Random.Range(1, maxPossibleInfractions);
        List<string> infractions = new List<string>();
        string infraction = "";

        for (int i = 0; i < infractionNumber; i++)
        {
            int chance = 0;
            chance = Random.Range(0, 100);

            if (chance > 60)
            {
                infraction = Infractions1[Random.Range(0, Infractions1.Count)];
            }
            else if (chance >= 50 && chance < 60)
            {
                infraction = Infractions2[Random.Range(0, Infractions2.Count)];
            }
            else if (chance >= 25 && chance < 50)
            {
                infraction = Infractions3[Random.Range(0, Infractions3.Count)];
            }
            else if (chance >= 10 && chance < 25)
            {
                infraction = Infractions4[Random.Range(0, Infractions4.Count)];
            }
            else if (chance < 10)
            {
                infraction = Infractions5[Random.Range(0, Infractions5.Count)];
            }

            if (infractions.Contains(infraction))
            {
                break;
            }
            else
            {
                infractions.Add(infraction);
            }
        }

        return infractions;
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

    
