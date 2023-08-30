using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    private int sentenceIndex = -1;
    public Scenes currentScene;
    public static bool finished;
    private State state = State.COMPLETED;
    public bool appearComputer;
    private Animator animator;
    public Transition catAnimator;
    private bool isHidden = false;

    private IEnumerator lineAppear;

    private enum State
    {
        PLAYING, COMPLETED
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // button animation for hiding choices
    public void Hide()
    {
        if (!isHidden)
        {
            animator.SetTrigger("Hide");
            isHidden = true;
        }
        
    }

    // button animation for showing choices
    public void Show()
    {
        animator.SetTrigger("Show");
        isHidden = false;
    }

    public void ClearText()
    {
        barText.text = "";
    }
    
    // plays the current scene and starts playing the next sentence
    // starts negative since we are adding each time
    public void PlayScene(Scenes scene)
    {
        currentScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsFirstSentence()
    {
        return currentScene.sceneName == "Start";
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    public bool IsFinalScene()
    {
        return currentScene.nextScene == null;
    }

    public bool ChangeCat()
    {
        return currentScene.sceneName == "Intro";
    }

    public bool appearGlimmer()
    {
        if (currentScene.sceneName == "Trolley" && sentenceIndex == 1)
        {
            return true;
        }
        else
        {
            return currentScene.sceneName == "Lab" && sentenceIndex == 2;
        }


    }

    public bool leaveGlimmer()
    {
        if (currentScene.sceneName == "PostTrolley" && sentenceIndex == 3)
        {
            return true;
        }
        else
        {
            return (currentScene.sceneName == "Finished" && sentenceIndex == 1);
        }

    }


    // If the player double clicks, the state is now complete, the coroutine stops, and now the text immediately shows the sentence immediately
    public void FinishSentence()
    {
        state = State.COMPLETED;
        StopCoroutine(lineAppear);
        finished = false;
        barText.text = currentScene.sentences[sentenceIndex].text;

    }

    // unless there is no next sentence in the scene and the text is completed, this function plays the next sentence
    // we get one of the sentences in the list from the current sentence index and enter it into the coroutine
    // change the player name or color if the speaker changes
    public void PlayNextSentence()
    {
        if (!IsLastSentence() && !finished) { 
            lineAppear = TypeText(currentScene.sentences[++sentenceIndex].text);
            StartCoroutine(lineAppear);
            personNameText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
            personNameText.color = currentScene.sentences[sentenceIndex].speaker.textColor;
        }
    }

    // designates that the dialogue is playing and sets the text to empty 
    // starts the index at 0 and continues until the state is complete
    // adds each letter into the text box from the intended string to be returned
    // yield return changes timing of when text appears
    // if the index ever equals the text length, the state is now completed and the enumerator ends
    private IEnumerator TypeText(string text)
    {
        barText.text = "";
        state = State.PLAYING; 
        int wordIndex = 0;

        while (state != State.COMPLETED) 
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f);
            if (++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (currentScene.sceneName == "DangerCat")
        {
            Health.health = 2;
            catAnimator.dangerCat();
        }

        if (currentScene.sceneName == "WorriedCat")
        {
            catAnimator.ReturnCat();
            catAnimator.worriedCat();
        }

        if (currentScene.sceneName == "StressedCat")
        {
            catAnimator.ReturnCat();
            catAnimator.stressedCat();
        }

        if (currentScene.sceneName == "CatAgain")
        {
            catAnimator.ReturnCat();
        }

        if (appearGlimmer())
        {
            Debug.Log("activated");
            catAnimator.glimmerActivate();
        }

        if (leaveGlimmer())
        {
            catAnimator.glimmerDeactivate();
        }
    }
}
