using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneChangeButtonListener : ButtonAddListener, ICommand
{
    [SerializeField]
    SceneAsset _nextScene;

    public void Execute()
    {
        SceneChangeBehaviour NewSceneChange = new SceneChangeBehaviour(_nextScene);
        NewSceneChange.ButtonBehaviour();
    }

    public override ICommand ReturnButtonBehaviour()
    {       
        return this;
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
