using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckLevelProg : MonoBehaviour
{
    public Button tutorialButton, level1, level2, level3, level4, level5;

    public Sprite l1Sprite, l2Sprite, l3Sprite, l4Sprite, l5Sprite, levelLocked;

    public GameObject zeroStar, oneStar, twoStar, threeStar;

    public SaveObject so;

    //public Button[] levelButtons;
    //public Sprite[] levelSprites;

    private void Awake()
    {
        //levelProgress = PlayerPrefs.GetInt("levelProgress");
        //Instantiate(twoStar, level1.transform);
        //SaveManager.Save(so);
        if (!SaveManager.SaveExists())
           SaveManager.Save(so);        
        else
        {
            so = SaveManager.Load();
            CheckLevelProgress();
            CheckLevelRating();
        }
        //Debug.Log(so.tutorialComplete);
        //Debug.Log("level prog: " + so.levelProgress);


        
    }


    void CheckLevelProgress()
    {

        switch (so.levelProgress)
        {
            case 0:
                Instantiate(zeroStar, tutorialButton.transform);
                break;
            case 1:
                level1.GetComponent<Image>().sprite = l1Sprite;
                level1.interactable = true;           
                Instantiate(zeroStar, level1.transform);
               
                break;
            case 2:
                level1.GetComponent<Image>().sprite = l1Sprite;
                level2.GetComponent<Image>().sprite = l2Sprite;
                level1.interactable = true;
                level2.interactable = true;
                Instantiate(zeroStar, level2.transform);
                break;
            case 3:
                level1.GetComponent<Image>().sprite = l1Sprite;
                level2.GetComponent<Image>().sprite = l2Sprite;
                level3.GetComponent<Image>().sprite = l3Sprite;
                level1.interactable = true;
                level2.interactable = true;
                level3.interactable = true;
                Instantiate(zeroStar, level3.transform);
                break;
            case 4:
                level1.GetComponent<Image>().sprite = l1Sprite;
                level2.GetComponent<Image>().sprite = l2Sprite;
                level3.GetComponent<Image>().sprite = l3Sprite;
                level4.GetComponent<Image>().sprite = l4Sprite;
                level1.interactable = true;
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                Instantiate(zeroStar, level4.transform);
                break;
            case 5:
                level1.GetComponent<Image>().sprite = l1Sprite;
                level2.GetComponent<Image>().sprite = l2Sprite;
                level3.GetComponent<Image>().sprite = l3Sprite;
                level4.GetComponent<Image>().sprite = l4Sprite;
                level5.GetComponent<Image>().sprite = l5Sprite;
                level1.interactable = true;
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                level5.interactable = true;
                Instantiate(zeroStar, level4.transform);
                break;
            default:
                break;
        }
    }

    void CheckLevelRating()
    {
        switch (so.tutorialRating)
        {
            
            case 1:
                Instantiate(oneStar, tutorialButton.transform);
                break;
            case 2:
                Instantiate(twoStar, tutorialButton.transform);
                break;
            case 3:
                Instantiate(threeStar, tutorialButton.transform);
                break;
            default:
                break;
        }

        switch (so.l1Rating)
        {
            
            case 1:
                Instantiate(oneStar, level1.transform);
                break;
            case 2:
                Instantiate(twoStar, level1.transform);
                break;
            case 3:
                Instantiate(threeStar, level1.transform);
                break;
            default:
                break;
        }
    }
}
