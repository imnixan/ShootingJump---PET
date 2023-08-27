using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ShotGun : Gun
{
    protected override void SetupWeapon()
    {
        bouncePower = 30f;
        animStepDuration = 0.1f;
        recoilForce = bouncePower / 2;
        magazineValue = 10;
    }

    protected override void Shoot()
    {
        fireAnim.Restart();
        flash.Stop();
        flash.Play();
        CreateBullet();
        AudioManager.PlayShootSound();
        if (!CheckTarget())
        {
            ammoLeft--;
            UpdateAmmo();
        }
        Recoil();
    }

    protected override void CreateBullet()
    {
        for (int i = 0; i < 12; i++)
        {
            ammoPool
                .GetBullet()
                .Init(
                    muzzle.position,
                    transform.right
                        + (transform.up * Random.Range(-0.5f, 0.5f))
                        + (transform.forward * Random.Range(-0.5f, 0.5f))
                );
        }
    }

    protected override void CreateFireAnim()
    {
        fireAnim = DOTween.Sequence();
        fireAnim
            .PrependCallback(OnRechargeStart)
            .PrependInterval(0.1f)
            .Append(slide.DOLocalMoveZ(slideShootEnd, animStepDuration))
            .Join(trigger.DOLocalMoveZ(triggerShootEnd, animStepDuration))
            .AppendCallback(DropSleeve)
            .Append(slide.DOLocalMoveZ(slideShootStart, animStepDuration))
            .Join(trigger.DOLocalMoveZ(triggerShootStart, animStepDuration))
            .AppendCallback(OnRechargeEnd);
    }
}
