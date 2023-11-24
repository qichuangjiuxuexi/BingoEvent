namespace UAction
{
    public class UAction
    {
        public float startTime;
        public float endTime;
        public float duration;
        public bool active;

        public virtual void DoAwake()
        {
            duration = endTime - startTime;
        }

        public virtual void Apply(float timeSinceEnter)
        {

        }

        public virtual void Reset()
        {

        }

        public virtual void Retarget()
        {

        }

        public virtual void DoDestroy()
        {

        }
    }
}