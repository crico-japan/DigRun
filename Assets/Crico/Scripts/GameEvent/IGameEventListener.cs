namespace Crico.GameEvents
{
    public interface IGameEventListener
    {
        void OnEventRaised();
    }

    public interface IGameEventListener<T0>
    {
        void OnEventRaised(T0 arg0);
    }

    public interface IGameEventListener<T0, T1>
    {
        void OnEventRaised(T0 arg0, T1 arg1);
    }

}
