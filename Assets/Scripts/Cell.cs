using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int row;
    public int coll;
    public int value = 0;
    public int hiddenCorrectValue;
    public bool isEditable = false;
    public List<int> numbersLeftToTry = new();
    public List<Cell> cellsTried = new();
    public GameObject cell;

    public Cell(int row, int coll, GameObject cell)
    {
        this.row = row;
        this.coll = coll;
        this.cell = cell;
    }

    public void ResetNumbersTried()
    {
        numbersLeftToTry.Clear();

        for (int x = 1; x <= 9; x++)
        {
            numbersLeftToTry.Add(x);
        }

        ShuffleNumbers(numbersLeftToTry);
    }

    void ShuffleNumbers(List<int> numbers)
    {
        int x = numbers.Count;

        while (x > 1)
        {
            x--;
            int y = Random.Range(0, x + 1);
            int value = numbers[y];
            numbers[y] = numbers[x];
            numbers[x] = value;
        }
    }
}
