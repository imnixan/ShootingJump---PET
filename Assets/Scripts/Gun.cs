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

    protected virtual void Start()
    {
        gameSpeedChanger = FindAnyObjectByType<GameSpeedChanger>();
        rb = GetComponent<Rigidbody>();
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
        Instantiate(bullet, muzzle.position, new Quaternion()).Init(transform.right);
        Recoil();
        gameSpeedChanger.SlowTime();
    }

    protected virtual void Recoil()
    {
        rb.AddForce(transform.right * -GunPower, ForceMode.Impulse);
        rb.angularVelocity = new Vector3(0, 0, GunPower / 2);
    }
}
