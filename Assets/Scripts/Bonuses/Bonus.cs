using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private Dictionary<int, string> bonusModels = new Dictionary<int, string>()
    {
        { 0, "Prefabs/PistolMagazin" },
        { 1, "Prefabs/RevolverDrum" },
        { 2, "Prefabs/KrissMagazin" },
        { 3, "Prefabs/AKMagazin" },
        { 4, "Prefabs/ShotGunMagazin" }
    };

    private Transform model;

    private void Start()
    {
        gameObject.tag = "Ammo";
        int gunType = PlayerPrefs.GetInt("CurrentGun");
        model = Instantiate(Resources.Load<GameObject>(bonusModels[gunType]), transform).transform;
    }

    private void FixedUpdate()
    {
        model.Rotate(0, 1f, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            Destroy(gameObject);
        }
    }
}
