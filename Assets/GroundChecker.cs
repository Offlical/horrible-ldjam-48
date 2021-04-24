using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public LayerMask ground;
    public bool onGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collission");

        bool isGround = ground == (ground | (1 << collision.gameObject.layer));

        if (isGround)
        {
            onGround = true;
            Debug.Log("is ground");
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");

        bool isGround = ground == (ground | (1 << collision.gameObject.layer));

        if (isGround)
        {
            onGround = false;
            Debug.Log("exit ground");
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }
}
