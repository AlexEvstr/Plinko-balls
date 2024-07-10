using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenCups : MonoBehaviour
{
    public Transform cup1;
    public Transform cup2;
    public Transform cup3;
    public float speed = 2.0f;
    public float delayBetweenShuffles = 2.0f;
    private Vector3[] positions;
    private Transform[] cups;

    void Start()
    {
        positions = new Vector3[3];
        positions[0] = cup1.position;
        positions[1] = cup2.position;
        positions[2] = cup3.position;

        cups = new Transform[] { cup1, cup2, cup3 };

        StartCoroutine(ShuffleCups());


        StartCoroutine(LoadMenuScene());
    }

    private IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MenuScene");
    }

    IEnumerator ShuffleCups()
    {
        while (true)
        {
            int index1 = Random.Range(0, cups.Length);
            int index2 = Random.Range(0, cups.Length);
            while (index2 == index1)
            {
                index2 = Random.Range(0, cups.Length);
            }

            Transform cupA = cups[index1];
            Transform cupB = cups[index2];
            Vector3 posA = positions[index1];
            Vector3 posB = positions[index2];

            float elapsedTime = 0;

            while (elapsedTime < speed)
            {
                cupA.position = Vector3.Lerp(posA, posB, (elapsedTime / speed));
                cupB.position = Vector3.Lerp(posB, posA, (elapsedTime / speed));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            cupA.position = posB;
            cupB.position = posA;

            positions[index1] = posB;
            positions[index2] = posA;

            yield return new WaitForSeconds(delayBetweenShuffles);
        }
    }
}