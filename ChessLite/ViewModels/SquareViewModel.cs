using System.Reactive.Linq;
using Avalonia.Media;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace ChessLite.ViewModels;

public partial class SquareViewModel : ViewModelBase {

    [Reactive]
    private int _piece;
    [Reactive]
    private int _x, _y;

    [ObservableAsProperty]
    private (int, int) _xY;

    [ObservableAsProperty] 
    private bool _hasPiece;

    [ObservableAsProperty]
    private IImmutableSolidColorBrush _background;
    
    [ObservableAsProperty]
    private string _path;

    public SquareViewModel() {
        _xYHelper = this
            .WhenAnyValue(t => t.X, t => t.Y)
            .ToProperty(this, t => t.XY);
        _hasPieceHelper = this
            .WhenAnyValue(t => t.Piece)
            .Select(piece => (piece & Model.Piece.ColourMask) > 0)
            .ToProperty(this, t => t.HasPiece);
        _backgroundHelper = this
            .WhenAnyValue(t => t.XY)
            .Select(xy => (xy.Item1 + xy.Item2) % 2 == 0 ? Brushes.Bisque : Brushes.Coral)
            .ToProperty(this, t => t.Background);
        _pathHelper = this
            .WhenAnyValue(t => t.Piece)
            .Select(piece => "/Assets/" + ((piece & Model.Piece.ColourMask) == Model.Piece.Black ? "b" : "w") + (piece & Model.Piece.PieceMask) + ".svg")
            .ToProperty(this, t => t.Path);
    }
}