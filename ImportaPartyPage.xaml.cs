using CombatTracker.ViewModel;

namespace CombatTracker;

public partial class ImportaPartyPage : ContentPage
{
	public ImportaPartyPage(ImportaPartyViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}