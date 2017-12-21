using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerControl : MonoBehaviour {

    private ThirdPersonCharacter _character;
    private Vector3 _currentClickTarget;
    private CameraRaycat _cameraRaycat;

    [SerializeField] private float _walkMoveStopRadius = 0.1f;

    void Start ()
    {
        _character = GetComponent<ThirdPersonCharacter>();
        _cameraRaycat = Camera.main.GetComponent<CameraRaycat>();
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            switch(_cameraRaycat.LayerHit)
            {
                case Layers.Walkable:
                    _currentClickTarget = _cameraRaycat.RaycastHit.point;
                    break;
                case Layers.Enemy:
                    print("not move to enemy");
                    break;
            }
        }

        var playerToClickPoint = _currentClickTarget - transform.position;
        
        if (playerToClickPoint.magnitude >= _walkMoveStopRadius)
        {
            _character.Move(playerToClickPoint, false, false);
        }
        else
        {
            _character.Move(Vector3.zero, false, false);
        }
    }
}