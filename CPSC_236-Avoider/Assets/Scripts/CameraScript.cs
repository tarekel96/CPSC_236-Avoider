using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform followTransform;
    private Vector3 smoothPosition;
    private float smoothSpeed = 0.5f;
    public GameObject cameraLeftBorder;
    public GameObject cameraRightBorder;
    public GameObject cameraTopBorder;
    public GameObject cameraBottomBorder;

    private float cameraHalfWidth;


    // Start is called before the first frame update
    void Start()
    {
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void FixedUpdate()
    {
        float borderLeft = cameraLeftBorder.transform.position.x + cameraHalfWidth;
        float borderRight = cameraRightBorder.transform.position.x - cameraHalfWidth;

        float borderTop = cameraTopBorder.transform.position.y - cameraHalfWidth;
        float borderBottom = cameraBottomBorder.transform.position.y + cameraHalfWidth;

        //smoothPosition = Vector3.Lerp(this.transform.position,
        //    new Vector3(Mathf.Clamp(followTransform.position.x, borderLeft, borderRight),
        //    this.transform.position.y,
        //    this.transform.position.z), smoothSpeed);
        smoothPosition = Vector3.Lerp(this.transform.position,
            new Vector3(Mathf.Clamp(followTransform.position.x, borderLeft, borderRight),
            //this.transform.position.y,
            Mathf.Clamp(followTransform.position.y, borderTop, borderBottom),
            this.transform.position.z), smoothSpeed);

        this.transform.position = smoothPosition;
    }
}
