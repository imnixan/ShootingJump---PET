using System.Collections;
using UnityEngine;
using DG.Tweening;

public abstract class AutoFireGun : Gun
{
    public bool fire;

    protected override void PullTrigger()
    {
        Debug.Log("pullTrigger");
        fire = true;
        base.PullTrigger();
    }

    protected override void PullOffTrigger()
    {
        fire = false;
        base.PullOffTrigger();
    }

    protected override void OnRechargeEnd()
    {
        canShoot = true;
        Debug.Log("Here in recharhe");
        if (fire)
        {
            PullTrigger();
        }
    }
}
