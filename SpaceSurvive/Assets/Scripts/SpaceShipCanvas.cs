using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipCanvas : MonoBehaviour {

    public GameObject spaceship;

	void Start () 
    {
        spaceship = GameObject.Find("SpaceShip(Clone)");
	}
	
	void Update () 
    {
        transform.position = new Vector2(spaceship.transform.position.x + 0.9f, spaceship.transform.position.y + 0.6f);
	}
}
