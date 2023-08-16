using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CombatTracker.ViewModel;


namespace CombatTracker
{
    public static class Costanti
    {
        public static readonly Color ColorGiocatore = Colors.Green;
        public static readonly Color ColorAlleato = Colors.LightBlue;
        public static readonly Color ColorNemico = Colors.Red;
        public static readonly Color ColorMagia = Colors.Purple;
        public static readonly Color ColorAmbiente = Colors.Yellow;

        public static readonly Color ColorSottraiPf = Colors.Salmon;
        public static readonly Color ColorAggiungiPf = Colors.Khaki;

        public const string Giocatore = "GIOCATORE";
        public const string Alleato = "ALLEATO";
        public const string Nemico = "NEMICO";
        public const string Magia = "MAGIA";
        public const string Ambiente = "AMBIENTE";

        public const string NomeListaPartyFile = "listaParty.json";

        public const string QueryCreaturaStatica = "CreaturaStatica";
        public const string QueryPartyDaAggiungere = "PartyDaAggiungere";
        public const string QueryCreaturaEdit = "CreaturaEdit";
        public const string QueryCreatureDaSalvare = "CreatureDaSalvare";
        public const string QueryCreaturaModificaPf = "CreaturaModificaPf";
        public static bool TryGetValueRemoveKey(this IDictionary<string, object> query, string key, out object CreaturaStatica)
        {
            bool ret = query.TryGetValue(key, out CreaturaStatica);
            if (ret)
                query.Remove(key);
            return ret;
        }
    }
}
