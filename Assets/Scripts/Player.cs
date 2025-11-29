using UnityEngine;




public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField] 
    private GameObject _trippleShotPrefab;
    [SerializeField] //set a speed 
    private float _speed = 3f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    
    [SerializeField]
    private int _lives= 3;

    //Communitcate with Spawn Manager
   private SpawnManager _spawnManager;

    //Bools for powerups
    [SerializeField]
    private bool _trippleShotActive = false;


// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        //take curent position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
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
        if (_trippleShotActive)
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
        _lives--;
     
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();

            Destroy(this.gameObject);
        }
    }

}
