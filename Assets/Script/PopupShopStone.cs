public class PopupShopStone : PopupBase
{
    public void AddStone(int amount)
    {
        ResourceManager.Instance.AddStoneGreen(amount);
    }

    public override void DestroyPopup()
    {
        base.DestroyPopup();
        PopupLoss lossPopup = FindObjectOfType<PopupLoss>();
        if (lossPopup != null)
        {
            lossPopup.ResumeTimer();
        }

    }
}