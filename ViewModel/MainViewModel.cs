using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Nodes;

using CombatTracker.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CombatTracker.ViewModel;
public partial class MainViewModel : ObservableObject, IQueryAttributable
{
    public MainViewModel() => Creature = new ObservableCollection<CharacterModel>();

    [ObservableProperty]
    private ObservableCollection<CharacterModel> creature;

    public INavigation Navigation { get; internal set; }

    [RelayCommand]
    private async Task ApriCreaNuovo()
    {
        await Navigation.PushAsync(new CreaNuovoPage());
        //await Shell.Current.GoToAsync($"{nameof(CreaNuovoPage)}");
    }
    [RelayCommand]
    private async Task CaricaParty() => await Shell.Current.GoToAsync($"{nameof(ImportaPartyPage)}");
    [RelayCommand]
    private async Task SalvaParty()
    {
        List<CharacterLightModel> creatureDaSalvare = Creature.Where(x => x.Type == CharacterType.Giocatore || x.Type == CharacterType.Alleato).Select(x => new CharacterLightModel
        {
            TypeLight = x.Type,
            NameLight = x.Name,
            CALight = x.CA,
            PFLight = x.PF,
        }).ToList();
        if (creatureDaSalvare.Any())
            await Shell.Current.GoToAsync($"{nameof(SalvaPartyPage)}", new Dictionary<string, object>
            {
                {Costanti.QueryCreatureDaSalvare, creatureDaSalvare }
            });
    }    
    [RelayCommand]
    private void RemoveCreatura(CharacterModel item) => Creature.Remove(item);
    [RelayCommand]
    private async Task ModificaPf(CharacterModel item) => await Shell.Current.GoToAsync($"{nameof(ModificaPfPage)}", new Dictionary<string, object>
        {
            {Costanti.QueryCreaturaModificaPf, item }
        });
    [RelayCommand]
    private void UccidiCreatura(CharacterModel item)
    {
        item.PF = 0;
        item.IsDead = true;
        //item.BackColor = Colors.Gray;
        UpSert(item);
    }
    [RelayCommand]
    private void RianimaCreatura(CharacterModel item)
    {
        if (item.PF > 0)
            return;
        item.PF = 1;
        item.IsDead = false;
        item.BackColor = Colors.Black;
        UpSert(item);
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValueRemoveKey(Costanti.QueryCreaturaStatica, out object CreaturaStatica))
        {
            CharacterModel model = CreaturaStatica as CharacterModel;
            UpSert(model);
        }
        if (query.TryGetValueRemoveKey(Costanti.QueryCreaturaModificaPf, out object CreaturaPfAggiornati))
        {
            CharacterModel model = CreaturaPfAggiornati as CharacterModel;
            UpSert(model);
        }
        if (query.TryGetValueRemoveKey(Costanti.QueryPartyDaAggiungere, out object party))
        {
            List<CharacterLightModel> listaParty = party as List<CharacterLightModel>;
            for(int i = 0; i < listaParty.Count; i++)
            {
                CharacterLightModel c = listaParty[i];
                UpSert(new CharacterModel()
                {
                    Name = c.NameLight,
                    CA = c.CALight,
                    Type = c.TypeLight,
                    PF = c.PFLight
                });
            }
            Creature = new ObservableCollection<CharacterModel>(Creature.OrderByDescending(x => x.Initiative));
        }
    }

    private void UpSert(CharacterModel model)
    {
        CharacterModel esistente = Creature.FirstOrDefault(x => x.Id == model.Id);
        int indice = esistente is null ? -1 : Creature.IndexOf(esistente);
        if (indice < 0)
        {
            model.Id = Creature.Count + 1;
            Creature.Add(model);
        }
        else
            Creature[indice] = model;

        Creature = new ObservableCollection<CharacterModel>(Creature.OrderByDescending(x => x.Initiative));
    }

    [RelayCommand]
    private async Task ModificaCreatura(CharacterModel item) => await Shell.Current.GoToAsync($"{nameof(EditPage)}", new Dictionary<string, object>
        {
            {Costanti.QueryCreaturaEdit, item }
        });
    //public void AggiornaCreature() => Creature = new ObservableCollection<CharacterModel>(StaticDatas.ListaStaticaCreature);
}
public partial class CharacterModel : ObservableObject
{
    private readonly Color defaultForeColor = Colors.Green;
    private readonly Color defaultBackgroundColor = Colors.Black;
    [ObservableProperty]
    private int id;
    [ObservableProperty]
    private int initiative;
    [ObservableProperty]
    private CharacterType type;
    [ObservableProperty]
    private string name;
    [ObservableProperty]
    private int cA;
    [ObservableProperty]
    private int pF;
    [ObservableProperty]
    private Color foreColor;
    [ObservableProperty]
    private Color backColor;
    [ObservableProperty]
    private bool isPlaying;
    [ObservableProperty]
    private bool isDead;
    public CharacterModel()
    {
        ForeColor = defaultForeColor;
        BackColor = defaultBackgroundColor;
        IsPlaying = false;
        IsDead = false;
    }
    partial void OnTypeChanged(CharacterType value)
    {
        ForeColor = value == CharacterType.Giocatore
            ? Costanti.ColorGiocatore
            : value == CharacterType.Alleato
            ? Costanti.ColorAlleato
            : value == CharacterType.Nemico
            ? Costanti.ColorNemico
            : value == CharacterType.Magia
            ? Costanti.ColorMagia
            : value == CharacterType.Ambiente
            ? Costanti.ColorAmbiente
            : Colors.White;
    }
}
public enum CharacterType
{
    Giocatore,
    Alleato,
    Nemico,
    Magia,
    Ambiente
}
