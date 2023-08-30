using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Ak : AutoFireGun
{
    protected override void SetupWeapon()
    {
        bouncePower = 20f;
        animStepDuration = 0.06f;
        recoilForce = bouncePower / 4;
        magazineValue = 50;
    }

    protected override void CreateEndAmmoAnim()
    {
        base.CreateEndAmmoAnim();
        endAmmoAnim.Append(slide.DOLocalMoveZ(slideShootEnd, animStepDuration));
    }
}
