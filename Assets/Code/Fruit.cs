using UnityEngine;


public class Fruit : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _item_success;
    [SerializeField] private AudioClip _item_appear;

    private bool IsSlot;
    private bool _isDragging;
    private bool _isPlaced;

    private int _delay;

    private Vector2 _offset;
    private Vector2 _startPosition;

    private Fruit _slotFruit;
    private Spawner _spawner;
    private MoveAnimation _moveAnimation;


    public void TakeFruitSlot(Fruit slotFruit)
    {
        _slotFruit = slotFruit;
    }

    public void TakeSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }

    public void TakeDelay(int delay)
    {
        _delay = delay;
    }

    public void TakeInfoIsSlot(bool isSlot)
    {
        IsSlot = isSlot;

        if (isSlot)
        {
            _moveAnimation = new MoveAnimation(transform, MoveFrom.FromAbove, _delay, _audioSource, _item_appear);
        }
        else
        {
            _moveAnimation = new MoveAnimation(transform, MoveFrom.FromRight, _delay, _audioSource, _item_appear);
        }
    }

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_moveAnimation != null)
        {
            _moveAnimation.Movement();
        }

        if (_isPlaced) return;
        if (!_isDragging) return;

        var mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition - _offset;
    }

    private void OnMouseDown()
    {
        if (IsSlot) return;
        _isDragging = true;

        _offset = GetMousePosition() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        if (IsSlot) return;

        if (Vector2.Distance(transform.position, _slotFruit.transform.position) < 1)
        {
            Debug.Log("!");
            transform.position = _slotFruit.transform.position;
            _audioSource.PlayOneShot(_item_success);
            _isPlaced = true;
            _spawner.SpawnFruit();
        }
        else
        {
            _isDragging = false;
            transform.position = _startPosition;
        }
    }

    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

