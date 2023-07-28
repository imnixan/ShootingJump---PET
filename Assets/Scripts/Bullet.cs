using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float bulletSpeed;

    public virtual void Init(Vector2 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        transform.up = direction;
        rb.velocity = transform.up * bulletSpeed;
    }
}
