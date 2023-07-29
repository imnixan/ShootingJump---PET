using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPoolManager : MonoBehaviour
{
    [SerializeField]
    private Ammo bulletPref,
        sleevePref;

    private float MinAmmo;
    private List<Sleeve> sleevePool = new List<Sleeve>();
    private List<Bullet> bulletsPool = new List<Bullet>();
    private AmmoSettings currentBullet,
        currentSleeve;
    private Ammo newAmmo;

    public void Init(AmmoSettings bullet, AmmoSettings sleeve, int magazineValue)
    {
        this.currentBullet = bullet;
        this.currentSleeve = sleeve;
        MinAmmo = magazineValue * 2;
    }

    public Bullet GetBullet()
    {
        if (bulletsPool.Count > 0 && bulletsPool.Count < MinAmmo)
        {
            newAmmo = bulletsPool[0];
            bulletsPool.RemoveAt(0);
            if (newAmmo.Type != currentBullet.AmmoType)
            {
                newAmmo.SetupAmmo(currentBullet);
            }
        }
        else
        {
            CreateNewBullet();
            newAmmo.SetupAmmo(currentBullet);
        }
        return (Bullet)newAmmo;
    }

    public Sleeve GetSleeve()
    {
        if (sleevePool.Count > 0)
        {
            newAmmo = sleevePool[0];
            sleevePool.RemoveAt(0);
            if (newAmmo.Type != currentSleeve.AmmoType)
            {
                newAmmo.SetupAmmo(currentSleeve);
            }
        }
        else
        {
            CreateNewSleeve();
            newAmmo.SetupAmmo(currentSleeve);
        }
        return (Sleeve)newAmmo;
    }

    private void CreateNewBullet()
    {
        newAmmo = Instantiate(bulletPref, transform);
    }

    private void CreateNewSleeve()
    {
        newAmmo = Instantiate(sleevePref, transform);
    }

    public void ReturnnPool(Ammo ammo)
    {
        if (ammo is Bullet)
        {
            bulletsPool.Add((Bullet)ammo);
        }
        else
        {
            sleevePool.Add((Sleeve)ammo);
        }
    }
}
