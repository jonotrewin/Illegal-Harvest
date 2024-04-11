using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPests : MonoBehaviour
{
    [SerializeField] GameObject[] enemiesPrefabs;
    [SerializeField] static List<GameObject> enemies = new List<GameObject>();
    [SerializeField] List<GameObject> instantiatedEnemies = new List<GameObject>();

    [SerializeField] List<GameObject> spawnLocations = new List<GameObject>();

    [SerializeField] int _maxDistance = 30;

  

    [SerializeField] float _spawnFrequency = 20;

    [SerializeField] GameObject beingAttackTextUI;

    bool _isRunning;

    private void Start()
    {
      

        if(enemies.Count == 0)
            foreach (var enemy in enemiesPrefabs)
            {
                enemies.Add(enemy);
            }
        
        
        Debug.Log(enemies.Count);
        if (!CheckForVeggies()) return;
        _isRunning = true;
        StartCoroutine(FlashAttack());
        InvokeRepeating("SpawnEnemy", 0, _spawnFrequency);
        foreach (GameObject enemy in enemies)
        {
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.SetActive(false);
            instantiatedEnemies.Add(newEnemy);
            
        }
        

        
    }

    private IEnumerator FlashAttack()
    {
        beingAttackTextUI.SetActive(true);
        yield return new WaitForSeconds(1);
        beingAttackTextUI.SetActive(false);
        yield return new WaitForSeconds(1);
        beingAttackTextUI.SetActive(true);
        yield return new WaitForSeconds(1);
        beingAttackTextUI.SetActive(false);
        yield return new WaitForSeconds(1);
        beingAttackTextUI.SetActive(true);
        yield return new WaitForSeconds(1);
        beingAttackTextUI.SetActive(false);

    }

    private void Update()
    {
       if(CheckForVeggies() && !_isRunning)
        { 
            Start(); 
            _isRunning = true;
           
        }

       if(!CheckForVeggies() && _isRunning)
        {
            
            _isRunning = false;
            CleanUp();
        }
    }

    private bool CheckForVeggies()
    {

        foreach (Veggie veg in GameObject.FindObjectsByType(typeof(Veggie), FindObjectsSortMode.None))
        {
            if(veg.gameObject.activeSelf)
            return Vector3.Distance(this.transform.position, veg.transform.position) <= _maxDistance;
        }
        return false;
    }

    private void SpawnEnemy()
    {

        foreach (GameObject enemy in instantiatedEnemies)
        {
            if (enemy.activeSelf == false)
            {
                //PrefabUtility.ResetToPrefabState(enemy);
                enemy.transform.position = spawnLocations[Random.Range(0,spawnLocations.Count)].transform.position;
                enemy.SetActive(true);
                return;
            }
            
        }
        
    }

    private void OnDisable()
    {
        CleanUp();
    }

    private void CleanUp()
    {
        CancelInvoke("SpawnEnemy");
        foreach (GameObject enemy in instantiatedEnemies)
        {
            Pest enemyPest = enemy.GetComponent<Pest>();
            enemyPest.animator.SetBool("IsWalking", true);
            enemyPest.NavMeshAgent.destination = spawnLocations[Random.Range(0, spawnLocations.Count - 1)].transform.position;




        }
        StartCoroutine(ResetInstantiatedEnemiesList());
    }

    private IEnumerator ResetInstantiatedEnemiesList()
    {
        yield return new WaitForSeconds(5f);
        foreach(GameObject enemy in instantiatedEnemies)
        { 
            Destroy(enemy);
        }
        instantiatedEnemies.Clear();
        _isRunning = false;
    }
}
