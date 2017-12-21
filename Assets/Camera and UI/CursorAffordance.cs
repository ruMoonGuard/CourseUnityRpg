using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D _walkCursor = null;
    [SerializeField] Texture2D _attackCursor = null;
    [SerializeField] Texture2D _stopCursor = null;

    [SerializeField] Vector2 hotspot = new Vector2(96, 96);

    public CameraRaycat _cameraRaycat;

	// Use this for initialization
	void Start ()
    {
		if(_cameraRaycat == null)
        {
            _cameraRaycat = GetComponent<CameraRaycat>();
        }
	}

    private void Update()
    {
        switch (_cameraRaycat.LayerHit)
        {
            case Layers.Enemy:
                Cursor.SetCursor(_attackCursor, hotspot, CursorMode.Auto);
                break;
            case Layers.Walkable:
                Cursor.SetCursor(_walkCursor, hotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(_stopCursor, hotspot, CursorMode.Auto);
                break;
        }
    }
}
