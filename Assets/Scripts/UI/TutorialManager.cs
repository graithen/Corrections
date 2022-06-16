using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("Custom Components")]
    [SerializeField]
    private DateTimeSystem dateTimeSystem;

    [Header("UI")]
    [SerializeField]
    private GameObject tutorialCanvas;
    [SerializeField]
    private List<GameObject> instructions;
    [SerializeField]
    private List<Vector2> maskPosition;
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject finishButton;
    [SerializeField]
    private RectTransform maskHole;

    private int currInstructionIndex;

    public void EnableTutorialCanvas(bool enable)
    {
        tutorialCanvas.SetActive(enable);
        dateTimeSystem.Pause();
        if (enable)
        {
            nextButton.SetActive(true);
            currInstructionIndex = 0;
            EnableInstruction(currInstructionIndex);
        }
    }

    public void NextInstruction()
    {
        currInstructionIndex++;
        if (currInstructionIndex == instructions.Count - 1)
        {
            //currInstructionIndex = 0;
            nextButton.SetActive(false);
            finishButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            finishButton.SetActive(false);
        }

        EnableInstruction(currInstructionIndex);
    }

    private void EnableInstruction(int currIndex)
    {
        for (int i = 0; i < instructions.Count; i++)
        {
            if (i == currInstructionIndex)
            {
                instructions[i].SetActive(true);
                maskHole.anchoredPosition = maskPosition[i];
            }
            else
            {
                instructions[i].SetActive(false);
            }
        }
    }
}
