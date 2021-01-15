using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public string characterName;

    public BoxCollider2D boxCollider;
    public LayerMask layerMask;

    public float speed;
    protected Vector3 vector;
    public int walkCount;
    protected int currentWalkcount;
    public Animator animator;

    protected bool NPCcanMove;

    public Queue<string> queue;
    public void Move(string _dir,int _frequency=5)
    {
        StartCoroutine(MoveCoroutine(_dir, _frequency));
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
       while(queue.Count != 0)
        {
            string direction = queue.Dequeue();
            NPCcanMove = false;
            vector.Set(0, 0, vector.z);

            switch (direction)
            {
                case "UP":
                    vector.y = 1f;
                    break;
                case "DOWN":
                    vector.y = -1f;
                    break;
                case "LEFT":
                    vector.x = -1f;
                    break;
                case "RIGHT":
                    vector.x = 1f;
                    break;
            }
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

            while (currentWalkcount < walkCount)
            {
                transform.Translate(vector.x * speed, vector.y * speed, 0);
                currentWalkcount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkcount = 0;
            if (_frequency != 5)
                animator.SetBool("Walking", false);
            NPCcanMove = true;
        }
        animator.SetBool("Walking", false);
    }

    protected bool CheckCollision()
    {
        RaycastHit2D hit;
        Vector2 Start = transform.position;
        Vector2 End = Start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(Start, End, layerMask);
        boxCollider.enabled = true;

        if (hit.transform != null)
            return true;

        return false;
    }
}

