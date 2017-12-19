using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    public CameraRaycat _cameraRaycat;

	// Use this for initialization
	void Start ()
    {
		if(_cameraRaycat == null)
        {
            _cameraRaycat = GetComponent<CameraRaycat>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
        if(_cameraRaycat)
        {
            Debug.Log(_cameraRaycat.LayerHit);
        }
	}
}
