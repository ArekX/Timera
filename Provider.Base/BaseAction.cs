using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Base
{
    public abstract class BaseAction
    {
        protected bool isConfigurable = false;

        public bool IsConfigurable {
            get {
                return isConfigurable;
            }
        }

        public abstract void OpenConfiguration();
    }
}
