using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int currInstructionIndex;

    public void EnableTutorialCanvas(bool enable)
    {
        tutorialCanvas.SetActive(enable);
        dateTimeSystem.Pause();
        if (enable)
        {
            currInstructionIndex = 0;
            EnableInstruction(currInstructionIndex);      
        }
    }

    public void NextInstruction()
    {
        currInstructionIndex++;
        if (currInstructionIndex >= instructions.Count - 1)
        {
            currInstructionIndex = 0;
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
            }
            else
            {
                instructions[i].SetActive(false);
            }
        }
    }
}
