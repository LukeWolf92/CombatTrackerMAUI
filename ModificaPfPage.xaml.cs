using CombatTracker.ViewModel;

namespace CombatTracker;

public partial class ModificaPfPage : ContentPage
{
	public ModificaPfPage(ModificaPfViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}