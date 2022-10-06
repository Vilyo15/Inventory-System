using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //state machine references
    private BaseState _currentState;
    private StateFactory _factory;
    
    //character reference
    private Rigidbody2D _character;
    private PlayerControls _playerInput;
    private Animator _animator;

    //variables to store animator bool hashes
    private int _isWalkingHash;
    private int _isRunningHash;
    private int _directionHash;
    private int _directionValue = 0;


    //constants
    private int CONSTANT_ZERO = 0;
    private float CONSTANT_RUN_MULTIPLIER = 4.0f;


    //variables to store player input values
    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _appliedMovement;
    private bool _isMovementPressed;
    private bool _isRunPressed;

    public BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Animator Animator { get { return _animator; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public bool IsRunPressed { get { return _isRunPressed; } }
    public int IsWalkingHash { get { return _isWalkingHash; } }
    public int IsRunningHash { get { return _isRunningHash; } }
    public int DirectionHash { get { return _directionHash; } }
    public int DirectionValue { get { return _directionValue; } }
    public float CurrentMovementX { get { return _currentMovement.x; } set { _currentMovement.x = value; } }
    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    public float RunMultiplier { get { return CONSTANT_RUN_MULTIPLIER; } }

    private void Awake()
    {
        //set references
        _character = GetComponent<Rigidbody2D>();
        _playerInput = new PlayerControls();
        _animator = GetComponentInChildren<Animator>();
        _character.constraints = RigidbodyConstraints2D.FreezeRotation;

        //set factory and state
        _factory = new StateFactory(this);
        _currentState = _factory.Idle();
        _currentState.EnterState();

        //set the parameter hash references
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _directionHash = Animator.StringToHash("direction");

        //set player input callbacks
        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControls.Move.performed += OnMovementInput;
        _playerInput.CharacterControls.Run.started += OnRun;
        _playerInput.CharacterControls.Run.canceled += OnRun;
    }
 


    void Update()
    {
        _currentState.UpdateState();
        CheckDirection(); 
    }

    private void FixedUpdate()
    {
        if (_isMovementPressed)
        {
            _character.velocity = _appliedMovement * 3;
        }
        else
        {
            _character.velocity = Vector2.zero;
        }
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        
        _currentMovementInput = context.ReadValue<Vector2>();
        
        _isMovementPressed = _currentMovementInput.x != CONSTANT_ZERO || _currentMovementInput.y != CONSTANT_ZERO;
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        
        _isRunPressed = context.ReadValueAsButton();
    }

    private void CheckDirection()
    {
        if (_appliedMovement.x == 1 && _appliedMovement.y == 0)
        {
            _directionValue = 2;
        }
        else if (_appliedMovement.x == -1 && _appliedMovement.y == 0)
        {
            _directionValue = 4;
        }
        else if(_appliedMovement.x == 0 && _appliedMovement.y == 1)
        {
            _directionValue = 1;
        }
        else if (_appliedMovement.x == 0 && _appliedMovement.y == -1)
        {
            _directionValue = 3;
        }
    }


    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
    }
}
