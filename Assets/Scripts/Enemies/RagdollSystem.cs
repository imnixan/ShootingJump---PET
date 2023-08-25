using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSystem : MonoBehaviour
{
    private BodyPart[] bodyParts;
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
}
