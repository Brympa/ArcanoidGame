using UnityEngine;

public class BallController : MonoBehaviour
{
    public float BallSpeed = 8f;
    public Transform Platform;

    private Vector2 _direction;
    private bool _isLaunched = false;
    private float _ballToPlatformDistance = 0.3f;

    [SerializeField] private Rigidbody2D _rig;
    private AudioSource _audioSource;
    private GameManager _gameManager;

    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (!_isLaunched && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !_gameManager.IsWindowOpen)
        {
            LaunchBall();
        }

        if (!_isLaunched)
        {
            FollowPlatform();
        }
    }

    void FixedUpdate()
    {
        if (_isLaunched)
        {
            _rig.velocity = _direction * BallSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_audioSource != null && _audioSource.clip != null)
        {
            _audioSource.Play();
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            ContactPoint2D contact = collision.contacts[0];

            float hitPoint = contact.point.x - collision.gameObject.transform.position.x;
            float platformWidth = collision.collider.bounds.size.x;
            float hitFactor = hitPoint / (platformWidth / 2);

            _direction = new Vector2(hitFactor, 1).normalized;
        }
        else if (collision.gameObject.CompareTag("DeathZone"))
        {
            _gameManager.LoseLife();
            _isLaunched = false;
            ConnectToPlatform();
        }
        else
        {
            _direction = Vector2.Reflect(_direction, collision.contacts[0].normal);
        }
    }

    public void ConnectToPlatform()
    {
        transform.position = new Vector3(Platform.position.x, Platform.position.y + _ballToPlatformDistance, 0);
        _direction = Vector3.up;
        _rig.velocity = _direction * 0;
    }

    private void FollowPlatform()
    {
        transform.position = new Vector3(Platform.position.x, Platform.position.y + _ballToPlatformDistance, 0);
        _rig.isKinematic = true;
    }

    private void LaunchBall()
    {
        _isLaunched = true;
        _rig.isKinematic = false;
        _direction = Vector2.up;
        _rig.velocity = _direction * BallSpeed * Time.deltaTime;
    }
}