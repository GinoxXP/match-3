using DG.Tweening;

public class SimpleChip : Chip
{
    public override void Click()
    {
        if (swapChips.SetSwapChip(this))
            base.Click();
    }
}
