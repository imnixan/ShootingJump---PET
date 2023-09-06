using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    private Enemy.BodyPartType bodyPartType;

    [SerializeField]
    private ParticleSystem explode;

    [SerializeField]
    private BreakableObject breakable;

    private Enemy enemy;
    private Rigidbody rb;
    private bool shooted;

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.tag = "Enemy";
        if (bodyPartType == Enemy.BodyPartType.Body)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint cp = collision.contacts[0];
        float p = collision.relativeVelocity.magnitude;
        if (p > 15)
            if (collision.gameObject.CompareTag("Bullet") && p > 15)
            {
                AudioManager.Vibrate();
                if (GetEnemyHp() > 0)
                {
                    enemy.TakeDamage(bodyPartType);
                }
                if (breakable && !shooted)
                {
                    Instantiate(explode, collision.GetContact(0).point, new Quaternion());
                    breakable.BreakObject(collision.GetContact(0).point);
                    rb.AddForceAtPosition(collision.impulse, collision.GetContact(0).point);
                    if (bodyPartType != Enemy.BodyPartType.Body)
                    {
                        transform.localScale = Vector3.zero;
                    }
                    shooted = true;
                }
            }
    }

    public int GetEnemyHp()
    {
        return enemy.HP;
    }

    public void TurnOffKinematic()
    {
        rb.isKinematic = false;
    }
}
