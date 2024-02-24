using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    int currentRowSelected = 0;
    int currentCollSelected = 0;


    GenerateGridAlgorhytm generateGridAlgorhytm;

    private void Awake()
    {
        generateGridAlgorhytm = FindObjectOfType<GenerateGridAlgorhytm>();
    }

    private void Update()
    {
        OnButtonPress();
    }

    private void OnButtonPress()
    {

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentCollSelected > 0)
        {
            Cell[,] grid = generateGridAlgorhytm.GetGrid();
            grid[currentRowSelected, currentCollSelected - 1].cell.GetComponent<CellsCoordinates>().ChangeCell(currentRowSelected, currentCollSelected - 1);
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentCollSelected < 8)
        {
            Cell[,] grid = generateGridAlgorhytm.GetGrid();
            grid[currentRowSelected, currentCollSelected + 1].cell.GetComponent<CellsCoordinates>().ChangeCell(currentRowSelected, currentCollSelected + 1);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && currentRowSelected > 0)
        {
            Cell[,] grid = generateGridAlgorhytm.GetGrid();
            grid[currentRowSelected - 1, currentCollSelected].cell.GetComponent<CellsCoordinates>().ChangeCell(currentRowSelected - 1, currentCollSelected);
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && currentRowSelected < 8)
        {
            Cell[,] grid = generateGridAlgorhytm.GetGrid();
            grid[currentRowSelected + 1, currentCollSelected].cell.GetComponent<CellsCoordinates>().ChangeCell(currentRowSelected + 1, currentCollSelected);
        }
    }

    public int GetCurrentRowSelected()
    {
        return currentRowSelected;
    }

    public void SetCurrentRowSelected(int row)
    {
        currentRowSelected = row;
    }

    public int GetCurrentCollSelected()
    {
        return currentCollSelected;
    }

    public void SetCurrentCollSelected(int coll)
    {
        currentCollSelected = coll;
    }
}
