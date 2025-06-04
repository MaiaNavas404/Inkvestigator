using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public AudioClip pickupSound; // Assign this in the Inspector
	private AudioSource audioSource;
	public int musicLayerIndex;

	public GameObject _popUp;

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

				//  Trigger the music layer
				FindObjectOfType<MusicLayerManager>().ActivateLayer(musicLayerIndex);

				Destroy(gameObject, pickupSound != null ? pickupSound.length : 0f);
			}

			// Instantiate the popup at the item's position (pop-up is parent)
			GameObject popUpInstance = Instantiate(_popUp, transform.position, Quaternion.identity);

			if (popUpInstance != null)
			{
				// Get the SpriteRenderer component from the item
				SpriteRenderer itemSpriteRenderer = GetComponent<SpriteRenderer>();
				if (itemSpriteRenderer != null)
				{
					// Get the SpriteRenderer from the pop-up prefab to change the sprite
					SpriteRenderer popUpSpriteRenderer = popUpInstance.GetComponentInChildren<SpriteRenderer>();
					if (popUpSpriteRenderer != null)
					{
						// Change the sprite of the pop-up sprite renderer to the item's sprite
						popUpSpriteRenderer.sprite = itemSpriteRenderer.sprite;
					}
					else
					{
						Debug.LogError("No SpriteRenderer found in the pop-up prefab!");
					}
				}
				else
				{
					Debug.LogError("No SpriteRenderer found on the item!");
				}
			}

			if (playerInventory != null)
			{
				playerInventory.ItemsCollected();

				if (pickupSound != null)
				{
					audioSource.PlayOneShot(pickupSound);
				}

				// Destroy the item after the sound duration, but do not destroy the popup
				Destroy(gameObject, pickupSound != null ? pickupSound.length : 0f);
			}
		}
	}
}
