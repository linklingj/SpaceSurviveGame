using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {

    public GameObject bulletPrefab;
    public int amount = 30;

    List<GameObject> pool;

	void Start () 
    {
        pool = new List<GameObject>();
        for (int i = 0; i < amount;i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.SetParent(transform);
            bullet.SetActive(false);
            pool.Add(bullet);
        }
            
	}

    public void fire(Vector2 pos, Vector2 dir)
    {
        if(pool.Count>0)
        {
            pool[0].SetActive(true);
            pool[0].transform.position = pos;
            pool[0].transform.up = dir;
            pool.RemoveAt(0);
        } else
        {
            Debug.LogError("Not Enough Bullet");
        }
    }
    public void BulletExpired(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;
        pool.Add(bullet);
    }
}
