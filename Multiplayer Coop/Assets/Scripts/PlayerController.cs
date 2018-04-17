﻿using System;
using System.Collections;
using System.Linq;
using Barebones.MasterServer;
using Barebones.Utils;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : NetworkBehaviour
{
    public Transform LookAt;
    public Transform camTransform;

    public Camera cam;

    protected float distance = 10.0f;
    protected float currentX = 0.0f;
    protected float currentY = 0.0f;
    protected float sensitivityX = 0.0f;
    protected float sensitivityY = 0.0f;

    protected CharacterController _characterController;

    protected float _fallVelocity;
    protected readonly float _fallVelocityMax = 20f;
    protected readonly float _forwardMaxSpeed = 5f;

    protected float _forwardSpeed;

    protected readonly float _yDeathValue = -20f;
    protected readonly float _rotationMaxVelocity = 270;

    //protected Vector3 _rotationVelocity;
    protected float _rotationVelocity;

    public Color CurrentPlayerColor;
    public SpriteRenderer Direction;

    [SyncVar]
    public string Name;

    [SyncVar(hook= "OnFlagChange")]
    public string FlagColor = "";

    protected Text _nameObject;
    public Text NamePrefab;

    public Transform NameTransform;
    public GameObject Shape;

    public GameObject Flag;

    // Use this for initialization
    protected void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _characterController.detectCollisions = false;

        StartCoroutine(DisplayName());
    }

    public void Setup(string username)
    {
        Name = username;
    }

    public override void OnStartClient()
    {
        SetFlagColor(FlagColor);
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        // Change colors
        var color = CurrentPlayerColor;
        Direction.color = new Color(color.r, color.g, color.b, 0.5f);
        Shape.GetComponent<MeshRenderer>().material.color = color;

        // Notify UI
        if (GameUi.Instance != null)
        {
            GameUi.Instance.OnPlayerSpawned(this);
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        // Ignore input from other players
        if (!isLocalPlayer)
            return;

        UpdateMovement();

        // Input
        if (GameUi.Instance != null && GameUi.Instance.IsAutoWalk)
        {
            _forwardSpeed = 0.7f * _forwardMaxSpeed;
            _rotationVelocity = 0.3f * _rotationMaxVelocity;
        }
        else
        {
            _forwardSpeed = Input.GetAxis("Vertical") * _forwardMaxSpeed;
            _rotationVelocity = Input.GetAxis("Horizontal") * _rotationMaxVelocity;
        }
    }

    protected void UpdateMovement()
    {
        var moveDirection = transform.forward*_forwardSpeed*Time.deltaTime;

        // Reset fall velocity if grounded
        if (_characterController.isGrounded)
            _fallVelocity = 0;

        // Gravity application
        _fallVelocity += _fallVelocityMax*Time.deltaTime;
        _fallVelocity = Mathf.Min(_fallVelocity, _fallVelocityMax);
        moveDirection.y -= _fallVelocity*Time.deltaTime;

        // Movement update
        _characterController.Move(moveDirection);
        transform.Rotate(Vector3.up*_rotationVelocity*Time.deltaTime);

        // Death and "respawn"
        if (transform.position.y < _yDeathValue)
            MoveToRandomSpawnPoint();
    }

    public void MoveToRandomSpawnPoint()
    {
        transform.position = new Vector3(Random.Range(0f, 4f), 10, Random.Range(0f, 4f));
    }

    public void OnFlagChange(string color)
    {
        FlagColor = color;

        SetFlagColor(FlagColor);
    }

    public void SetFlagColor(string color)
    {
        if (string.IsNullOrEmpty(color))
        {
            // Hide the flag
            Flag.transform.parent.gameObject.SetActive(false);
            return;
        }

        // Display the flag
        Flag.transform.parent.gameObject.SetActive(true);

        // Inefficient, but works for the demo
        Flag.GetComponent<MeshRenderer>().material.color = BmHelper.HexToColor(color);
    }

    protected void OnTriggerEnter(Collider collider)
    {
        if (!isServer)
            return;
    }

    protected void OnDestroy()
    {
        // Cleanup the name object
        if (_nameObject != null)
            Destroy(_nameObject);
    }

    public IEnumerator DisplayName()
    {
        // Create a player name
        _nameObject = Instantiate(NamePrefab).GetComponent<Text>();
        _nameObject.text = Name ?? ".";
        _nameObject.transform.SetParent(FindObjectOfType<Canvas>().transform);

        while (true)
        {
            if ((_nameObject.text != Name) && (Name != null))
                _nameObject.text = Name;

            // While we're still "online"
            _nameObject.transform.position = RectTransformUtility
                                                .WorldToScreenPoint(Camera.main, NameTransform.position) + Vector2.up*30;

            yield return 0;
        }
    }
}