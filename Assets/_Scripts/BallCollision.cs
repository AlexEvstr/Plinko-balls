using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallCollision : MonoBehaviour
{
    [SerializeField] private ShuffleCups _shuffleCups;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cup"))
        {
            transform.SetParent(collision.transform);
            gameObject.SetActive(false);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            _shuffleCups.BeginShuffle();
        }
    }
}