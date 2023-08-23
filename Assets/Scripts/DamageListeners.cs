using System.Collections;
using UnityEngine;

public abstract class DamageListeners : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        Enemy.DamageTaken += OnDamageTaken;
        Enemy.EnemyKilled += OnEnemyKill;
    }

    protected abstract void OnDamageTaken(float damage);

    protected abstract void OnEnemyKill();
}
