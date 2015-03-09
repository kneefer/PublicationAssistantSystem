﻿using PublicationAssistantSystem.Core.Mappers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationAssistantSystem.Core.Mappers.Common
{
    public class WOSRecord : IRecord
    {
        #region IRecord
        public string Title { get; set; }

        public IEnumerable<Author> Authors { get; set; }

        public PublicationDateTime PublicationDate { get; set; }
        #endregion IRecord
    }
}
