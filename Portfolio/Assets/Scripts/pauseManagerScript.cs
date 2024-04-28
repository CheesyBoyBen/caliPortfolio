using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class pauseManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.gameObject.active = false;
    }
    public void restart()
    {
        StartCoroutine(restartWait());

    }
    public void home()
    {
        StartCoroutine(homeWait());


    }
    public IEnumerator restartWait()
    {

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }
    public IEnumerator homeWait()
    {

        yield return new WaitForSeconds(1);
        
        Destroy(GameObject.Find("Background Music"));
        SceneManager.LoadScene("Main Menu");


    }
}
