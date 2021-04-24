using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransfer : MonoBehaviour
{
    public int areaLoad;
    public int areaIn;

    private GameManager manager;
    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(manager.currentArea != areaLoad) {

            manager.SwitchArea(areaLoad);
        
        }
    }

}
