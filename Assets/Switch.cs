using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public GameObject popUp;
    public UnityEvent onSwitch;
    public Sprite onSprite;
    public bool canTurnOn = false;
    public bool turnedOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        popUp.SetActive(true);
        canTurnOn = true;
    }

    private void Update()
    {
        if(canTurnOn && Input.GetKeyDown(KeyCode.E))
        {
            canTurnOn = false;
            GetComponent<SpriteRenderer>().sprite = onSprite;
            popUp.SetActive(false);
            turnedOn = true;
            GetComponent<Collider2D>().enabled = false;
           // onSwitch.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        popUp.SetActive(false);
        canTurnOn = false;
    }

}
