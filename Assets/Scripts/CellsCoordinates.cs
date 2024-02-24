using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CellsCoordinates : MonoBehaviour
{
    [Header("Coordinates")]
    [SerializeField] int row;
    [SerializeField] int coll;

    Cell[,] grid;

    Color normalColor = new(1f, 1f, 1f);
    Color currentNumberColor = new(0.211f, 0.505f, 0.632f);
    Color relatedNumberColor = new(0.478f, 0.631f, 0.725f);
    Color relatedRowsCollsColor = new(0.768f, 0.796f, 0.839f);

    //Components
    GenerateGridAlgorhytm generalGridAlgorhytm;
    PlayerInput playerInput;

    private void Awake()
    {
        generalGridAlgorhytm = FindObjectOfType<GenerateGridAlgorhytm>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    public void ChangeCell(int row, int coll)
    {
        grid = generalGridAlgorhytm.GetGrid();
        int currentCellValue = grid[this.row, this.coll].value;

        playerInput.SetCurrentRowSelected(grid[row, coll].row);
        playerInput.SetCurrentCollSelected(grid[row, coll].coll);

        //Reset all cell collors
        foreach (Cell cell in grid)
        {
            cell.cell.GetComponent<Image>().color = normalColor;
        }

        foreach (Cell cell in grid)
        {
            HighlightRelatedNumber(currentCellValue, cell);
            HighlightRowsAndColls(row, coll, cell);
            HighlightSquare(cell);
            HighlightCurrentNumber(row, coll, cell);
        }
    }

    public void HighlightCurrentNumber(int row, int coll, Cell cell)
    {
        if (cell.row == row && cell.coll == coll)
        {
            cell.cell.GetComponent<Image>().color = currentNumberColor;
        }
    }

    public void HighlightRelatedNumber(int currentCellValue, Cell cell)
    {
        if (cell.value == currentCellValue && cell.value != 0)
        {
            cell.cell.GetComponent<Image>().color = relatedNumberColor;
        }
    }

    public void HighlightRowsAndColls(int row, int coll, Cell cell)
    {
        if (cell.row == row || cell.coll == coll)
        {
            cell.cell.GetComponent<Image>().color = relatedRowsCollsColor;
        }
    }

    public void HighlightSquare(Cell cell)
    {
        if (playerInput.GetCurrentRowSelected() < 3 && playerInput.GetCurrentRowSelected() >= 0 && cell.row < 3 && cell.row >= 0)
        {
            if (playerInput.GetCurrentCollSelected() < 3 && playerInput.GetCurrentCollSelected() >= 0 && cell.coll < 3 && cell.coll >= 0)
            {
                cell.cell.GetComponent<Image>().color = relatedRowsCollsColor;
            }
            else if (playerInput.GetCurrentCollSelected() < 6 && playerInput.GetCurrentCollSelected() >= 3 && cell.coll < 6 && cell.coll >= 3)
            {
                cell.cell.GetComponent<Image>().color = relatedRowsCollsColor;
            }
            else if (playerInput.GetCurrentCollSelected() < 9 && playerInput.GetCurrentCollSelected() >= 6 && cell.coll < 9 && cell.coll >= 6)
            {
                cell.cell.GetComponent<Image>().color = relatedRowsCollsColor;
            }
        }

        else if (playerInput.GetCurrentRowSelected() < 6 && playerInput.GetCurrentRowSelected() >= 3 && cell.row < 6 && cell.row >= 3)
        {
            if (playerInput.GetCurrentCollSelected() < 3 && playerInput.GetCurrentCollSelected() >= 0 && cell.coll < 3 && cell.coll >= 0)
            {
                cell.cell.GetComponent<Image>().color = relatedRowsCollsColor;
            }
            else if (playerInput.GetCurrentCollSelected() < 6 && playerInput.GetCurrentCollSelected() >= 3 && cell.coll < 6 && cell.coll >= 3)
            {
                cell.cell.GetComponent<Image>().color = relatedRowsCollsColor;
            }
            else if (playerInput.GetCurrentCollSelected() < 9 && playerInput.GetCurrentCollSelected() >= 6 && cell.coll < 9 && cell.coll >= 6)
            {
                cell.cell.GetComponent<Image>().color = relatedRowsCollsColor;
            }
        }

        else if (playerInput.GetCurrentRowSelected() < 9 && playerInput.GetCurrentRowSelected() >= 6 && cell.row < 9 && cell.row >= 6)
        {
            if (playerInput.GetCurrentCollSelected() < 3 && playerInput.GetCurrentCollSelected() >= 0 && cell.coll < 3 && cell.coll >= 0)
            {
                cell.cell.GetComponent<Image>().color = relatedRowsCollsColor;
            }
            else if (playerInput.GetCurrentCollSelected() < 6 && playerInput.GetCurrentCollSelected() >= 3 && cell.coll < 6 && cell.coll >= 3)
            {
                cell.cell.GetComponent<Image>().color = relatedRowsCollsColor;
            }
            else if (playerInput.GetCurrentCollSelected() < 9 && playerInput.GetCurrentCollSelected() >= 6 && cell.coll < 9 && cell.coll >= 6)
            {
                cell.cell.GetComponent<Image>().color = relatedRowsCollsColor;
            }
        }
    }

    public void OnMouseClick()
    {
        ChangeCell(row, coll);
    }

    public int GetRow()
    {
        return row;
    }

    public int GetColl()
    {
        return coll;
    }
}
