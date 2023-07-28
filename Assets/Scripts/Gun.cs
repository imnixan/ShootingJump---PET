using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField]
    protected Bullet bullet;

    [SerializeField]
    protected Transform muzzle;

    protected Rigidbody rb;
    protected GameSpeedChanger gameSpeedChanger;
    protected float GunPower;
    protected bool _falling;
    protected RaycastHit raycastHit;

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
        Falling = true;
        Instantiate(bullet, muzzle.position, new Quaternion()).Init(transform.right);
        CheckTarget();
        Recoil();
    }

    protected void CheckTarget()
    {
        if (Physics.Raycast(muzzle.transform.position, transform.right, out raycastHit))
        {
            if (raycastHit.collider.CompareTag("Enemy"))
            {
                gameSpeedChanger.SlowTime();
            }
            else
            {
                Debug.Log(raycastHit.collider.name);
            }
        }
    }

    protected virtual void Recoil()
    {
        rb.velocity = transform.right * -GunPower;
        rb.angularVelocity = new Vector3(0, 0, GunPower / 3);
    }
}
