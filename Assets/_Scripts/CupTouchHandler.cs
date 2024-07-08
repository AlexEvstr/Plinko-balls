using UnityEngine;
using System.Collections;

public class CupTouchHandler : MonoBehaviour
{
    private CupShuffler _cupShuffler;

    private void Start()
    {
        _cupShuffler = GetComponent<CupShuffler>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && _cupShuffler.CanChooseCup)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 touchPosition2D = new Vector2(touchWorldPosition.x, touchWorldPosition.y);

                RaycastHit2D hit = Physics2D.Raycast(touchPosition2D, Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.CompareTag("Cup"))
                    {
                        StartCoroutine(MoveAndRotateCup(hit.collider.gameObject, new Vector3(0, 0.6f, 0), Quaternion.Euler(0, 0, 210), 0.5f));

                        if (hit.collider.gameObject.transform.childCount > 1)
                        {
                            OnCupTouchedWithChildren(hit.collider.gameObject);
                        }
                        else
                        {
                            OnCupTouchedWithoutChildren(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }

    void OnCupTouchedWithChildren(GameObject cup)
    {
        Debug.Log("true");
        Transform child = cup.transform.GetChild(1);
        StartCoroutine(MoveChildToPosition(child, new Vector2(0.1f, 1f), 1f));

    }

    void OnCupTouchedWithoutChildren(GameObject cup)
    {
        Debug.Log("false");
        StartCoroutine(HandleCupWithoutChildren(cup, 1.0f));
    }

    private IEnumerator MoveAndRotateCup(GameObject cup, Vector3 positionChange, Quaternion targetRotation, float duration)
    {
        Vector3 startPosition = cup.transform.position;
        Quaternion startRotation = cup.transform.rotation;
        Vector3 endPosition = startPosition + positionChange;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            cup.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            cup.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cup.transform.position = endPosition;
        cup.transform.rotation = targetRotation;
    }

    private IEnumerator HandleCupWithoutChildren(GameObject cup, float delay)
    {
        Vector3 originalPosition = cup.transform.position - new Vector3(0, 0, 0);
        Quaternion originalRotation = Quaternion.Euler(0, 0, 180);

        yield return new WaitForSeconds(delay);

        Vector3 startPosition = cup.transform.position;
        Quaternion startRotation = cup.transform.rotation;
        float duration = 0.5f;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            cup.transform.position = Vector3.Lerp(startPosition, originalPosition, elapsedTime / duration);
            cup.transform.rotation = Quaternion.Lerp(startRotation, originalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cup.transform.position = originalPosition;
        cup.transform.rotation = originalRotation;

        // Поднимаем все стаканы
        foreach (GameObject cupObject in GameObject.FindGameObjectsWithTag("Cup"))
        {
            StartCoroutine(MoveCupUp(cupObject, 0.6f, 0.5f));
            cupObject.transform.GetChild(0).gameObject.SetActive(true);
            if (cupObject.transform.childCount > 1)
            {
                cupObject.transform.GetChild(1).gameObject.SetActive(true);
                StartCoroutine(MoveChildToPosition(cupObject.transform.GetChild(1), new Vector2(0.1f, 1f), 1f));
            }
        }


    }

    private IEnumerator MoveCupUp(GameObject cup, float distance, float duration)
    {
        Vector3 startPosition = cup.transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, distance, 0);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            cup.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cup.transform.position = endPosition;

        foreach (var cupObject in GameObject.FindGameObjectsWithTag("Cup"))
        {
            if (cupObject.transform.childCount > 0)
            {
                //OnCupTouchedWithChildren(cupObject);
            }
        }
    }

    private IEnumerator MoveChildToPosition(Transform child, Vector2 targetPosition, float duration)
    {
        yield return new WaitForSeconds(0.5f);
        child.gameObject.SetActive(true);
        Vector2 startPosition = child.localPosition;
        float elapsedTime = 0;

        foreach (var cup in GameObject.FindGameObjectsWithTag("Cup"))
        {
            if (cup.transform.childCount > 1)
            {
                cup.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        while (elapsedTime < duration)
        {
            child.localPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        child.localPosition = targetPosition;

        
    }
}
