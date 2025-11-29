using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);

        //if other = Player     
     
       
        if ( other.tag == "Player")
        {       
            //damage the Player
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            //destroy Us 
            Destroy(this.gameObject);
        }
        

        //if other = laser
        if(other.tag == "Laser")
        {
            //destroy laser 
            Destroy(other.gameObject);
            //destroy us
            Destroy(this.gameObject);

        }

    }

  
}
