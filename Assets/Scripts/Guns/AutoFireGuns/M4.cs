using System.Collections;
using UnityEngine;
using DG.Tweening;

public class M4 : AutoFireGun
{
    protected override void SetupWeapon()
    {
        bouncePower = 30f;
        animStepDuration = 0.03f;
        recoilForce = bouncePower / 6;
        magazineValue = 50;
    }

    protected override void CreateFireAnim()
    {
        fireAnim = DOTween.Sequence();
        fireAnim
            .PrependCallback(OnRechargeStart)
            .Append(slide.DOLocalMoveX(slideShootEnd, animStepDuration))
            .Join(trigger.DOLocalMoveX(triggerShootEnd, animStepDuration))
            .AppendCallback(DropSleeve)
            .Append(slide.DOLocalMoveX(slideShootStart, animStepDuration))
            .Join(trigger.DOLocalMoveX(triggerShootStart, animStepDuration))
            .AppendCallback(OnRechargeEnd);
    }
}
