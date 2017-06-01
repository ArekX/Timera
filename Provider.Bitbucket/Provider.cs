using Provider.Base;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Provider.Bitbucket
{
    public class Provider : BaseProvider
    {
        public Provider() {

        }

        public override string Name {
            get { return "Bitbucket"; }
        }

        public override void ExecuteAction(BaseAction action) {
            throw new NotImplementedException();
        }

        public override void ExecuteActionFlow(BaseActionFlow flow) {
            throw new NotImplementedException();
        }

        public override List<BaseAction> GetActions() {
            throw new NotImplementedException();
        }

        public override void Activate() {
      
        }

        public override void Deactivate() {
            throw new NotImplementedException();
        }

        public override Hashtable GetStoreableSettings() {
            return null;
        }

        public override void LoadSettings(Hashtable settings) {
            throw new NotImplementedException();
        }
    }
}
