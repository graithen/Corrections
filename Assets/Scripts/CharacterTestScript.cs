using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterTestScript : MonoBehaviour
{
    public CharacterGeneration CharGen;
    public Dictionary<int, (string, string, int, bool)> CharacterPool;

    public int CharacterID = 0;
    public string SecondName;
    public string FirstName;
    public int Age;
    public string Gender;

    public TextMeshProUGUI FNameText;
    public TextMeshProUGUI SNameText;
    public TextMeshProUGUI AgeText;
    public TextMeshProUGUI GenderText;

    // Start is called before the first frame update
    void Start()
    {
        CharGen.InitializeData();

        CharGen.NumberOfCharacters = 10000;
        CharacterPool = CharGen.GenerateCharacters();
        GetCharacterInfo();
        BuildUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PreviousCharacter()
    {
        if (CharacterID > 0)
            CharacterID--;

        GetCharacterInfo();
        BuildUI();
    }

    public void NextCharacter()
    {
        if(CharacterID < CharacterPool.Count)
            CharacterID++;

        GetCharacterInfo();
        BuildUI();
    }

    public void GetCharacterByID()
    {
        
    }

    void GetCharacterInfo()
    {
        (string, string, int, bool) Character;
        CharacterPool.TryGetValue(CharacterID, out Character);

        FirstName = Character.Item1;
        SecondName = Character.Item2;
        Age = Character.Item3;

        if (Character.Item4)
        {
            Gender = "Male";
        }

        else
            Gender = "Female";
    }

    void BuildUI()
    {
        SNameText.text = SecondName;
        FNameText.text = FirstName;
        AgeText.text = Age.ToString();
        GenderText.text = Gender;
    }
}
