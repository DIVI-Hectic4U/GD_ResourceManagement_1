using UnityEngine;
using System.Collections.Generic;


public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    private Camera _cam;

    [Header("Zoom Settings")]
    [SerializeField] private float _zoomMin;
    [SerializeField] private float _zoomMax;
    [SerializeField] private float _zoomSpeed;
    private float _targetZoom;



    private Vector3 _dragOrigin;



    private void Start()
    {
        _targetZoom = _cam.orthographicSize; // Intializing target zoom
    }

    void Update()
    {
        // Calling the PanCamera function
        PanCamera();
        ZoomCamera();
    }

    private void PanCamera()
    {
        //save position of mouse in world space when drag starts (first time clicked)

        if (Input.GetMouseButtonDown(1))
        {
            _dragOrigin = _cam.ScreenToWorldPoint(Input.mousePosition);
        }


        //calculate distance between drag origin and new position if it is still held down

        if (Input.GetMouseButton(1))
        {
            Vector3 _diff = _dragOrigin - _cam.ScreenToWorldPoint(Input.mousePosition);

            print("Origin" + _dragOrigin + "newPosition" + _cam.ScreenToWorldPoint(Input.mousePosition) + " =difference" + _diff); // Give Console output for the updation for Distance value.

            //move the camera by that distance
            _cam.transform.position += _diff;
        }

    }


    private void ZoomCamera()
    {
        float scroll = Input.mouseScrollDelta.y; //Get scroll input

        if (scroll != 0)
        {

            _targetZoom -= scroll; //Adjust zoom level
            _targetZoom = Mathf.Clamp(_targetZoom, _zoomMin, _zoomMax); //Keep Zoom withing limits

        }

        //Smoothly interpolate between current zoom and target zoom
        _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, _targetZoom, Time.deltaTime * _zoomSpeed);

    }
    

    


}
