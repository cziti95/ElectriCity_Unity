using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    public float maxTime;
    public float minSwipeDistance;

    private float startTime;
    private float endTime;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float swipeDistance;
    private float swipeTime;

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    startTime = Time.time;
                    startPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    endTime = Time.time;
                    endPosition = touch.position;

                    swipeDistance = (endPosition - startPosition).magnitude;
                    swipeTime = endTime - startTime;

                    if (swipeTime < maxTime && swipeDistance > minSwipeDistance)
                    {
                        Swipe();
                    }

                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTime = Time.time;
                startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            } else if (Input.GetMouseButtonUp(0))
            {
                endTime = Time.time;
                endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                swipeDistance = (endPosition - startPosition).magnitude;
                swipeTime = endTime - startTime;

                if (swipeTime < maxTime && swipeDistance > minSwipeDistance)
                {
                    Swipe();
                }
            }
        }
    }

    private void Swipe()
    {
        Vector2 distance = endPosition - startPosition;
        if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            if (distance.x > 0)
            {
                GameManager.instance.NextPole("Right");
            }
            if (distance.x < 0)
            {
                GameManager.instance.NextPole("Left");
            }
        }
        else if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
        {
            if (distance.y > 0)
            {
                GameManager.instance.NextPole("Up");
            }
            if (distance.y < 0)
            {
                GameManager.instance.NextPole("Down");
            }
        }
    }
}
