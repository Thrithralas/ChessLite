namespace ChessLite.Model;

// ReSharper disable once InconsistentNaming
public static class FENLoader {
    // ReSharper disable once InconsistentNaming
    public const string StartingFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

    // ReSharper disable once InconsistentNaming
    public struct FEN {
        public Board Board;
        public bool WhitesTurn;
        public bool CanWhiteCastleKingSide;
        public bool CanWhiteCastleQueenSide;
        public bool CanBlackCastleKingSide;
        public bool CanBlackCastleQueenSide;
        public (int, int)? EnPassantSquare;
        public int HalfMoveClock;
        public int FullMoveClock;
    }

    private static Dictionary<char, int> _mappings = new() {
        ['k'] = Piece.King,
        ['p'] = Piece.Pawn,
        ['b'] = Piece.Bishop,
        ['n'] = Piece.Knight,
        ['r'] = Piece.Rook,
        ['q'] = Piece.Queen
    };

    private static (int, int)? ParseSquare(string s) {
        if (s == "-") return null;
        return (s[0] - 'a', int.Parse(s[1..]));
    }

    public static FEN StartingBoard() => FromString(StartingFEN);
    
    public static FEN FromString(string FEN, Board? initialBoard = null, bool relaxedBounds = false) {
        
        initialBoard ??= new Board();
        string[] segments = FEN.Split();
        
        // Six segments: Board, Move, Castling, EnPassant, HMC, FMC
        if (segments.Length != 6) throw new ArgumentException("FEN has incorrect number of segments", nameof(FEN));

        string[] rows = segments[0].Split('/');
        
        // Should have 8 rows
        if (rows.Length != 8 && !relaxedBounds) throw new ArgumentException("FEN has incorrect number of rows", nameof(FEN));
        for (int i = 0; i < initialBoard.Pieces.GetLength(0); i++) {
            int j = 0;
            foreach (char pieceOrSkip in rows[i]) {
                if (int.TryParse(pieceOrSkip.ToString(), out int num)) {
                    j += num;
                }
                else if (_mappings.TryGetValue(char.ToLower(pieceOrSkip), out var mapping)) {
                    initialBoard[i, j] = (char.IsUpper(pieceOrSkip) ? Piece.White : Piece.Black) | mapping;
                    j++;
                }
                else throw new ArgumentException("Illegal piece on board", nameof(FEN));
            }
        }

        return new FEN() {
            Board = initialBoard,
            WhitesTurn = segments[1].Contains('w'),
            CanWhiteCastleKingSide = segments[2].Contains('K'),
            CanWhiteCastleQueenSide = segments[2].Contains('Q'),
            CanBlackCastleKingSide = segments[2].Contains('k'),
            CanBlackCastleQueenSide = segments[2].Contains('q'),
            EnPassantSquare = ParseSquare(segments[3]),
            HalfMoveClock = int.Parse(segments[4]),
            FullMoveClock = int.Parse(segments[5])
        };


    }
    
}