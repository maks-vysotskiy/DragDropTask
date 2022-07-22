using UnityEngine;

public class MoveAnimation
{
    private Vector3 _targetPosition;
    private Transform _objectPosition;
    private MoveFrom _moveFrom;

    private int _delay;
    private float _time = 0;

    private bool _OnPlaceAfterAnimation;
    private bool _WasSound;

    private AudioSource _audioSource;
    private AudioClip _item_appear;

    

    public MoveAnimation(Transform objectPosition, MoveFrom moveFrom, int delay, AudioSource audioSource, AudioClip audioClip)
    {
        _objectPosition = objectPosition;
        _targetPosition = _objectPosition.position;

        _moveFrom = moveFrom;
        _delay = delay;
        _audioSource = audioSource;
        _item_appear = audioClip;

        _OnPlaceAfterAnimation = false;
        _WasSound = false;

        GetParametrs();
    }

    private void GetParametrs()
    {
        if (_moveFrom == MoveFrom.FromAbove)
        {
            _objectPosition.position += Vector3.up * _delay;
        }
        else if (_moveFrom == MoveFrom.FromRight)
        {
            _objectPosition.position += Vector3.right * _delay;
        }
    }


    public void Movement()
    {
        if (_OnPlaceAfterAnimation) return;

        _objectPosition.position = Vector3.Lerp(_objectPosition.position, _targetPosition, _time / _delay);
        _time += Time.deltaTime;

        var distance = Vector3.Distance(_objectPosition.position, _targetPosition);
             
        if(distance < 1 && !_WasSound)
        {
            _audioSource.PlayOneShot(_item_appear);
            _WasSound = true;
        }

        if (distance < 0.1)
        {
           
            _OnPlaceAfterAnimation = true;
        }

    }


}