
namespace Business.Core
{
    public interface IComposer<TComponent>
    {
        void Compose(TComponent component);
    }
}
