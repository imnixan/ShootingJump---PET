using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField]
    private GameObject[] guns;

    private Slider slider;

    private GameObject currentGun;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Application.targetFrameRate = 300;
        slider = GetComponentInChildren<Slider>();
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
        Destroy(currentGun);
        currentGun = Instantiate(guns[(int)slider.value], new Vector3(-5, 0, 0), new Quaternion());
        currentGun.GetComponent<Gun>().enabled = false;
    }
}
