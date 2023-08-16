namespace CombatTracker;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(CreaNuovoPage), typeof(CreaNuovoPage));
        Routing.RegisterRoute(nameof(EditPage), typeof(EditPage));
        Routing.RegisterRoute(nameof(ImportaPartyPage), typeof(ImportaPartyPage));
        Routing.RegisterRoute(nameof(ModificaPfPage), typeof(ModificaPfPage));
        Routing.RegisterRoute(nameof(SalvaPartyPage), typeof(SalvaPartyPage));
    }
}
