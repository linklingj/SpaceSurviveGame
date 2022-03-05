using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public float speed;
    public float damage;
    public GameObject effectPool;

    void Update () 
    {
        transform.position += transform.up.normalized * speed * Time.deltaTime;
	}
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!col.gameObject.CompareTag("Obstacle")&&!col.CompareTag("Player"))
        {
            return;
        }
        if(col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SpaceShip>().Damage(damage);
        }
        GameObject.Find("EffectPool").GetComponent<EffectPool>().UseEffect("bullet",transform.position,-1*transform.up);
        transform.parent.gameObject.GetComponent<BulletPool>().BulletExpired(gameObject);
    }
}
