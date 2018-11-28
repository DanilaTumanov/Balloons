
namespace Balloons
{

    /// <summary>
    /// Интерфейс взаимодействия с объектами, которые имеют очки
    /// </summary>
    public interface IScoresHolder
    {

        /// <summary>
        /// Очки, начисляемые за взаимодействие с объектом
        /// </summary>
        int Scores { get; }

    }

}


