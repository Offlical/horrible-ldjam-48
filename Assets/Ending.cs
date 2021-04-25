using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{

    private Player player;

    public int endingID;
    public string endingName;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.Ending(endingID, endingName);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
