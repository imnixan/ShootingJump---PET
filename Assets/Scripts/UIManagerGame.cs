using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManagerGame : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] hideUI;

    [SerializeField]
    private RectTransform scoresUI;

    [SerializeField]
    private TextMeshProUGUI damageText,
        killsText,
        timeText,
        bestTimeText,
        timeBonusText,
        totalScoresText,
        balanceText;

    private Sequence showScoresAnim;

    public void EndGame(GameEndData endData)
    {
        HideGameUI();
        PrepareAnim(endData);
        showScoresAnim.Play();
    }

    private void HideGameUI()
    {
        foreach (RectTransform rt in hideUI)
        {
            DOTween
                .To(
                    () => rt.anchoredPosition,
                    x => rt.anchoredPosition = x,
                    new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y * -10),
                    1
                )
                .Play();
        }
    }

    private void PrepareAnim(GameEndData endData)
    {
        balanceText.text = endData.Balance;
        showScoresAnim = DOTween.Sequence();
        showScoresAnim.Append(
            DOTween.To(
                () => scoresUI.anchoredPosition,
                x => scoresUI.anchoredPosition = x,
                new Vector2(0, 0),
                1.5f
            )
        );

        showScoresAnim.AppendCallback(() =>
        {
            AudioManager.PlayShootSound();
            damageText.text = endData.Damage;
        });
        showScoresAnim.Append(
            damageText.transform.parent.DOPunchScale(
                damageText.transform.parent.localScale * 1.1f,
                0.1f
            )
        );

        showScoresAnim.AppendCallback(() =>
        {
            AudioManager.PlayShootSound();
            killsText.text = endData.Kills;
        });
        showScoresAnim.Append(
            killsText.transform.parent.DOPunchScale(
                killsText.transform.parent.localScale * 1.1f,
                0.1f
            )
        );

        showScoresAnim.AppendCallback(() =>
        {
            AudioManager.PlayShootSound();
            bestTimeText.text = endData.BestTime;
        });
        showScoresAnim.Append(
            bestTimeText.transform.parent.DOPunchScale(
                bestTimeText.transform.parent.localScale * 1.1f,
                0.1f
            )
        );

        showScoresAnim.AppendCallback(() =>
        {
            AudioManager.PlayShootSound();
            timeText.text = endData.Time;
        });
        showScoresAnim.Append(
            timeText.transform.parent.DOPunchScale(
                timeText.transform.parent.localScale * 1.1f,
                0.1f
            )
        );

        showScoresAnim.AppendCallback(() =>
        {
            AudioManager.PlayShootSound();
            timeBonusText.text = endData.TimeBonus;
        });
        showScoresAnim.Append(
            timeBonusText.transform.parent.DOPunchScale(
                timeBonusText.transform.parent.localScale * 1.1f,
                0.1f
            )
        );

        showScoresAnim.AppendCallback(() =>
        {
            AudioManager.PlayShootSound();
            totalScoresText.text = endData.TotalScores;
        });
        showScoresAnim.Append(
            totalScoresText.transform.parent.DOPunchScale(
                totalScoresText.transform.parent.localScale * 1.1f,
                0.1f
            )
        );

        showScoresAnim.AppendCallback(() =>
        {
            AudioManager.PlayShootSound();
            balanceText.text = endData.NewBalance;
        });
        showScoresAnim.Append(
            balanceText.transform.parent.DOPunchScale(
                balanceText.transform.parent.localScale * 1.1f,
                0.1f
            )
        );
    }
}
