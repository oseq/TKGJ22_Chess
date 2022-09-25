using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArcadeGameData")]
public class ArcadeGameData : ScriptableObjectSingleton<ArcadeGameData>
{
    [SerializeField]
    private List<PieceData> _pieces;

    private Dictionary<PieceType, PieceData> dictonary = new Dictionary<PieceType, PieceData>();

    public Dictionary<PieceType, PieceData> Dictonary
    {
        get
        {
            if (dictonary == null || dictonary.Count == 0)
                Initialize();
            return dictonary;
        }
    }

    private void Initialize()
    {
        _pieces.ForEach(piece =>
        {
            dictonary.Add(piece.type, piece);
        });
    }
}
