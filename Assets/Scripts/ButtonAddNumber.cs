using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonAddNumber : MonoBehaviour
{
    [SerializeField] int number;

    Cell[,] grid;
    Cell currentCell;

    Color conflictNumberColor = new(0.811f, 0.218f, 0.218f);
    Color relatedNumberColor = new(0.478f, 0.631f, 0.725f);
    Color relatedRowsCollsColor = new(0.768f, 0.796f, 0.839f);

    //Win Screen
    [SerializeField] Canvas winScreenCanvas;
    [SerializeField] TextMeshProUGUI totalTime;
    [SerializeField] TextMeshProUGUI difficultyText;

    //Components
    PlayerInput playerInput;
    GenerateGridAlgorhytm generateGridAlgorhytm;
    IconFunctionalities iconFunctionalities;
    MistakeCounter mistakeCounter;

    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        generateGridAlgorhytm = FindObjectOfType<GenerateGridAlgorhytm>();
        iconFunctionalities = FindObjectOfType<IconFunctionalities>();
        mistakeCounter = FindObjectOfType<MistakeCounter>();
    }

    public void OnNumberButtonPress()
    {
        grid = generateGridAlgorhytm.GetGrid();
        int currentRow = playerInput.GetCurrentRowSelected();
        int currentColl = playerInput.GetCurrentCollSelected();

        currentCell = grid[currentRow, currentColl];

        if (!currentCell.isEditable) { return; }


        if (iconFunctionalities.GetNotesActive())
        {
            currentCell.cell.GetComponentInChildren<NotesGrid>().WriteNotes(currentCell, number);
        }

        else
        {
            if (number == currentCell.hiddenCorrectValue)
            {
                currentCell.cell.GetComponentInChildren<NotesGrid>().gameObject.SetActive(false);
                currentCell.isEditable = false;
            }

            currentCell.value = number;
            currentCell.cell.GetComponentInChildren<CellNumber>().GetComponent<TextMeshProUGUI>().text = currentCell.value.ToString();

            int emptyCellsLeft = 0;
            foreach (Cell cell in grid)
            {
                if (cell.value == 0)
                {
                    emptyCellsLeft++;
                }
            }

            if (emptyCellsLeft == 0) //win sequence
            {
                winScreenCanvas.enabled = true;
                totalTime.text = "Total time: " + FindObjectOfType<Timer>().GetGameTime();
                difficultyText.text = "Difficulty: " + iconFunctionalities.DifficultyName();
            }

            CheckCorrectCurrentValue(currentCell);

            foreach (Cell cell in grid)
            {
                HighlightSameRowsAndColls(currentRow, currentColl, cell);
                HighlightSameSquare(cell);
            }
        }
    }

    private void CheckCorrectCurrentValue(Cell currentCell)
    {
        if (currentCell.value != currentCell.hiddenCorrectValue && currentCell.value != 0)
        {
            currentCell.cell.GetComponentInChildren<CellNumber>().GetComponent<TextMeshProUGUI>().color = Color.red;
            mistakeCounter.IncreaseMistakeCount();
        }
        else if (currentCell.value != 0)
        {
            grid = generateGridAlgorhytm.GetGrid();

            currentCell.cell.GetComponentInChildren<CellNumber>().GetComponent<TextMeshProUGUI>().color = Color.black;

            foreach (Cell cell in grid)
            {
                if (cell.cell.GetComponent<Image>().color == conflictNumberColor)
                {
                    cell.cell.GetComponent<Image>().color = relatedRowsCollsColor;
                }
                if (cell.value == currentCell.value)
                {
                    cell.cell.GetComponent<Image>().color = relatedNumberColor;
                }
            }
        }
        else
        {
            currentCell.cell.GetComponentInChildren<CellNumber>().GetComponent<TextMeshProUGUI>().color = Color.black;
        }
    }

    private void HighlightSameRowsAndColls(int row, int coll, Cell cell)
    {
        if (cell.value == 0) { return;  }

        if ((cell.row == row || cell.coll == coll) && cell.value == currentCell.value)
        {
            if (cell.row == currentCell.row && cell.coll == currentCell.coll) { return; }

            cell.cell.GetComponent<Image>().color = conflictNumberColor;
        }
    }

    private void HighlightSameSquare(Cell cell)
    {
        if (cell.value != currentCell.value || cell.value == 0) { return; }
        if (cell.row == currentCell.row && cell.coll == currentCell.coll) { return; }

        if (playerInput.GetCurrentRowSelected() < 3 && playerInput.GetCurrentRowSelected() >= 0 && cell.row < 3 && cell.row >= 0)
        {
            if (playerInput.GetCurrentCollSelected() < 3 && playerInput.GetCurrentCollSelected() >= 0 && cell.coll < 3 && cell.coll >= 0)
            {
                cell.cell.GetComponent<Image>().color = conflictNumberColor;
            }
            else if (playerInput.GetCurrentCollSelected() < 6 && playerInput.GetCurrentCollSelected() >= 3 && cell.coll < 6 && cell.coll >= 3)
            {
                cell.cell.GetComponent<Image>().color = conflictNumberColor;
            }
            else if (playerInput.GetCurrentCollSelected() < 9 && playerInput.GetCurrentCollSelected() >= 6 && cell.coll < 9 && cell.coll >= 6)
            {
                cell.cell.GetComponent<Image>().color = conflictNumberColor;
            }
        }

        else if (playerInput.GetCurrentRowSelected() < 6 && playerInput.GetCurrentRowSelected() >= 3 && cell.row < 6 && cell.row >= 3)
        {
            if (playerInput.GetCurrentCollSelected() < 3 && playerInput.GetCurrentCollSelected() >= 0 && cell.coll < 3 && cell.coll >= 0)
            {
                cell.cell.GetComponent<Image>().color = conflictNumberColor;
            }
            else if (playerInput.GetCurrentCollSelected() < 6 && playerInput.GetCurrentCollSelected() >= 3 && cell.coll < 6 && cell.coll >= 3)
            {
                cell.cell.GetComponent<Image>().color = conflictNumberColor;
            }
            else if (playerInput.GetCurrentCollSelected() < 9 && playerInput.GetCurrentCollSelected() >= 6 && cell.coll < 9 && cell.coll >= 6)
            {
                cell.cell.GetComponent<Image>().color = conflictNumberColor;
            }
        }

        else if (playerInput.GetCurrentRowSelected() < 9 && playerInput.GetCurrentRowSelected() >= 6 && cell.row < 9 && cell.row >= 6)
        {
            if (playerInput.GetCurrentCollSelected() < 3 && playerInput.GetCurrentCollSelected() >= 0 && cell.coll < 3 && cell.coll >= 0)
            {
                cell.cell.GetComponent<Image>().color = conflictNumberColor;
            }
            else if (playerInput.GetCurrentCollSelected() < 6 && playerInput.GetCurrentCollSelected() >= 3 && cell.coll < 6 && cell.coll >= 3)
            {
                cell.cell.GetComponent<Image>().color = conflictNumberColor;
            }
            else if (playerInput.GetCurrentCollSelected() < 9 && playerInput.GetCurrentCollSelected() >= 6 && cell.coll < 9 && cell.coll >= 6)
            {
                cell.cell.GetComponent<Image>().color = conflictNumberColor;
            }
        }
    }

    public void EraseNumber()
    {
        grid = generateGridAlgorhytm.GetGrid();
        int currentRow = playerInput.GetCurrentRowSelected();
        int currentColl = playerInput.GetCurrentCollSelected();

        currentCell = grid[currentRow, currentColl];

        if (!currentCell.isEditable) { return; }

        currentCell.value = 0;
        currentCell.cell.GetComponentInChildren<CellNumber>().GetComponent<TextMeshProUGUI>().text = "";

        CheckCorrectCurrentValue(currentCell);

        foreach (Cell cell in grid)
        {
            HighlightSameRowsAndColls(currentRow, currentColl, cell);
            HighlightSameSquare(cell);
        }
    }
}
