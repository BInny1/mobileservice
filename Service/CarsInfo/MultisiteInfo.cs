using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarsInfo
{
    public class MultisiteInfo
    {
        private string _AASuccess;

        public string AASuccess
        {
            get { return _AASuccess; }
            set { _AASuccess = value; }
        }

        private string _MultisitesURL;

        public string MultisitesURL
        {
            get { return _MultisitesURL; }
            set { _MultisitesURL = value; }
        }

        private string _MultiSiteName;

        public string MultiSiteName
        {
            get { return _MultiSiteName; }
            set { _MultiSiteName = value; }
        }

        private string _ValidDays;


        public string ValidDays
        {
            get { return _ValidDays; }
            set { _ValidDays = value; }
        }


        private string _PostedDate;

        public string PostedDate
        {
            get { return _PostedDate; }
            set { _PostedDate = value; }
        }

    }
}
