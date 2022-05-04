using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaseManager : MonoBehaviour
{
    private WorkloadGenerator workloadGenerator;

    private int elapsedDays = 0;

    [SerializeField]
    private GameObject caseButton;
    [SerializeField]
    private GameObject caseFilesContainer;

    [SerializeField]
    private List<CaseData> todaysCases;
    private int currCaseIndex = 0;

    [Header ("Contents UI")]
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

    private void Start()
    {
        workloadGenerator = FindObjectOfType<WorkloadGenerator>();
        CreateDailyCases(); //Call on New Day - when day system is made
    }

    public void PopulateContainer(int index)
    {
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
        if(elapsedDays %3 == 0)
        {
            story = true;
        }

        todaysCases = workloadGenerator.GenerateWorkload(story);

        //Spawn Case Buttons and Assign Cases to them
        for (int i = 0; i < todaysCases.Count; i++)
        {
            GameObject button = Instantiate(caseButton, transform);
            button.GetComponent<CaseButton>().myCaseNumber = i;
            //Remember to Assign Populate Function to Case Button using Apt Case File
        }

        PopulateContainer(currCaseIndex);
    }

    public void Submit()
    {
        todaysCases.RemoveAt(currCaseIndex);
        Destroy(transform.GetChild(currCaseIndex).gameObject);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<CaseButton>().myCaseNumber = i-1;
        }

        currCaseIndex = 0;
        PopulateContainer(currCaseIndex);
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
}
