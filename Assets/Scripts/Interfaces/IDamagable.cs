
namespace Balloons
{

    /// <summary>
    /// Интерфейс взаимодействия с объектами, способными получать урон
    /// </summary>
    public interface IDamagable
    {

        /// <summary>
        /// Обработчик получения урона
        /// </summary>
        void OnDamaged();

    }

}
