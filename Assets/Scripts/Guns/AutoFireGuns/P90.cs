using System.Collections;
using UnityEngine;
using DG.Tweening;

public class P90 : AutoFireGun
{
    protected override void Start()
    {
        base.Start();
        rb.centerOfMass = Vector3.zero;
    }

    protected override void SetupWeapon()
    {
        bouncePower = 15f;
        animStepDuration = 0.025f;
        recoilForce = bouncePower / 5f;
        magazineValue = 80;
    }

    protected override void CreateEndAmmoAnim()
    {
        base.CreateEndAmmoAnim();
        endAmmoAnim.Append(slide.DOLocalMoveZ(slideShootEnd, animStepDuration));
    }
}
