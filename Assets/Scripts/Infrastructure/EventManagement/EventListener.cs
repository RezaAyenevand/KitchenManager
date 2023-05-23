namespace Infrastructure.EventManagement
{
    public interface EventListener
    {
        void OnEvent(GameEvent evt, object sender);
    }
}