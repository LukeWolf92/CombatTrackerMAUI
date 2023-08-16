using CombatTracker.ViewModel;
using CommunityToolkit.Mvvm.Input;

namespace CombatTracker;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext= vm;
        vm.Navigation = Navigation;
	}

    // Comodo per far partire metodi del ViewModel quando viene caricata la pagina
    protected override void OnAppearing()
    {
        if (BindingContext is MainViewModel vm)
        {
            //CharacterModel creaturaDaAggiungere = StaticDatas.GetCreaturaStatica();
            //if (creaturaDaAggiungere is not null)
            //    vm.Creature.Add(creaturaDaAggiungere);
        }
    }
}

