using System.Security.Cryptography;

namespace ChessLite.Model;

public class Board {
    public int[,] Pieces { get; set; } = new int[8, 8];

    public int this[int x, int y] {
        get => Pieces[x, y];
        set => Pieces[x, y] = value;
    }

    public static Board DefaultBoard() => FENLoader.StartingBoard().Board;
}