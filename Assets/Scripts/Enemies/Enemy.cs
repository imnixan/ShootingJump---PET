using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public static event UnityAction<float> DamageTaken;
    public static event UnityAction EnemyKilled;

    [SerializeField]
    private float HP;

    public ParticleSystem Blood;

    private RagdollSystem ragdollSystem;

    private Dictionary<BodyPartType, float> DamageDic = new Dictionary<BodyPartType, float>
    {
        { BodyPartType.Head, 125 },
        { BodyPartType.Body, 55 },
        { BodyPartType.Leg, 30 },
        { BodyPartType.Arm, 20 }
    };

    public enum BodyPartType
    {
        Head,
        Body,
        Arm,
        Leg
    }

    private void Start()
    {
        ragdollSystem = gameObject.AddComponent<RagdollSystem>();
        ragdollSystem.Init(this);
    }

    public void TakeDamage(BodyPartType bodyPart)
    {
        ragdollSystem.SetBodyGravity(true);
        HP -= DamageDic[bodyPart];
        DamageTaken?.Invoke(DamageDic[bodyPart]);
        if (HP <= 0)
        {
            EnemyKilled?.Invoke();
            ragdollSystem.UnlockBody();
        }
    }
}
