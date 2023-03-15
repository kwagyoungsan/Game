using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{

    public string transferMapName;

    public Transform target;
    public BoxCollider2D target_bound;
    private CameraManager theCamera;
    private MovingObject thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent : 단일 객체 / FindObjectOfType : 다수 객체
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<MovingObject>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            
            thePlayer.currentMapName = transferMapName;
            //SceneManager.LoadScene(transferMapName);
            theCamera.setBound(target_bound);
            theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position = target.transform.position;
            
        }
    }

}
