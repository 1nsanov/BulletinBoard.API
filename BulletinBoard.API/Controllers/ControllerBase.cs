namespace BulletinBoard.API.Controllers
{
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
