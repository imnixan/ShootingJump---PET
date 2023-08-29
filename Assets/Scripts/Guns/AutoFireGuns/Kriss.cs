using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Kriss : AutoFireGun
{
    protected override void Start()
    {
        base.Start();
        rb.centerOfMass = Vector3.zero;
    }

    protected override void SetupWeapon()
    {
        bouncePower = 20f;
        animStepDuration = 0.02f;
        recoilForce = bouncePower / 4.5f;
        magazineValue = 50;
    }

    protected override void CreateEndAmmoAnim()
    {
        base.CreateEndAmmoAnim();
        endAmmoAnim.Append(slide.DOLocalMoveZ(slideShootEnd, animStepDuration));
    }
}
