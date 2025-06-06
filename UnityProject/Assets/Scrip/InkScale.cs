using UnityEngine;

public class InkScale : MonoBehaviour
{
    public float _scale = 1f;

	private void Start()
	{
        transform.localScale = new Vector3(5, _scale, 1);

	}
}
