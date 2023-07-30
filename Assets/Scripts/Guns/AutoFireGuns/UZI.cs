using System.Collections;
using UnityEngine;
using DG.Tweening;

public class UZI : AutoFireGun
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void SetupWeapon()
    {
        bouncePower = 20f;
        animStepDuration = 0.03f;
        recoilForce = bouncePower / 4;
        magazineValue = 50;
    }

    protected override void CreateEndAmmoAnim()
    {
        base.CreateEndAmmoAnim();
        endAmmoAnim.Append(slide.DOLocalMoveZ(slideShootEnd, animStepDuration));
    }
}
