using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static int instanceID = -1;
    public GameObject grid;

    public int currentArea;
    public bool gameEnded = false;


    private void Awake()
    {
        if (instanceID == -1)
            instanceID = gameObject.GetInstanceID();
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchArea(int area)
    {
        Transform oldArea = grid.transform.GetChild(area);
        oldArea.gameObject.SetActive(false);

        Transform areaTransform = grid.transform.GetChild(area);
        areaTransform.gameObject.SetActive(true);

        Camera camera = Camera.main;
        camera.transform.position = areaTransform.GetChild(0).position;

        currentArea = area;
      
    }
}
