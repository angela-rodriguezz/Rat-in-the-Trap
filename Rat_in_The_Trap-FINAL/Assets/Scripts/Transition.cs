using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public bool isSwitched = false;
    public bool catOut = false;
    public GameObject Cat;
    public Image background1;
    public Image background2;
    public Animator animator;
    public Animator animator2;
    public bool nextLevel;

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

    public void worriedCat()
    {
        
        animator2.SetTrigger("Worried");
        
        
    }

    public void stressedCat()
    {
        
        
        animator2.SetTrigger("Stressed");
        
        
        
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
    // checks if the current scene's background isn't null
    public bool CheckImage(Sprite sprite)
    {
        return sprite != null;
    }
    // starts changing the backgrounds
    public void SwitchImage(Sprite sprite)
    {
        SetImage(sprite);
    }

    public void StartImage(Sprite sprite)
    {
        background1.sprite = sprite;
    }
    // if the background isn't switched...
        // the background sprite is changed to the new background, the fading animation begins, and is now switched
    // if the background is switched
        // change back to the previous background, reverse fading animation begins, and is now not switched
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
