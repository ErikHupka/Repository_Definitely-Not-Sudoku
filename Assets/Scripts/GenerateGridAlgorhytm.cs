using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateGridAlgorhytm : MonoBehaviour
{
    [SerializeField] List<GameObject> gridCells;
    Cell[,] grid = new Cell[9, 9];

    int difficulty = 45; // Easy = 40, Medium = 45, Hard = 50, Expert = 55, Insane = 60
    List<Cell> cellsRemoved = new();
    bool firstRun = true;

    private void Start()
    {
        CreateEmptyGrid();
    }

    private void CreateEmptyGrid()
    {
        int cellNum = 0;

        for (int row = 0; row < 9; row++)
        {
            for (int coll = 0; coll < 9; coll++)
            {
                grid[row, coll] = new Cell(row, coll, gridCells[cellNum]);
                grid[row, coll].ResetNumbersTried();
                cellNum++;
            }
        }
    }

    public void ButtonFunction()
    {
        ClearGrid();
        GenerateGrid(0, 0);
        SetHiddenCorrectValue();
        cellsRemoved.Clear();
        cellsRemoved.Add(grid[0, 0]);
        RemoveRandomNumber(true);
    }

    public void GenerateGrid(int row, int coll)
    {
        if (row == 9) { return; }

        if (grid[row, coll].numbersLeftToTry.Count == 0)
        {
            int lastColl = coll - 1;
            int lastRow = row - 1;

            grid[row, coll].ResetNumbersTried();
            grid[row, coll].value = 0;

            if (coll == 0 && row > 0)
            {
                GenerateGrid(lastRow, 8);
                return;
            }
            else if (coll > 0 && row > 0)
            {
                GenerateGrid(row, lastColl);
                return;
            }
            else
            {
                GenerateGrid(0, 0);
                return;
            }
        }

        SetCellValue(row, coll, grid[row, coll].numbersLeftToTry[0]);
        grid[row, coll].numbersLeftToTry.RemoveAt(0);


        if (CheckValidPlacement(row, coll, grid[row, coll].value))
        {
            MoveToNextCell(row, coll);
            return;
        }

        else
        {
            MoveToLastCell(row, coll);
            return;
        }
    }

    private void MoveToNextCell(int row, int coll)
    {
        int nextColl = coll + 1;
        int nextRow = row + 1;
        if (coll < 8 && row < 9)
        {
            GenerateGrid(row, nextColl);
            return;
        }
        else if (coll == 8 && row < 9)
        {
            GenerateGrid(nextRow, 0);
            return;
        }
        else
        {
            return;
        }
    }

    private void MoveToLastCell(int row, int coll)
    {
        if (grid[row, coll].numbersLeftToTry.Count > 0)
        {
            GenerateGrid(row, coll);
            return;
        }

        int lastColl = coll - 1;
        int lastRow = row - 1;

        grid[row, coll].ResetNumbersTried();
        grid[row, coll].value = 0;

        if (coll == 0 && row > 0)
        {
            GenerateGrid(lastRow, 8);
            return;
        }
        else if (coll > 0 && row > 0)
        {
            GenerateGrid(row, lastColl);
            return;
        }
        else
        {
            GenerateGrid(0, 0);
            return;
        }
    }

    private bool CheckValidPlacement(int row, int coll, int newValue)
    {

        //Check Row and Coll

        for (int x = 0; x < 9; x++)
        {
            if ((newValue == grid[row, x].value && x != coll)
                || (newValue == grid[x, coll].value && x != row))
            {
                return false;
            }
        }

        //Check Square

        if (row < 3)
        {
            if (coll < 3)
            {
                if (!CheckSquareGrid(3, 3, newValue, row, coll)) { return false; };
            }

            else if (coll < 6)
            {
                if (!CheckSquareGrid(3, 6, newValue, row, coll)) { return false; };
            }
            else if (coll < 9)
            {
                if (!CheckSquareGrid(3, 9, newValue, row, coll)) { return false; };
            }
        }

        else if (row < 6)
        {
            if (coll < 3)
            {
                if (!CheckSquareGrid(6, 3, newValue, row, coll)) { return false; };
            }

            else if (coll < 6)
            {
                if (!CheckSquareGrid(6, 6, newValue, row, coll)) { return false; };
            }
            else if (coll < 9)
            {
                if (!CheckSquareGrid(6, 9, newValue, row, coll)) { return false; };
            }
        }

        else if (row < 9)
        {
            if (coll < 3)
            {
                if (!CheckSquareGrid(9, 3, newValue, row, coll)) { return false; };
            }

            else if (coll < 6)
            {
                if (!CheckSquareGrid(9, 6, newValue, row, coll)) { return false; };
            }
            else if (coll < 9)
            {
                if (!CheckSquareGrid(9, 9, newValue, row, coll)) { return false; };
            }
        }

        return true;
    }

    private void ClearGrid()
    {
        foreach (Cell cell in grid)
        {
            SetCellValue(cell.row, cell.coll, 0);
            cell.hiddenCorrectValue = 0;
            cell.isEditable = false;
            cell.cell.GetComponentInChildren<CellNumber>().GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            cell.cellsTried.Clear();
            ResetNumbersTried(true);
        }
    }

    private void SetCellValue(int row, int coll, int value)
    {
        grid[row, coll].value = value;
        return;
    }

    private bool CheckSquareGrid(int a, int b, int newValue, int row, int coll)
    {
        for (int x = a - 3; x < a; x++)
        {
            for (int y = b - 3; y < b; y++)
            {
                if (grid[x, y].value == newValue && x != row && y != coll)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void RemoveRandomNumber(bool moveForward)
    {
        ResetNumbersTried(moveForward);

        int row, coll;

        //Choose random Cell

        if (cellsRemoved.Count < difficulty)
        {
            if (cellsRemoved.Count > 0)
            {
                if (cellsRemoved.Count < 81)
                {
                    do
                    {
                        row = Random.Range(0, 9);
                        coll = Random.Range(0, 9);
                    }
                    while (cellsRemoved.Contains(grid[row, coll]) || cellsRemoved[cellsRemoved.Count - 1].cellsTried.Contains(grid[row, coll]));
                }

                else
                {
                    grid[cellsRemoved[cellsRemoved.Count - 1].row, cellsRemoved[cellsRemoved.Count - 1].coll].value =
                        grid[cellsRemoved[cellsRemoved.Count - 1].row, cellsRemoved[cellsRemoved.Count - 1].coll].hiddenCorrectValue;
                    grid[cellsRemoved[cellsRemoved.Count - 1].row, cellsRemoved[cellsRemoved.Count - 1].coll].ResetNumbersTried();
                    grid[cellsRemoved[cellsRemoved.Count - 1].row, cellsRemoved[cellsRemoved.Count - 1].coll].isEditable = false;
                    cellsRemoved.RemoveAt(cellsRemoved.Count - 1);
                    RemoveRandomNumber(false);
                    return;
                }
            }

            else //No possible combination, came to the beginning
            {
                ButtonFunction();
                return;
            }

            if (firstRun)
            {
                cellsRemoved.RemoveAt(0);
                firstRun = false;
            }

            grid[row, coll].value = 0;
            SolveGrid(row, coll);
            return;
        }

        else
        {
            PrintGridPerValue();
            return;
        }
    }

    private void ResetNumbersTried(bool moveForward)
    {
        if (moveForward)
        {
            foreach (Cell cell in grid)
            {
                cell.ResetNumbersTried();
            }
        }
    }

    private void SolveGrid(int row, int coll)
    {
        foreach (Cell cell in grid)
        {
            int possibleCombinations = 0;
            int correctUniqueNumber = 0;

            if (cell.value == 0 && cell.numbersLeftToTry.Count > 0)
            {
                do
                {
                    SetCellValue(cell.row, cell.coll, cell.numbersLeftToTry[0]);

                    if (CheckValidPlacement(cell.row, cell.coll, cell.numbersLeftToTry[0]))
                    {
                        correctUniqueNumber = cell.numbersLeftToTry[0];
                        possibleCombinations++;
                    }

                    cell.numbersLeftToTry.RemoveAt(0);
                }
                while (cell.numbersLeftToTry.Count > 0 && possibleCombinations <= 1);

                if (possibleCombinations == 1 && correctUniqueNumber != 0)
                {
                    cell.value = 0;
                    cell.isEditable = true;
                }
                else // BackStrack
                {
                    cell.isEditable = false;
                    cell.value = cell.hiddenCorrectValue;
                    cell.ResetNumbersTried();
                    grid[row, coll].cellsTried.Add(cellsRemoved[cellsRemoved.Count - 1]);
                    cellsRemoved[cellsRemoved.Count - 1].cellsTried.Clear();
                    RemoveRandomNumber(false);
                    return;
                }
            }
        }

        cellsRemoved.Add(grid[row, coll]);

        grid[row, coll].cellsTried.Add(grid[row, coll]);


        RemoveRandomNumber(true);
        return;
    }

    private void PrintGridPerValue()
    {
        foreach (Cell cell in grid)
        {
            cell.value = cell.hiddenCorrectValue;
            cell.isEditable = false;
            cell.cell.GetComponentInChildren<CellNumber>().GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
            cell.cell.GetComponentInChildren<CellNumber>().GetComponent<TextMeshProUGUI>().text = cell.hiddenCorrectValue.ToString();
        }

        foreach (Cell cell in cellsRemoved)
        {
            cell.value = 0;
            cell.isEditable = true;
            cell.cell.GetComponentInChildren<CellNumber>().GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            cell.cell.GetComponentInChildren<CellNumber>().GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    private void SetHiddenCorrectValue()
    {
        foreach (Cell cell in grid)
        {
            cell.hiddenCorrectValue = cell.value;
        }
    }

    public Cell[,] GetGrid()
    {
        return grid;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }

    public void SetDifficulty(int newDifficulty)
    {
        difficulty = newDifficulty;
    }
}