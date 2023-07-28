using System.Collections;
using UnityEngine;

public class PistolBullet : Bullet
{
    public override void Init(Vector2 direction)
    {
        bulletSpeed = 10f;
        base.Init(direction);
    }
}
