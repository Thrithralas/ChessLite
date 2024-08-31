using System.Collections.ObjectModel;
using ChessLite.Model;
using ReactiveUI.SourceGenerators;

namespace ChessLite.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    public ObservableCollection<SquareViewModel> Squares { get; set; } = [];
    
    private (int, int)? _previous;

    [ReactiveCommand]
    private void UpdatePrevious((int x, int y) ab) {
        if (_previous == ab) _previous = null;
        else if (_previous is null) _previous = ab;
        else {
            (int x, int y) = _previous.Value;
            Squares[ab.x * 8 + ab.y].Piece = Squares[x * 8 + y].Piece;
            Squares[x * 8 + y].Piece = 0;
            _previous = null;
        }
    }

    public MainWindowViewModel() {
        InitializeCommands();
        Board b = Board.DefaultBoard();
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                Squares.Add(new SquareViewModel{
                    X = i,
                    Y = j,
                    Piece = b[i,j]
                });
            }
        }
    }
}