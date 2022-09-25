using System;

public enum PieceType
{
    Bishop,
    King,
    Knight,
    Pawn,
    Queen,
    Rook
}

public static class PieceTypeUtils
{
    public static PieceType Convert(Character character)
    {
        return character switch
        {
            Bishop => PieceType.Bishop,
            King => PieceType.King,
            Knight => PieceType.Knight,
            Pawn => PieceType.Pawn,
            Queen => PieceType.Queen,
            Rook => PieceType.Rook,
            _ => throw new ArgumentOutOfRangeException(nameof(character), character, null)
        };
    }
}