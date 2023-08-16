using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CombatTracker.ViewModel;

public partial class CreaNuovoViewModel : ObservableObject
{
    public CreaNuovoViewModel() { }

    [ObservableProperty]
    private int newInitiative;
    [ObservableProperty]
    private string newName;
    [ObservableProperty]
    private string newType2 = Costanti.Nemico;
    [ObservableProperty]
    private int newCA;
    [ObservableProperty]
    private int newPF;

    [ObservableProperty]
    private Color foreTypeColor = Costanti.ColorNemico;

    [ObservableProperty]
    public List<string> listaType = new() { Costanti.Giocatore, Costanti.Alleato, Costanti.Nemico, Costanti.Magia, Costanti.Ambiente };
    private CharacterType SelectedType { get; set; } = CharacterType.Nemico;

    [RelayCommand] // ".." indica di tornare indietro di uno step
    private async Task Crea()
    {
        if (string.IsNullOrWhiteSpace(NewName))
            NewName = "BOT";
        await Shell.Current.GoToAsync("..", new Dictionary<string, object>
        {
            { Costanti.QueryCreaturaStatica, new CharacterModel
                {
                    Id = 0,
                    Initiative = NewInitiative,
                    Name = NewName.ToUpper(),
                    Type = SelectedType,
                    CA = NewCA,
                    PF = NewPF,
                    IsDead = NewPF <= 0
                } }
        });
    }
    partial void OnNewNameChanged(string value)
    {
        NewName = value.ToUpper();
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
    //[RelayCommand]
    //async Task GoBack()
    //{

    //    await Shell.Current.GoToAsync(".."); // ".." indica di tornare indietro di uno step

    //    //string name = "prova";
    //    //int initiative = 10;
    //    //await Shell.Current.GoToAsync($"..?name={name}&initiative={initiative}"); //non funziona
    //}
}
