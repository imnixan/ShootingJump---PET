using System.Collections;
using UnityEngine;

public class PistolBullet : Bullet
{
    public override void Init(Vector2 direction)
    {
        bulletSpeed = 30f;
        base.Init(direction);
    }
}
