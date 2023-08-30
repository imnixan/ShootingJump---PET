using System.Collections;
using UnityEngine;

public class GameManager : DamageListeners
{
    private int Gun;
    private float startTime;

    [SerializeField]
    private GameObject[] guns;

    [SerializeField]
    private ScoresCounter scoreCounter;

    [SerializeField]
    private Transform Enemies;

    [SerializeField]
    private CameraMover cameraMove;

    [SerializeField]
    private UIManagerGame uiManager;

    private bool gameGoin;

    private int totalEnemies,
        enemiesKilled,
        scores;

    private void Start()
    {
        startTime = Time.time;
        gameGoin = true;
        Gun = PlayerPrefs.GetInt("CurrentGun");
        cameraMove.Init(Instantiate(guns[Gun]).transform);
        totalEnemies = Enemies.childCount;
    }

    public void EndGame(bool win)
    {
        Debug.Log("EndGame");
        if (gameGoin)
        {
            GameEndData endData = win
                ? new GameEndData(scores, enemiesKilled, totalEnemies, Time.time - startTime)
                : new GameEndData(enemiesKilled, totalEnemies);

            uiManager.EndGame(endData);
            gameGoin = false;
        }
    }

    protected override void OnDamageTaken(int damage)
    {
        scores += damage;
        scoreCounter.ShowScores(scores);
    }

    protected override void OnEnemyKill()
    {
        enemiesKilled++;
    }
}
