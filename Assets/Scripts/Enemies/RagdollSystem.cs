using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSystem : MonoBehaviour
{
    private BodyPart[] bodyParts;
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

    public void SaveAnimPos()
    {
        foreach (var body in bodyParts)
        {
            body.SavePos();
        }
    }

    public void RestoreAnimPos(float secs)
    {
        StartCoroutine(RestoreAnimCoroutine(secs));
    }

    private IEnumerator RestoreAnimCoroutine(float secs)
    {
        foreach (var body in bodyParts)
        {
            body.RestorePos(secs);
        }
        yield return new WaitForSeconds(secs);
        enemy.TurnOnAnim();
    }
}
