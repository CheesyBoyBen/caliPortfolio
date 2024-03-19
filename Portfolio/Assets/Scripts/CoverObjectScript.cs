using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverObjectScript : MonoBehaviour
{
    private GameObject player;
    
    private bool north;
    private bool south;
    private bool east;
    private bool west;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");


        north = false;
        south = false;
        east = false;
        west = false;
    }

    // Update is called once per frame
    void Update()
    {

        #region Handles Direction
        if (player.transform.position.x < (transform.position.x - (transform.localScale.x / 2)))
        {
            south = true;
            north = false;
        }
        else if (player.transform.position.x > (transform.position.x + (transform.localScale.x / 2)))
        {
            south = false;
            north = true;
        }
        else
        {
            south = false;
            north = false;
        }

        if (player.transform.position.z < (transform.position.z - (transform.localScale.z / 2)))
        {
            west = false;
            east = true;
        }
        else if (player.transform.position.z > (transform.position.z + (transform.localScale.z / 2)))
        {
            west = true;
            east = false;
        }
        else
        {
            west = false;
            east = false;
        }

        #endregion


        #region Handles Light Cover

        transform.Find("NEL").GetChild(0).GetComponent<CoverPointScript>().coverOverride = false;
        transform.Find("SWR").GetChild(0).GetComponent<CoverPointScript>().coverOverride = false;
        transform.Find("SWL").GetChild(0).GetComponent<CoverPointScript>().coverOverride = false;
        transform.Find("SER").GetChild(0).GetComponent<CoverPointScript>().coverOverride = false;
        transform.Find("SEL").GetChild(0).GetComponent<CoverPointScript>().coverOverride = false;
        transform.Find("NWL").GetChild(0).GetComponent<CoverPointScript>().coverOverride = false;
        transform.Find("NWR").GetChild(0).GetComponent<CoverPointScript>().coverOverride = false;
        transform.Find("NER").GetChild(0).GetComponent<CoverPointScript>().coverOverride = false;


        if (north)
            {
                if (east)
                {
                transform.Find("NWL").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true;
                transform.Find("SER").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;

                }
                else if (west)
                {
                transform.Find("NER").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;
                transform.Find("SWL").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;

                }
                else
                {
                transform.Find("NWL").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;
                transform.Find("NER").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;

                }
            }
            else if (south)
            {
                if (east)
                {
                transform.Find("NEL").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;
                transform.Find("SWR").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;

                }
                else if (west)
                {
                transform.Find("NWR").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;
                transform.Find("SEL").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;

                }
                else
                {
                transform.Find("SWR").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;
                transform.Find("SEL").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;

                }
            }
            else
            {
                if (east)
                {
                transform.Find("NEL").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;
                transform.Find("SER").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;

                }
                else if (west)
                {
                transform.Find("SWL").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;
                transform.Find("NWR").GetChild(0).GetComponent<CoverPointScript>().coverOverride = true; ;

                }
            }
        

        #endregion


    }
}
