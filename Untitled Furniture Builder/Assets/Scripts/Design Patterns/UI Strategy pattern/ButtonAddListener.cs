using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public abstract class ButtonAddListener : MonoBehaviour

{
  




    protected GameManager _instance;
    //[SerializeField] bool _isBackButton;
    EventTrigger.Entry entry, exit;

    #region Event Subscription Code Region
    protected void OnEnable()
    {
        
        this.GetComponent<Button>().onClick.AddListener(delegate { ButtonAction(); });
        

        if (this.GetComponent<EventTrigger>() == null)
            return;
        #region Event Trigger to code
        //EventTrigger trigger = GetComponent<EventTrigger>();
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((BaseEventData) => { HoverEnterAction(); });
        this.gameObject.GetComponent<EventTrigger>().triggers.Add(entry);
        #endregion

        #region Event Trigger to code
        //EventTrigger trigger = GetComponent<EventTrigger>();
        exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerDown;
        exit.callback.AddListener((BaseEventData) => { PointerClickAction(); });
        this.gameObject.GetComponent<EventTrigger>().triggers.Add(exit);
        #endregion
    }
    protected void UnSubscribe()
    {

        if (this.GetComponent<Button>().onClick != null)
            this.GetComponent<Button>().onClick.RemoveListener(delegate { ButtonAction(); });
    }
    protected void OnDisable()
    {

        UnSubscribe();
    }
    protected void OnDestroy()
    {
        UnSubscribe();

    }
    #endregion

    protected virtual void Awake()
    {
          
    }

    protected  void Start()
    {
        _instance = GameManager.Instance;
    }
    public abstract ICommand ReturnButtonBehaviour();
    protected virtual void ButtonAction()
    {
        if (_instance == null)
            Debug.Log("Singleton instance is null");
        else
        {
           
                _instance.PerformButtonBehaviour(ReturnButtonBehaviour());
        }
    }
    protected virtual void HoverEnterAction()
    {
        Debug.Log("Hovering Enter Action");
        _instance.AudioManager.PlayHoverAudio();
    }

    protected virtual void PointerClickAction()
    {
        Debug.Log("Hovering Exit Action");
        _instance.AudioManager.PlayClickAudio();
    }



}
