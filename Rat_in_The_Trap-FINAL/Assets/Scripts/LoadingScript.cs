using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    /** --PREVIOUS CODE BEFORE CHANGE--
     * public enum Scene
    {
        OpeningScene,
        LoadingScene,
        LevelTwo,
    }

    private static Action onLoaderCallback;

    public static void Load(Scene scene)
    {
        // Set the loader callback action to load the target scene
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
        // Load the loading scene
        SceneManager.LoadScene(scene.ToString());
    }

    public static void LoaderCallback()
    {
        // Triggered after the first Update which lets the screen refresh
        // Execute the loader callback action which will load the target scene
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
    **/
    private void Start()
    {
        Loading(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Loading(int LVL)
    {
        StartCoroutine(LoadingScreen(LVL));
    }

    IEnumerator LoadingScreen(int lvl)
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            break;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
