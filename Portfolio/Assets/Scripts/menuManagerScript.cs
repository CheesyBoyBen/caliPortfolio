using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class menuManagerScript : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject background;
    public GameObject sfx;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("background", 1.0f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start()
    {
        StartCoroutine(startWait());

    }

    public void options()
    {
        StartCoroutine(optionstWait());

    }

    public void exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void back()
    {
        StartCoroutine(backWait());

    }

    public IEnumerator startWait()
    {

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Lvl 1");


    }

    public IEnumerator optionstWait()
    {

        yield return new WaitForSeconds(0.3f);
        optionsMenu.active = true;
        mainMenu.active = false;


    }

    public IEnumerator backWait()
    {

        yield return new WaitForSeconds(0.3f);
        optionsMenu.active = false;
        mainMenu.active = true;


    }

    public void backgroundValue()
    {
        PlayerPrefs.SetFloat("background", background.GetComponent<Slider>().value);

    }

    public void sfxValue()
    {
        AudioListener.volume = sfx.GetComponent<Slider>().value;
    }



}
