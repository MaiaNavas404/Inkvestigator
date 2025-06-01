using UnityEngine;

public class CamaraFollorplayer : MonoBehaviour
{
	public Transform _playerTarget;
	[SerializeField] private CharcaterControllerIgnacio _playerController;
	[SerializeField] private float _revealDuration = 3f;
	[SerializeField] private float _normalSize = 13.9f;
	[SerializeField] private float _transitionDuration = 2f;
	[SerializeField] private GameObject _startBackround;
	[SerializeField] private GameObject _topFog;

	public bool _skipIntro = false;

	// Win sequence
	private bool _winTransitionRequested = false;
	private Transform _winFocusPoint;
	private float _winPauseDuration = 2f;

	private float _revealTimer;
	private float _transitionTimer;

	private bool _isStart = true;
	private bool _isTransitioning = false;
	private bool _isFollowingPlayer = false;
	private bool _isWinMoving = false;
	private bool _isWinReturning = false;
	private float _winTimer = 0f;

	private Camera _camera;
	private Vector3 _startPos;
	private float _startSize;
	private Vector3 _winStartPos;
	private float _winStartSize;

	public GameObject _door;

	void Start()
	{
		_camera = Camera.main;

		if (_skipIntro)
		{
			FollowPlayerImmediately();
			return;
		}

		_revealTimer = _revealDuration;
		_playerController._isPaused = true;
	}

	void Update()
	{
		// Handle win sequence if requested
		if (_winTransitionRequested)
		{
			HandleWinSequence();
			return;
		}

		// Original intro logic
		if (_isFollowingPlayer)
		{
			FollowPlayer();
			return;
		}

		if (_skipIntro)
		{
			FollowPlayerImmediately();
			return;
		}

		if (_isStart && !_isTransitioning)
		{
			_revealTimer -= Time.deltaTime;
			if (_revealTimer <= 0f)
			{
				// Begin intro transition
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
	}

	private void HandleWinSequence()
	{
		// First move to win focus point
		if (_isWinMoving)
		{
			_transitionTimer += Time.deltaTime;
			float t = Mathf.Clamp01(_transitionTimer / _transitionDuration);
			transform.position = Vector3.Lerp(_winStartPos, new Vector3(_winFocusPoint.position.x, _winFocusPoint.position.y, -200f), t);
			if (t >= 1f)
			{
				_isWinMoving = false;
				_winTimer = 0f;
			}
		}
		// Then pause at win point
		else if (!_isWinReturning)
		{
			_winTimer += Time.deltaTime;
			if (_winTimer >= _winPauseDuration)
			{
				// Make Door Desapear
				if (_winTimer >= 0.35f)
					_door.SetActive(false);

				// Begin return to player
				_isWinReturning = true;
				_transitionTimer = 0f;
				_winStartPos = transform.position;
			}
		}
		// Finally return to player
		else
		{
			_transitionTimer += Time.deltaTime;
			float t = Mathf.Clamp01(_transitionTimer / _transitionDuration);
			transform.position = Vector3.Lerp(_winStartPos, new Vector3(_playerTarget.position.x, _playerTarget.position.y, -200f), t);
			if (t >= 1f)
			{
				_isWinReturning = false;
				_winTransitionRequested = false;
				_isFollowingPlayer = true;
				_playerController._isPaused = false;
			}
		}
	}

	void FollowPlayer()
	{
		_startBackround.SetActive(false);
		_topFog.SetActive(true);
		Vector3 pos = transform.position;
		pos.x = _playerTarget.position.x;
		pos.y = _playerTarget.position.y;
		pos.z = -200f;
		transform.position = pos;
		_playerController._isPaused = false;
	}

	void FollowPlayerImmediately()
	{
		_isStart = false;
		_isTransitioning = false;
		_isFollowingPlayer = true;
		_camera.orthographicSize = _normalSize;
		transform.position = new Vector3(_playerTarget.position.x, _playerTarget.position.y, -200f);
		_startBackround.SetActive(false);
		_topFog.SetActive(true);
		_playerController._isPaused = false;
	}

	// Called by GameController when win condition met
	public void StartWinTransition(Transform winFocus)
	{
		if (_winTransitionRequested) return;
		_winTransitionRequested = true;
		_isFollowingPlayer = false;
		_isWinMoving = true;
		_isWinReturning = false;
		_winFocusPoint = winFocus;
		_transitionTimer = 0f;
		_winStartPos = transform.position;
		_playerController._isPaused = true;
	}
}
