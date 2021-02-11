using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CommandProcessor))]
public class ButtonListenerManager : MonoBehaviour
{
    CommandProcessor _commandProcessor;
    private void Start()
    {
        _commandProcessor = GetComponent<CommandProcessor>();
    }
    public void PerformButtonBehaviour(ICommand _buttonbehaviour)//called on the buttons to make the behaviours flexible for all kinds of behaviour
    {
        _commandProcessor.ExecuteBehaviour(_buttonbehaviour);

    }

    public void PerformUndoBehaviour()
    {
        _commandProcessor.UndoBehaviour();
    }
}
