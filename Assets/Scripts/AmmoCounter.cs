using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    private static Image ammoValue;

    private static TextMeshProUGUI ammoValueText;

    private void Start()
    {
        ammoValue = transform.GetChild(0).GetComponent<Image>();
        ammoValueText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public static void UpdateAmmoCounter(int currentAmmo, int maxAmmo)
    {
        float fillValue = (float)currentAmmo / (float)maxAmmo;
        ammoValue.fillAmount = fillValue;
        ammoValueText.text = currentAmmo.ToString();
    }
}
