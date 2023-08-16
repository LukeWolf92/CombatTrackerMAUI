using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CombatTracker.Models;
public partial class PartyDatasModel : ObservableObject
{
    [ObservableProperty]
    private string nomeGruppo;
    [ObservableProperty]
    private int dimensioneGruppo;
}
