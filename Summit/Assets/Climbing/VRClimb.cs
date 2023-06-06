using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class VRClimb : MonoBehaviour
{
    public static event Action ClimbActive;
    public static event Action ClimbInActive;

    public Transform characterTransform;
    public Transform leftControllerTransform;
    public Transform rightControllerTransform;

    public float climbSpeed = 0.5f; // Adjust this value to control the climbing speed
    public float climbLimit = 5f; // Adjust this value to set the climb limit
    public CharacterController characterController;

    private bool _rightActive = false;
    private bool _leftActive = false;

    private Vector3 _initialLeftControllerPosition;
    private Vector3 _initialRightControllerPosition;

    private Vector3 _previousLeftControllerPosition;
    private Vector3 _previousRightControllerPosition;

    public AudioClip winSound;
    public AudioClip grabSound; 
    private AudioSource audioSource;
    


    private void Start()
    {
        XRDirectClimbInteractor.ClimbHandActivated += HandActivated;
        XRDirectClimbInteractor.ClimbHandDeactivated += HandDeactivated;
        audioSource = gameObject.AddComponent<AudioSource>();

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
            _previousLeftControllerPosition = _initialLeftControllerPosition;
            Debug.Log("Left Hand Activated");

            audioSource.PlayOneShot(grabSound); // Play grab sound
        }
        else if (controllerName == "RightHand")
        {
            _leftActive = false;
            _rightActive = true;
            _initialRightControllerPosition = rightControllerTransform.position;
            _previousRightControllerPosition = _initialRightControllerPosition;
            Debug.Log("Right Hand Activated");

            audioSource.PlayOneShot(grabSound); // Play grab sound
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
        if (_leftActive || _rightActive)
        {
            Climb();
        }
        else
        {
            MoveWithController();
        }
    }

    private void Climb()
    {
        if (_leftActive)
        {
            Vector3 currentControllerPosition = leftControllerTransform.position;
            Vector3 initialControllerPosition = _initialLeftControllerPosition;
            Vector3 climbVector = (initialControllerPosition - currentControllerPosition) * climbSpeed;
            Vector3 newPosition = characterTransform.position + climbVector;

            // Apply constraints
            newPosition.x = Mathf.Clamp(newPosition.x, characterTransform.position.x - climbLimit, characterTransform.position.x + climbLimit);
            newPosition.y = Mathf.Clamp(newPosition.y, characterTransform.position.y - climbLimit, characterTransform.position.y + climbLimit);
            newPosition.z = Mathf.Clamp(newPosition.z, characterTransform.position.z - climbLimit, characterTransform.position.z + climbLimit);

            characterTransform.position = newPosition;

            // Check win condition
            if (characterTransform.position.y > 6.8f)
            {
                audioSource.PlayOneShot(winSound); // Play win sound
                SceneManager.LoadScene("WinScene");
            }
        }

        if (_rightActive)
        {
            Vector3 currentControllerPosition = rightControllerTransform.position;
            Vector3 initialControllerPosition = _initialRightControllerPosition;
            Vector3 climbVector = (initialControllerPosition - currentControllerPosition) * climbSpeed;
            Vector3 newPosition = characterTransform.position + climbVector;

            // Apply constraints
            newPosition.x = Mathf.Clamp(newPosition.x, characterTransform.position.x - climbLimit, characterTransform.position.x + climbLimit);
            newPosition.y = Mathf.Clamp(newPosition.y, characterTransform.position.y - climbLimit, characterTransform.position.y + climbLimit);
            newPosition.z = Mathf.Clamp(newPosition.z, characterTransform.position.z - climbLimit, characterTransform.position.z + climbLimit);

            characterTransform.position = newPosition;

            // Check win condition
            if (characterTransform.position.y > 6.8f)
            {
                SceneManager.LoadScene("WinScene");
            }
        }
    }

private void MoveWithController()
    {
        if (_leftActive)
        {
            Vector3 currentPosition = leftControllerTransform.position;
            Vector3 movement = currentPosition - _previousLeftControllerPosition;
            characterTransform.position += movement;
            _previousLeftControllerPosition = leftControllerTransform.position;
            _previousRightControllerPosition = rightControllerTransform.position;
        }
        if (_rightActive)
        {
            Vector3 currentPosition = rightControllerTransform.position;
            Vector3 movement = currentPosition - _previousRightControllerPosition;
            characterTransform.position += movement;
            _previousLeftControllerPosition = leftControllerTransform.position;
            _previousRightControllerPosition = rightControllerTransform.position;
        }
        

        
        
        //Debug.Log("Here is your previous Controller Position:" + _previousLeftControllerPosition);
    }
}

