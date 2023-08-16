using CombatTracker.ViewModel;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CombatTracker.Models;
public partial class CharacterLightModel : ObservableObject
{
    [ObservableProperty]
    private string nameLight;
    [ObservableProperty]
    private CharacterType typeLight;
    [ObservableProperty]
    private int cALight;
    [ObservableProperty]
    private int pFLight;
}
