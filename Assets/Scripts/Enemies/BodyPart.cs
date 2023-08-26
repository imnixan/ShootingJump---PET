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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioManager.Vibrate();
            AudioManager.PlayGlassSound(collision.GetContact(0).point);
            enemy.TakeDamage(bodyPartType);
            if (breakable && !shooted)
            {
                Instantiate(explode, collision.GetContact(0).point, new Quaternion());
                breakable.BreakObject(collision.GetContact(0).point);
                if (bodyPartType != Enemy.BodyPartType.Body)
                {
                    transform.localScale = Vector3.zero;
                }
                shooted = true;
            }
        }
    }
}
