using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Visualization.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        _currentPage = _pages[0];
    }
    
    private readonly ViewModelBase[] _pages =
    [
        new ProbeSequenceGraphViewModel(),
        new AboutViewModel()
    ];

    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        private set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }
    private ViewModelBase _currentPage;
}