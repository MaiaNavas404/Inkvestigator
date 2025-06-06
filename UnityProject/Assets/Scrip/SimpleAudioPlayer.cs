using UnityEngine;

public class SimpleAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;  // Drag your AudioSource here in the Inspector

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();  // Plays the assigned AudioClip
        }
        else
        {
            Debug.LogWarning("AudioSource not assigned.");
        }
    }
}
