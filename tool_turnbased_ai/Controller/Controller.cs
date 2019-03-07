namespace tool_turnbased_ai.Controller
{
    public interface IController
    {
        void Update();        
    }

    public abstract class Controller : IController
    {
        protected bool          isEditMode;
        public    abstract void Update();
        protected abstract void EnableEditMode();
        protected abstract void EnableReportMode();
    }
}
