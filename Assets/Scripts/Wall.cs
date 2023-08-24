using System.Collections;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem Hit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ContactPoint cp = collision.contacts[0];
            float p = collision.relativeVelocity.magnitude;
            if (p > 15)
            {
                Instantiate(Hit, cp.point, new Quaternion()).Emit(1);
            }
        }
    }
}
