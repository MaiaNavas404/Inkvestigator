using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public AudioClip pickupSound; // Assign this in the Inspector
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource if the object doesn't already have one
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

			if (playerInventory != null)
			{
				playerInventory.ItemsCollected();
                if (pickupSound != null)
                {
                    audioSource.PlayOneShot(pickupSound);
                }

                Destroy(gameObject, pickupSound != null ? pickupSound.length : 0f);
			}
		}
	}
}
