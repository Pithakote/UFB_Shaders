using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public GameObject audioObj1;
    public GameObject audioObj2;
    public GameObject audioObj3;

    public void SpawnAudio1()
    {
        Instantiate(audioObj1, transform.position, transform.rotation);
    }
    public void SpawnAudio2()
    {
        Instantiate(audioObj2, transform.position, transform.rotation);
    }
    public void SpawnAudio3()
    {
        Instantiate(audioObj3, transform.position, transform.rotation);
    }
}
