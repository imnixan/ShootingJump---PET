using System.Collections;
using UnityEngine;

public class Pistol : Gun
{
    protected override void Start()
    {
        GunPower = 10f;
        base.Start();
    }
}
