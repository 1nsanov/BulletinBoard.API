namespace BulletinBoard.API.Controllers
{
    /// <summary>
    /// Базовый класс для контроллеров
    /// </summary>
    /// <typeparam name="T">Тип сервиса</typeparam>
    public abstract class ControllerBase<T>
        where T : class
    {
        protected T _service { get; set; }

        protected ControllerBase(T service)
        {
            _service = service;
        }
    }
}
