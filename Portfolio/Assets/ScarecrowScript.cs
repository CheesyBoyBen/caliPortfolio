using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowScript : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickUp()
    {
        transform.parent = player.transform;
        transform.localPosition = new Vector3(0f, -1.15f, 2f);
    }

    public void Drop()
    {
        transform.parent = null;
    }
}
