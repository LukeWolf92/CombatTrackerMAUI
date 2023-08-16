using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

using CombatTracker.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

//using HealthKit;

namespace CombatTracker.ViewModel;

public partial class ImportaPartyViewModel : ObservableObject
{
    public ImportaPartyViewModel() => Task.Run(ReadListaPartyFile);
    [ObservableProperty]
    private ObservableCollection<PartyDatasModel> listaParty;
    [ObservableProperty]
    private ObservableCollection<CharacterLightModel> creatureSalvate;

    [ObservableProperty]
    private string nomePartyFile;

    [RelayCommand]
    private async Task RecuperaCreature(string NomeGruppo)
    {
        NomePartyFile = NomeGruppo;
        await Task.Run(ReadPartyFile);
    }
    [RelayCommand]
    private async Task CancellaGruppo(PartyDatasModel item)
    {
        ListaParty.Remove(item);
        if (!ListaParty.Any() || ListaParty == null) 
            ListaParty = new ObservableCollection<PartyDatasModel>();

        await Task.Run(WriteListaPartyFile);
    }

    [RelayCommand]
    private async Task AggiungiParty()
    {
        if (string.IsNullOrWhiteSpace(NomePartyFile))
        {
            await Application.Current.MainPage.DisplayAlert("Nessuna selezione", "Non hai selezionato un gruppo","Indietro");
            return;
        }
        await Shell.Current.GoToAsync("..", new Dictionary<string, object>
        {
            {Costanti.QueryPartyDaAggiungere, CreatureSalvate.ToList() }
        });
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
    private async Task ReadPartyFile()
    {
        try
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, NomePartyFile + ".json");
            if (!File.Exists(path))
            {
                using StreamWriter file = new(path);
            }
            using StreamReader sr = File.OpenText(path);
            string DataResourceText = await sr.ReadToEndAsync();
            CharacterLightModel[] crLight = JsonSerializer.Deserialize<CharacterLightModel[]>(DataResourceText);

            CreatureSalvate = new ObservableCollection<CharacterLightModel>();
            foreach (CharacterLightModel cr in crLight)
                CreatureSalvate.Add(cr);
        }
        catch (FileNotFoundException)
        {
            throw;
        }
    }
    private async Task WriteListaPartyFile()
    {
        string listaPartyJson = ListaParty.Any() ? JsonSerializer.Serialize(ListaParty) : "{}";
        try
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, Costanti.NomeListaPartyFile);
            using (StreamWriter sw = new(path))
            {
                sw.WriteLine(listaPartyJson);
            }
            File.WriteAllText(path, listaPartyJson);
            //using StreamReader sr = File.OpenText(path);
            //listaPartyJson = await sr.ReadToEndAsync();
        }
        catch (FileNotFoundException)
        {
            throw;
        }
    }
}
