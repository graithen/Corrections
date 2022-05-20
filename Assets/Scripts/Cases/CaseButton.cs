using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaseButton : MonoBehaviour
{
    private CaseManager caseManager;

    public int myCaseNumber;

    private void Start()
    {
        caseManager = FindObjectOfType<CaseManager>();
        GetComponent<Button>().onClick.AddListener(() => caseManager.PopulateContainer(myCaseNumber));
    }
}
