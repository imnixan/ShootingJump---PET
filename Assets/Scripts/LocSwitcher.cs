using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class LocSwitcher : MonoBehaviour
{
    private TextMeshProUGUI locText;

    private int currentLanguageInt;

    public void Awake()
    {
        locText = GetComponentInChildren<TextMeshProUGUI>();
        currentLanguageInt = PlayerPrefs.GetInt("Localization");
        locText.text = Localization.SetLanguage(
            (Localization.Language)PlayerPrefs.GetInt("Localization")
        );
    }

    public void ChangeLoc()
    {
        currentLanguageInt++;
        if (currentLanguageInt >= Enum.GetValues(typeof(Localization.Language)).Length)
        {
            currentLanguageInt = 0;
        }
        PlayerPrefs.SetInt("Localization", currentLanguageInt);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
