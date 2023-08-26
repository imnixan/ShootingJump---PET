using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource sound;

    private static AudioClip[] ShootSounds,
        SleeveSounds;
    private static AudioClip glassBreak;
    public static float Pitch
    {
        set { sound.pitch = value; }
    }

    private void Start()
    {
        sound = gameObject.AddComponent<AudioSource>();
        glassBreak = Resources.Load<AudioClip>("Sounds/GlassBreak");
        InitPistolSounds();
    }

    private void InitPistolSounds()
    {
        ShootSounds = Resources.LoadAll<AudioClip>("Sounds/Pistol/PistolShoot");
        SleeveSounds = Resources.LoadAll<AudioClip>("Sounds/Pistol/PistolSleeve");
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
#if !PLATFORM_STANDALONE_WIN
        Handheld.Vibrate();
#endif
    }

    public static void PlayGlassSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(glassBreak, position);
    }
}
