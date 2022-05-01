using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI Containers")]
    [SerializeField]
    private GameObject caseFileCountainer;
    [SerializeField]
    private GameObject newsContainer;
    [SerializeField]
    private GameObject performanceReviewContainer;
    [SerializeField]
    private GameObject eMailContainer;

    [Header("Menu Buttons")]
    [SerializeField]
    private Button caseFileButton;
    [SerializeField]
    private Button newsButton;
    [SerializeField]
    private Button performanceReviewButton;
    [SerializeField]
    private Button eMailButton;

    private void Start()
    {
        AssignMenuButtons();
        OpenCaseFileContainer();
    }

    private void OpenCaseFileContainer()
    {
        CloseAllContainers();
        caseFileCountainer.SetActive(true);
    }

    private void OpenNewsContainer()
    {
        CloseAllContainers();
        newsContainer.SetActive(true);
    }

    public void OpenPerformanceReviewContainer()
    {
        CloseAllContainers();
        performanceReviewContainer.SetActive(true);
    }

    public void OpenEMailContainer()
    {
        CloseAllContainers();
        eMailContainer.SetActive(true);
    }

    private void CloseAllContainers()
    {
        caseFileCountainer.SetActive(false);
        newsContainer.SetActive(false);
        performanceReviewContainer.SetActive(false);
        eMailContainer.SetActive(false);
    }

    private void AssignMenuButtons()
    {
        caseFileButton.onClick.AddListener(() => OpenCaseFileContainer());
        newsButton.onClick.AddListener(() => OpenNewsContainer());
        performanceReviewButton.onClick.AddListener(() => OpenPerformanceReviewContainer());
        eMailButton.onClick.AddListener(() => OpenEMailContainer());
    }

}
