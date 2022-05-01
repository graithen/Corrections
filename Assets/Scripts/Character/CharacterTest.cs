using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterTest : MonoBehaviour
{
    [SerializeField]
    private CharacterGeneration CharGen;

    public Dictionary<int, (string, string, int, bool)> CharacterPool;

    private int CharacterID = 0;
    private string SecondName;
    private string FirstName;
    private int Age;
    private string Gender;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI FNameText;
    [SerializeField]
    private TextMeshProUGUI SNameText;
    [SerializeField]
    private TextMeshProUGUI AgeText;
    [SerializeField]
    private TextMeshProUGUI GenderText;

    void Start()
    {
        CharGen.InitializeData();

        CharGen.NumberOfCharacters = 10000;
        CharacterPool = CharGen.GenerateCharacters();

        DisplayCharacters();
    }

    public void PreviousCharacter()
    {
        if (CharacterID > 0)
            CharacterID--;

        DisplayCharacters();
    }

    public void NextCharacter()
    {
        if(CharacterID < CharacterPool.Count)
            CharacterID++;

        DisplayCharacters();
    }

    public void GetCharacterByID()
    {
        
    }

    private void GetCharacterInfo()
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
        {
            Gender = "Female";
        }  
    }

    private void BuildUI()
    {
        SNameText.text = SecondName;
        FNameText.text = FirstName;
        AgeText.text = Age.ToString();
        GenderText.text = Gender;
    }

    private void DisplayCharacters()
    {
        GetCharacterInfo();
        BuildUI();
    }
}
