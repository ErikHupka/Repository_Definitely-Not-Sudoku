using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class IconFunctionalities : MonoBehaviour
{
    bool notesOn = false;
    [SerializeField] Image notesButton;

    [SerializeField] TextMeshProUGUI hintCountText;
    int hintCount = 3;

    ButtonAddNumber buttonAddNumber;
    [SerializeField] Canvas pauseMenuCanvas;
    [SerializeField] Canvas endGameCanvas;
    [SerializeField] Canvas winScreenCanvas;

    Timer timer;

    [SerializeField] TextMeshProUGUI difficultyText;

    //Components
    GenerateGridAlgorhytm generateGridAlgorhytm;
    MistakeCounter mistakeCounter;

    private void Awake()
    {
        buttonAddNumber = FindObjectOfType<ButtonAddNumber>();
        timer = FindObjectOfType<Timer>();
        generateGridAlgorhytm = FindObjectOfType<GenerateGridAlgorhytm>();
        mistakeCounter = FindObjectOfType<MistakeCounter>();
    }

    public void EraseButton()
    {
        buttonAddNumber.EraseNumber();
    }

    public void UndoButton()
    {
        //I have realized, that it is really not necessary, as you have only one unique solution... Erase button also, but I already finished it
    }

    public void NotesButton()
    {
        notesOn = !notesOn;

        if (notesOn)
        {
            notesButton.color = Color.yellow;
        }
        else
        {
            notesButton.color = new Color(1, 1, 1);
        }
    }

    public bool GetNotesActive()
    {
        return notesOn;
    }

    public void HintButton()
    {
        if (hintCount > 0)
        {
            Cell[,] grid = generateGridAlgorhytm.GetGrid();

            List<Cell> remainingCells = new();

            foreach (Cell cell in grid)
            {
                if (cell.value == 0)
                {
                    remainingCells.Add(cell);
                }
            }

            if (remainingCells.Count == 0) { return; }

            int chosenCellIndex = UnityEngine.Random.Range(0, remainingCells.Count - 1);
            Cell chosenCell = remainingCells[chosenCellIndex];

            chosenCell.value = chosenCell.hiddenCorrectValue;
            chosenCell.cell.GetComponentInChildren<CellNumber>().GetComponent<TextMeshProUGUI>().text = chosenCell.value.ToString();
            chosenCell.isEditable = false;

            hintCount--;
            hintCountText.text = hintCount.ToString();
            chosenCell.cell.GetComponentInChildren<NotesGrid>().gameObject.SetActive(false);
        }
    }

    private void ResetHintCount()
    {
        hintCount = 3;
    }

    public void PauseButton()
    {
        pauseMenuCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        pauseMenuCanvas.enabled = false;
        Time.timeScale = 1;
    }

    public void NewGameButton()
    {
        try
        {
            pauseMenuCanvas.enabled = false;
            endGameCanvas.enabled = false;
            winScreenCanvas.enabled = false;
            Time.timeScale = 1;
            timer.SetNewGameTime();
            difficultyText.text = "Difficulty: " + DifficultyName();
            ResetHintCount();
            mistakeCounter.ResetMistakeCount();

            Cell[,] grid = generateGridAlgorhytm.GetGrid();
            foreach (Cell cell in grid)
            {
                cell.cell.GetComponent<Image>().color = Color.white;
                cell.cell.GetComponentInChildren<CellNumber>().GetComponent<TextMeshProUGUI>().color = Color.black;
            }
        }
        catch (Exception)
        {
            Debug.Log("An unexpected issue occured");
        }

    }

    public string DifficultyName()
    {
        int currentDifficulty = generateGridAlgorhytm.GetDifficulty();

        return currentDifficulty switch
        {
            40 => "EASY",
            45 => "MEDIUM",
            50 => "HARD",
            55 => "EXPERT",
            60 => "INSANE",
            _ => "AN ERROR OCCURED",
        };
    }
}
