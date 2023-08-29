using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutWallPusher : MonoBehaviour
{
    [SerializeField]
    private PushSide pushSide;

    private Dictionary<PushSide, Vector3> pushDirection = new Dictionary<PushSide, Vector3>()
    {
        { PushSide.Left, Vector3.left * 1f },
        { PushSide.Right, Vector3.right * 1f }
    };

    private enum PushSide
    {
        Left,
        Right
    }

    public Vector3 GetPushForce()
    {
        return pushDirection[pushSide];
    }
}
