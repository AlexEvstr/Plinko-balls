using UnityEngine;

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
    }
}