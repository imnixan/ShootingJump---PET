using System.Collections;
using UnityEngine;

public abstract class Ammo : MonoBehaviour
{
    public enum AmmoType
    {
        Null,
        PistolBullet,
        PistolSleeve
    }

    protected AmmoType _currenType;
    protected Rigidbody rb;
    protected MeshFilter meshFilter;
    protected MeshRenderer mr;
    protected float ammoSpeed;
    protected TrailRenderer ammoTrailRenderer;
    protected Collider collider;

    private AmmoPoolManager ammoPool;
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
        SetupTrail(ammoSettings.AmmoTrail);
    }

    protected virtual void BaseInit()
    {
        ammoPool = GetComponentInParent<AmmoPoolManager>();
        rb = GetComponent<Rigidbody>();
        meshFilter = GetComponent<MeshFilter>();
        mr = GetComponent<MeshRenderer>();
        ammoTrailRenderer = GetComponent<TrailRenderer>();
    }

    public virtual void Init(Vector3 startPos, Vector3 direction_or_rotation)
    {
        transform.position = startPos;
        ammoTrailRenderer.Clear();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Gun"))
        {
            ammoPool.ReturnnPool(this);
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
