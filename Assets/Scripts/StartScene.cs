using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class StartScene : MonoBehaviour
{
    public List<int> playerClicks = new List<int>() { 0 };
    private bool _lastClickwasLeft;

    [SerializeField]
    private GunsInfo[] guns;

    [SerializeField]
    private Vector3 showPos,
        hidePos;

    [SerializeField]
    private RectTransform pressToBuy,
        gunWindow;

    [SerializeField]
    private Image lockIcon;

    [SerializeField]
    private Sprite buy,
        locked;

    [SerializeField]
    private TextMeshProUGUI buyText,
        header,
        description,
        balanceText;

    private int playerBalance;
    private GameObject currentGun;
    private GunsInfo currentGunInfo;
    private int currentGunIndex;
    private Gun gun;
    private Sequence changeWeapon,
        hintAnimShow,
        hintAnimHide,
        cheatAnim;

    private bool LastClickWasLeft
    {
        set
        {
            if (_lastClickwasLeft == value)
            {
                playerClicks[playerClicks.Count - 1]++;
            }
            else
            {
                playerClicks.Add(0);
                playerClicks[playerClicks.Count - 1]++;
                _lastClickwasLeft = value;
            }
        }
    }

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Application.targetFrameRate = 300;
    }

    private void Start()
    {
        pressToBuy.localScale = Vector2.zero;
        playerBalance = PlayerPrefs.GetInt("Balance");
        balanceText.text = playerBalance.ToString();
        DOTween.KillAll();
        Time.timeScale = 1;
        SetAnim();

        OpenFirstGun();
        currentGunIndex = PlayerPrefs.GetInt("CurrentGun");
        OnGunChange();
    }

    private void OpenFirstGun()
    {
        guns[0].OpenWeapon();
    }

    private void SetAnim()
    {
        changeWeapon = DOTween.Sequence();
        changeWeapon.Append(transform.DOMove(hidePos, 0.3f));
        changeWeapon.Join(
            DOTween.To(
                () => gunWindow.anchoredPosition,
                x => gunWindow.anchoredPosition = x,
                new Vector2(0, -155),
                0.3f
            )
        );
        changeWeapon.AppendCallback(() =>
        {
            Destroy(currentGun);
            currentGun = Instantiate(guns[currentGunIndex].gameObject, transform);
            currentGunInfo = currentGun.GetComponent<GunsInfo>();
        });
        changeWeapon.AppendCallback(GunInit);
        changeWeapon.Append(transform.DOMove(showPos, 0.3f));
        changeWeapon.Join(
            DOTween.To(
                () => gunWindow.anchoredPosition,
                x => gunWindow.anchoredPosition = x,
                new Vector2(0, 0),
                0.3f
            )
        );

        hintAnimShow = DOTween.Sequence();
        hintAnimShow.Append(pressToBuy.DOScale(new Vector3(1, 1, 1), 0.3f));
        hintAnimShow.Join(
            DOTween.To(
                () => pressToBuy.anchoredPosition,
                x => pressToBuy.anchoredPosition = x,
                new Vector2(0, 100),
                0.3f
            )
        );

        hintAnimHide = DOTween.Sequence();
        hintAnimHide.Append(pressToBuy.DOScale(new Vector3(0, 0, 0), 0.3f));
        hintAnimHide.Join(
            DOTween.To(
                () => pressToBuy.anchoredPosition,
                x => pressToBuy.anchoredPosition = x,
                new Vector2(0, 0),
                0.3f
            )
        );

        cheatAnim = DOTween.Sequence();
        cheatAnim.AppendCallback(() =>
        {
            balanceText.text = "Cheat activated!";
        });
        cheatAnim.AppendInterval(1f);
        cheatAnim.AppendCallback(() =>
        {
            playerBalance += 10000;
            PlayerPrefs.SetInt("Balance", playerBalance);
            PlayerPrefs.Save();
            balanceText.text = playerBalance.ToString();
            ;
        });
    }

    public void StartLevel()
    {
        if (currentGunInfo.GunOpened)
        {
            PlayerPrefs.SetInt("CurrentGun", currentGunIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Level1");
        }
        else
        {
            TryBuyWeapon();
        }
    }

    private void TryBuyWeapon()
    {
        if (playerBalance >= currentGunInfo.GunPrice)
        {
            playerBalance -= currentGunInfo.GunPrice;
            balanceText.text = playerBalance.ToString();
            PlayerPrefs.SetInt("Balance", playerBalance);
            PlayerPrefs.Save();
            currentGunInfo.OpenWeapon();
            OnGunChange();
        }
    }

    public void OnGunChange()
    {
        changeWeapon.Restart();
    }

    private void GunInit()
    {
        description.text = currentGunInfo.GunDescription;
        header.text = currentGunInfo.GunName;
        if (currentGunInfo.GunOpened)
        {
            lockIcon.enabled = false;

            if (pressToBuy.localScale.x > 0)
            {
                hintAnimHide.Restart();
            }
        }
        else
        {
            lockIcon.enabled = true;

            if (playerBalance >= currentGunInfo.GunPrice)
            {
                buyText.text =
                    $"{Localization.CurrentLanguage["Buy_text"]}\n({currentGunInfo.GunPrice})";
                lockIcon.sprite = buy;
            }
            else
            {
                buyText.text =
                    $"{Localization.CurrentLanguage["No_honey_text"]}\n({currentGunInfo.GunPrice})";
                lockIcon.sprite = locked;
            }

            lockIcon.SetNativeSize();

            if (pressToBuy.localScale.x == 0)
            {
                hintAnimShow.Restart();
            }
        }
    }

    public void NextWeapon()
    {
        currentGunIndex++;
        if (currentGunIndex >= guns.Length)
        {
            currentGunIndex = 0;
        }
        OnGunChange();
        LastClickWasLeft = false;
        CheckCheat();
    }

    public void PrevWeapon()
    {
        currentGunIndex--;
        if (currentGunIndex < 0)
        {
            currentGunIndex = guns.Length - 1;
        }
        OnGunChange();
        LastClickWasLeft = true;
        CheckCheat();
    }

    private void CheckCheat()
    {
        if (playerClicks.Count > 4)
        {
            if (
                playerClicks[0] == 1
                && playerClicks[1] == 3
                && playerClicks[2] == 3
                && playerClicks[3] == 7
            )
            {
                cheatAnim.Restart();
            }
            playerClicks.Clear();
            playerClicks.Add(0);
            _lastClickwasLeft = false;
        }
    }
}
