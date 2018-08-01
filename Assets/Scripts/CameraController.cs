using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {
    public static CameraController Instance;

    public Vector3 offSet;
    [SerializeField]
    List<GameObject> Targets = new List<GameObject>();

    public Transform CharacterTransform;

    public float minZoom = 10f;
    public float maxZoom = 40f;
    public float zoomLimiter = 50f;

    public bool Character;
    Bounds boundtemp;
    CinemachineVirtualCamera cam;
    Vector3 velocity;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();

    }
    void Update()
    {
        if (Character)
        {

           // if()

           // transform.position = Vector3.Lerp(transform.position, CharacterTransform.position+new Vector3(0,1,-10), Time.deltaTime);



            transform.localRotation = Quaternion.Lerp(transform.localRotation, CharacterTransform.localRotation, Time.deltaTime);
            if(Mathf.Round( cam.m_Lens.OrthographicSize)!=4)
            cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, 4f, Time.deltaTime);
            return;
        }


        
    }



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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boundtemp.size);
    }
}
