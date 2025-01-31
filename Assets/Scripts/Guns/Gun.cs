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

    [SerializeField]
    protected bool chillin;

    [SerializeField] protected float flashLightIntencity = 100;
    protected Light flashLight;
    
    protected GameManager gameManager;
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
        gameManager = FindAnyObjectByType<GameManager>();
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
        flashLight = GetComponentInChildren<Light>();
        flashLight.intensity = 0;
    }

    protected virtual void Update()
    {
        CheckTrigger();

        if (ammoLeft == 0 && chillin)
        {
            canShoot = false;
            gameManager.EndGame(false);
        }
    }

    protected virtual void LateUpdate()
    {
        chillin = rb.angularVelocity == Vector3.zero;
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
        AmmoCounter.UpdateAmmoCounter(ammoLeft);
    }

    protected bool CheckTarget()
    {
        if (Physics.Raycast(muzzle.transform.position, transform.right, out raycastHit, 25))
        {
            if (
                raycastHit.collider.CompareTag("Enemy")
                && raycastHit.collider.GetComponent<BodyPart>().GetEnemyHp() > 0
            )
            {
                // gameSpeedChanger.StartSlowTime();
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

    public virtual void PullTrigger()
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
        PlayFlash();
        
        AudioManager.PlayShootSound();
        CreateBullet();
        if (!CheckTarget())
        {
            ammoLeft--;
            UpdateAmmo();
        }
        Recoil();
    }

    protected void PlayFlash()
    {
        flash.Play();
        Sequence flashAnim = DOTween.Sequence();
        flashAnim.Append(flashLight.DOIntensity(flashLightIntencity, 0.15f))
            .Append(flashLight.DOIntensity(0, 0.1f)).Restart();
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
        if (other.CompareTag("EndLevel"))
        {
            canShoot = false;
            gameManager.EndGame(true);
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("pushZone"))
        {
            rb.AddForce(
                other.GetComponent<OutWallPusher>().GetPushForce() + Vector3.up * 0.5f,
                ForceMode.Impulse
            );
        }
    }

    protected virtual void ReloadAmmo()
    {
        ammoLeft = magazineValue;
        UpdateAmmo();
    }

    private void OnDisable()
    {
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
    }
}
