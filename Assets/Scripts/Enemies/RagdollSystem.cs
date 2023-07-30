using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSystem : MonoBehaviour
{
    private Rigidbody[] rbs;
    private bool ragdollEnabled;

    public void Init()
    {
        rbs = transform.GetComponentsInChildren<Rigidbody>();
        TurnOffRb();
    }

    public void TurnOnRb()
    {
        if (!ragdollEnabled)
        {
            foreach (var rb in rbs)
            {
                rb.isKinematic = false;
                rb.interpolation = RigidbodyInterpolation.Interpolate;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                rb.tag = "Enemy";
            }
            ragdollEnabled = true;
        }
    }

    private void TurnOffRb()
    {
        foreach (var rb in rbs)
        {
            rb.isKinematic = true;
        }
        ragdollEnabled = false;
    }
}
