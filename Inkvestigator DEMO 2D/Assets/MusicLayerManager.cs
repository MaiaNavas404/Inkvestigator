using UnityEngine;
using System.Collections;

public class MusicLayerManager : MonoBehaviour
{
    public AudioSource[] musicLayers;

    private void Start()
    {
        foreach (AudioSource layer in musicLayers)
        {
            layer.volume = 0f;
            layer.loop = true;
            layer.Play(); // All layers play silently until activated
        }
    }

    public void ActivateLayer(int layerIndex)
    {
        if (layerIndex >= 0 && layerIndex < musicLayers.Length)
        {
            StartCoroutine(FadeIn(musicLayers[layerIndex], 1f, 2f));
        }
        else
        {
            Debug.LogWarning($"Invalid music layer index: {layerIndex}");
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
