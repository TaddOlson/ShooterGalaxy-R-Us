﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _thruster;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldsActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _speedBoostVisualizer;

    [SerializeField]
    private GameObject _rightEngine, _leftEngine;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;
    
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the Player is NULL.");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }


        _rightEngine.gameObject.SetActive(false);
        _leftEngine.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(movement * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 5.9f), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {

            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        //play the laser audio clip
        _audioSource.Play();

    }
    

    public void Damage()
    {
       
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;

        if (_lives == 2)
        {
            _rightEngine.gameObject.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngine.gameObject.SetActive(true);
        }
        
        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        _thruster.gameObject.SetActive(false);
        _speedBoostVisualizer.gameObject.SetActive(true);
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
        _thruster.gameObject.SetActive(true);
        _speedBoostVisualizer.gameObject.SetActive(false);
    }

    public void ShieldsActive()
    {  
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

  
}  

    
