using System.Collections;
using UnityEngine;
using TMPro;

public class ScoresCounter : MonoBehaviour
{
    private TextMeshProUGUI scoresUI;

    private void Start()
    {
        scoresUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowScores(int scores)
    {
        scoresUI.text = scores.ToString();
    }
}
