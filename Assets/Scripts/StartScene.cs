using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;

public class StartScene : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gunsObj;

    [SerializeField]
    private Vector3 showPos,
        hidePos;

    [SerializeField]
    private Slider slider;

    private GameObject currentGun;

    private Gun gun;
    private Sequence changeWeapon;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Application.targetFrameRate = 300;
        slider.maxValue = gunsObj.Length - 1;

        changeWeapon = DOTween.Sequence();
        changeWeapon.Append(transform.DOMove(hidePos, 0.3f));
        changeWeapon.AppendCallback(() =>
        {
            Destroy(currentGun);
            currentGun = Instantiate(gunsObj[(int)slider.value], transform);
        });
        changeWeapon.Append(transform.DOMove(showPos, 0.3f));

        OnGunChange();
    }

    public void StartLevel()
    {
        PlayerPrefs.SetInt("CurrentGun", (int)slider.value);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level1");
    }

    public void OnGunChange()
    {
        changeWeapon.Restart();
    }
}
