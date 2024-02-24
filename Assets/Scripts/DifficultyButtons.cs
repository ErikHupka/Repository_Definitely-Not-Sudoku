using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyButtons : MonoBehaviour
{
    [SerializeField] int buttonDifficulty;

    Color normalColor = new(0.474f, 0.631f, 0.725f);
    Color highlitedColor = new(0.0549f, 0.215f, 0.290f);

    [SerializeField] List<TextMeshProUGUI> allButtons;

    //Components
    GenerateGridAlgorhytm generateGridAlgorhytm;

    private void Awake()
    {
        generateGridAlgorhytm = FindObjectOfType<GenerateGridAlgorhytm>();
    }

    public void ButtonClick()
    {
        generateGridAlgorhytm.SetDifficulty(buttonDifficulty);
        foreach (TextMeshProUGUI button in allButtons)
        {
            button.color = normalColor;
        }

        GetComponent<TextMeshProUGUI>().color = highlitedColor;
    }
}