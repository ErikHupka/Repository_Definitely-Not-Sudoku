using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotesGrid : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> notesNumber;

    //Components
    IconFunctionalities iconFunctionalities;

    private void Awake()
    {
        iconFunctionalities = FindObjectOfType<IconFunctionalities>();
    }

    public void WriteNotes(Cell cell, int numberPressed)
    {
        if (cell.value == 0)
        {
            notesNumber[numberPressed - 1].enabled = !notesNumber[numberPressed - 1].enabled;
        }
    }
}
