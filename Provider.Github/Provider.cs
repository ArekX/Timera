using Provider.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Provider.Github
{
    public class Provider : BaseProvider
    {
        public ProviderSettings settings;

        public Provider() {
            settings = new ProviderSettings();
        }

        public override string Name {
            get { return "Github"; }
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
            TokenForm form = new TokenForm();

            form.Tag = this;

            form.ShowDialog();
            Debug.WriteLine("TEST!");
        }

        public override void Deactivate() {
            throw new NotImplementedException();
        }

        public override Hashtable GetStoreableSettings() {
            return settings.GetSettings();
        }

        public override void LoadSettings(Hashtable settings) {
            this.settings.SetSettings(settings);
        }
        
    }
}
