public interface ButtonTriggered
{
	ButtonObject[] ConnectedButtons { get; }

	//Methods for button activation
	void ButtonActivated();
	void ButtonReleased();

	//Methods to link and unlink delegates
	void LinkButtons();
	void UnlinkButtons();
}
