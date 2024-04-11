using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    Spawner[] _spawnLocations;

    [SerializeField] int _timeToSpawnTimerThreshold;
    [SerializeField] int _currentTimeToSpawnTimer;

    [SerializeField] int _totalWaves;
    [SerializeField] int _currentWave = 0;

    [SerializeField] int _enemiesThisWave;
    [SerializeField] int _enemiesLeft = 0;

    [SerializeField] int _baseWaveEnemyCount;
    [SerializeField] int _perWaveEnemyIncrease;

    [SerializeField] List<GameObject> _instantiatedEnemies = new List<GameObject>();
    [SerializeField] List<GameObject> _spawnPool = new List<GameObject>();

    public SpawnManager(int timeToSpawnEnemy, int totalWaves, int enemiesPerWave, int perWaveIncrease, List<GameObject> spawnPool)
    {
        timeToSpawnEnemy = _timeToSpawnTimerThreshold;
        totalWaves = _totalWaves;
        enemiesPerWave = _baseWaveEnemyCount;
        perWaveIncrease = _perWaveEnemyIncrease;
        spawnPool = _spawnPool;
    }
    // Start is called before the first frame update

    private void Start()
    {



        _spawnLocations = FindObjectsByType<Spawner>(FindObjectsSortMode.None);

        GameObject instantiatedEnemy = null;
        foreach (GameObject enemy in _spawnPool)
        {
            instantiatedEnemy = Instantiate(enemy);

            _instantiatedEnemies.Add(instantiatedEnemy);
            instantiatedEnemy.SetActive(false);




        }

        _enemiesThisWave = _baseWaveEnemyCount;


    }

    private IEnumerator Victory()
    {
        Debug.Log("Victory!");
        yield return new WaitForSeconds(5);
        foreach(GameObject go in _instantiatedEnemies)
        {
            Destroy(go);
        }
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (_currentWave > _totalWaves)
        {
            StartCoroutine(Victory());
            return;
        }

        
        SpawnNewEnemies();
        MoveToNextWave();
        foreach (GameObject enemy in _instantiatedEnemies)
        {
            if (enemy.GetComponent<FighterStats>().CurrentHealth <= 0)
            {
                enemy.SetActive(true);
                ResetDisabledEnemy(enemy);
                enemy.SetActive(false);
                _enemiesLeft--;
            }


        }


    }

    private void MoveToNextWave()
    {
        if (_enemiesLeft <= 0)
        {
            _currentWave++;
            _enemiesThisWave += _perWaveEnemyIncrease;
            _enemiesLeft = _enemiesThisWave;


        }
    }

    private void SpawnNewEnemies()
    {

        _currentTimeToSpawnTimer++;


        if (_currentTimeToSpawnTimer >= _timeToSpawnTimerThreshold)
        {

            GameObject enemy = _instantiatedEnemies[Random.Range(0, _instantiatedEnemies.Count - 1)];
            if (enemy.activeSelf) return;

            enemy.transform.position = _spawnLocations[Random.Range(0, _spawnLocations.Length - 1)].transform.position;
            enemy.gameObject.SetActive(true);
            _enemiesLeft--;
            _currentTimeToSpawnTimer = 0;
        }
    }

    private void ResetDisabledEnemy(GameObject enemy)
    {

        //PrefabUtility.RevertPrefabInstance(enemy, InteractionMode.AutomatedAction);
        
        



    }
}
