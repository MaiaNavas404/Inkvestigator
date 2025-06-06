using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Max Items")]
    public int _maxNumOfItems;

	public void ItemsCollected()
    {
        if (_maxNumOfItems > 0)
            _maxNumOfItems--;
	}
}
