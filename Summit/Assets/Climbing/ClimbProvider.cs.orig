using System;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.InputSystem;
>>>>>>> f0dfaee2e1eaf54c17495eedb30fdb47ba31cd11

public class ClimbProvider : MonoBehaviour
{
    public static event Action ClimbActive;
    public static event Action ClimbInActive;

    public Transform characterTransform;
    public Transform leftControllerTransform;
    public Transform rightControllerTransform;

<<<<<<< HEAD
    public float climbSpeed = 0.5f; // Adjust this value to control the climbing speed
    public float climbLimit = 5f; // Adjust this value to set the climb limit
=======
    public CharacterController characterController;

    //public Vector3 velocityRight = new Vector3(1.0f, 0.0f, 0.0f);
    //public Vector3 velocityLeft = new Vector3(0.0f, 0.0f, 0.0f);
    public InputActionProperty velocityRight;
    public InputActionProperty velocityLeft;
>>>>>>> f0dfaee2e1eaf54c17495eedb30fdb47ba31cd11

    private bool _rightActive = false;
    private bool _leftActive = false;

    private Vector3 _initialLeftControllerPosition;
    private Vector3 _initialRightControllerPosition;

    private void Start()
    {
        XRDirectClimbInteractor.ClimbHandActivated += HandActivated;
        XRDirectClimbInteractor.ClimbHandDeactivated += HandDeactivated;
    }

    private void OnDestroy()
    {
        XRDirectClimbInteractor.ClimbHandActivated -= HandActivated;
        XRDirectClimbInteractor.ClimbHandDeactivated -= HandDeactivated;
    }

    private void HandActivated(string controllerName)
    {
        if (controllerName == "LeftHand")
        {
            _leftActive = true;
            _rightActive = false;
            _initialLeftControllerPosition = leftControllerTransform.position;
            Debug.Log("Left Hand Activated");
        }
        else if (controllerName == "RightHand")
        {
            _leftActive = false;
            _rightActive = true;
            _initialRightControllerPosition = rightControllerTransform.position;
            Debug.Log("Right Hand Activated");
        }

        ClimbActive?.Invoke();
    }

    private void HandDeactivated(string controllerName)
    {
        if (controllerName == "LeftHand" && _leftActive)
        {
            _leftActive = false;
            ClimbInActive?.Invoke();
            Debug.Log("Left Hand Deactivated");
        }
        else if (controllerName == "RightHand" && _rightActive)
        {
            _rightActive = false;
            ClimbInActive?.Invoke();
            Debug.Log("Right Hand Deactivated");
        }
    }

    private void Update()
    {
<<<<<<< HEAD
        if (_leftActive)
        {
            Vector3 currentLeftControllerPosition = leftControllerTransform.position;
            Vector3 climbVector = (_initialLeftControllerPosition - currentLeftControllerPosition) * climbSpeed;
            Vector3 newPosition = characterTransform.position + climbVector;

            // Apply constraints
            newPosition.x = Mathf.Clamp(newPosition.x, characterTransform.position.x - climbLimit, characterTransform.position.x + climbLimit);
            newPosition.y = Mathf.Clamp(newPosition.y, characterTransform.position.y - climbLimit, characterTransform.position.y + climbLimit);
            newPosition.z = characterTransform.position.z;

            characterTransform.position = newPosition;

            // Update the initial controller position to keep it in place
            _initialLeftControllerPosition = currentLeftControllerPosition;
            leftControllerTransform.position = _initialLeftControllerPosition;
        }
        else if (_rightActive)
        {
            Vector3 currentRightControllerPosition = rightControllerTransform.position;
            Vector3 climbVector = (_initialRightControllerPosition - currentRightControllerPosition) * climbSpeed;
            Vector3 newPosition = characterTransform.position + climbVector;

            // Apply constraints
            newPosition.x = Mathf.Clamp(newPosition.x, characterTransform.position.x - climbLimit, characterTransform.position.x + climbLimit);
            newPosition.y = Mathf.Clamp(newPosition.y, characterTransform.position.y - climbLimit, characterTransform.position.y + climbLimit);
            newPosition.z = characterTransform.position.z;

            characterTransform.position = newPosition;

            // Update the initial controller position to keep it in place
            _initialRightControllerPosition = currentRightControllerPosition;
            rightControllerTransform.position = _initialRightControllerPosition;
        }
    }
}
=======
        if (_leftActive || _rightActive)
        {
            Climb();
        }
    }

    private void Climb()
    {
        //Vector3 velocity;
        //if (_leftActive)
        //{
        //    velocity = velocityLeft;
        //    Debug.Log("Here is the velocity of left: " + velocity);
        //}
        //else if (_rightActive)
        //{
        //    velocity = velocityRight;
        //    Debug.Log("Here is the velocity of right: " + velocity);
        //}
        //else
        //{
        //    velocity = Vector3.zero;
        //}


        Vector3 velocity = _leftActive ? velocityLeft.action.ReadValue<Vector3>() : velocityRight.action.ReadValue<Vector3>();
        if (_leftActive)
        {
            Debug.Log("Here is the velocity of left: " + velocity);
        }
        else if (_rightActive)
        {
            Debug.Log("Here is the velocity of right: " + velocity);
        }
        Debug.Log("Here is the velocity of left: " + velocityLeft.action.ReadValue<Vector3>());
        characterController.Move(characterController.transform.rotation * -velocity * Time.fixedDeltaTime);
    }

    //Vector3 velocity = Vector3.up * climbSpeed; // Set a fixed upward velocity
    //characterController.Move(velocity * Time.fixedDeltaTime);
    //Debug.Log("Here is the velocity: " + climbSpeed);

}
>>>>>>> f0dfaee2e1eaf54c17495eedb30fdb47ba31cd11
