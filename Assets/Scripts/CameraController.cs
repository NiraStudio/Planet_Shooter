using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {

    public static CameraController Instance;
    public float OthSize;
    public Transform Character;
    public Vector3 offSet;
    public float DistanceSize,rotationSmoothness,followSmoothness;
    CinemachineVirtualCamera cam;
    void Awake()
    {
        Instance = this;
    }
    IEnumerator Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        while (Mathf.Round(cam.m_Lens.OrthographicSize) != OthSize)
        {
            cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, OthSize, Time.deltaTime);
            yield return null;
        }

    }
    void FixedUpdate()
    {

        /*if (Vector2.Distance(Character.position+offSet, transform.position) > DistanceSize)
            transform.position = Vector3.Lerp(transform.position, Character.transform.position + offSet, Time.deltaTime*followSmoothness);*/

       // transform.position = Character.position+offSet;

        if(cam.transform.localEulerAngles!= Character.localEulerAngles)
        {

            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Character.rotation, Time.deltaTime*rotationSmoothness);
        }

       
       



    }


    /*
    void Move()
    {
        transform.position = Vector3.SmoothDamp(transform.position, GetCenterOfTargets() + offSet, ref velocity, 0.5f);
    }
    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatSize() / zoomLimiter);
        cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, newZoom, Time.deltaTime);

    }


    public void ChangeTargets(List<GameObject> characters)
    {
        Targets = characters;
    }


    public void AddTarget(GameObject obj)
    {
        Targets.Add(obj);
    }

    Vector3 GetCenterOfTargets()
    {
        if (Targets.Count == 1)
            return Targets[0].transform.position;
        var bound = new Bounds();
        if (Targets[0] != null)
            bound = new Bounds(Targets[0].transform.position, Vector3.zero);

        foreach (var item in Targets)
        {
            if (item != null)
                bound.Encapsulate(item.transform.position);
        }
        return bound.center;

    }

    float GetGreatSize()
    {
        boundtemp = new Bounds(Targets[0].transform.position, Vector3.zero);

        foreach (var item in Targets)
        {
            if (item != null)
                boundtemp.Encapsulate(item.transform.position);
        }
        return boundtemp.size.x;

    }

    public void ZoomToChatacter()
    {
        Character = true;
    }
    */
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position, boundtemp.size);
    }


}
