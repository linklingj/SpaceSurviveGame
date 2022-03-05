using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    public float health;
    [Header("Charge/Shoot")]
    public float shootPower = 1f;
    public float velocityLoss = 20f;
    public float rotateSpeed = 30f;
    [Header("Invinsible Ability")]
    public float invMeter;
    public bool invinsible;

    [HideInInspector]
    public bool disabled;

    Vector2 shootDir;
    bool invLock = false;
    float chargeTime;
    float velocity;

    [Header("Effects/GameObjects")]
    public GameObject effect1;
    public GameObject effect2;
    public Transform effectPos;
    public Image invinsMeter;
    public Image hp;
    public MainCamera mainCamera;
    public List<Sprite> hearts;
    public Sprite gameOverSprite;


    Rigidbody2D rb;

	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
        invinsMeter = GameObject.Find("invinsMeter").GetComponent<Image>();
        hp = GameObject.Find("heart").GetComponent<Image>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<MainCamera>();
        health = maxHealth;
        velocity = 0f;
        invMeter = 10;
	}
	
	void Update () 
    {
        if (disabled)
            return;
        chargeTime += Time.deltaTime;
        Invinsible();
        if (invinsible)
        {
            invMeter -= Time.deltaTime * 3;
            if (invMeter < 0)
                invMeter = 0;
        }
        else
        {
            invMeter += Time.deltaTime;
            if (invMeter > 10)
                invMeter = 10;
        }
        RotateBody();
        Shoot();
        transform.position = transform.position + new Vector3(shootDir.x, shootDir.y, 0f) * velocity;
        if (velocity > 0)
            velocity -= Time.deltaTime * velocityLoss;
        if (velocity < 0)
            velocity = 0;
        UIUpdate();
	}


    void RotateBody()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, 0f);
        direction.Normalize();
        Vector2 newDir = Vector2.Lerp(transform.up, direction, rotateSpeed * Time.deltaTime);
        transform.up = newDir;
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            chargeTime = 0f;
            transform.GetChild(0).GetComponent<SpaceShipAnimator>().Charge();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            GameObject fireEffect;
            if (chargeTime <= 0.3f)
            {
                velocity = 0.2f * shootPower;
                transform.GetChild(0).GetComponent<SpaceShipAnimator>().Release(1);
                fireEffect = Instantiate(effect1, effectPos.position, Quaternion.identity);
                fireEffect.GetComponent<HitBoxPropery>().Damage = 2;
                fireEffect.transform.localScale = new Vector3(0.15f, -0.15f, 0f);
                Destroy(fireEffect, 0.33f);
            }
            else if (chargeTime <= 0.6f)
            {
                velocity = 0.3f * shootPower;
                transform.GetChild(0).GetComponent<SpaceShipAnimator>().Release(2);
                StartCoroutine(mainCamera.Shake(0.05f, 0.05f));
                fireEffect = Instantiate(effect1, effectPos.position, Quaternion.identity);
                fireEffect.GetComponent<HitBoxPropery>().Damage = 3;
                fireEffect.transform.localScale = new Vector3(0.25f, -0.25f, 0f);
                Destroy(fireEffect, 0.33f);
            }
            else if (chargeTime <= 1f)
            {
                velocity = 0.45f * shootPower;
                transform.GetChild(0).GetComponent<SpaceShipAnimator>().Release(3);
                StartCoroutine(mainCamera.Shake(0.1f, 0.08f));
                fireEffect = Instantiate(effect1, effectPos.position, Quaternion.identity);
                fireEffect.GetComponent<HitBoxPropery>().Damage = 4;
                fireEffect.transform.localScale = new Vector3(0.4f, -0.4f, 0f);
                Destroy(fireEffect, 0.33f);
            }
            else
            {
                velocity = 0.6f * shootPower;
                transform.GetChild(0).GetComponent<SpaceShipAnimator>().Release(4);
                StartCoroutine(mainCamera.Shake(0.2f, 0.15f));
                fireEffect = Instantiate(effect2, effectPos.position, Quaternion.identity);
                fireEffect.transform.localScale = new Vector3(0.8f, 0.8f, 0);
                Destroy(fireEffect, 1f);
            }
            shootDir = transform.up;
            fireEffect.transform.up = transform.up;
        }
    }
    void Invinsible()
    {
        if(Input.GetMouseButton(1)&&!invLock)
        {
            if(invMeter>0)
            {
                invinsible = true;
                return;
            }else
            {
                invLock = true;
            }
        }
        invinsible = false;
        if(Input.GetMouseButtonUp(1))
        {
            invLock = false;
        }
    }
    public void Damage(float damage)
    {
        if(invinsible)
        {
            return;
        }
        health -= damage;
        StartCoroutine(mainCamera.Shake(0.2f, 0.15f));
        if(health<=0)
        {
            health = 0;
            Death();
        }
    }
    void UIUpdate()
    {
        invinsMeter.fillAmount = invMeter / 10;
        if (health <= 0)
        {
            hp.sprite = gameOverSprite;
        }
        else
        {
            float heartCount = health / maxHealth * 10;
            if ((int)heartCount > 10)
                hp.sprite = hearts[9];
            hp.sprite = hearts[(int)heartCount-1];
        }
    }
    public void Death()
    {
        
    }
}
