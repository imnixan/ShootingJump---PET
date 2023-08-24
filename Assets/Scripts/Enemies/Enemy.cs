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

    public ParticleSystem Blood;
    private float restoreTime;
    private RagdollSystem ragdollSystem;
    private Animator animator;
    private Dictionary<BodyPartType, float> DamageDic = new Dictionary<BodyPartType, float>
    {
        { BodyPartType.Head, 125 },
        { BodyPartType.Body, 55 },
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
                StopAllCoroutines();
                float damage = _hp - value;
                restoreTime = damage * 0.002f;
                Debug.Log($"Restore time {restoreTime} damage {damage}");
                StartCoroutine(StopAnim(restoreTime));
                _hp = value;
            }
        }
    }

    public enum BodyPartType
    {
        Head,
        Body,
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
        HP -= DamageDic[bodyPart];
        DamageTaken?.Invoke(DamageDic[bodyPart]);
    }

    public void TurnOnAnim()
    {
        animator.enabled = true;
    }

    private IEnumerator StopAnim(float secs)
    {
        ragdollSystem.SaveAnimPos();
        animator.enabled = false;
        yield return new WaitForSeconds(secs);
        if (HP > 0)
        {
            ragdollSystem.RestoreAnimPos(1);
        }
        else
        {
            EnemyKilled?.Invoke();
        }
    }
}
