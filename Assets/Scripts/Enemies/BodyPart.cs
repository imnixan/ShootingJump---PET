using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    private Enemy.BodyPartType bodyPartType;

    private BreakableObject breakable;

    private Enemy enemy;
    private Rigidbody rb;

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.tag = "Enemy";
        breakable = GetComponentInChildren<BreakableObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("HIT");
            enemy.TakeDamage(bodyPartType);
            if (breakable)
            {
                breakable.BreakObject(collision.GetContact(0).point);
                transform.localScale = Vector3.zero;
            }
        }
    }
}
