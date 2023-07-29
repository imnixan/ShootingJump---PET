using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Pistol : Gun
{
    protected override void Start()
    {
        bouncePower = 10f;
        animStepDuration = 0.025f;
        magazineValue = 30;
        base.Start();
    }

    protected override void CreateFireAnim()
    {
        base.CreateFireAnim();
        fireAnim
            .PrependCallback(OnRechargeStart)
            .Append(slide.DOLocalMoveZ(slideShootEnd, animStepDuration))
            .Join(trigger.DOLocalMoveZ(triggerShootEnd, animStepDuration))
            .AppendCallback(DropSleeve)
            .Append(slide.DOLocalMoveZ(slideShootStart, animStepDuration))
            .Join(trigger.DOLocalMoveZ(triggerShootStart, animStepDuration))
            .AppendCallback(OnRechargeEnd);
    }
}
