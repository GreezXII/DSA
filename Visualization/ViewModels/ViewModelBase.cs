using ReactiveUI;

namespace Visualization.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public virtual string Title { get; protected set; } = string.Empty;
}