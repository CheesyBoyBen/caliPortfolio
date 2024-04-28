using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class levelManagerScript : MonoBehaviour
{
    public int totalScarecrows;
    public string level;
    GameObject door;
    public GameObject pause;

    public TMP_Text scarecrowText;

    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.Find("Door");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.active = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void complete()
    {
        
        door.GetComponent<BoxCollider>().enabled = true;
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(level);
    }

    public void updateText(int scarecrows)
    {
        scarecrowText.text = scarecrows.ToString();
    }
}
