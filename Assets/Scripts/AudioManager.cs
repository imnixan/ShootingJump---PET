using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource sound;

    public static float Pitch
    {
        set { sound.pitch = value; }
    }

    private void Start()
    {
        sound = gameObject.AddComponent<AudioSource>();
    }

    public static void PlaySound(AudioClip clip)
    {
        sound.PlayOneShot(clip);
    }
}
