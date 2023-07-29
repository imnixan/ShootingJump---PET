using UnityEngine;

[CreateAssetMenu(fileName = "TrailSettings", menuName = "TrailSettings", order = 0)]
public class TrailSettings : ScriptableObject
{
    [SerializeField]
    private float _lifeTime,
        _startWidth,
        _endWidth;

    [SerializeField]
    private Material _trailMaterial;

    [SerializeField]
    private Color _startColor,
        _endColor;

    public Color StartColor
    {
        get { return _startColor; }
    }

    public Color EndColor
    {
        get { return _endColor; }
    }

    public float LifeTime
    {
        get { return _lifeTime; }
    }

    public float StartWidth
    {
        get { return _startWidth; }
    }

    public float EndWidth
    {
        get { return _endWidth; }
    }

    public Material TrailMaterial
    {
        get { return _trailMaterial; }
    }
}
