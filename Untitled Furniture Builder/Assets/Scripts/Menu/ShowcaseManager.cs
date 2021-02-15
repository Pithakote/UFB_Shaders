using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseManager : MonoBehaviour
{
    [Header("Objects to spawn")]
    [SerializeField]
    GameObject level1;
    [SerializeField]
    GameObject level2, level3, level4, level5;

    [Header("Object Positions")]
    [SerializeField]
    GameObject level1Pos;
    [SerializeField]
    GameObject level2Pos, level3Pos, level4Pos, level5Pos;

    SaveObject so;
    private void Start()
    {
        so = SaveManager.Load();
        if (so.l1Complete)
        {
            Instantiate(level1, level1Pos.transform.position, Quaternion.identity);
        }
        if (so.l2Complete)
        {
            Instantiate(level2, level2Pos.transform.position, Quaternion.identity);
        }
        if (so.l3Complete)
        {
            Instantiate(level3, level3Pos.transform.position, Quaternion.identity);
        }
        if (so.l4Complete)
        {
            Instantiate(level4, level4Pos.transform.position, Quaternion.identity);
        }
        if (so.l5Complete)
        {
            Instantiate(level5, level5Pos.transform.position, Quaternion.identity);
        }

        SaveManager.Save(so);
    }
}
