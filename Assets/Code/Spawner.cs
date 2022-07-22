using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPointsOfSlots;
    [SerializeField] private List<FruitConfig> _fruitPairs;
    [SerializeField] private Transform _spawnPointofFruit;
    [SerializeField] private int _countOfSpawnPointsOfSlots = 3;
    [SerializeField] private Fruit _fruitPrefab;

    private int _delay = 10;

    private List<Fruit> _fruitList;
    private MoveAnimation _moveAnimation;

    private void Start()
    {
        _fruitList = new List<Fruit>();
        SpawnFruitSlots();
        SpawnFruit();
    }

    private void SpawnFruitSlots()
    {
        var randomSet = _fruitPairs.OrderBy(s => Random.value).Take(_countOfSpawnPointsOfSlots).ToList();

        for (int i = 0; i < _spawnPointsOfSlots.Count; i++)
        {
            var slot = Instantiate(_fruitPrefab, _spawnPointsOfSlots[i].position, Quaternion.identity);
            var spriteSlotRenderer = slot.GetComponent<SpriteRenderer>();
            spriteSlotRenderer.sprite = randomSet[i].FruitPair.FruitSlot;
            spriteSlotRenderer.sortingOrder = 0;
            slot.TakeDelay(_delay * (i + 1));
            slot.TakeInfoIsSlot(true);


            var fruit = Instantiate(_fruitPrefab, _spawnPointofFruit.position, Quaternion.identity);
            var spriteFruitRenderer = fruit.GetComponent<SpriteRenderer>();
            spriteFruitRenderer.sprite = randomSet[i].FruitPair.Fruit;
            spriteFruitRenderer.sortingOrder = 1;

            fruit.TakeDelay(_delay);
            fruit.TakeInfoIsSlot(false);
            fruit.TakeFruitSlot(slot);
            fruit.TakeSpawner(this);

            _fruitList.Add(fruit);
            fruit.gameObject.SetActive(false);
        }
    }


    public void SpawnFruit()
    {
        if (_fruitList.Count > 0)
        {
            _fruitList[_fruitList.Count - 1].gameObject.SetActive(true);
            _fruitList.RemoveAt(_fruitList.Count - 1);
        }
        else
        {
            Debug.Log("Congratulations!");
        }
    }
}

