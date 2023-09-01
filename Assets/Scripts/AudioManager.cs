using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource sound;

    private static AudioClip[] ShootSounds,
        SleeveSounds;
    private static AudioClip glassBreak;

    private Dictionary<int, string> SoundHash = new Dictionary<int, string>()
    {
        { 0, "Sounds/Pistol/PistolShoot" },
        { 1, "Sounds/Revolver" },
        { 2, "Sounds/Kriss" },
        { 3, "Sounds/Ak" },
        { 4, "Sounds/ShotGun/ShotGunShoot" }
    };
    private Dictionary<int, string> SleeveSoundHash = new Dictionary<int, string>()
    {
        { 0, "Sounds/Pistol/PistolSleeve" },
        { 1, "Sounds/Pistol/PistolSleeve" },
        { 2, "Sounds/Pistol/PistolSleeve" },
        { 3, "Sounds/Pistol/PistolSleeve" },
        { 4, "Sounds/ShotGun/ShotGunSleeve" }
    };

    public static float Pitch
    {
        set { sound.pitch = value; }
    }

    private void Start()
    {
        sound = gameObject.AddComponent<AudioSource>();
        glassBreak = Resources.Load<AudioClip>("Sounds/GlassBreak");
        InitSounds();
    }

    private void InitSounds()
    {
        ShootSounds = Resources.LoadAll<AudioClip>(SoundHash[PlayerPrefs.GetInt("CurrentGun")]);
        SleeveSounds = Resources.LoadAll<AudioClip>(
            SleeveSoundHash[PlayerPrefs.GetInt("CurrentGun")]
        );
    }

    public static void PlayShootSound()
    {
        sound.PlayOneShot(ShootSounds.GetRandomElement());
    }

    public static void PlaySleeveSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(SleeveSounds.GetRandomElement(), position);
    }

    public static void PlaySound(AudioClip clip)
    {
        sound.PlayOneShot(clip);
    }

    public static void Vibrate()
    {
#if !PLATFORM_STANDALONE_WIN && !PLATFORM_WEBGL
        Handheld.Vibrate();
#endif
    }

    public static void PlayGlassSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(glassBreak, position);
    }
}
