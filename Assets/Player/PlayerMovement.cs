using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    private ThirdPersonCharacter _thirdPersonCharacter;
    private Vector3 _currentClickTarget;
    private CameraRaycat _cameraRaycat;

    [SerializeField] private float _walkMoveStopRadius = 0.1f;

    private bool isInDirectMode = false;

    void Start ()
    {
        _thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        _cameraRaycat = Camera.main.GetComponent<CameraRaycat>();
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.G)) //TODO: add to menu
        {
            isInDirectMode = !isInDirectMode;
            _currentClickTarget = transform.position;
        }

        if(isInDirectMode)
        {
            ProccessDirectMovement();
        }
        else
        {
            ProccessMouseMovement();
        }
    }

    private void ProccessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        _thirdPersonCharacter.Move(movement, false, false);
    }

    private void ProccessMouseMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (_cameraRaycat.CurrentLayerHit)
            {
                case Layer.Walkable:
                    _currentClickTarget = _cameraRaycat.RaycastHit.point;
                    break;
                case Layer.Enemy:
                    print("not move to enemy");
                    break;
            }
        }

        var playerToClickPoint = _currentClickTarget - transform.position;

        if (playerToClickPoint.magnitude >= _walkMoveStopRadius)
        {
            _thirdPersonCharacter.Move(playerToClickPoint, false, false);
        }
        else
        {
            _thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }
}