using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Pistol : Gun
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void SetupWeapon()
    {
        bouncePower = 15f;
        animStepDuration = 0.07f;
        recoilForce = bouncePower / 4;
        magazineValue = 1500;
    }

    protected override void CreateEndAmmoAnim()
    {
        base.CreateEndAmmoAnim();
        endAmmoAnim.Append(slide.DOLocalMoveZ(slideShootEnd, animStepDuration));
    }
}
