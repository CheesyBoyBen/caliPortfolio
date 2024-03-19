using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour
{

    public GameObject[] enemies;
    public GameObject[] coverPoints;
    public List<GameObject> validCover;

    public int stealth;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        coverPoints = GameObject.FindGameObjectsWithTag("Cover");
    }

    // Update is called once per frame
    void Update()
    {
        findValidCover();

        foreach (GameObject e in enemies)
        {
            if (e.GetComponent<EnemyController>().safe == false)
            {
                GameObject targetCover = validCover[Random.Range(0, validCover.Count)];
                e.GetComponent<EnemyController>().setTarget(targetCover);
                validCover.Remove(targetCover);

            }
        }
    }

    private void findValidCover()
    {
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
}
