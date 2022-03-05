using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour {

    [Header("BulletEffect")]
    public GameObject bulletEffectPrefab;
    public int amountA = 30;
    List<GameObject> bulletEffectPool = new List<GameObject>();
    [Header("Enemy1DeathEffect")]
    public GameObject enemy1DeathEffect;
    public int amountB = 30;
    List<GameObject> enemy1DeathEffectPool = new List<GameObject>();


	void Start () 
    {
        InstantiateEffect(bulletEffectPool, bulletEffectPrefab, amountA);
        InstantiateEffect(enemy1DeathEffectPool, enemy1DeathEffect, amountB);
	}

    private void InstantiateEffect(List<GameObject> effects,GameObject prefab,int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject effect = Instantiate(prefab, transform.position, Quaternion.identity);
            effect.transform.SetParent(transform);
            effect.SetActive(false);
            effects.Add(effect);
        }
    }

    public void UseEffect(string name,Vector2 pos, Vector2 dir)
    {
        if(name == "bullet")
        {
            if (bulletEffectPool.Count > 0)
            {
                bulletEffectPool[0].SetActive(true);
                bulletEffectPool[0].transform.position = pos;
                bulletEffectPool[0].transform.up = dir;
                StartCoroutine("WaitForBullet", bulletEffectPool[0]);
                bulletEffectPool.RemoveAt(0);
            }
            else
            {
                Debug.LogError("Not Enough BulletEffect");
            }
        }
        else if(name == "enemyDeath")
        {
            if (enemy1DeathEffectPool.Count > 0)
            {
                enemy1DeathEffectPool[0].SetActive(true);
                enemy1DeathEffectPool[0].transform.position = pos;
                enemy1DeathEffectPool[0].transform.up = dir;
                StartCoroutine("WaitForDeathEffect", enemy1DeathEffectPool[0]);
                enemy1DeathEffectPool.RemoveAt(0);
            }
            else
            {
                Debug.LogError("Not Enough EnemyDeathEffect");
            }
        }
    }

    IEnumerator WaitForBullet(GameObject effect)
    {
        yield return new WaitForSeconds(1.5f);
        effect.SetActive(false);
        effect.transform.position = transform.position;
        effect.transform.rotation = Quaternion.identity;
        bulletEffectPool.Add(effect);
    }
    IEnumerator WaitForDeathEffect(GameObject effect)
    {
        yield return new WaitForSeconds(4);
        effect.SetActive(false);
        effect.transform.position = transform.position;
        effect.transform.rotation = Quaternion.identity;
        bulletEffectPool.Add(effect);
    }
}
