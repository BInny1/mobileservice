using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CarsInfo
{
    [Serializable]
    public class PackagesInfo
    {

        private string _AASuccess;

        public string AASuccess
        {
            get { return _AASuccess; }
            set { _AASuccess = value; }
        }


        private string _UID;

        public string UID
        {
          get { return _UID; }
          set { _UID = value; }
        }


        private string _PackageID;

        public string PackageID
        {
            get { return _PackageID; }
            set { _PackageID = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _ValidityPeriod;

        public string ValidityPeriod
        {
            get { return _ValidityPeriod; }
            set { _ValidityPeriod = value; }
        }

        private string _ProcessingFee;

        public string ProcessingFee
        {
            get { return _ProcessingFee; }
            set { _ProcessingFee = value; }
        }

        private string _MaxPhotos;

        public string MaxPhotos
        {
            get { return _MaxPhotos; }
            set { _MaxPhotos = value; }
        }

        private string _Price;

        public string Price
        {
            get { return _Price; }
            set { _Price = value; }
        }


        private string _UserPackID;

        public string UserPackID
        {
            get { return _UserPackID; }
            set { _UserPackID = value; }
        }

        private string _CarsCount;

        public string CarsCount
        {
            get { return _CarsCount; }
            set { _CarsCount = value; }
        }

        private string _MaxCars;

        public string MaxCars
        {
            get { return _MaxCars; }
            set { _MaxCars = value; }
        }

        private string _PayDate;

        public string PayDate
        {
            get { return _PayDate; }
            set { _PayDate = value; }
        }

        private string _CreatedDate;

        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        private string _PDDate;

        public string PDDate
        {
            get { return _PDDate; }
            set { _PDDate = value; }
        }
    }

}
