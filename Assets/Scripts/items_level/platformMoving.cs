using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMoving : MonoBehaviour
{
    public bool activatePlatform;

    public float moveSpeed;
    float move;
    public float moveDistance;
    public bool movesHorizontal;

    Vector2 startPosition;
    Vector2 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition.x = startPosition.x + moveDistance;
        endPosition.y = startPosition.y + moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(movesHorizontal)
        {
            if (moveDistance > 0)
            {
                if (transform.position.x >= endPosition.x)
                {
                    move = -moveSpeed;
                }
                else if (transform.position.x <= startPosition.x)
                {
                    move = moveSpeed;
                }
            }
            else
            {
                if (transform.position.x <= endPosition.x)
                {
                    move = moveSpeed; 
                }
                else if (transform.position.x >= startPosition.x)
                {
                    move = -moveSpeed;
                }
            }

        } else 
        {
            if (moveDistance > 0)
            {
                if (transform.position.y >= endPosition.y)
                {
                    move = -moveSpeed;
                }
                else if (transform.position.y <= startPosition.y)
                {
                    move = moveSpeed;
                }
            }
            else
            {
                if (transform.position.y <= endPosition.y)
                {
                    move = moveSpeed; 
                }
                else if (transform.position.y >= startPosition.y)
                {
                    move = -moveSpeed;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (activatePlatform)
        {
            if (movesHorizontal)
            {                
                transform.position += (new Vector3(move, 0, 0) * Time.fixedDeltaTime);
            }
            else
            {                
                transform.position += (new Vector3(0, move, 0) * Time.fixedDeltaTime);
            }
        }
    }

    

    


}
