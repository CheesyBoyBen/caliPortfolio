using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = (1 / AudioListener.volume) * (0.25f * PlayerPrefs.GetFloat("background"));

    }
}
