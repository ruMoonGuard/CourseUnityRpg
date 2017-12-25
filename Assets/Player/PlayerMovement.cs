using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    private ThirdPersonCharacter _thirdPersonCharacter;
    private Vector3 _currentDestination, _clickToPoint;
    private CameraRaycat _cameraRaycat;

    [SerializeField] private float _walkMoveStopRadius = 0.2f;
    [SerializeField] private float _attackRadius = 3f;

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
            _currentDestination = transform.position;
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
            _clickToPoint = _cameraRaycat.RaycastHit.point;
            switch (_cameraRaycat.CurrentLayerHit)
            {
                case Layer.Walkable:
                    _currentDestination = ShortDestination(_clickToPoint, _walkMoveStopRadius);
                    break;
                case Layer.Enemy:
                    _currentDestination = ShortDestination(_clickToPoint, _attackRadius);
                    break;
            }
        }

        WalkToDestination();
    }

    private void WalkToDestination()
    {
        var playerToClickPoint = _currentDestination - transform.position;

        if (playerToClickPoint.magnitude >= 0)
        {
            _thirdPersonCharacter.Move(playerToClickPoint, false, false);
        }
        else
        {
            _thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }

    private Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, _clickToPoint);
        Gizmos.DrawSphere(_clickToPoint, 0.2f);
        Gizmos.DrawSphere(_currentDestination, 0.1f);

        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}