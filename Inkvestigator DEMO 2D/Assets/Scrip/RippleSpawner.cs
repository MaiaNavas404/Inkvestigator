using UnityEngine;

using UnityEngine;
using System.Collections.Generic;

public class RippleSpawner : MonoBehaviour
{
	[SerializeField] private GameObject _ripple;
	[SerializeField] private Transform _player;  // Use Transform for easier position access
	[SerializeField] private List<GameObject> _Trash;

	public float _spawnInterval = 60f;  // 1 minute
	[SerializeField] private float _timer = 0f;

	private void Update()
	{
		_timer += Time.deltaTime;
		if (_timer >= _spawnInterval)
		{
			_timer = 0f;
			SpawnRippleAtClosestTrash();
		}
	}

	private void SpawnRippleAtClosestTrash()
	{
		if (_Trash == null || _Trash.Count == 0) return;

		GameObject closestTrash = null;
		float closestDistance = Mathf.Infinity;

		foreach (GameObject trash in _Trash)
		{
			if (trash == null) continue; // skip nulls if any

			float dist = Vector3.Distance(_player.position, trash.transform.position);
			if (dist < closestDistance)
			{
				closestDistance = dist;
				closestTrash = trash;
			}
		}

		if (closestTrash != null)
		{
			Instantiate(_ripple, closestTrash.transform.position, Quaternion.identity);
		}
	}
}

