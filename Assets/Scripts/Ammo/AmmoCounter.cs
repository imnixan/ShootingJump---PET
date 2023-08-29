using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    private static Image ammoValue;

    private static TextMeshProUGUI ammoValueText;

    private void Awake()
    {
        ammoValueText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public static void UpdateAmmoCounter(int currentAmmo)
    {
        ammoValueText.text = currentAmmo.ToString();
    }
}
