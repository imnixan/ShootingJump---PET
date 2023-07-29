using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Gun : MonoBehaviour
{
    [SerializeField]
    protected AmmoSettings bulletSettings;

    [SerializeField]
    protected AudioClip[] shootSounds;

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
    public bool canShoot;
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

    protected virtual void Start()
    {
        ammoLeft = magazineValue;
        UpdateAmmo();
        recoilForce = bouncePower / 4;
        gameSpeedChanger = FindAnyObjectByType<GameSpeedChanger>();
        ammoPool = FindAnyObjectByType<AmmoPoolManager>();
        ammoPool.Init(bulletSettings, sleeveSettings, magazineValue);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        canShoot = true;
        flash = GetComponentInChildren<ParticleSystem>();
        CreateFireAnim();
        CreateNoAmmoAnim();
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    protected virtual void Shoot()
    {
        if (canShoot)
        {
            if (ammoLeft > 0)
            {
                Falling = true;
                fireAnim.Restart();
                flash.Play();
                AudioManager.PlaySound(shootSounds[Random.Range(0, shootSounds.Length)]);
                ammoPool.GetBullet().Init(muzzle.position, transform.right);
                //if (!CheckTarget())
                //{
                //    ammoLeft--;
                //    UpdateAmmo();
                //}
                gameSpeedChanger.SlowTime();
                Recoil();
            }
            else
            {
                OnAmmoLeft();
            }
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

    protected void OnRechargeEnd()
    {
        canShoot = true;
    }

    protected void OnRechargeStart()
    {
        canShoot = false;
    }

    protected virtual void CreateFireAnim()
    {
        fireAnim = DOTween.Sequence();
    }

    protected virtual void CreateNoAmmoAnim()
    {
        endAmmoAnim = DOTween.Sequence();
    }

    protected virtual void DropSleeve()
    {
        ammoPool.GetSleeve().Init(outlet.position, transform.eulerAngles);
    }

    protected void OnAmmoLeft()
    {
        Debug.Log("GameOver");
        endAmmoAnim.Restart();
        Destroy(GetComponent<FixedJoint>());
    }
}
