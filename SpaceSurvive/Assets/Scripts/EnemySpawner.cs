using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject Enemy1;
    public GameManager gameManager;

	void Update () 
    {
	}
    public void StartPhase(int phase)
    {
        StartCoroutine(Enemies());
    }
    public IEnumerator Enemies ()
    {
        while (gameManager.state == "playing")
        {
            yield return StartCoroutine(Enemy(3f));
            //yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator Enemy(float time)
    {
        Spawn(Enemy1, new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.7f, 4.7f), 0));
        yield return new WaitForSeconds(time);
    }

    void Spawn(GameObject enemy,Vector3 position)
    {
        Instantiate(enemy,position,Quaternion.identity);
    }
}
