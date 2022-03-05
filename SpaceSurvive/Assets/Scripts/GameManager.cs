using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public EnemySpawner enemySpawner;
    public GameObject spaceshipPrefab;
    GameObject playerShip;
    public string state;

    private void Awake()
    {
        SpawnAll();
        StartCoroutine(GameLoop());
        state = "starting";
    }
    void SpawnAll()
    {
        Vector3 spawnPoint = new Vector3(0, 0, 0 );
        playerShip = Instantiate(spaceshipPrefab, transform.position,Quaternion.identity);
    }

    IEnumerator GameLoop()
    {
        yield return StartCoroutine(Starting());
        yield return StartCoroutine(Playing());
    }
    private IEnumerator Starting()
    {
        state = "starting";
        playerShip.GetComponent<SpaceShip>().disabled = true;
        //3,2,1 text code here
        yield return new WaitForSeconds(4f);
    }
    private IEnumerator Playing()
    {
        state = "playing";
        playerShip.GetComponent<SpaceShip>().disabled = false;
        enemySpawner.StartPhase(1);
        yield return new WaitForSeconds(100f);
    }/*
    private IEnumerator Ending()
    {

    }*/
}
