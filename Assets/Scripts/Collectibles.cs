using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collectibles : MonoBehaviour
{

    private int score = 0;

    [SerializeField] private AudioSource CollectingSound;
    [SerializeField] private Text scoreText;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            CollectingSound.Play();
            Destroy(collision.gameObject);
            score = score + 1;
            scoreText.text = "score: " + score;
        }
    }
}
