using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThrewPlatform : MonoBehaviour
{
    private Collider2D _collider;
    private bool _playerOnPlatform;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_playerOnPlatform && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }

    private void SetPlayerOnPlatform(bool value)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var cocoTransform = player.transform.Find("Coco");
            var coco = cocoTransform.gameObject;
            if (coco != null)
            {
                _playerOnPlatform = value;

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerOnPlatform(true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        SetPlayerOnPlatform(false);
    }
}
