using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckLevelProg : MonoBehaviour
{
    public Button level1, level2, level3, level4, level5;

    public Sprite l1Sprite, l2Sprite, l3Sprite, l4Sprite, l5Sprite, levelLocked;

    public GameObject zeroStar, oneStar, twoStar, threeStar;

    SaveObject so;

    //public Button[] levelButtons;
    //public Sprite[] levelSprites;

    private void Awake()
    {
        //levelProgress = PlayerPrefs.GetInt("levelProgress");
        Instantiate(twoStar, level1.transform);
        so = SaveManager.Load();

    }

    private void Update()
    {
        switch (so.levelProgress)
        {
            case 1:
                level1.GetComponent<Image>().sprite = l1Sprite;
                break;
            case 2:
                level1.GetComponent<Image>().sprite = l1Sprite;
                level2.GetComponent<Image>().sprite = l2Sprite;
                level2.interactable = true;
                break;
            case 3:
                level1.GetComponent<Image>().sprite = l1Sprite;
                level2.GetComponent<Image>().sprite = l2Sprite;
                level3.GetComponent<Image>().sprite = l3Sprite;
                level2.interactable = true;
                level3.interactable = true;
                break;
            case 4:
                level1.GetComponent<Image>().sprite = l1Sprite;
                level2.GetComponent<Image>().sprite = l2Sprite;
                level3.GetComponent<Image>().sprite = l3Sprite;
                level4.GetComponent<Image>().sprite = l4Sprite;
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                break;
            case 5:
                level1.GetComponent<Image>().sprite = l1Sprite;
                level2.GetComponent<Image>().sprite = l2Sprite;
                level3.GetComponent<Image>().sprite = l3Sprite;
                level4.GetComponent<Image>().sprite = l4Sprite;
                level5.GetComponent<Image>().sprite = l5Sprite;
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                level5.interactable = true;
                break;
            default:               
                break;




        }
       

        

    }
}
