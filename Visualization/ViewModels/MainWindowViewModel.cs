using System.Collections.Generic;
using ReactiveUI;

namespace Visualization.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        var hashTablesPages = new List<ViewModelBase>
        {
            new ChainingViewModel(),
            new OpenAddressingViewModel()
        };
        var hashTablesNode = new NodeViewModel("Hash Tables", hashTablesPages);
        NavigationTree = [hashTablesNode];

        _currentPage = hashTablesPages[1];
    }

    public List<NodeViewModel> NavigationTree { get; }

    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }
    private ViewModelBase _currentPage;
}

public sealed class NodeViewModel : ViewModelBase
{
    public NodeViewModel(string title, List<ViewModelBase> pages)
    {
        Title = title;
        Pages = pages;
    }

    public List<ViewModelBase> Pages { get; } 
}