using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarsInfo
{
    public class MobileCarInfo
    {

        private string _AASuccess;

        public string AASuccess
        {
            get { return _AASuccess; }
            set { _AASuccess = value; }
        }

        private string _CarID;

        public string CarID
        {
            get { return _CarID; }
            set { _CarID = value; }
        }

        private string _UID;

        public string UID
        {
            get { return _UID; }
            set { _UID = value; }
        }

        private string _Reason;

        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }


    }
}
