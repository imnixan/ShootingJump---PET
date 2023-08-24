using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSystem : MonoBehaviour
{
    public BodyPart[] bodyParts;
    private bool ragdollEnabled;
    private Enemy enemy;

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        bodyParts = GetComponentsInChildren<BodyPart>();
        InitBodyParts();
    }

    private void InitBodyParts()
    {
        foreach (var bodyPart in bodyParts)
        {
            bodyPart.Init(enemy);
        }
    }

    public void UnlockBody()
    {
        foreach (var bodyPart in bodyParts)
        {
            bodyPart.UnLockAxis();
        }
    }

    public void SetBodyGravity(bool turnedOn)
    {
        foreach (var bodyPart in bodyParts)
        {
            bodyPart.SetGravity(turnedOn);
        }
    }
}
