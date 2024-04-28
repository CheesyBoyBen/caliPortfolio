using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class completeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void home()
    {
        StartCoroutine(homeWait());


    }

    public IEnumerator homeWait()
    {

        yield return new WaitForSeconds(1);

        Destroy(GameObject.Find("Background Music"));
        SceneManager.LoadScene("Main Menu");


    }
}
