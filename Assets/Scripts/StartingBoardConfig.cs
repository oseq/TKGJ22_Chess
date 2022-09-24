using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StartingBoardConfig
{
    [SerializeField] public List<Item> items;

    [System.Serializable]
    public class Item
    {
        [SerializeField] public Vector2Int position;
        [SerializeField] public Character character;
        [SerializeField] public Player owner;
    }
}