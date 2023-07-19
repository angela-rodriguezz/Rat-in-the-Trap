using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayDialogue : MonoBehaviour
{
    public GameScene currentScene;
    public DialogueManager bottomBar;
    public Transition backgroundController;
    public SelectionScreen chooseController;

    private State state = State.IDLE;

    private enum State
    {
        IDLE, ANIMATE, CHOOSE
    }
    

    // Start is called before the first frame update
    void Start()
    {
        if (currentScene is Scenes)
        {
            Scenes storyScene = currentScene as Scenes;
            bottomBar.PlayScene(storyScene);
            backgroundController.StartImage(storyScene.background);
        }
    }
        

    // Update is called once per frame
    void Update()
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

    private IEnumerator EnterLoad()
    {
        yield return new WaitForSeconds(0.05f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

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
