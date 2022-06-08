using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaseManager : MonoBehaviour
{
    public enum Sentences
    {
        Tier1 = 0,
        Tier2 = 1,
        Tier3 = 2,
        Tier4 = 3,
        Tier5 = 4,
    }

    public Sentences currSentence;
    private List<string> sentences;

    [SerializeField]
    private List<string> chapterOneSentences;
    [SerializeField]
    private List<string> chapterTwoSentences;
    [SerializeField]
    private List<string> chapterThreeSentences;
    [SerializeField]
    private List<string> chapterFourSentences;

    private WorkloadGenerator workloadGenerator;
    private GameplayTracking gameplayTracking;
    private StoryManager storyManager;

    private int elapsedDays = 0;

    [SerializeField]
    private GameObject caseButton;

    [SerializeField]
    private List<CaseData> todaysCases;
    private int currCaseIndex = 0;

    [Header("Contents UI")]
    [SerializeField]
    private GameObject contentScreen;
    [SerializeField]
    private Image picture;
    [SerializeField]
    private TextMeshProUGUI sName;
    [SerializeField]
    private TextMeshProUGUI fName;
    [SerializeField]
    private TextMeshProUGUI DoB;
    [SerializeField]
    private TextMeshProUGUI iDNumber;
    [SerializeField]
    private TextMeshProUGUI occupation;
    [SerializeField]
    private TextMeshProUGUI infractionDetails;
    [SerializeField]
    private TextMeshProUGUI infractionNotes;

    [SerializeField]
    private TextMeshProUGUI noCasesRightNowText;

    [SerializeField]
    private Button endDayButton;

    [SerializeField]
    private TMP_Dropdown sentenceDropdown;
    [SerializeField]
    private Scrollbar verticalScroll;

    private void Start()
    {
        workloadGenerator = FindObjectOfType<WorkloadGenerator>();
        gameplayTracking = FindObjectOfType<GameplayTracking>();
        storyManager = FindObjectOfType<StoryManager>();
        CreateDailyCases(); //Call on New Day - when day system is made
    }

    public void PopulateContainer(int index)
    {
        noCasesRightNowText.gameObject.SetActive(false);
        endDayButton.gameObject.SetActive(false);

        currCaseIndex = index;
        //Update UI Elements within container here
        //picture = todaysCases[index];
        fName.text = todaysCases[index].FirstName;
        sName.text = todaysCases[index].SecondName;
        DoB.text = todaysCases[index].Age.ToString();
        //iDNumber.text = todaysCases[index];
        //occupation.text = todaysCases[index];
        infractionDetails.text = ListToString(todaysCases[index].InfractionDetails);
        infractionNotes.text = todaysCases[index].InfractionNotes;

    }

    public void CreateDailyCases()
    {
        currCaseIndex = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        bool story = false;
        elapsedDays++;
        //if(elapsedDays %3 == 0)
        //{
            //story = true;
        //}

        todaysCases = workloadGenerator.GenerateWorkload(story);

        //Spawn Case Buttons and Assign Cases to them
        for (int i = 0; i < todaysCases.Count; i++)
        {
            GameObject button = Instantiate(caseButton, transform);
            button.GetComponent<CaseButton>().myCaseNumber = i;
            //Remember to Assign Populate Function to Case Button using Apt Case File
        }

        contentScreen.SetActive(true);
        PopulateContainer(currCaseIndex);
        PopulateSentenceDropdown();
    }

    public void Submit()
    {
        CheckSentenceDropdown();

        todaysCases.RemoveAt(currCaseIndex);
        Destroy(transform.GetChild(currCaseIndex).gameObject);
      
        if (todaysCases.Count > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<CaseButton>().myCaseNumber = i - 1;
            }

            currCaseIndex = 0;

            PopulateContainer(currCaseIndex);
        }
        else
        {
            contentScreen.SetActive(false);
            noCasesRightNowText.gameObject.SetActive(true);
            endDayButton.gameObject.SetActive(true);
        }

        verticalScroll.value = 1;
    }

    private void PopulateSentenceDropdown()
    {
        sentenceDropdown.options.Clear();

        if(storyManager.currentChapter == StoryManager.Chapter.One)
        {
            sentences = chapterOneSentences;
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Two)
        {
            sentences = chapterTwoSentences;
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Three)
        {
            sentences = chapterThreeSentences;
        }
        else if (storyManager.currentChapter == StoryManager.Chapter.Four)
        {
            sentences = chapterFourSentences;
        }

        sentenceDropdown.AddOptions(sentences);      
    }

    private void CheckSentenceDropdown()
    {
        string sentence = sentenceDropdown.options[sentenceDropdown.value].text;

        for (int i = 0; i < sentences.Count; i++)
        {
            if (sentence == sentences[i])
            {
                currSentence = intToSentence(i);
                gameplayTracking.ProcessVictim(FullNameToString(todaysCases[currCaseIndex]),EvaluateSentence(currSentence),todaysCases[currCaseIndex].InfractionLevel);
                break;
            }
        }
    }

    private int EvaluateSentence(Sentences sentence)
    {
        int evaluation = 0;

        switch(sentence)
        {
            case Sentences.Tier1:
                evaluation = 1;
                break;
            case Sentences.Tier2:
                evaluation = 2;
                break;
            case Sentences.Tier3:
                evaluation = 3;
                break;
            case Sentences.Tier4:
                evaluation = 4;
                break;
            case Sentences.Tier5:
                evaluation = 5;
                break;
        }

        return evaluation;
    }

    private string FullNameToString(CaseData caseData)
    {
        string fullName = caseData.FirstName + " " + caseData.SecondName;
        return fullName;
    }

    private string ListToString(List<string> toConvert)
    {
        string converted = "";

        for (int i = 0; i < toConvert.Count; i++)
        {
            converted += toConvert[i] + "\n";
        }

        return converted;
    }

    private Sentences intToSentence(int index)
    {
        Sentences thisSentence = Sentences.Tier1;

        switch(index)
        {
            case 0:
                thisSentence = Sentences.Tier1;
                break;
            case 1:
                thisSentence = Sentences.Tier2;
                break;
            case 2:
                thisSentence = Sentences.Tier3;
                break;
            case 3:
                thisSentence = Sentences.Tier4;
                break;
            case 4:
                thisSentence = Sentences.Tier5;
                break;
        }

        return thisSentence;
    }
}
