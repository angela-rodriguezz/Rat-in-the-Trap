using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayDialogue : MonoBehaviour
{
    public GameScene currentScene;
    public DialogueManager bottomBar;
    public Transition backgroundController;
    public SelectionScreen chooseController;

    private State state = State.IDLE; // sets the current scene to a regular dialogue scene
    
    // idle state: the regular dialogue playing 
    // animate state: the scene transitioning to a new background
    // choose state: the scene changing to a choosing dialogue prompt
    private enum State
    {
        IDLE, ANIMATE, CHOOSE
    }
    

    // checks if the starting scene is a regular dialogue scene
    // then declares it a storyScene and starts playing the dialogue
    // sets the background to the sprite
    void Start()
    {
        if (currentScene is Scenes)
        {
            Scenes storyScene = currentScene as Scenes;
            bottomBar.PlayScene(storyScene);
            backgroundController.StartImage(storyScene.background);
        }
    }
        

    // if the player is hitting space or clicking
        // checks if there is no more sentences in the scene b/c then goes to the next level
        // otherwise if sentence is completed, then...
            // checks if hiding cat, then cat hide animation
            // checks if last sentence in scene, then plays the next scene
            // else plays the next sentence
        // otherwise the player is double clicking and therefore we autofill the sentence
    void Update()
    {
        if (currentScene is not ChooseScene)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (bottomBar.IsCompleted() && bottomBar.IsLastSentence() && bottomBar.IsFinalScene())
                {
                    StartCoroutine(EnterLoad());
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else if (bottomBar.IsCompleted())
                {
                    if (bottomBar.ChangeCat() && bottomBar.IsLastSentence())
                    {
                        backgroundController.NoCat();
                    }
                    if (state == State.IDLE && bottomBar.IsLastSentence())
                    {
                        PlayScene((currentScene as Scenes).nextScene);
                    }
                    else
                    {
                        bottomBar.PlayNextSentence();
                    }

                }
                else
                {
                    DialogueManager.finished = true;
                    bottomBar.FinishSentence();
                }

            }
        }
        
    }

    // wait until loading next level
    private IEnumerator EnterLoad()
    {
        yield return new WaitForSeconds(0.05f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // starts switching to a new background scene
    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    // sets the state to animate
    // sets the currentscene to given scene
    // if the scene is not a choose scene...
        // if the given scene's background isn't null and there is no more sentences in scene...
            // switches the backgrounds and waits until returning
        // plays the scene's dialogue and sets the state to idle
    // if the scene is a choose scene...
        // sets up the choice animations, changes the text of buttons to choices, and starts timer
    private IEnumerator SwitchScene(GameScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        if (scene is Scenes)
        {
            Scenes storyScene = scene as Scenes;
            if (backgroundController.CheckImage(storyScene.background) && bottomBar.IsLastSentence())
            {
                backgroundController.SwitchImage(storyScene.background);
                yield return new WaitForSeconds(1f);
            }
            
            bottomBar.PlayScene(storyScene);
            state = State.IDLE;
        }
        else if (scene is ChooseScene)
        {
            state = State.CHOOSE;
            chooseController.SetupChoose(scene as ChooseScene);
        }
    }

}
