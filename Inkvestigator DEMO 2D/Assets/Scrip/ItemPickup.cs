using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

			if (playerInventory != null)
			{
				playerInventory.ItemsCollected();
				gameObject.SetActive(false);
			}

			Debug.Log("Has Hit the Player");
		}
	}
}
