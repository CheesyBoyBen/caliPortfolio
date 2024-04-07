using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPointScript : MonoBehaviour
{
    public Material noCover;
    public Material LightCover;
    public Material HeavyCover;
    public int coverLevel;

    private GameObject mirror;
    public List<GameObject> targets;

    public GameObject coverObject;



    public bool coverOverride;

    // Start is called before the first frame update
    void Start()
    {
        coverLevel = 1;
        mirror = GameObject.Find("Mirror");
        coverObject = transform.parent.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        targets.Clear();

        targets.AddRange(GameObject.FindGameObjectsWithTag("Player"));


        #region Handles Raycast
        RaycastHit hit;

        foreach (GameObject t in targets)
        {
            if (Physics.Raycast(transform.position, (t.transform.position - transform.position).normalized, out hit, Mathf.Infinity))
            {

                if (hit.transform.gameObject == coverObject)
                {
                    coverLevel = 2;
                }
                if (hit.transform.gameObject.tag == "Player")
                {
                    Debug.DrawRay(transform.position, (t.transform.position - transform.position).normalized * hit.distance, Color.yellow);

                    coverLevel = 0;
                    break;
                }
            }
        }

        if (mirror != null)
        {
            for (int i = 0; i < mirror.transform.childCount; i++)
            {
                if (Physics.Raycast(transform.position, (mirror.transform.GetChild(i).position - transform.position).normalized, out hit, Mathf.Infinity))
                {

                    if (hit.transform.gameObject.tag == "Mirror")
                    {
                        Debug.DrawRay(transform.position, (mirror.transform.GetChild(i).transform.position - transform.position).normalized * hit.distance, Color.yellow);

                        coverLevel = 0;
                        break;
                    }
                }
            }
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
