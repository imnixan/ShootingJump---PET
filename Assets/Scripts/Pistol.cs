using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Pistol : Gun
{
    protected override void Start()
    {
        GunPower = 10f;
        stepDuration = 0.1f;
        base.Start();
    }

    protected override void CreateFireAnim()
    {
        base.CreateFireAnim();
        fireAnim
            .PrependCallback(OnRechargeStart)
            .Append(slide.DOLocalMoveZ(slideShootEnd, stepDuration))
            .Join(trigger.DOLocalMoveZ(triggerShootEnd, stepDuration))
            .AppendCallback(DropSleeve)
            .Append(slide.DOLocalMoveZ(slideShootStart, stepDuration))
            .Join(trigger.DOLocalMoveZ(triggerShootStart, stepDuration))
            .AppendCallback(OnRechargeEnd);
    }
}
