using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPointScript : MonoBehaviour
{
    public Material noCover;
    public Material LightCover;
    public Material HeavyCover;
    public int coverLevel;

    private GameObject player;
    private GameObject coverObject;



    public bool coverOverride;

    // Start is called before the first frame update
    void Start()
    {
        coverLevel = 2;
        player = GameObject.Find("Player");
        coverObject = transform.parent.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        #region Handles Raycast
        LayerMask layer = LayerMask.GetMask("Player");
        RaycastHit hit;


        if (Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * hit.distance, Color.yellow);
            if (hit.transform.gameObject == coverObject)
            {
                coverLevel = 2;
            }
            if (hit.transform.gameObject == player)
            {
                coverLevel = 0;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * 1000, Color.white);            
        }

        #endregion

        if ((coverOverride) && (coverLevel == 2))
        {
            coverLevel = 1;
        }

        #region Handles Colouring
        switch (coverLevel)
        {
            case 0:
                this.gameObject.transform.parent.gameObject.GetComponent<MeshRenderer>().material = noCover;
                break;
            case 1:
                this.gameObject.transform.parent.gameObject.GetComponent<MeshRenderer>().material = LightCover;
                break;
            case 2:
                this.gameObject.transform.parent.gameObject.GetComponent<MeshRenderer>().material = HeavyCover;
                break;
        }

        #endregion region


    }
}
