using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycat : MonoBehaviour {

    public Layers[] priorityLayers = new Layers[] { Layers.Enemy, Layers.Walkable };

    private RaycastHit _raycastHit;
    public RaycastHit RaycastHit { get { return _raycastHit; } }

    private Layers _layerHit;
    public Layers LayerHit { get { return _layerHit; } }

    [SerializeField] float distanceToBackground = 100f;

    Camera mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (var layer in priorityLayers)
        {
            var hit = LayerRaycast(layer);

            if(hit.HasValue)
            {
                _raycastHit = hit.Value;
                _layerHit = layer;
                return;
            }
        }

        _raycastHit.distance = distanceToBackground;
        _layerHit = Layers.EndToPoint;
	}

    RaycastHit? LayerRaycast(Layers layer)
    {
        int layerMask = 1 << (int)layer;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);

        if (hasHit) return hit;

        return null;
    }
}
