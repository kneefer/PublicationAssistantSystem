using PublicationAssistantSystem.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationAssistantSystem.DAL.Miscellaneous
{
    public class PublicationAssistantSystemDBInitializer : DropCreateDatabaseIfModelChanges<PublicationAssistantContext>
    {
        protected override void Seed(PublicationAssistantContext context)
        {
            base.Seed(context);




            context.SaveChanges();
        }
    }
}
