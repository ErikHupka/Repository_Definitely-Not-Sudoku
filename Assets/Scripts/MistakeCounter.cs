using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MistakeCounter : MonoBehaviour
{
    int mistakeCount = 0;
    [SerializeField] TextMeshProUGUI mistakeCountText;
    [SerializeField] Canvas endScreenCanvas;

    public void IncreaseMistakeCount()
    {
        mistakeCount++;
        mistakeCountText.GetComponent<TextMeshProUGUI>().text = "Mistakes: " + mistakeCount + "/3";

        if (mistakeCount == 3)
        {
            endScreenCanvas.enabled = true;
        }
    }

    public int GetMistakeCount()
    {
        return mistakeCount;
    }

    public void ResetMistakeCount()
    {
        mistakeCount = 0;
    }

}
