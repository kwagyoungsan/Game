using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript_1 : MonoBehaviour
{

    BGMManager BGM;

    public int playMusicPlay;
    // Start is called before the first frame update
    void Start()
    {
        BGM = FindObjectOfType<BGMManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BGM.Play(playMusicPlay);
        this.gameObject.SetActive(false);
    }

}
