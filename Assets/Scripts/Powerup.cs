using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    private Player _player;

    //0 tripleshot //1 speed //2 shield
    [SerializeField]
    private int _powerUpID;
    


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
    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player"))
        {
            _player = other.transform.GetComponent<Player>();
            if(_player != null)
            {             

                switch (_powerUpID)
                {
                    case 0:
                        _player.TripleShotActive();
                        break;
                    case 1:
                        _player.SpeedPowerupActive();
                        break;
                    case 2:
                        _player.ShieldPowerupActive();
                        break;
                    default:
                        Debug.Log("Default Value" + _powerUpID);
                        break;



                }

            }

            Destroy(this.gameObject);
            
        }
    }
}
