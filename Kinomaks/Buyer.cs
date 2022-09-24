namespace Kinomaks
{
    internal class Buyer
    {
        public void DoBuy(IBuy buy)
        {
            if (buy != null)
                buy.DoBuy();
        }
    }
}