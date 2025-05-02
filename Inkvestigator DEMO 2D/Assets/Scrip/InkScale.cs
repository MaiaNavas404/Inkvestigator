using UnityEngine;

public class InkScale : MonoBehaviour
{
    public float scale = 1f;

	private void Start()
	{
        transform.localScale = new Vector3(5, scale, 1);

	}
}
