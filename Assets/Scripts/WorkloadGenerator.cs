using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkloadGenerator : MonoBehaviour
{
    [SerializeField]
    private CharacterGeneration CharGen;
    public Dictionary<int, (string, string, int, bool)> CharacterPool;

    private int CharacterID = 0;

    private List<CaseData> DailyCaseList = new List<CaseData>();
    private int CaseListLength = 10;

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

        newCase.FirstName = Character.Item1;
        newCase.SecondName = Character.Item2;
        newCase.Age = Character.Item3;

        if (Character.Item4)
        {
            newCase.Gender = "Male";
        }

        else
            newCase.Gender = "Female";

        CharacterID++;
        return newCase;
    }

    private CaseData GenerateStoryCaseInfo()
    {
        CaseData newCase = new CaseData();

        (string, string, int, bool) Character;
        CharacterPool.TryGetValue(CharacterID, out Character);

        newCase.FirstName = Character.Item1;
        newCase.SecondName = Character.Item2;
        newCase.Age = Character.Item3;

        if (Character.Item4)
        {
            newCase.Gender = "Male";
        }

        else
            newCase.Gender = "Female";

        CharacterID++;
        return newCase;
    }
}
