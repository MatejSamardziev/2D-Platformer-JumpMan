using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishAudio;
    private bool levelCompleted = false;
    // Start is called before the first frame update
    void Start()
    {
        finishAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player"&& !levelCompleted)
        {
            finishAudio.Play();
            levelCompleted = true;
            Invoke("CompleteLevel",2f);

        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
