using System.Collections;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    private Enemy.BodyPartType bodyPartType;

    private Enemy enemy;
    private Rigidbody rb;

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.tag = "Enemy";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            enemy.TakeDamage(bodyPartType);
            Destroy(collision.gameObject);
        }
    }

    public void SetPhysic(bool physic)
    {
        rb.isKinematic = !physic;
    }
}
