using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycat))]
public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D _walkCursor = null;
    [SerializeField] Texture2D _attackCursor = null;
    [SerializeField] Texture2D _stopCursor = null;

    [SerializeField] Vector2 hotspot = new Vector2(0, 0);

    public CameraRaycat _cameraRaycat;

	// Use this for initialization
	void Start ()
    {
		if(_cameraRaycat == null)
        {
            _cameraRaycat = GetComponent<CameraRaycat>();
        }

        _cameraRaycat.OnLayerChanged += OnLayerChanged;
	}

    private void OnLayerChanged(Layer newLayer)
    {
        Debug.Log("LayerChanged");
        switch (newLayer)
        {
            case Layer.Enemy:
                Cursor.SetCursor(_attackCursor, hotspot, CursorMode.Auto);
                break;
            case Layer.Walkable:
                Cursor.SetCursor(_walkCursor, hotspot, CursorMode.Auto);
                break;
            case Layer.EndToPoint:
                Cursor.SetCursor(_stopCursor, hotspot, CursorMode.Auto);
                break;
            default:
                Debug.LogError("Cursor don't know this layer");
                break;
        }
    }

    //TODO: consider de-registering OnLayerChanged on leaving all game scenes
}
