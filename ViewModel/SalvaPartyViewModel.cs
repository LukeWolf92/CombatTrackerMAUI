using System.Collections.ObjectModel;
using System.Text.Json;

using CombatTracker.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

//using HealthKit;

namespace CombatTracker.ViewModel;

public partial class SalvaPartyViewModel : ObservableObject, IQueryAttributable
{
    public SalvaPartyViewModel() => Task.Run(ReadListaPartyFile);
    [ObservableProperty]
    private ObservableCollection<PartyDatasModel> listaParty;
    [ObservableProperty]
    private string nuovoNomeParty;

    private List<CharacterLightModel> CreatureDaSalvare = new();
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue( Costanti.QueryCreatureDaSalvare, out object tempo))
            CreatureDaSalvare = tempo as List<CharacterLightModel>;
    }
    [RelayCommand]
    private void AssegnaNome(string tempoNome)
    {
        NuovoNomeParty = tempoNome;
    }
    private async Task ReadListaPartyFile()
    {
        ListaParty = new ObservableCollection<PartyDatasModel>();
        try
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, Costanti.NomeListaPartyFile);
            if (!File.Exists(path))
            {
                using StreamWriter file = new(path);
            }
            using StreamReader sr = File.OpenText(path);
            string DataResourceText = await sr.ReadToEndAsync();
            
            PartyDatasModel[] tempo = JsonSerializer.Deserialize<PartyDatasModel[]>(DataResourceText);
            foreach (PartyDatasModel p in tempo)
                ListaParty.Add(p);
        }
        catch (FileNotFoundException)
        {
            throw;
        }
    }
    [RelayCommand]
    private async void ChiediConfermaNomeGruppo()
    {
        if (!CreatureDaSalvare.Any())
            return;

        NuovoNomeParty = await Application.Current.MainPage.DisplayPromptAsync("Nome party", "","OK","Annulla",null,20,Keyboard.Plain,NuovoNomeParty);
        if (string.IsNullOrWhiteSpace(NuovoNomeParty))
            return;
        
        await Task.Run(WritePartyFile);
        UpdateListaParty();
        await Task.Run(WriteListaPartyFile);

    }
    private void UpdateListaParty()
    {
        var tempo = ListaParty.FirstOrDefault(x => x.NomeGruppo == NuovoNomeParty);
        if (tempo == null)
            ListaParty.Add(new PartyDatasModel { NomeGruppo = NuovoNomeParty, DimensioneGruppo = CreatureDaSalvare.Count });
        else
            tempo.DimensioneGruppo = CreatureDaSalvare.Count;
    }
    private async Task WritePartyFile()
    {
        string creatureJson = JsonSerializer.Serialize(CreatureDaSalvare);
        try
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, NuovoNomeParty + ".json");
            using (StreamWriter sw = new(path))
            {
                sw.WriteLine(creatureJson);
            }
            using StreamReader sr = File.OpenText(path);
            creatureJson = await sr.ReadToEndAsync();
        }
        catch (FileNotFoundException)
        {
            throw;
        }
    }
    private async Task WriteListaPartyFile()
    {
        string listaPartyJson = JsonSerializer.Serialize(ListaParty);
        try
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, Costanti.NomeListaPartyFile);
            using (StreamWriter sw = new(path))
            {
                sw.WriteLine(listaPartyJson);
            }
            using StreamReader sr = File.OpenText(path);
            listaPartyJson = await sr.ReadToEndAsync();
        }
        catch (FileNotFoundException)
        {
            throw;
        }
    }
}
