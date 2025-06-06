using UnityEngine;
using System.Collections;

public class MusicLayerManager : MonoBehaviour
{
    public AudioSource[] musicLayers; //  drag 8 layers here in Inspector

    void Start()
    {
        foreach (AudioSource layer in musicLayers)
        {
            layer.volume = 0f;
            layer.loop = true;
            layer.Play(); // All layers silent until triggered
        }
    }

    public void ActivateLayer(int index)
    {
        if (index >= 0 && index < musicLayers.Length)
        {
            StartCoroutine(FadeIn(musicLayers[index], 1f, 2f));
        }
        else
        {
            Debug.LogWarning($"Layer index {index} out of bounds.");
        }
    }

    private IEnumerator FadeIn(AudioSource audioSource, float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}