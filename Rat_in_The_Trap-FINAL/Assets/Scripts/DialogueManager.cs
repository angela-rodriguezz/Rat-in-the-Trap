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

    public void Hide()
    {
        if (!isHidden)
        {
            animator.SetTrigger("Hide");
            isHidden = true;
        }
        
    }

    public void Show()
    {
        animator.SetTrigger("Show");
        isHidden = false;
    }

    public void ClearText()
    {
        barText.text = "";
    }
    
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

    public void FinishSentence()
    {
        state = State.COMPLETED;
        StopCoroutine(lineAppear);
        finished = false;
        barText.text = currentScene.sentences[sentenceIndex].text;

    }

    public void PlayNextSentence()
    {
        lineAppear = TypeText(currentScene.sentences[++sentenceIndex].text);
        StartCoroutine(lineAppear);
        personNameText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        personNameText.color = currentScene.sentences[sentenceIndex].speaker.textColor;
    }

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
    }
}
