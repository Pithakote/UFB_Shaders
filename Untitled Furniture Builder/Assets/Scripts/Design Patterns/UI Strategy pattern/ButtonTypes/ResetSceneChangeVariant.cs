using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSceneChangeVariant : SceneChangeButtonListener
{
    public override void Execute()
    {
        _instance.SaveTest.ResetProgress();
        base.Execute();
    }


}
