using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewChooseScene", menuName = "Data/New Choose Scene")]
[System.Serializable]

public class ChooseScene : GameScene
{
    // each of the difference possible choices
    public List<ChooseLabel> labels;

    [System.Serializable]

    public struct ChooseLabel
    {
        public string text;
        public Scenes nextScene;
       
    }
    
}
