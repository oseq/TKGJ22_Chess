using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CrossSceneDataTransfer
{
    public static PieceType OffensivePlayer = PieceType.Knight;
    public static PieceColor OffensivePlayerColor;
    public static PieceType DeffensivePlayer = PieceType.Rook;
    public static PieceColor DeffensivePlayerColor;
    public static bool OffsensivePlayerWon;
}

public enum PieceColor
{
    White,
    Black
}
