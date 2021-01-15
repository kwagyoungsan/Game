using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    static public PlayerManager instance;
    public string currentMapName; // tranferMapName 스크립트에 있는 transferMapName 변수 값을 저장

    public float runSpeed;
    private float ApplyrunSpeed;
    private bool applyRunFlag;

    private bool canMove = true;

    public string walkSound_1;
    public string walkSound_2;
    public string walkSound_3;
    public string walkSound_4;

    private AudioManager theAudio;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            queue = new Queue<string>();
            instance = this;
            DontDestroyOnLoad(this.gameObject); // 객체가 파괴되지 않고 저장
            animator = GetComponent<Animator>();
            theAudio = FindObjectOfType<AudioManager>();
            boxCollider = GetComponent<BoxCollider2D>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ApplyrunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                ApplyrunSpeed = 0;
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            bool checkCollisionFlag = base.CheckCollision();
            if (checkCollisionFlag)
                break;

            animator.SetBool("Walking", true);

            int temp = Random.Range(1, 4);
            switch (temp)
            {
                case 1:
                    theAudio.Play(walkSound_1);
                    break;
                case 2:
                    theAudio.Play(walkSound_2);
                    break;
                case 3:
                    theAudio.Play(walkSound_3);
                    break;
                case 4:
                    theAudio.Play(walkSound_4);
                    break;
            }

            while (currentWalkcount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + ApplyrunSpeed), 0, 0);
                }

                else
                {
                    transform.Translate(0, vector.y * (speed + ApplyrunSpeed), 0);
                }
                if (applyRunFlag)
                    currentWalkcount++;
                currentWalkcount++;
                yield return new WaitForSeconds(0.01f);


            }
            currentWalkcount = 0;

        }
        animator.SetBool("Walking", false);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

    }
}
