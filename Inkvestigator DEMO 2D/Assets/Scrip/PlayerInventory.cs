using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Max Items")]
    public int _maxNumOfItems;

    [Header("UI")]
    [SerializeField] private ItemUIScript _itemUiScript;

	private void Start()
	{
		_itemUiScript.UpdateNumberOfItems(_maxNumOfItems);
	}

	public void ItemsCollected()
    {
        if (_maxNumOfItems > 0)
            _maxNumOfItems--;
        _itemUiScript.UpdateNumberOfItems(_maxNumOfItems);
	}
}
