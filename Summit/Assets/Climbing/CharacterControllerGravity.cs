using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControllerGravity : MonoBehaviour
{
    private CharacterController _characterController;
    private bool _climbing = false;
    private float _fallThreshold = 0f; // Fall threshold in the y direction
    private float _fallStartPosition = 0f; // Starting position when falling
    public AudioClip fallSound; // Assign the desired sound clip in the Inspector
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        ClimbProvider.ClimbActive += ClimbActive;
        ClimbProvider.ClimbInActive += ClimbInActive;
        audioSource = gameObject.AddComponent<AudioSource>();
        DontDestroyOnLoad(audioSource.gameObject); // Prevent the AudioSource from being destroyed when a new scene is loaded
    }

    private void OnDestroy()
    {
        ClimbProvider.ClimbActive -= ClimbActive;
        ClimbProvider.ClimbInActive -= ClimbInActive;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_characterController.isGrounded && !_climbing)
        {
            if (_fallStartPosition == 0f)
            {
                _fallStartPosition = transform.position.y;
            }

            _characterController.SimpleMove(new Vector3());
            //Debug.Log("You are falling!");

            // Check if falling below the fall threshold
            if (Mathf.Abs(transform.position.y - _fallStartPosition) > 1f)
            {
                audioSource.PlayOneShot(fallSound); // Play the fall sound
                SceneManager.LoadScene("GameOverScene");
            }
        }
        else
        {
            _fallStartPosition = 0f; // Reset fall start position when grounded
        }
    }

    private void ClimbActive()
    {
        _climbing = true;
    }

    private void ClimbInActive()
    {
        _climbing = false;
    }
}