using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterGeneration
{
    [Header("Resources")]
    public TextAsset MaleNames;
    public TextAsset FemaleNames;
    public TextAsset SecondNames;
    public TextAsset Occupations;

    [Header("Generation Settings")]
    private int numberOfCharacters = 100;
    public int NumberOfCharacters { get { return numberOfCharacters; } set { numberOfCharacters = value; } }

    Dictionary<int, (string, string, int, bool, string)> Characters = new Dictionary<int, (string, string, int, bool, string)>(); //Dictionary with tuples (Surname, First Name, Age, Is Male?)

    //Loaded Data
    public string[] MaleNameList, FemaleNameList, SecondNameList, OccupationsList;

    public void InitializeData()
    {
        string text = "";

        text = MaleNames.ToString();
        MaleNameList = text.Split('\n');

        text = FemaleNames.ToString();
        FemaleNameList = text.Split('\n');

        text = SecondNames.ToString();
        SecondNameList = text.Split('\n');

        text = Occupations.ToString();
        OccupationsList = text.Split('\n');
    }

    public Dictionary<int, (string, string, int, bool, string)> GenerateCharacters()
    {
        for (int i = 0; i < NumberOfCharacters; i++)
        {
            string firstName = "";
            string secondName = "";
            int age = 28;
            bool isMale = true;
            string occupation = "";

            //Roll Gender - 30% chance is female
            int rand = Random.Range(0, 100);
            if(rand < 30)
            {
                isMale = false;
            }

            if(isMale)
            {
                firstName = MaleNameList[Random.Range(0, MaleNameList.Length - 1)];
            }

            if(!isMale)
            {
                firstName = FemaleNameList[Random.Range(0, FemaleNameList.Length - 1)];
            }

            secondName = SecondNameList[Random.Range(0, SecondNameList.Length - 1)];
            age = Random.Range(14, 65);
            
            occupation = OccupationsList[Random.Range(0, OccupationsList.Length - 1)];
            
            Characters.Add(i, (secondName, firstName, age, isMale, occupation));
        }

        return Characters;
    }    
}
