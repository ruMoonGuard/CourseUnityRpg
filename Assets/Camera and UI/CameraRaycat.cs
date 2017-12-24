using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycat : MonoBehaviour {

    public Layer[] priorityLayers = new Layer[] { Layer.Enemy, Layer.Walkable };

    private RaycastHit _raycastHit;
    public RaycastHit RaycastHit { get { return _raycastHit; } }

    private Layer _layerHit;
    public Layer CurrentLayerHit
    {
        get { return _layerHit; }
        private set
        {
            if(_layerHit != value)
            {
                _layerHit = value;
                OnLayerChanged(_layerHit);
            }
        }
    }

    public delegate void onLayerChanged(Layer newLayer);
    public event onLayerChanged OnLayerChanged;

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
                CurrentLayerHit = layer;
                _raycastHit = hit.Value;
                return;
            }
        }

        _raycastHit.distance = distanceToBackground;
        CurrentLayerHit = Layer.EndToPoint;
	}

    RaycastHit? LayerRaycast(Layer layer)
    {
        int layerMask = 1 << (int)layer;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);

        if (hasHit) return hit;

        return null;
    }
}
