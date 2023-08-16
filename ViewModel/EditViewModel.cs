using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CombatTracker.ViewModel;

public partial class EditViewModel : ObservableObject, IQueryAttributable
{
    public EditViewModel() 
    {
    }

    [ObservableProperty]
    private int id;
    [ObservableProperty]
    private int newInitiative;
    [ObservableProperty]
    private string newName;
    [ObservableProperty]
    private string newType2;
    [ObservableProperty]
    private int newCA;
    [ObservableProperty]
    private int newPF;

    [ObservableProperty]
    private Color foreTypeColor = Colors.White;
    [ObservableProperty]
    private string newIsDead;
    [ObservableProperty]
    private Color uccidiBackColor = Colors.Green;

    
    [ObservableProperty]
    public List<string> listaType = new() { Costanti.Giocatore, Costanti.Alleato, Costanti.Nemico, Costanti.Magia, Costanti.Ambiente };

    private CharacterType SelectedType { get; set; }

    [RelayCommand]
    private void Uccidi()
    {
        NewIsDead = NewIsDead == "VIVO" ? "MORTO" : "VIVO";
        if (NewIsDead == "MORTO")
            NewPF = 0;
        else if (NewIsDead == "VIVO" && NewPF <= 0)
            NewPF = 1;
    }

    [RelayCommand] // ".." indica di tornare indietro di uno step
    private async Task Edit()
    {
        if (NewPF <= 0)
            NewIsDead = "MORTO";
        await Shell.Current.GoToAsync("..", new Dictionary<string, object>
        {
            { Costanti.QueryCreaturaStatica, new CharacterModel
                {
                    Id = Id,
                    Initiative = NewInitiative,
                    Name = NewName,
                    Type = SelectedType,
                    CA = NewCA,
                    PF = NewPF,
                    IsDead = NewIsDead == "MORTO",
                } }
        });
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(Costanti.QueryCreaturaEdit, out object CreaturaEdit))
        {
            CharacterModel c = CreaturaEdit as CharacterModel;
            Id = c.Id;
            NewInitiative= c.Initiative;
            NewName = c.Name;
            NewType2 = c.Type.ToString().ToUpper();
            NewCA = c.CA;
            NewPF = c.PF;
            
            NewIsDead = c.IsDead ? "MORTO" : "VIVO";
            UccidiBackColor = c.IsDead ? Colors.Gray : Colors.Green;

        }
    }
    partial void OnNewPFChanged(int value)
    {
        if (value > 0)
        {
            NewIsDead = "VIVO";
            UccidiBackColor = Colors.Green;
        }
        else
        {
            NewIsDead = "MORTO";
            UccidiBackColor = Colors.Gray;
        }
    }
    partial void OnNewType2Changed(string value)
    {
        switch (value)
        {
            case Costanti.Giocatore:
                {
                    SelectedType = CharacterType.Giocatore;
                    ForeTypeColor = Costanti.ColorGiocatore;
                    break;
                }
            case Costanti.Alleato:
                {
                    SelectedType = CharacterType.Alleato;
                    ForeTypeColor = Costanti.ColorAlleato;
                    break;
                }
            case Costanti.Nemico:
                {
                    SelectedType = CharacterType.Nemico;
                    ForeTypeColor = Costanti.ColorNemico;
                    break;
                }
            case Costanti.Magia:
                {
                    SelectedType = CharacterType.Magia;
                    ForeTypeColor = Costanti.ColorMagia;
                    break;
                }
            case Costanti.Ambiente:
                {
                    SelectedType = CharacterType.Ambiente;
                    ForeTypeColor = Costanti.ColorAmbiente;
                    break;
                }
        }
    }
}
