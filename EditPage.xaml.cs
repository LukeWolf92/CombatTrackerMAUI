using CombatTracker.ViewModel;

namespace CombatTracker;

public partial class EditPage : ContentPage
{
	public EditPage(EditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}