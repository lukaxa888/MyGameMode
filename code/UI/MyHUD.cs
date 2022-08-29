using Sandbox;
using Sandbox.UI;

public partial class MyHud :  HudEntity<RootPanel>
{
	public MyHud()
	{
		if ( !IsClient ) return;
		RootPanel.AddChild<Vitals>();
	}
}
