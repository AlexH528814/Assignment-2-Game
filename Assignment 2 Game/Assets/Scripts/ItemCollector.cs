using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public int coin { get { return coins; } }
    [SerializeField] int coins = 0;

    public Text coinText { get { return coinsText; } }
    [SerializeField] private Text coinsText;
    [SerializeField] private AudioSource collectItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);

            collectItem.Play();

            coins = coins + 1;         
            
            coinsText.text = "Coins:" + coins;
        }
    }
}
