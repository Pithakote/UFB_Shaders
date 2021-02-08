using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonListener : ButtonAddListener, ICommand
{
    [SerializeField]
    protected RectTransform _currentUI;
    [SerializeField]
    protected RectTransform _nextUI;
    [SerializeField]
    protected Vector2 _currentUIEndPos;
    [SerializeField]
    protected Vector2 _nextUIEndPos;
    [SerializeField]
    protected float _duration;
    protected override void Awake()
    {
        if (_currentUI == null)
            _currentUI = gameObject.GetComponent<RectTransform>();
    }
    public override ICommand ReturnButtonBehaviour()
    {      
        return this;
    }

    public void Execute()
    {
        UINextPanelBehaviour UImove = new UINextPanelBehaviour(_currentUI,
                                                            _nextUI,
                                                            _currentUIEndPos,
                                                            _nextUIEndPos,
                                                            _duration
                                                            );

        UImove.ButtonBehaviour();
    }

    public void Undo()
    {
        UINextPanelBehaviour UndoMove = new UINextPanelBehaviour(_nextUI,
                                                            _currentUI,
                                                            _currentUIEndPos,
                                                            _nextUIEndPos,
                                                            _duration
                                                            );

        UndoMove.ButtonBehaviour();

        Debug.Log("UIButtonListener undo function");
    }
}
