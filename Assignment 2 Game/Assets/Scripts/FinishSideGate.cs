using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishSideGate : MonoBehaviour
{
    private AudioSource finishSound;

    private SpriteRenderer sprite;

    private bool levelcompleted = false;


    // Start is called before the first frame update
    void Start()
    {
        finishSound = GetComponent<AudioSource>();
       
        sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelcompleted)
        {
            finishSound.Play();
            levelcompleted = true;
            sprite.sortingOrder = 1;
            Invoke("CompleteLevel", 1f);
        }
    }

    void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
