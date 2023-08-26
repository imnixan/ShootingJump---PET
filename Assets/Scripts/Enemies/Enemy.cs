using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public static event UnityAction<float> DamageTaken;
    public static event UnityAction EnemyKilled;

    [SerializeField]
    private float _hp;

    private RagdollSystem ragdollSystem;
    private Animator animator;
    private Dictionary<BodyPartType, float> DamageDic = new Dictionary<BodyPartType, float>
    {
        { BodyPartType.Head, 125 },
        { BodyPartType.Body, 55 },
        { BodyPartType.Chest, 100 },
        { BodyPartType.Leg, 30 },
        { BodyPartType.Arm, 20 }
    };

    private float HP
    {
        get { return _hp; }
        set
        {
            if (_hp > 0)
            {
                _hp = value;
                if (_hp == 0)
                {
                    EnemyKilled?.Invoke();
                }
            }
        }
    }

    public enum BodyPartType
    {
        Head,
        Body,
        Chest,
        Arm,
        Leg
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        ragdollSystem = gameObject.AddComponent<RagdollSystem>();
        ragdollSystem.Init(this);
    }

    public void TakeDamage(BodyPartType bodyPart)
    {
        Destroy(animator);
        HP -= DamageDic[bodyPart];
        DamageTaken?.Invoke(DamageDic[bodyPart]);
    }
}
