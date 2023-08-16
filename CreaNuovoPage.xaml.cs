using CombatTracker.ViewModel;

namespace CombatTracker;

public partial class CreaNuovoPage : ContentPage
{
	public CreaNuovoPage(CreaNuovoViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}	
	public CreaNuovoPage()
	{
		InitializeComponent();
		BindingContext = new CreaNuovoViewModel();
	}
}