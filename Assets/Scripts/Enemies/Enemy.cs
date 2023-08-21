using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private RagdollSystem ragdollSystem;

    private void Start()
    {
        ragdollSystem = gameObject.AddComponent<RagdollSystem>();
        ragdollSystem.Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ragdollSystem.TurnOnRb();
        }
        else
        {
            Debug.Log($"{other.gameObject.tag} - {other.gameObject.name} HIT ME");
        }
    }
}
