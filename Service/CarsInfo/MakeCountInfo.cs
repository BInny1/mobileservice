using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarsInfo
{
    [Serializable] 
    public class MakeCountInfo
    {
        private string _AASuccess;

        public string AASuccess
        {
            get { return _AASuccess; }
            set { _AASuccess = value; }
        }
        private string _Make;

        public string Make
        {
            get { return _Make; }
            set { _Make = value; }
        }

        private int _MakeID;

        public int MakeID
        {
            get { return _MakeID; }
            set { _MakeID = value; }
        }

        private int _MakeCount;

        public int MakeCount
        {
            get { return _MakeCount; }
            set { _MakeCount = value; }
        }
    }
}
