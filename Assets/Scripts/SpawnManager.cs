using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _spawnTime = 5f;

    private bool _stopSpawning = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {       
       
        while ( !_stopSpawning)
        {
            float randomX = Random.Range(-9.5f, 9.5f);
            float randomY = Random.Range(7.4f, 10f);
           GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnTime);
            if(_spawnTime > .75f) { _spawnTime -= .03f; }
            
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
