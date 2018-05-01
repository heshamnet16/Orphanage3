using OrphanageV3.Controlls;

namespace OrphanageV3.Views.Interfaces
{
    public interface IView
    {
        OrphanageGridView GetOrphanageGridView();

        void Update(int ObjectId);

        string GetTitle();
    }
}