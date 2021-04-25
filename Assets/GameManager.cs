using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static int instanceID = -1;
    public GameObject grid;
    public GameObject endings;

    public GameObject startLight;

    public int currentArea;
    public bool gameEnded = false;
    public bool gameStart = false;


    private void Awake()
    { 

        startLight = GameObject.FindGameObjectWithTag("StartLight");
        grid = GameObject.FindGameObjectWithTag("Grid");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(grid == null)
             grid = GameObject.FindGameObjectWithTag("Grid");
        else if(startLight == null)
            startLight = GameObject.FindGameObjectWithTag("StartLight");
    }


    public void ToggleStartLight()
    {
        startLight.SetActive(!startLight.activeInHierarchy);
    }

    public void SwitchArea(int area)
    {
        if (!gameStart && currentArea == 0)
        {
            gameStart = true;
            startLight.SetActive(false);
        }

        Transform oldArea = grid.transform.GetChild(currentArea);
        Debug.Log(oldArea.gameObject.name);
        oldArea.gameObject.SetActive(!oldArea.gameObject.activeSelf);
        

        Transform areaTransform = grid.transform.GetChild(area);
        areaTransform.gameObject.SetActive(true);

        Camera camera = Camera.main;
        camera.transform.position = areaTransform.GetChild(0).position;

        currentArea = area;
      
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentArea = 0;
        gameEnded = false;
        gameStart = false;
        startLight = GameObject.FindGameObjectWithTag("StartLight");
        grid = GameObject.FindGameObjectWithTag("Grid");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
  
}
