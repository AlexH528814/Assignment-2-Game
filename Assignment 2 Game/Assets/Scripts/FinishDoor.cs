using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishDoor : MonoBehaviour
{
    private AudioSource finishSound;
    private Animator animator;
    private SpriteRenderer sprite;

    [SerializeField] private GameObject script;

    private bool levelcompleted = false;
   

    // Start is called before the first frame update
    void Start()
    {
        finishSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
      
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemCollector collector = GetComponent<ItemCollector>();

        if (collision.gameObject.name == "Player" && !levelcompleted)
        {        
            finishSound.Play();
            levelcompleted = true;
            animator.SetTrigger("opened");

            Invoke("sortingOrderup", 0.5f);

            Invoke("CompleteLevel", 1f);
        }

    }

    void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void sortingOrderup()
    {
        sprite.sortingOrder = 1;
    }

}
