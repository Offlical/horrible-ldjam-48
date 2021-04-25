using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.AddLight(50f);
            Destroy(gameObject);
        }
    }
}
