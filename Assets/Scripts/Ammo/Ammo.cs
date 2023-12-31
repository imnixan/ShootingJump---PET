﻿using System.Collections;
using UnityEngine;

public abstract class Ammo : MonoBehaviour
{
    public enum AmmoType
    {
        Null,
        PistolBullet,
        PistolSleeve,
        M4Bullet,
        M4Sleeve,
        ShotGunSleeve
    }

    protected AmmoType _currenType;
    protected Rigidbody rb;
    protected MeshFilter meshFilter;
    protected MeshRenderer mr;
    protected float ammoSpeed;
    protected TrailRenderer ammoTrailRenderer;
    protected Collider collider;

    protected AmmoPoolManager ammoPool;
    public AmmoType Type
    {
        protected set { _currenType = value; }
        get { return _currenType; }
    }

    public virtual void SetupAmmo(AmmoSettings ammoSettings)
    {
        if (!rb)
        {
            BaseInit();
        }
        meshFilter.mesh = ammoSettings.AmmoMesh;
        Type = ammoSettings.AmmoType;
        rb.mass = ammoSettings.AmmoMass;
        mr.material = ammoSettings.AmmoMaterial;
        transform.localScale = ammoSettings.AmmoScale;
        SetupTrail(ammoSettings.AmmoTrail);
    }

    protected virtual void BaseInit()
    {
        ammoPool = GetComponentInParent<AmmoPoolManager>();
        rb = GetComponentInChildren<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Extrapolate;
        meshFilter = GetComponentInChildren<MeshFilter>();
        mr = GetComponentInChildren<MeshRenderer>();
        ammoTrailRenderer = GetComponentInChildren<TrailRenderer>();
        collider = GetComponentInChildren<Collider>();
    }

    public virtual void Init(Vector3 startPos, Vector3 direction_or_rotation)
    {
        transform.position = startPos;
        ammoTrailRenderer.Clear();
        ammoTrailRenderer.enabled = true;
        mr.enabled = true;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (
            !collision.gameObject.CompareTag("Gun")
            && !collision.gameObject.CompareTag("Enemy")
            && !collision.gameObject.CompareTag("Bullet")
        )
        {
            ammoPool.ReturnnPool(this);
            ammoTrailRenderer.enabled = false;
            AudioManager.PlaySleeveSound(transform.position);
        }
    }

    private void SetupTrail(TrailSettings ammo)
    {
        ammoTrailRenderer.time = ammo.LifeTime;
        ammoTrailRenderer.startWidth = ammo.StartWidth;
        ammoTrailRenderer.endWidth = ammo.EndWidth;
        ammoTrailRenderer.material = ammo.TrailMaterial;
        ammoTrailRenderer.startColor = ammo.StartColor;
        ammoTrailRenderer.endColor = ammo.EndColor;
    }
}
