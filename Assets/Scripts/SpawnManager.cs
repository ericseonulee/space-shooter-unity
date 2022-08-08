using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerupsContainer;
    [SerializeField]
    private GameObject[] _powerups;

    [SerializeField]
    private bool _stopSpawning = false;

    public void StartSpawning() {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine() {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false) {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 7.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine() {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false) {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 7.5f, 0);
            int randomPowerup = Random.Range(0, 3);
            GameObject powerup = Instantiate(_powerups[randomPowerup], posToSpawn, Quaternion.identity);
            powerup.transform.parent = _powerupsContainer.transform;
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void onPlayerDeath() {
        _stopSpawning = true;
    }
}
