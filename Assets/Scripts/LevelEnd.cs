using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    private AudioSource EndingSound;
    private bool EndingCheck = false;
    // Start is called before the first frame update
    private void Start()
    {
        EndingSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !EndingCheck)
        {
            EndingSound.Play();
            EndingCheck = true;
            Invoke("NextLevel", 2f);
            
        }
    }
    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
