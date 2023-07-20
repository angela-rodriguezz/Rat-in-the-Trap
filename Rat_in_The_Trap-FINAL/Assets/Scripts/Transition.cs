using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    private bool isSwitched = false;
    private bool catOut = false;
    private bool glimmerOut = false;

    public Image background1;
    public Image background2;
    public Animator animator;
    public Animator animator2;
    public Animator animator3;

    public void NoCat()
    {
        if (!catOut)
        {
            animator2.SetTrigger("Leaving");
        }
        catOut = true;
    }

    public void dangerCat()
    {
        if (catOut)
        {
            animator2.SetTrigger("Danger");
            animator2.SetTrigger("Now");
        }
        catOut = false;
        
    }

    
    public void glimmerActivate()
    {
        if (!glimmerOut)
        {
            animator3.SetTrigger("Enter");
            animator3.SetTrigger("Talk");
        }
        glimmerOut = true;
    }

    public void glimmerDeactivate()
    {
        if (glimmerOut)
        {
            animator3.SetTrigger("Disappear");
        }
        glimmerOut = false;
    }

    public void ReturnCat()
    {
        if (!catOut)
        {
            animator2.SetTrigger("Return");
            animator2.SetTrigger("Move");
        }
        catOut = true;
    }

    public bool CheckImage(Sprite sprite)
    {
        return sprite != null;
    }

    public void SwitchImage(Sprite sprite)
    {
        SetImage(sprite);
    }

    public void StartImage(Sprite sprite)
    {
        background1.sprite = sprite;
    }

    public void SetImage(Sprite sprite)
    {
        if (!isSwitched)
        {
            background2.sprite = sprite;
            animator.SetTrigger("SwitchBG");
            isSwitched = !isSwitched;
        }
        else if (isSwitched)
        {
            background1.sprite = sprite;
            animator.SetTrigger("ReverseBG");
            isSwitched = !isSwitched;           
        }
        
    }
    
}
