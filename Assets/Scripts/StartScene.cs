using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Application.targetFrameRate = 300;
        slider = GetComponentInChildren<Slider>();
    }

    public void StartLevel()
    {
        PlayerPrefs.SetInt("CurrentGun", (int)slider.value);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level1");
    }
}
