using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedChanger : MonoBehaviour
{
    private const float MaxGameSpeed = 1;
    private const float SlowMoSpeed = 0.1f;
    public float _currentGameSpeed;
    private float CurrentGameSpeed
    {
        get { return _currentGameSpeed; }
        set
        {
            _currentGameSpeed = value;
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
        CurrentGameSpeed = SlowMoSpeed;
    }

    private void FixedUpdate()
    {
        CurrentGameSpeed += Time.fixedDeltaTime;
    }
}
