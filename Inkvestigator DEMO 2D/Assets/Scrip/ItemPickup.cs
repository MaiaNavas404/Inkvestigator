using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public AudioClip pickupSound;           // Assign in Inspector
    public int musicLayerIndex;             // Set this per item in Inspector
    public GameObject _popUp;               // Assign pop-up prefab in Inspector

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

            // --- Show Pop-up ---
            if (_popUp != null)
            {
                GameObject popUpInstance = Instantiate(_popUp, transform.position, Quaternion.identity);

                SpriteRenderer itemSpriteRenderer = GetComponent<SpriteRenderer>();
                SpriteRenderer popUpSpriteRenderer = popUpInstance.GetComponentInChildren<SpriteRenderer>();

                if (itemSpriteRenderer != null && popUpSpriteRenderer != null)
                {
                    popUpSpriteRenderer.sprite = itemSpriteRenderer.sprite;
                }
                else
                {
                    Debug.LogWarning("Missing SpriteRenderers for popup!");
                }
            }

            // --- Update Player Inventory ---
            if (playerInventory != null)
            {
                playerInventory.ItemsCollected();
            }

            // --- Play Pickup Sound ---
            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            // --- Activate Music Layer ---
            MusicLayerManager musicManager = FindObjectOfType<MusicLayerManager>();
            if (musicManager != null)
            {
                musicManager.ActivateLayer(musicLayerIndex);
            }
            else
            {
                Debug.LogWarning("No MusicLayerManager found in the scene.");
            }

            // --- Destroy this item after sound (or immediately if no sound) ---
            float delay = pickupSound != null ? pickupSound.length : 0f;
            Destroy(gameObject, delay);
        }
    }
}
