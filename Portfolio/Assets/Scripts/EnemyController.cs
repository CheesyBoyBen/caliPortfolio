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
    public CapsuleCollider peekCollider;

    public bool safe;
    public bool seen;

    private void Start()
    {       

        safe = false;

        curCover = null;
    }

    void Update()
    {
        if (curCover == null)
        {
            safe = false;
        }
        else if (curCover.GetComponent<CoverPointScript>().coverLevel <= manager.GetComponent<EnemyManagerScript>().stealth)
        {
            safe = false; 
        }
        else if (seen)
        {
            safe = false;
            seen = false;
        }
        else
        {
            safe = true;
        }


        if (agent.velocity.magnitude > 0)
        {
            Walk.SetActive(true);
            Peek.SetActive(false);

            
        }
        else
        {
            Walk.SetActive(false);
            Peek.SetActive(true);

            if (curCover != null)
            {
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

    }


    public void setTarget(GameObject cover)
    {
        curCover = cover;
        agent.SetDestination(curCover.transform.position);
    }

    public void run()
    {
        seen = true;
    }
}
