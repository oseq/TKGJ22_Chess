using System;
using UnityEngine;

public enum AttackType
{
}

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Board board;

    public Tuple<Vector2Int, AttackType>[] AvailableMoves(Character character)
    {
        throw new NotImplementedException();
    }
}