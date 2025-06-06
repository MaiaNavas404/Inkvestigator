using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class ItemPickup : MonoBehaviour
{
	public AudioClip pickupSound;           // Assign in Inspector
	public AudioClip _echoSound;           // Assign in Inspector
	public int musicLayerIndex;             // Set this per item in Inspector
	public GameObject _popUp;               // Assign pop-up prefab in Inspector

	private AudioSource audioSource;

	[Header("Ripple")]
	public GameObject _ripple;
	[SerializeField] private bool _isRipple = false;
	private bool _rippleCreated = false;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
	}

	private void Update()
	{
		if (_isRipple && !_rippleCreated)
		{
			_rippleCreated = true;  // prevent further ripples
			_isRipple = false;

			GameObject ripple = Instantiate(_ripple, transform.position, Quaternion.identity);
			RippleScript rippleScript = ripple.GetComponent<RippleScript>();
			rippleScript._isItem = true;
			StartCoroutine(ResetRippleFlag(1f)); // Reset after 1 second

		}
	}

	IEnumerator ResetRippleFlag(float delay)
	{
		yield return new WaitForSeconds(delay);
		_rippleCreated = false;
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

			// Warning(active)    CS0618  'Object.FindObjectOfType<T>()' is obsolete: 'Object.FindObjectOfType has been deprecated. Use Object.FindFirstObjectByType instead or if finding any instance is acceptable the faster Object.FindAnyObjectByType'  Assembly - CSharp C: \Users\ignav\OneDrive\Documentos\GitHub\Inkvestigator\Inkvestigator DEMO 2D\Assets\Scrip\ItemPickup.cs    60

			// --- Activate Music Layer ---
			MusicLayerManager musicManager = FindFirstObjectByType<MusicLayerManager>();
			if (musicManager != null)
			{
				musicManager.ActivateLayer(musicLayerIndex);
			}


			// --- Destroy this item after sound (or immediately if no sound) ---
			float delay = pickupSound != null ? pickupSound.length : 0f;
			Destroy(gameObject, delay);
		}

		if (other.CompareTag("Ripple") && !_isRipple && !_rippleCreated)
		{
			RippleScript rippleScript = other.GetComponent<RippleScript>();
			audioSource.PlayOneShot(_echoSound);

			if (rippleScript._isPlayer)
				_isRipple = true;
		}


	}
}
