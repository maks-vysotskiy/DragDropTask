using UnityEngine;

[CreateAssetMenu(fileName = "FruitConfig", menuName = "Config/FruitConfig")]
public class FruitConfig : ScriptableObject
{
    [SerializeField] private FruitPair _fruitPair;

    public FruitPair FruitPair => _fruitPair;
}
