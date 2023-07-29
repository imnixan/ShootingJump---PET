using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Gun : MonoBehaviour
{
    [SerializeField]
    protected Bullet bulletPrefab;

    [SerializeField]
    protected Sleeve sleevePrefab;

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

    protected Rigidbody rb;
    protected GameSpeedChanger gameSpeedChanger;
    protected float GunPower;
    protected RaycastHit raycastHit;
    public bool canShoot;
    protected float stepDuration;
    protected Sequence fireAnim;
    protected ParticleSystem flash;

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
        gameSpeedChanger = FindAnyObjectByType<GameSpeedChanger>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        canShoot = true;
        flash = GetComponentInChildren<ParticleSystem>();
        CreateFireAnim();
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
            //Falling = true;
            fireAnim.Restart();
            flash.Play();
            Instantiate(bulletPrefab, muzzle.position, new Quaternion()).Init(transform.right);
            CheckTarget();
            //Recoil();
        }
    }

    protected void CheckTarget()
    {
        if (Physics.Raycast(muzzle.transform.position, transform.right, out raycastHit))
        {
            if (raycastHit.collider.CompareTag("Enemy"))
            {
                gameSpeedChanger.SlowTime();
            }
        }
    }

    protected virtual void Recoil()
    {
        rb.velocity = transform.right * -GunPower;
        rb.angularVelocity = new Vector3(0, 0, GunPower / 3);
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

    protected virtual void DropSleeve()
    {
        Instantiate(sleevePrefab, outlet.position, new Quaternion()).Init(transform.eulerAngles);
    }
}
