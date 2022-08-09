using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // public or private reference
    // data types in C# (int, float, bool, string)
    // every variable as a name
    // optional value assigned

    // default value of zero. If you forget the f at the end, unity won't conmpile and it will tell you that you have an error
    // for private value, underscore in front of the variable name is .NET standard.
    [SerializeField]
    private float _speed = 4.5f;
    [SerializeField] // by using serialized fielid attribute over any private variable, you will be able to see any private variable in the inspector.
    private GameObject _laserPrefab; // don't forget underscore.
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canfire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    private bool _isTripleshotActive = false;
    [SerializeField]
    private GameObject _TripleShotPrefab;

    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;

    private Coroutine _shieldPowerupCoroutine;
    private Coroutine _speedPowerupCoroutine;
    private Coroutine _tripleShotPowerupCoroutine;

    [SerializeField]
    private int _score;
    private UIManager _uiManager;

    [SerializeField]
    private GameObject _rightEngine, _LeftEngine;

    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start() {
        transform.position = new Vector3(0, -3, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_spawnManager == null) {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManager == null) {
            Debug.LogError("The UI Manager is NULL.");
        }

        if (_audioSource == null) {
            Debug.LogError("AudioSource on the player is NULL.");
        }
        else {
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update() {
        CalculateMovement();

        if (Input.GetKey(KeyCode.Space) && Time.time > _canfire) FireLaser();
    }

    public void AddScore(int points) {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    void CalculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y >= 5.7f) {
            transform.position = new Vector3(transform.position.x, 5.7f, transform.position.z);
        }
        else if (transform.position.y <= -3.65f) {
            transform.position = new Vector3(transform.position.x, -3.65f, transform.position.z);
        }

        if (transform.position.x >= 9.5f) {
            transform.position = new Vector3(-9.5f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -9.5f) {
            transform.position = new Vector3(9.5f, transform.position.y, transform.position.z);
        }
    }

    public void Damage() {

        if (_isShieldActive == true) {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;

        if (_lives == 2) {
            _rightEngine.SetActive(true);
        }
        else if (_lives == 1) {
            _LeftEngine.SetActive(true);
        }


        _uiManager.UpdateLives(_lives);

        if (_lives < 1) {
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    void FireLaser() {
        _canfire = Time.time + _fireRate;

        if (_isTripleshotActive) Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
        else Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        _audioSource.volume = 0.5f;
        _audioSource.Play();
    }

    public void ShieldActive() {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void SpeedActive() {
        _speed = 10f;
        if (_speedPowerupCoroutine != null) StopCoroutine(_speedPowerupCoroutine);
        _speedPowerupCoroutine = StartCoroutine(SpeedPowerDownRoutine());
    }

    public void TripleShotActive() {
        _isTripleshotActive = true;
        if (_tripleShotPowerupCoroutine != null) StopCoroutine(_tripleShotPowerupCoroutine);
        _tripleShotPowerupCoroutine = StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine() {
        yield return new WaitForSeconds(5.0f);
        _speed= 5f;
    }

    IEnumerator TripleShotPowerDownRoutine() {
        yield return new WaitForSeconds(5.0f);
        _isTripleshotActive = false;
    }
}
