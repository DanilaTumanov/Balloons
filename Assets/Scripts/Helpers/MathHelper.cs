
namespace Helpers
{

    public static class MathHelper
    {

        public static int RoundToOrder(this float value, int order)
        {
            return (int)(value / order) * order;
        }

    }

}


