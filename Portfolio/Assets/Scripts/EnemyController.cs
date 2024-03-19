using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;    
    public GameObject curCover;
    public GameObject manager;

    public GameObject Walk;
    public GameObject Peek;

    public bool safe;

    private void Start()
    {       
        //curCover = manager.GetComponent<EnemyManagerScript>().coverPoints[0];
        agent.SetDestination(curCover.transform.position);
        safe = false;

        //Walk = transform.GetChild(0).gameObject;
        //Peek = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if ((curCover.GetComponent<CoverPointScript>().coverLevel <= manager.GetComponent<EnemyManagerScript>().stealth) || (curCover == null))
        {
            safe = false; 
        }
        else
        {
            safe = true;

        }


        if (agent.velocity.magnitude > 0)
        {
            Walk.SetActive(true);
            Peek.SetActive(false);

            //transform.eulerAngles = agent.velocity;
        }
        else
        {
            Walk.SetActive(false);
            Peek.SetActive(true);

            if (curCover.transform.parent.name.Substring(2) == "R")
            {
                Peek.transform.localScale = new Vector3(-1, Peek.transform.localScale.y, Peek.transform.localScale.z);
            }
            else if (curCover.transform.parent.name.Substring(2) == "L")
            {
                Peek.transform.localScale = new Vector3(1, Peek.transform.localScale.y, Peek.transform.localScale.z);
            }

            transform.rotation = curCover.transform.rotation;

        }

    }


    public void setTarget(GameObject cover)
    {
        curCover = cover;
        agent.SetDestination(curCover.transform.position);
    }
}
