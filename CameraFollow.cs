using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera Maincamera;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = Maincamera.transform.position - transform.position;
        Debug.Log(offset);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        Maincamera.transform.position = transform.position + offset;
    }
}
