using AndroidX.ConstraintLayout.Core.Motion.Utils;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CombatTracker.ViewModel;

public partial class ModificaPfViewModel : ObservableObject, IQueryAttributable
{
    public ModificaPfViewModel() 
    {
    }
    [ObservableProperty]
    private CharacterModel creaturaPf;

    [ObservableProperty]
    private string spfName;
    [ObservableProperty]
    private int spfPF;
    [ObservableProperty]
    private bool isAdding;
    [ObservableProperty]
    private Color backgroundColorOperazione;
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(Costanti.QueryCreaturaModificaPf, out object CreaturaSottraiPf))
        {
            CreaturaPf = CreaturaSottraiPf as CharacterModel;
            SpfName = CreaturaPf.Name;
            SpfPF = CreaturaPf.PF;
        }
        IsAdding = false;
        BackgroundColorOperazione = Costanti.ColorSottraiPf;
    }
    [RelayCommand]
    private void Operazione(string value)
    {
        if (IsAdding) 
            SpfPF += Convert.ToInt32(value);
        else
            SpfPF -= Convert.ToInt32(value);
    }
    [RelayCommand]
    private void InvertiOperazione()
    {
        if (IsAdding)
        {
            IsAdding = false;
            BackgroundColorOperazione = Costanti.ColorSottraiPf;
        }
        else
        {
            IsAdding = true;
            BackgroundColorOperazione = Costanti.ColorAggiungiPf;
        }
            
    }
    [RelayCommand]
    private async Task SalvaModificaPf()
    {
        CreaturaPf.PF = SpfPF;
        CreaturaPf.IsDead = CreaturaPf.PF <= 0;
        await Shell.Current.GoToAsync("..", new Dictionary<string, object>
        {
            { Costanti.QueryCreaturaModificaPf, CreaturaPf }
        });
    }
}
