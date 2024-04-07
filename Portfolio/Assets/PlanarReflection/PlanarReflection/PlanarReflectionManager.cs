using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanarReflectionManager : MonoBehaviour
{

    Camera mainCamera;

    public GameObject[] reflectionPlane;
    public GameObject ball;

    [Range(0.0f,1.0f)]
    public float reflectionFactor = 0.5f;

    private void Start()
    {
        reflectionPlane = GameObject.FindGameObjectsWithTag("Mirror");
    }

    private void Update()
    {
        mainCamera = Camera.main;
        Shader.SetGlobalFloat("_reflectionFactor", reflectionFactor);
    }

    private void OnPostRender()
    {
        foreach (GameObject m in reflectionPlane)
        {
            RenderReflection(m);
        }

        
    }

    void RenderReflection(GameObject mirror)
    {
        mirror.GetComponent<MirrorScript>().reflectionCam.CopyFrom(mainCamera);

        Vector3 cameraDirectionWorldSpace = mainCamera.transform.forward;
        Vector3 cameraUpWorldSpace = mainCamera.transform.up;
        Vector3 cameraPositionWorldSpace = mainCamera.transform.position;

        //Transform the vectors to the floor's space
        Vector3 cameraDirectionPlaneSpace = mirror.transform.InverseTransformDirection(cameraDirectionWorldSpace);
        Vector3 cameraUpPlaneSpace = mirror.transform.InverseTransformDirection(cameraUpWorldSpace);
        Vector3 cameraPositionPlaneSpace = mirror.transform.InverseTransformPoint(cameraPositionWorldSpace);

        //Mirror the vectors
        cameraDirectionPlaneSpace.y *= -1.0f;
        cameraUpPlaneSpace.y *= -1.0f;
        cameraPositionPlaneSpace.y *= -1.0f;

        //Transform the vectors back to world space
        cameraDirectionWorldSpace = mirror.transform.TransformDirection(cameraDirectionPlaneSpace);
        cameraUpWorldSpace = mirror.transform.TransformDirection(cameraUpPlaneSpace);
        cameraPositionWorldSpace = mirror.transform.TransformPoint(cameraPositionPlaneSpace);

        //Set camera position and rotation
        mirror.GetComponent<MirrorScript>().reflectionCam.transform.position = cameraPositionWorldSpace;
        mirror.GetComponent<MirrorScript>().reflectionCam.transform.LookAt(cameraPositionWorldSpace + cameraDirectionWorldSpace, cameraUpWorldSpace);

        //Set render target for reflection cam
        mirror.GetComponent<MirrorScript>().reflectionCam.targetTexture = mirror.GetComponent<MirrorScript>().renderTarget;

        //Find the closest point on the mirror to the camera and calculate the distance
        Vector3 closestPoint = mirror.GetComponent<MeshCollider>().ClosestPoint(transform.position);
        float dist = (closestPoint - transform.position).magnitude;

        //Set the near clipping plane of the camera to a little less than the distance of the closest point
        mirror.GetComponent<MirrorScript>().reflectionCam.nearClipPlane = dist * 0.9f;

        if (mirror.GetComponent<MirrorScript>().reflectionCam.nearClipPlane < 0)
        {
            mirror.GetComponent<MirrorScript>().reflectionCam.nearClipPlane *= -1;
        }

        //Render the reflection camera
        mirror.GetComponent<MirrorScript>().reflectionCam.Render();

        //Draw full screen quad
        DrawQuad(mirror);
    }

    void DrawQuad(GameObject mirror)
    {
        GL.PushMatrix();

        //Use ground Material to draw the quad
        mirror.GetComponent<MirrorScript>().floorMaterial.SetPass(0);
        mirror.GetComponent<MirrorScript>().floorMaterial.SetTexture("_ReflectionTex", mirror.GetComponent<MirrorScript>().renderTarget);

        GL.LoadOrtho();

        GL.Begin(GL.QUADS);
        GL.TexCoord2(1.0f, 0.0f);
        GL.Vertex3(0.0f, 0.0f, 0.0f);
        GL.TexCoord2(1.0f, 1.0f);
        GL.Vertex3(0.0f, 1.0f, 0.0f);
        GL.TexCoord2(0.0f, 1.0f);
        GL.Vertex3(1.0f, 1.0f, 0.0f);
        GL.TexCoord2(0.0f, 0.0f);
        GL.Vertex3(1.0f, 0.0f, 1.0f);
        GL.End();

        GL.PopMatrix();
    }
}
