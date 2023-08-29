using System.Collections;
using UnityEngine;
using TMPro;

public class ScoresCounter : DamageListeners
{
    private TextMeshProUGUI scoresUI;
    public int scores;
    public int enemyKilled;

    private void Start()
    {
        scoresUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void OnDamageTaken(int damage)
    {
        scores += damage;
        scoresUI.text = scores.ToString();
    }

    protected override void OnEnemyKill()
    {
        enemyKilled++;
    }
}
