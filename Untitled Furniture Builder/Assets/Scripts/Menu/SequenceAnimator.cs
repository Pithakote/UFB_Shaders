using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SequenceAnimator : MonoBehaviour
{
    List<Animator> _animators;
    public float WaitBetween = 0.1f;
    public float WaitEnd = 3f;
    public GameObject[] titleText;
    List<RectTransform> titleLetters;
    Coroutine myCoroutine;

    void OnEnable()        
    {
        _animators = new List<Animator>(GetComponentsInChildren<Animator>());   // get reference to the animator in each child
        foreach (var animator in _animators)
        {
            animator.ResetTrigger("DoAnim");
        }
        myCoroutine = StartCoroutine(DoAnimation());      // start the coroutine
    }
    private void OnDisable()
    {
        StopCoroutine(myCoroutine);
       
    }

    IEnumerator DoAnimation()
    {
        while (true)
        {
            foreach (var animator in _animators)        // loop over each animator in the list of animators
            {
                animator.SetTrigger("DoAnim");          // set the trigger which causes the text objects to trigger their animation
                yield return new WaitForSeconds(WaitBetween);   // wait for some time inbetween each animation
            }

            yield return new WaitForSeconds(WaitEnd);       // after each text component has animated, it waits for a few seconds until starting the coroutine again
        }
    }
}
