using UnityEngine;

public class CeilingFan : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float variation = 10f;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        currentSpeed = speed + Mathf.Sin(Time.time) * variation;
        transform.Rotate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}