using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private float _nextFireTime;
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private AudioSource _laserAudio;

    private Player _player;
    private Animator _anim;
    
    //Audio
   private AudioSource _audioSource;


    
    void Start()
    {
        
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        _laserAudio = GameObject.Find("Laser_Shot").GetComponent<AudioSource>();
        //null check player
        if(_player == null)
        {
            Debug.LogError("Player object NULL");
        }        
        //assign the component to Anim
        _anim = GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.LogError("Animator NULL");
        }
     

    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
        //if bottom of screen 
        //respawn at top with a new random x position
        
        if(transform.position.y <= -5.4f)
        {
            float randomX = Random.Range(-9.5f, 9.5f);
            float randomY = Random.Range(7.4f, 10f);
            transform.position = new Vector2(randomX, randomY);
        }

        if(Time.time >= _nextFireTime)
        {
            FireLaser();
            _nextFireTime = Time.time + Random.Range(3,8);
        }
    }


    void FireLaser()
    {      
        Instantiate(_laserPrefab, transform.position + new Vector3(0, -1.05f, 0), Quaternion.identity);
     
        _laserAudio.Play();       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);

        //if other = Player     

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            //damage the Player

            if (player != null)
            {
                player.Damage();
            }
            //trigger anim
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            //destroy Us 
            
            _audioSource.Play();
            Destroy(GetComponent<BoxCollider2D>());

            Destroy(this.gameObject,2.8f);
        }


        //if other = laser
        if (other.tag == "Laser")
        {

            
            //destroy laser 
            Destroy(other.gameObject);

            //triger anim
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            //destroy us
            
            _audioSource.Play();
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this.gameObject,2.8f);
            if (_player != null)
            {
                _player.addScore(10);

            }


        }

    }

  
}
