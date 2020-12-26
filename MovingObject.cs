using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    public float speed;

    private Vector3 vector;

    public float runSpeed;
    private float ApplyrunSpeed;
    private bool applyRunFlag;

    public int walkCount;
    private int currentWalkcount;

    private bool canMove = true;


    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
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

            RaycastHit2D hit;

            Vector2 Start = transform.position;
            Vector2 End = Start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(Start, End, layerMask);
            boxCollider.enabled = true;

            if (hit.transform != null)
                break;

            animator.SetBool("Walking", true);

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
