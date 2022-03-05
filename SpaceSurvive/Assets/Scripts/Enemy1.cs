using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {

    public GameObject bulletPool;
    public GameObject effectPool;
    public Transform player;
    public float speed;
    private bool stateLock;
    private Vector2 targetPos;
    private Animator spriteAnimator;

    public float maxHp;
    private float hp;
    [SerializeField] private string state;
    //idle,move,attack

	void Start () 
    {
        spriteAnimator = transform.GetChild(0).GetComponent<Animator>();
        bulletPool = GameObject.Find("BulletPool");
        effectPool = GameObject.Find("EffectPool");
        player = GameObject.Find("SpaceShip(Clone)").GetComponent<Transform>();
        hp = maxHp;
        stateLock = false;
        SetTargetPos();
	}
	
	void Update () 
    {
        if(state=="attack"||state=="move")
        {
            Rotation(player.position);
        } 
        else if(state=="idle")
        {
            Rotation(targetPos);
        }
        if(!stateLock)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if(distance>8)
            {
                state = "idle";
                Idle();
            }
            else if(distance<=3)
            {
                state = "attack";
                Attack();
            }
            else
            {
                state = "move";
                Move();
            }
        }
        if(hp<=0)
        {
            Death();
        }
	}
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SpaceShip>().Damage(2);
        }
        if(col.CompareTag("HitBox"))
        {
            hp -= col.transform.parent.GetComponent<HitBoxPropery>().Damage;
        }
    }
    void SetTargetPos()
    {
        targetPos = transform.position + new Vector3(Random.Range(-3, 3), Random.Range(-3, 3));
        if (targetPos.x > 8.5)
            targetPos.x = Random.Range(7, 8.5f);
        if (targetPos.x < -8.5)
            targetPos.x = Random.Range(-7, -8.5f);
        if (targetPos.y > 4.5)
            targetPos.y = Random.Range(3.5f, 4.5f);
        if (targetPos.y < -4.5)
            targetPos.y = Random.Range(-3.5f, -4.5f);
    }
    void Rotation(Vector3 target)
    {
        transform.up += ((target - transform.position).normalized-transform.up)*0.1f;
    }
    void Idle()
    {
        transform.position += transform.up * Time.deltaTime * speed * 0.7f;
        if(Vector3.Distance(targetPos,transform.position)<0.2f)
        {
            SetTargetPos();
        }
    }
    void Move()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }
    void Attack()
    {
        stateLock = true;
        spriteAnimator.SetTrigger("Attack");
        StartCoroutine("PhysicalAttack");
    }
    IEnumerator PhysicalAttack()
    {
        yield return new WaitForSeconds(0.8f);
        bulletPool.GetComponent<BulletPool>().fire(transform.position, transform.up);
        yield return new WaitForSeconds(1.2f);
        stateLock = false;
    }
    void Death()
    {
        effectPool.GetComponent<EffectPool>().UseEffect("enemyDeath", transform.position, Vector3.up);
        Destroy(gameObject);
    }
}
