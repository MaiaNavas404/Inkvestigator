using UnityEngine;

public class CamaraFollorplayer : MonoBehaviour
{
	public Transform _playerTarget;
	[SerializeField] private CharcaterControllerIgnacio _playerController;
	[SerializeField] private float _revealDuration = 3f;
	[SerializeField] private float _normalSize = 13.9f;
	[SerializeField] private float _transitionDuration = 2f;
	[SerializeField] private GameObject _startBackround;

	private float _revealTimer;
	private float _transitionTimer;

	private bool _isStart = true;
	private bool _isTransitioning = false;
	private bool _isFollowingPlayer = false;

	private Camera _camera;
	private Vector3 _startPos;
	private float _startSize;

	void Start()
	{
		_camera = Camera.main;
		_revealTimer = _revealDuration;
		_playerController._isPaused = true;
	}

	void Update()
	{
		if (_isStart && !_isTransitioning && !_isFollowingPlayer)
		{
			_revealTimer -= Time.deltaTime;
			if (_revealTimer <= 0f)
			{
				// Begin transition
				_isTransitioning = true;
				_isStart = false;
				_transitionTimer = 0f;
				_startPos = transform.position;
				_startSize = _camera.orthographicSize;
			}
		}
		else if (_isTransitioning)
		{
			_transitionTimer += Time.deltaTime;
			float t = Mathf.Clamp01(_transitionTimer / _transitionDuration);

			transform.position = Vector3.Lerp(_startPos, new Vector3(_playerTarget.position.x, _playerTarget.position.y, -200f), t);
			_camera.orthographicSize = Mathf.Lerp(_startSize, _normalSize, t);

			if (t >= 1f)
			{
				_isTransitioning = false;
				_isFollowingPlayer = true;
			}
		}
		else if (_isFollowingPlayer)
		{
			_startBackround.SetActive(false);
			Vector3 pos = transform.position;
			pos.x = _playerTarget.position.x;
			pos.y = _playerTarget.position.y;
			pos.z = -200f;
			transform.position = pos;
			_playerController._isPaused = false;
		}
	}
}