using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Revolver : Gun
{
    private AudioClip gunClick;

    protected override void Start()
    {
        base.Start();
        canShoot = false;
        gunClick = Resources.Load<AudioClip>("Sounds/Pistol/PistolGateSound");
        Recharge();
    }

    protected override void SetupWeapon()
    {
        bouncePower = 18f;
        animStepDuration = 0.05f;
        recoilForce = bouncePower / 4;
        magazineValue = 15;
    }

    protected override void CreateEndAmmoAnim()
    {
        base.CreateEndAmmoAnim();
        endAmmoAnim.Append(slide.DOLocalMoveZ(slideShootEnd, animStepDuration));
    }

    protected override void PullTrigger()
    {
        Falling = true;
        if (canShoot)
        {
            if (ammoLeft > 0)
            {
                Shoot();
            }
            else
            {
                OnAmmoLeft();
            }
        }
        else
        {
            Recharge();
        }
    }

    protected override void CreateFireAnim() { }

    protected override void Shoot()
    {
        canShoot = false;
        slide.DOLocalRotate(new Vector3(slideShootEnd, 0, 0), 0.01f).Play();
        trigger.DOLocalRotate(new Vector3(triggerShootEnd, 0, 0), 0.01f).Play();

        flash.Play();
        AudioManager.PlayShootSound();
        CreateBullet();
        if (!CheckTarget())
        {
            ammoLeft--;
            UpdateAmmo();
        }
        Recoil();
    }

    private void Recharge()
    {
        AudioManager.PlaySound(gunClick);
        slide.DOLocalRotate(new Vector3(slideShootStart, 0, 0), 0.01f).Play();
        trigger.DOLocalRotate(new Vector3(triggerShootStart, 0, 0), 0.01f).Play();
        outlet.DOShakeRotation(1f, 1);
        canShoot = true;
    }
}
