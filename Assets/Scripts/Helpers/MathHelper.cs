
namespace Helpers
{

    /// <summary>
    /// Хэлпер с математическими расширениями
    /// </summary>
    public static class MathHelper
    {

        /// <summary>
        /// Округляет число, учитывая порядок (единицы, десяки, сотни и т.д)
        /// </summary>
        /// <param name="value">Число</param>
        /// <param name="order">Порядок</param>
        /// <returns></returns>
        public static int RoundToOrder(this float value, int order)
        {
            return (int)(value / order) * order;
        }

    }

}


