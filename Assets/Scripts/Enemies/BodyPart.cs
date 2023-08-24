using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    private Enemy.BodyPartType bodyPartType;

    private Enemy enemy;
    private Rigidbody rb;
    private Vector3 savedPos;
    private Quaternion savedRotation;

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
            enemy.TakeDamage(bodyPartType);
            Destroy(collision.gameObject);
            Instantiate(enemy.Blood, collision.GetContact(0).point, new Quaternion());
        }
    }

    public void SavePos()
    {
        savedPos = transform.localPosition;
        savedRotation = transform.localRotation;
    }

    public void RestorePos(float secs)
    {
        transform.DOLocalMove(savedPos, secs).Play();
        transform.DOLocalRotateQuaternion(savedRotation, secs).Play();
    }
}
