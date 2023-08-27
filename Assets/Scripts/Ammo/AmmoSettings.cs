using UnityEngine;

[CreateAssetMenu(fileName = "BulletSettings", menuName = "BulletSettings", order = 0)]
public class AmmoSettings : ScriptableObject
{
    [SerializeField]
    private Mesh _ammoMesh;

    [SerializeField]
    private float _ammoSpeed;

    [SerializeField]
    private Material _ammoMaterial;

    [SerializeField]
    private TrailSettings _ammoTrail;

    [SerializeField]
    private Ammo.AmmoType _ammoType;

    [SerializeField]
    private float _ammoMass;

    [SerializeField]
    private Vector3 _ammoScale;

    public Ammo.AmmoType AmmoType
    {
        get { return _ammoType; }
    }

    public Material AmmoMaterial
    {
        get { return _ammoMaterial; }
    }

    public Mesh AmmoMesh
    {
        get { return _ammoMesh; }
    }

    public float AmmoSpeed
    {
        get { return _ammoSpeed; }
    }

    public TrailSettings AmmoTrail
    {
        get { return _ammoTrail; }
    }

    public float AmmoMass
    {
        get { return _ammoMass; }
    }
    public Vector3 AmmoScale
    {
        get { return _ammoScale; }
    }
}
