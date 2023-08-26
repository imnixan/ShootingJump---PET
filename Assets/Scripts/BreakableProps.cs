using System.Collections;
using UnityEngine;

public class BreakableProps : Enemy
{
    [SerializeField]
    private int scores;

    [SerializeField]
    private ParticleSystem explode;

    private void OnCollisionEnter(Collision collision)
    {
        if (
            (collision.gameObject.CompareTag("Bullet") && collision.impulse.magnitude > 15)
            || collision.impulse.magnitude > 15
        )
        {
            InvokeDamageTaken(scores);
            Instantiate(explode, collision.GetContact(0).point, new Quaternion());
            GetComponent<BreakableObject>().BreakObject(collision.GetContact(0).point);
            Destroy(gameObject);
        }
    }
}
