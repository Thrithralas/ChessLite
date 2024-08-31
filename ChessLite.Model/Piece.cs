namespace ChessLite.Model;

public static class Piece {
    public const int None = 0;
    public const int King = 1;
    public const int Pawn = 2;
    public const int Knight = 3;
    public const int Bishop = 4;
    public const int Rook = 5;
    public const int Queen = 6;
    
    /* For now, unused */
    public const int CustomPiece1 = 7;
    public const int CustomPiece2 = 0b1000;
    public const int CustomPiece3 = 0b1001;
    public const int CustomPiece4 = 0b1010;
    public const int CustomPiece5 = 0b1011;
    public const int CustomPiece6 = 0b1100;
    public const int CustomPiece7 = 0b1101;
    public const int CustomPiece8 = 0b1110;
    public const int CustomPiece9 = 0b1111;


    /* Colours */
    public const int Black = 0b010000;
    public const int White = 0b100000;

    public const int ColourMask = 0b110000;
    public const int PieceMask = 0b001111;
}