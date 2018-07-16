namespace Crawler.Commands
{
    public interface ICommand
    {
        bool IsValid();
        void Resolve();
    }
}
