using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour
{

    public GameObject[] enemies;
    public GameObject[] coverPoints;
    public List<GameObject> validCover;

    public GameObject end;

    public int stealth;

    public GameObject levelManager;

    bool condition = false;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        coverPoints = GameObject.FindGameObjectsWithTag("Cover");

        levelManager = GameObject.FindGameObjectWithTag("GameController");

    }

    // Update is called once per frame
    void Update()
    {
        if (!condition)
        {
            findValidCover();

            foreach (GameObject e in enemies)
            {
                if (e.GetComponent<EnemyController>().safe == false)
                {

                    if (validCover.Count > 0)
                    {
                        GameObject targetCover = validCover[Random.Range(0, validCover.Count - 1)];
                        e.GetComponent<EnemyController>().setTarget(targetCover);
                        validCover.Remove(targetCover);
                    }
                    else
                    {
                        e.GetComponent<EnemyController>().setTarget(end);
                    }

                }
            }

            endCondition();
        }        
    }

    private void findValidCover()
    {
        validCover.Clear();

        foreach (GameObject cover in coverPoints)
        {
            if ((cover.GetComponent<CoverPointScript>().coverLevel > stealth))
            {
                validCover.Add(cover);
            }

            foreach (GameObject e in enemies)
            {
                if (e.GetComponent<EnemyController>().curCover == cover)
                {
                    validCover.Remove(cover);                    
                }
            }
        }
        
    }

    private void endCondition()
    {
        int i = 0;
        foreach (GameObject e in enemies)
        {
            if (e.GetComponent<EnemyController>().curCover == end)
            {
                i++;
            }
            
        }

        if (i == enemies.Length)
        {
            condition = true;
            levelManager.GetComponent<levelManagerScript>().complete();
        }
    }
}
