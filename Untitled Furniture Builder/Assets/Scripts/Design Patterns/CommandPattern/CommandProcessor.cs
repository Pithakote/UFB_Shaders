using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandProcessor : MonoBehaviour
{
    IButtonInteractable _buttonBehaviour;
    List<ICommand> _buttonBehaviours = new List<ICommand>();
    int _currentCommandIndex;
    public void ExecuteBehaviour(ICommand _buttonbehaviour)
    {
        _buttonBehaviours.Add(_buttonbehaviour);
        _buttonbehaviour.Execute();
        _currentCommandIndex = _buttonBehaviours.Count - 1;
    }

    public void UndoBehaviour()
    {
       // Debug.Log("CommandProcessor class UndoBehaviour function");
        if (_currentCommandIndex < 0)
            return;

        _buttonBehaviours[_currentCommandIndex].Undo();
        _buttonBehaviours.RemoveAt(_currentCommandIndex);
        _currentCommandIndex--;
    }
}
