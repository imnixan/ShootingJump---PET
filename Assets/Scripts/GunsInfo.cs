using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsInfo : MonoBehaviour
{
    [SerializeField]
    private string gunName;

    [SerializeField]
    private string gunDescription;

    [SerializeField]
    private int gunPrice;

    public bool GunOpened
    {
        get { return PlayerPrefs.HasKey(gunName); }
    }

    public string GunName
    {
        get { return gunName; }
    }

    public string GunDescription
    {
        get { return gunDescription; }
    }

    public int GunPrice
    {
        get { return gunPrice; }
    }

    public void OpenWeapon()
    {
        PlayerPrefs.SetInt(GunName, 1);
    }
}
