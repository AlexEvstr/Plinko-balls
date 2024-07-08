using System.Collections;
using UnityEngine;

public class ShuffleCups : MonoBehaviour
{
    [SerializeField] private Transform[] cups;
    private float swapDuration = 0.5f;
    private int numberOfSwaps = 10;
    private float flipDuration = 1.0f;

    public void BeginShuffle()
    {
        StartCoroutine(Shuffle());
    }

    private IEnumerator Shuffle()
    {
        yield return new WaitForSeconds(1.0f);

        yield return StartCoroutine(FlipCups());

        for (int i = 0; i < numberOfSwaps; i++)
        {
            int indexA = Random.Range(0, cups.Length);
            int indexB;
            do
            {
                indexB = Random.Range(0, cups.Length);
            } while (indexA == indexB);

            yield return StartCoroutine(SwapCups(cups[indexA], cups[indexB]));
        }
    }

    private IEnumerator FlipCups()
    {
        Quaternion startRotation = Quaternion.identity;
        Quaternion endRotation = Quaternion.Euler(0, 0, 180);

        float elapsedTime = 0;
        while (elapsedTime < flipDuration)
        {
            foreach (Transform cup in cups)
            {
                cup.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / flipDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (Transform cup in cups)
        {
            cup.rotation = endRotation;
        }
    }

    private IEnumerator SwapCups(Transform cupA, Transform cupB)
    {
        Vector3 positionA = cupA.position;
        Vector3 positionB = cupB.position;

        float elapsedTime = 0;
        while (elapsedTime < swapDuration)
        {
            cupA.position = Vector3.Lerp(positionA, positionB, elapsedTime / swapDuration);
            cupB.position = Vector3.Lerp(positionB, positionA, elapsedTime / swapDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cupA.position = positionB;
        cupB.position = positionA;
    }
}