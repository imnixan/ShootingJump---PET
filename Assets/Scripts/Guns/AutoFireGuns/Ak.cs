using System.Collections;
using UnityEngine;

public class Ak : AutoFireGun
{
    protected override void SetupWeapon()
    {
        bouncePower = 30f;
        animStepDuration = 0.06f;
        recoilForce = bouncePower / 4;
        magazineValue = 50;
    }
}
