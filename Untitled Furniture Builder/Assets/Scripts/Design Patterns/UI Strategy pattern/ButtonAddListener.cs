using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class ButtonAddListener : MonoBehaviour

{
  




    protected GameManager _instance;
    [SerializeField] bool _isBackButton;
    

    #region Event Subscription Code Region
    protected void OnEnable()
    {
        
        this.GetComponent<Button>().onClick.AddListener(delegate { ButtonAction(); });
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
            if (!_isBackButton)
                _instance.PerformButtonBehaviour(ReturnButtonBehaviour());
          //  else
         //       _instance.PerformUndoBehaviour(ReturnUndoButtonBehaviour());
        }
    }

   

 

   
}
