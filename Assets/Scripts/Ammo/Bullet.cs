using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Ammo
{
    public override void SetupAmmo(AmmoSettings ammoSettings)
    {
        base.SetupAmmo(ammoSettings);
        ammoSpeed = ammoSettings.AmmoSpeed;
    }

    public override void Init(Vector3 startPos, Vector3 direction)
    {
        base.Init(startPos, direction);
        transform.up = direction;
        rb.velocity = transform.up * ammoSpeed;
        collider.enabled = true;
        rb.angularVelocity = new Vector3(0, ammoSpeed, 0);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ammoPool.ReturnnPool(this);
            mr.enabled = false;
            collider.enabled = false;
            ammoTrailRenderer.enabled = false;
        }
    }
}
