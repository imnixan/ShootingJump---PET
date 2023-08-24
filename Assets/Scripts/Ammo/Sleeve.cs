using System.Collections;
using UnityEngine;

public class Sleeve : Ammo
{
    public override void Init(Vector3 startPos, Vector3 rotation)
    {
        base.Init(startPos, rotation);
        transform.eulerAngles = rotation;
        rb.velocity = Vector3.up * 4;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
