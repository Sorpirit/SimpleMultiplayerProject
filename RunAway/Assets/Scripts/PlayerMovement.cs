using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,IPunObservable
{
    [SerializeField] private float speed;

    private PhotonView _photonView;
    private SpriteRenderer _renderer;
    private bool _isMine;
    private bool _isCharging;
    
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _renderer = GetComponent<SpriteRenderer>();
        _isMine = _photonView.IsMine;
    }

    private void Update()
    {
        if (_isMine)
        {
            Move();
        }

        if (_isCharging)
        {
            _renderer.color = Color.red;
        }
        else
        {
            _renderer.color = Color.white;
        }
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_isCharging);
        }
        else
        {
            _isCharging = (bool) stream.ReceiveNext();
        }
    }

    private void Move()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();
        if (inputDir != Vector2.zero)
        {
            transform.position += (Vector3) inputDir * (speed * Time.deltaTime);
        }

        _isCharging = Input.GetKey(KeyCode.Space);
    }

    
}
