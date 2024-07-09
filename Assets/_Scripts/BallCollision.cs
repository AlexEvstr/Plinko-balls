using System.Collections;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class BallCollision : MonoBehaviour
{
    [SerializeField] private CupShuffler _cupShuffler;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cup"))
        {
            transform.SetParent(collision.transform);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            _cupShuffler.BeginShuffle();
            gameObject.SetActive(false);

            foreach (var cups in GameObject.FindGameObjectsWithTag("Cup"))
            {
                cups.transform.GetChild(0).gameObject.SetActive(false);
                cups.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }

        else if (collision.gameObject.CompareTag("CircleField"))
        {
            StartCoroutine(IncreaseCircleFieldSize(collision.gameObject));

        }
        else if (collision.gameObject.CompareTag("BottomBorder"))
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private IEnumerator IncreaseCircleFieldSize(GameObject circle)
    {
        circle.gameObject.transform.localScale = new Vector2(0.3f, 0.3f);
        yield return new WaitForSeconds(0.1f);
        circle.gameObject.transform.localScale = new Vector2(0.25f, 0.25f);
    }
}