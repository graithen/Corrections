using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkloadGenerator : MonoBehaviour
{
    public CharacterGeneration CharGen;
    public Dictionary<int, (string, string, int, bool)> CharacterPool;

    int CharacterID = 0;

    List<CaseStruct> DailyCaseList = new List<CaseStruct>();
    int CaseListLength = 10;

    // Start is called before the first frame update
    void Start()
    {
        CharGen.InitializeData();

        CharGen.NumberOfCharacters = 10000;
        CharacterPool = CharGen.GenerateCharacters();
    }

    public List<CaseStruct> GenerateWorkload(bool Story)
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

    CaseStruct GenerateCaseInfo()
    {
        CaseStruct newCase = new CaseStruct();

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

        return newCase;
    }

    CaseStruct GenerateStoryCaseInfo()
    {
        CaseStruct newCase = new CaseStruct();

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

        return newCase;
    }
}
