public class PopupShopStone : PopupBase
{
    public void AddStone(int amount)
    {

#if UNITY_EDITOR

        ResourceManager.Instance.AddStoneGreen(amount);
        DestroyPopup();


#else
        
#endif










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