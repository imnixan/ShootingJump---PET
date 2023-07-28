using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedChanger : MonoBehaviour
{
    private const float MaxGameSpeed = 1;
    private const float MinGameSpeed = 0.05f;
    private const float RestoreSpeed = 0.002f;
    private const float DecreaseScale = 2;
    public float _currentGameSpeed;
    private float CurrentGameSpeed
    {
        get { return _currentGameSpeed; }
        set
        {
            _currentGameSpeed = value;
            if (_currentGameSpeed < MinGameSpeed)
            {
                _currentGameSpeed = MinGameSpeed;
            }
            if (_currentGameSpeed > MaxGameSpeed)
            {
                _currentGameSpeed = MaxGameSpeed;
            }
            Time.timeScale = _currentGameSpeed;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
        CurrentGameSpeed = MaxGameSpeed;
    }

    public void SlowTime()
    {
        CurrentGameSpeed -= CurrentGameSpeed / DecreaseScale;
    }

    private void LateUpdate()
    {
        CurrentGameSpeed += RestoreSpeed;
    }
}
