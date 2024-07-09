using UnityEngine;

public class SkinsSwiper : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objects;

    [SerializeField]
    private RectTransform swipeArea;

    [SerializeField]
    private float minSwipeDistance = 50f;

    private int currentIndex = 0;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping;

    void Start()
    {
        SetActiveObject(currentIndex);
    }

    void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount <= 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            StartSwipe(touch.position);
        }
        else if (touch.phase == TouchPhase.Ended && isSwiping)
        {
            EndSwipe(touch.position);
        }
    }

    private void StartSwipe(Vector2 touchPosition)
    {
        if (IsTouchWithinSwipeArea(touchPosition))
        {
            isSwiping = true;
            startTouchPosition = touchPosition;
        }
    }

    private void EndSwipe(Vector2 touchPosition)
    {
        endTouchPosition = touchPosition;
        isSwiping = false;
        ProcessSwipe();
    }

    private bool IsTouchWithinSwipeArea(Vector2 touchPosition)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(swipeArea, touchPosition, Camera.main, out Vector2 localPoint))
        {
            return swipeArea.rect.Contains(localPoint);
        }
        return false;
    }

    private void ProcessSwipe()
    {
        if (Vector2.Distance(startTouchPosition, endTouchPosition) < minSwipeDistance) return;

        Vector2 swipeDirection = (endTouchPosition - startTouchPosition).normalized;
        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            if (swipeDirection.x > 0)
            {
                SwitchToPreviousObject();
            }
            else
            {
                SwitchToNextObject();
            }
        }
    }

    private void SwitchToPreviousObject()
    {
        currentIndex = (currentIndex - 1 + objects.Length) % objects.Length;
        SetActiveObject(currentIndex);
    }

    private void SwitchToNextObject()
    {
        currentIndex = (currentIndex + 1) % objects.Length;
        SetActiveObject(currentIndex);
    }

    private void SetActiveObject(int index)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == index);
        }
    }
}
