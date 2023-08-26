using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Gun : MonoBehaviour
{
    [SerializeField]
    protected AmmoSettings bulletSettings;

    [SerializeField]
    protected AmmoSettings sleeveSettings;

    [SerializeField]
    protected Transform muzzle,
        outlet;

    [SerializeField]
    protected Transform slide,
        trigger;

    [SerializeField]
    protected float slideShootStart,
        slideShootEnd,
        triggerShootStart,
        triggerShootEnd;

    protected bool _falling;
    protected AmmoPoolManager ammoPool;
    protected Rigidbody rb;
    protected GameSpeedChanger gameSpeedChanger;
    protected float bouncePower;
    protected float recoilForce;
    protected RaycastHit raycastHit;
    protected bool canShoot;
    protected float animStepDuration;
    protected Sequence fireAnim;
    protected Sequence endAmmoAnim;
    protected ParticleSystem flash;
    protected int magazineValue;
    protected int ammoLeft;

    protected bool Falling
    {
        get { return _falling; }
        set
        {
            if (_falling != value)
            {
                _falling = value;
                rb.isKinematic = !_falling;
            }
        }
    }

    protected abstract void SetupWeapon();

    protected virtual void Start()
    {
        SetupWeapon();
        InitFields();
        UpdateAmmo();

        CreateFireAnim();
        CreateEndAmmoAnim();
    }

    protected virtual void InitFields()
    {
        canShoot = true;
        ammoLeft = magazineValue;
        gameSpeedChanger = FindAnyObjectByType<GameSpeedChanger>();
        ammoPool = FindAnyObjectByType<AmmoPoolManager>();
        ammoPool.Init(bulletSettings, sleeveSettings, magazineValue);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        flash = GetComponentInChildren<ParticleSystem>();
    }

    protected virtual void Update()
    {
        CheckTrigger();
    }

    protected virtual void CheckTrigger()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PullTrigger();
        }

        if (Input.GetMouseButtonUp(0))
        {
            PullOffTrigger();
        }
    }

    protected void UpdateAmmo()
    {
        AmmoCounter.UpdateAmmoCounter(ammoLeft, magazineValue);
    }

    protected bool CheckTarget()
    {
        if (Physics.Raycast(muzzle.transform.position, transform.right, out raycastHit))
        {
            if (raycastHit.collider.CompareTag("Enemy"))
            {
                gameSpeedChanger.SlowTime();
                return true;
            }
        }
        return false;
    }

    protected virtual void Recoil()
    {
        rb.velocity = transform.right * -bouncePower;

        rb.angularVelocity = new Vector3(0, 0, recoilForce);
    }

    protected virtual void OnRechargeEnd()
    {
        canShoot = true;
    }

    protected virtual void OnRechargeStart()
    {
        canShoot = false;
    }

    protected virtual void CreateFireAnim()
    {
        fireAnim = DOTween.Sequence();
        fireAnim
            .PrependCallback(OnRechargeStart)
            .Append(slide.DOLocalMoveZ(slideShootEnd, animStepDuration))
            .Join(trigger.DOLocalMoveZ(triggerShootEnd, animStepDuration))
            .AppendCallback(DropSleeve)
            .Append(slide.DOLocalMoveZ(slideShootStart, animStepDuration))
            .Join(trigger.DOLocalMoveZ(triggerShootStart, animStepDuration))
            .AppendCallback(OnRechargeEnd);
    }

    protected virtual void CreateEndAmmoAnim()
    {
        endAmmoAnim = DOTween.Sequence();
    }

    protected virtual void DropSleeve()
    {
        ammoPool.GetSleeve().Init(outlet.position, transform.eulerAngles);
    }

    protected void OnAmmoLeft()
    {
        Debug.Log("EndAmmo");
        endAmmoAnim.Restart();
        Destroy(GetComponent<FixedJoint>());
    }

    protected virtual void PullTrigger()
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
    }

    protected virtual void Shoot()
    {
        fireAnim.Restart();
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

    protected virtual void CreateBullet()
    {
        ammoPool.GetBullet().Init(muzzle.position, transform.right);
    }

    protected virtual void PullOffTrigger() { }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo"))
        {
            ReloadAmmo();
        }
    }

    protected virtual void ReloadAmmo()
    {
        ammoLeft = magazineValue;
        UpdateAmmo();
    }
}
