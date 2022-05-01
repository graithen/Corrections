using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject caseButton;
    [SerializeField]
    private GameObject caseFilesContainer;

    private int caseLimit = 50;

    private void Start()
    {
        
    }

    private void PopulateContainer(/*Param for Case File*/)
    {
        //Update UI Elements within container here
    }

    private void AssignCase()
    {
        //Spawn Case Buttons and Assign Cases to them
        for (int i = 0; i < caseLimit; i++)
        {
            GameObject button = Instantiate(caseButton, transform);
            //button.assignCaseFileHere
            //Remember to Assign Populate Function to Case Button using Apt Case File
        }
    }
}
