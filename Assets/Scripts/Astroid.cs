using UnityEngine;

public class Astroid : MonoBehaviour
{    
    [SerializeField]
    private float _rotateSpeed = 20.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private SpawnManager _spawnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateZ();
    }

    private void RotateZ()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
        
    }

    //check for LASER collission (Trigger)
    //Instantiate explosion at the position of the astroid (us)
    //destroy the explosion after 3 sec

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {

            GameObject newExplosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject);
            

        }
    }
}

