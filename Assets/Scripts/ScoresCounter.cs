using System.Collections;
using UnityEngine;
using TMPro;

public class ScoresCounter : DamageListeners
{
    private TextMeshProUGUI scoresUI;
    private int scores;

    private void Start()
    {
        scoresUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void OnDamageTaken(int damage)
    {
        scores += damage;
        scoresUI.text = scores.ToString();
    }

    protected override void OnEnemyKill() { }
}
