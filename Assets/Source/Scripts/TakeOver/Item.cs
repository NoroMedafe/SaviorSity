using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Item : MonoBehaviour
{
    [SerializeField] private AnimationCurve _scaleAnimation;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed = 1;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private float _stepForScale = 0.01f;

    private Coroutine _coroutine;


    public void ScatteringItems()
    {
       
        transform.SetParent(null);
        gameObject.SetActive(true);
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(_direction, ForceMode.Impulse);
    }

    public void ItemSelection(GameObject point)
    {

            if (_coroutine == null)
            {
                _collider.enabled = false;
                _rigidbody.isKinematic = true;
                _coroutine = StartCoroutine(MovingItem(point));
            }
            else
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
                _collider.enabled = false;
                _rigidbody.isKinematic = true;
                _coroutine = StartCoroutine(MovingItem(point));
            }
    }

    private IEnumerator MovingItem(GameObject point)
    {
        float expiredSeconds = 0;
        float progress = 0;

        while (transform.position != point.transform.position)
        {
            expiredSeconds += Time.deltaTime;
            progress = expiredSeconds / _speed;

            transform.position = Vector3.MoveTowards(transform.position, point.transform.position, _speed *Time.deltaTime);
            transform.localScale = Vector3.one * _scaleAnimation.Evaluate(progress);

            yield return null;

        }

        _coroutine = null;
        gameObject.SetActive(false);
    }
}
