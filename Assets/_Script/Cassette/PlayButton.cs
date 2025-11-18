using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayButton : UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable
{
    public CassetteDeckManager deckManager;

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(OnButtonPressed);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(OnButtonPressed);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        if (deckManager != null)
        {
            deckManager.PlayAudio(); 
        }
    }
}