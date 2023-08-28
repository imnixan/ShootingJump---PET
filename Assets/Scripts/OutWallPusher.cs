using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutWallPusher : MonoBehaviour
{
    [SerializeField]
    private PushSide pushSide;

    public Rigidbody pushRb;

    private Dictionary<PushSide, Vector3> pushDirection = new Dictionary<PushSide, Vector3>()
    {
        { PushSide.Left, Vector3.left * 0.1f },
        { PushSide.Right, Vector3.right * 0.1f }
    };

    private enum PushSide
    {
        Left,
        Right
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + "in");
        if (other.CompareTag("Gun"))
        {
            Debug.Log("tryin get gun");
            pushRb = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag + "out");
        if (other.CompareTag("Gun"))
        {
            pushRb = null;
        }
    }

    private void Update()
    {
        if (pushRb)
        {
            pushRb.AddForce(pushDirection[pushSide], ForceMode.Impulse);
        }
    }
}
