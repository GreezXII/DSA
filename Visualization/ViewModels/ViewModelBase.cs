using ReactiveUI;

namespace Visualization.ViewModels;

public class ViewModelBase : ReactiveObject
{
    /*public string Title
    {
        get => _title; 
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }
    private string _title = string.Empty;*/

    public virtual string Title { get; protected set; } = string.Empty;
}