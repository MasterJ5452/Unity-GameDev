using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
        //when we leave the screen, destroy this object.
        if (transform.position.y < -5.9)
        {
            Destroy(this.gameObject);
        }

    }

    //OnTriggerCollision
    //Only be collectable by the Player
}
