using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAddListener : MonoBehaviour
{
    [SerializeField]
    RectTransform _currentUI;
    [SerializeField]
    RectTransform _nextUI;
    [SerializeField]
    Vector2 _currentUIEndPos;
    [SerializeField]
    Vector2 _nextUIEndPos;
    [SerializeField]
    float _duration;
   


    IButtonInteractable buttonInteraction;

    private void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate { OpenUI(); });
    }
    public void OpenUI()
    {
        UINextPanelBehaviour UImove = new UINextPanelBehaviour(_currentUI, _nextUI, _currentUIEndPos, _nextUIEndPos, _duration);
        buttonInteraction = UImove;
        buttonInteraction.ButtonBehaviour();
       
    }        
}
