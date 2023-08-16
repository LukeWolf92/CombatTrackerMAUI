using CombatTracker.ViewModel;

namespace CombatTracker;

public partial class SalvaPartyPage : ContentPage
{
    public SalvaPartyPage(SalvaPartyViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}