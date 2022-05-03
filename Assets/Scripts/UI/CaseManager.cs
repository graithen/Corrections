using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaseManager : MonoBehaviour
{
    private WorkloadGenerator workloadGenerator;

    [SerializeField]
    private GameObject caseButton;
    [SerializeField]
    private GameObject caseFilesContainer;

    [SerializeField]
    private List<CaseData> todaysCases;

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
        CreateDailyCases(isStoryCase()); //Call on New Day - when day system is made

        PopulateContainer(0);
    }

    public void PopulateContainer(int index)
    {
        //Update UI Elements within container here
        //picture = todaysCases[index];
        fName.text = todaysCases[index].FirstName;
        sName.text = todaysCases[index].SecondName;
        //DoB.text = todaysCases[index];
        //iDNumber.text = todaysCases[index];
        //occupation.text = todaysCases[index];
        infractionDetails.text = todaysCases[index].InfractionDetails;
        infractionNotes.text = todaysCases[index].InfractionNotes;

    }

    private void CreateDailyCases(bool story)
    {
        todaysCases = workloadGenerator.GenerateWorkload(story);

        //Spawn Case Buttons and Assign Cases to them
        for (int i = 0; i < todaysCases.Count; i++)
        {
            GameObject button = Instantiate(caseButton, transform);
            button.GetComponent<CaseButton>().myCaseNumber = i;
            //Remember to Assign Populate Function to Case Button using Apt Case File
        }
    }

    private bool isStoryCase()
    {
        //Logic for choosing story or not?
        return false;
    }
}
