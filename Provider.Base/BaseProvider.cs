using System.Collections.Generic;

namespace Provider.Base
{
    public abstract class BaseProvider
    {
        public virtual string Name {
            get { return "Unknown Provider"; }
        }

        public abstract void Activate();

        public abstract void Deactivate();

        public abstract List<BaseAction> GetActions();

        public abstract void ExecuteAction(BaseAction action);

        public abstract void ExecuteActionFlow(BaseActionFlow flow);
    }
}
