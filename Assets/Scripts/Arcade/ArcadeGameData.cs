using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArcadeGameData")]
public class ArcadeGameData : ScriptableObjectSingleton<ArcadeGameData>
{
    [SerializeField]
    private List<PieceData> _pieces;
    public Dictionary<PieceType, PieceData> dictonary = new Dictionary<PieceType, PieceData>();

    private void OnEnable()
    {
        _pieces.ForEach(piece =>
        {
            dictonary.Add(piece.type, piece);
        });
    }

}
