using System.Collections;
using UnityEngine;

public class GameEndData
{
    private const int TimeBonusMultiplier = 10;
    private const int KillAllMultiplayer = 2;
    private readonly int damage;
    private readonly int kills;
    private readonly int totalEnemies;
    private readonly float time;

    private float bestTime;

    private int bonus;
    private int timeBonus;
    private int totalScores;
    private int balance;
    private int newBalance;

    public string Damage
    {
        get { return damage.ToString(); }
    }

    public string Kills
    {
        get { return $"{kills}/{totalEnemies}"; }
    }

    public string Time
    {
        get { return string.Format("{0:d2}:{1:d2}", (int)(time / 60), (int)(time % 60)); }
    }

    public string BestTime
    {
        get { return string.Format("{0:d2}:{1:d2}", (int)(bestTime / 60), (int)(bestTime % 60)); }
    }

    public string TimeBonus
    {
        get { return timeBonus.ToString(); }
    }

    public string TotalScores
    {
        get { return totalScores.ToString(); }
    }

    public string Balance
    {
        get { return balance.ToString(); }
    }

    public string NewBalance
    {
        get { return newBalance.ToString(); }
    }

    public GameEndData(int damage, int kills, int totalEnemies, float time)
    {
        this.damage = damage;
        this.kills = kills;
        this.totalEnemies = totalEnemies;
        this.time = time;

        if (!PlayerPrefs.HasKey("BestTime"))
        {
            PlayerPrefs.SetFloat("BestTime", time + 10);
            PlayerPrefs.Save();
        }
        bestTime = PlayerPrefs.GetFloat("BestTime");
        balance = PlayerPrefs.GetInt("Balance");
        CalculateScores();
    }

    private void CalculateScores()
    {
        if (bestTime > time && damage > 0)
        {
            PlayerPrefs.SetFloat("BestTime", time);
            PlayerPrefs.Save();
            timeBonus = (int)((bestTime - time) * TimeBonusMultiplier);
            bestTime = time;
        }

        if (kills == totalEnemies)
        {
            bonus = damage * KillAllMultiplayer;
        }

        totalScores = damage + bonus + timeBonus;
        newBalance = balance + totalScores;
        PlayerPrefs.SetInt("Balance", newBalance);
    }

    public GameEndData(int kills, int totalEnemies)
    {
        this.kills = kills;
        this.totalEnemies = totalEnemies;
    }
}
