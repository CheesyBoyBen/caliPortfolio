using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    public List<GameObject> targets;
    public List<GameObject> mirrorTarget;

    private GameObject player;

    public RenderTexture renderTarget;
    public Camera reflectionCam;
    public Material floorMaterial;


    // Start is called before the first frame update
    void Start()
    {
        GameObject reflectionCameraGO = new GameObject("ReflectionCamera");
        reflectionCam = reflectionCameraGO.AddComponent<Camera>();
        reflectionCam.enabled = false;

        renderTarget = new RenderTexture(Screen.width, Screen.height, 24);


        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        targets.Clear();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        if (targets.Count > transform.childCount)
        {
            for (int i = 0; i < (targets.Count - transform.childCount); i++)
            {
                GameObject temp = new GameObject("test");
                temp.transform.SetParent(transform);
                mirrorTarget.Add(temp);
            }
        }

        for (int i = 0; i < (mirrorTarget.Count); i++)
        {
            mirrorTarget[i].transform.position = targets[i].transform.position;
            mirrorTarget[i].transform.localPosition = new Vector3(mirrorTarget[i].transform.localPosition.x, -mirrorTarget[i].transform.localPosition.y, mirrorTarget[i].transform.localPosition.z);
        }

        /*if ((Input.GetKeyDown(KeyCode.Mouse1)) && ((player.transform.position - transform.position).magnitude < 5))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 45, transform.eulerAngles.z);
        }*/


    }

    public void turn()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 45, transform.eulerAngles.z);

    }
}
