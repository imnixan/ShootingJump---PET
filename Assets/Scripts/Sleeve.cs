using System.Collections;
using UnityEngine;

public class Sleeve : MonoBehaviour
{
    public void Init(Vector3 rotation)
    {
        transform.eulerAngles = rotation;
    }
}
