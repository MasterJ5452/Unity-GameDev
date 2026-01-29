using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _speedBoostPrefab;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _enemySpawnTime = 5f;
    [SerializeField]
    private float _powerupSpawnTimeMin = 3f;
    [SerializeField]
    private float _powerupSpawnTimeMax = 7f;
    [SerializeField]
    private float _enemySpawnDelay = 2f;
    [SerializeField]
    private float _powerupSpawnDelay = 2f;

    [SerializeField]
    private GameObject[] _gameObjectPowerups;

    [SerializeField]

    private AudioSource _explosionAudio;
    private float randomX;
    private float randomY;

    private bool _stopSpawning = false;

    private void Start()
    {
        _explosionAudio = GameObject.Find("Explosion_sound").GetComponent<AudioSource>();
        if (_explosionAudio == null)
        {
            Debug.LogError("The Explosion Audio is NULL in Audio_Manager");
        }
        
    }
    public void StartSpawning()
    {
        _explosionAudio.Play();
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(_enemySpawnDelay);
        while (!_stopSpawning)
        {
            randomX = Random.Range(-9.5f, 9.5f);
            randomY = Random.Range(7.4f, 10f);
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnTime);
            if (_enemySpawnTime > .75f) { _enemySpawnTime -= .03f; }

        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(_powerupSpawnDelay);
        while (!_stopSpawning)
        {
            randomX = Random.Range(-9.5f, 9.5f);
            randomY = Random.Range(7.4f, 10f);
            yield return new WaitForSeconds(Random.Range(_powerupSpawnTimeMin, _powerupSpawnTimeMax));
            GameObject newObject = Instantiate(_gameObjectPowerups[Random.Range(0, _gameObjectPowerups.Length)], new Vector3(randomX, randomY, 0), Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _explosionAudio.Play();
        _stopSpawning = true;
    }
}
