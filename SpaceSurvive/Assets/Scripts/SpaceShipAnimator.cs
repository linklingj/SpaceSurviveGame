using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAnimator : MonoBehaviour 
{

    Animator anim;

    public _2dxFX_ColorRGB colorRGBFx;

    bool invinsible;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("Release", 0);
        ChangeRGB(0, 0, 0);
    }
	private void Update()
	{
        invinsible = transform.parent.GetComponent<SpaceShip>().invinsible;
        if(invinsible)
        {
            ChangeRGB(-0.5f, 0.4f, 1f);
        } else
        {
            ChangeRGB(0, 0, 0);
        }
	}
	public void Charge()
    {
        anim.SetInteger("Release", 0);
        anim.SetTrigger("Charge");
    }
    public void Release(int i)
    {
        anim.SetInteger("Release", i);
    }
    public void ChangeRGB(float r,float g,float b)
    {
        colorRGBFx._ColorR = r;
        colorRGBFx._ColorG = g;
        colorRGBFx._ColorB = b;
    }
}
