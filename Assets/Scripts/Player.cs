using System.Collections;
using UnityEngine;




public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField] 
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private UI_Manager _uiManager;

    [SerializeField] //set a speed 
    private float _speed = 3f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField]
    private int _score;
    
    //Health
    [SerializeField]
    private int _lives= 3;
    //private int _shield = 0;

    //Communitcate with Spawn Manager
   private SpawnManager _spawnManager;

    //POWERUPS
        //TripleShot
    [SerializeField]
    private bool _isTrippleShotActive = false;
    private Powerup _trippleShot;
    [SerializeField]
    private float _trippleShotCooldownTime = 5f;
        //SpeedBoost
    [SerializeField]
    private bool _isSpeedBoostActive = false;
    private Powerup _speedBoost;
    [SerializeField]
    private float _speedBoostCooldownTime = 5f;
    [SerializeField]
    private float _speedBoostMultiplier = 2f;
        //Shield
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private float _shieldActiveCooldownTime = 5f;
    [SerializeField]
    private GameObject _shieldVisualizer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //take curent position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Controlls movement
        Movement();

        //Fires the Players laser
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) { FireLaser(); }
        
        

       
      
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

   

        transform.Translate(new Vector2(horizontalInput, verticalInput) * _speed * Time.deltaTime);
        ////new Vector3(-5, 0, 0) * horizontalInput * speed * real time

        ////Horizontal movement
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        ////Vertical movment
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        //if the player position on y > 0 
        //y position = 0
        //else if position on the y is less that 3.8f

        if (transform.position.y >= 3)
        {
            transform.position = new Vector2(transform.position.x, 3);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector2(transform.position.x, -3.8f);
        }
        //transform.position = new Vector3 (transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        //Player Wraping
        //if player on the x > 11
        //x pos =-11
        //if player on the x < 11 
        // xpos = 11

        if (transform.position.x >= 10.5f)
        {
            transform.position = new Vector2(-10.5f, transform.position.y);

        }
        else if (transform.position.x <= -10.5f)
        {
            transform.position = new Vector2(10.5f, transform.position.y);
        }

    }

    void FireLaser()
    {
        //When hiting space spawn gameobject
        _canFire = Time.time + _fireRate;
        

        //if space key press,
        if (_isTrippleShotActive)
        {
            Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        //if tripleshotActive is true
            //fire 3 lasers (tripleshot prefab)

        //else fire 1 laser
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            StopCoroutine(ShieldPowerDownRoutine());
            return;
        }

        _lives--;

       _uiManager.updateLives(_lives);
     
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();

            

            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTrippleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(_trippleShotCooldownTime);
        _isTrippleShotActive = false;
    }

    public void SpeedPowerupActive()
    {
        _isSpeedBoostActive = true; 
        _speed *= _speedBoostMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());

    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {        
        yield return new WaitForSeconds(_speedBoostCooldownTime);
        _speed /= _speedBoostMultiplier;
        _isSpeedBoostActive = false;
    }

    public void ShieldPowerupActive()
    {
        _isShieldActive = true;
       _shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
        

    }

    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(_shieldActiveCooldownTime);
        _isShieldActive = false;
        _shieldVisualizer.SetActive(false);
    }

    //method to add 10 to the score!
    public void addScore(int points)
    {
        _score += points;
        _uiManager.updateScore(_score);
    }
    //Communicate with UIManager to update score
}
