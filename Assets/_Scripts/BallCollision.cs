using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class BallCollision : MonoBehaviour
{
    [SerializeField] private CupShuffler _cupShuffler;
    [SerializeField] private GameFeedbackManager _gameFeedbackManager;

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cup"))
        {
            _gameFeedbackManager.PlayCupCollisionSound();
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            yield return StartCoroutine(IncreaseCupSize(collision.gameObject));
            gameObject.SetActive(false);
            _cupShuffler.BeginShuffle();
            
            foreach (var cups in GameObject.FindGameObjectsWithTag("Cup"))
            {
                cups.transform.GetChild(0).gameObject.SetActive(false);
                cups.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }

        else if (collision.gameObject.CompareTag("CircleField"))
        {
            _gameFeedbackManager.PlayShotFeedback();
            StartCoroutine(IncreaseCircleFieldSize(collision.gameObject));

        }
        else if (collision.gameObject.CompareTag("BottomBorder"))
        {
            _gameFeedbackManager.PlaybottomCollisionSound();
        }
    }

    private IEnumerator IncreaseCupSize(GameObject cup)
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        transform.position = new Vector2(cup.transform.position.x, cup.transform.position.y);
        cup.gameObject.transform.localScale = new Vector2(1.2f, 1.2f);
        yield return new WaitForSeconds(0.1f);
        cup.gameObject.transform.localScale = new Vector2(1f, 1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        transform.SetParent(cup.transform);
    }

    private IEnumerator IncreaseCircleFieldSize(GameObject circle)
    {
        circle.gameObject.transform.localScale = new Vector2(0.3f, 0.3f);
        yield return new WaitForSeconds(0.1f);
        circle.gameObject.transform.localScale = new Vector2(0.25f, 0.25f);
    }
}