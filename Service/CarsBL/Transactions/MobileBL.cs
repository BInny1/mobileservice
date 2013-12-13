#region System References
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Linq;

#endregion System References
using CarsInfo;
#region Application References

#endregion Application References

#region Microsoft Application Block References
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Net.Mail;
using System.IO;

#endregion Microsoft Application Block References

namespace CarsBL.Transactions
{
    public class MobileBL
    {
        public IList<UserLoginInfo> PerformLoginMobile(string UsersID, string sPassword)
        {

            IList<UserLoginInfo> UsersInfoIList = new List<UserLoginInfo>();
            try
            {
                string spNameString = string.Empty;

                //Setting Connection
                //Global.INSTANCE_NAME = strCurrentConn;

                IDataReader CarInfoDataReader = null;

                Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
                spNameString = "[USP_MobilePerform_Login]";
                DbCommand dbCommand = null;
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);

                dbDatabase.AddInParameter(dbCommand, "@UName", System.Data.DbType.String, UsersID);
                dbDatabase.AddInParameter(dbCommand, "@Password", System.Data.DbType.String, sPassword);

                CarInfoDataReader = dbDatabase.ExecuteReader(dbCommand);

                while (CarInfoDataReader.Read())
                {
                    //Assign values to the MakesInfo object list
                    UserLoginInfo ObjUserInfo_Info = new UserLoginInfo();
                    AssignUserLoginInfo(CarInfoDataReader, ObjUserInfo_Info);
                    UsersInfoIList.Add(ObjUserInfo_Info);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return UsersInfoIList;
        }

        private void AssignUserLoginInfo(IDataReader CarInfoDataReader, UserLoginInfo ObjUserInfo_Info)
        {
            try
            {
                ObjUserInfo_Info.AASuccess = "User Existed";
                ObjUserInfo_Info.IsActive = CarInfoDataReader["isActive"].ToString() == "" ? "Emp" : CarInfoDataReader["isActive"].ToString();
                ObjUserInfo_Info.Name = CarInfoDataReader["Name"].ToString() == "" ? "Emp" : CarInfoDataReader["Name"].ToString();
                ObjUserInfo_Info.UserID = CarInfoDataReader["UserID"].ToString() == "" ? "Emp" : CarInfoDataReader["UserID"].ToString();
                ObjUserInfo_Info.UID = CarInfoDataReader["UId"].ToString() == "" ? "Emp" : CarInfoDataReader["UId"].ToString();
                ObjUserInfo_Info.PhoneNumber = CarInfoDataReader["PhoneNumber"].ToString() == "" ? "Emp" : CarInfoDataReader["PhoneNumber"].ToString();
                ObjUserInfo_Info.PackageID = CarInfoDataReader["PackageID"].ToString() == "" ? "Emp" : CarInfoDataReader["PackageID"].ToString();

            }
            catch (Exception ex)
            {
            }
        }
        public IList<MobileUserRegData> GetUSerDetailsByUserID(int UID)
        {
            IList<MobileUserRegData> obj = new List<MobileUserRegData>();
            try
            {
                DataSet dsCars = new DataSet();
                string spNameString = string.Empty;
                Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
                spNameString = "[USP_GetMobileUSerDetailsByUserID]";
                DbCommand dbCommand = null;
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);
                dbDatabase.AddInParameter(dbCommand, "@UID", System.Data.DbType.Int32, UID);
                dsCars = dbDatabase.ExecuteDataSet(dbCommand);
                MobileUserRegData objRegInfo = new MobileUserRegData();
                if (dsCars.Tables.Count > 0)
                {
                    if (dsCars.Tables[0].Rows.Count > 0)
                    {
                        objRegInfo.AASucess = "Success";
                        objRegInfo.UId = dsCars.Tables[0].Rows[0]["UId"].ToString() == "" ? 0 : Convert.ToInt32(dsCars.Tables[0].Rows[0]["UId"].ToString());
                        objRegInfo.UserID = dsCars.Tables[0].Rows[0]["UserID"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["UserID"].ToString();
                        objRegInfo.Name = dsCars.Tables[0].Rows[0]["Name"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["Name"].ToString();
                        objRegInfo.BusinessName = dsCars.Tables[0].Rows[0]["BusinessName"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["BusinessName"].ToString();
                        objRegInfo.UserName = dsCars.Tables[0].Rows[0]["UserName"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["UserName"].ToString();
                        objRegInfo.AltEmail = dsCars.Tables[0].Rows[0]["AltEmail"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["AltEmail"].ToString();
                        objRegInfo.PhoneNumber = dsCars.Tables[0].Rows[0]["PhoneNumber"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["PhoneNumber"].ToString();
                        objRegInfo.AltPhone = dsCars.Tables[0].Rows[0]["AltPhone"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["AltPhone"].ToString();
                        objRegInfo.Address = dsCars.Tables[0].Rows[0]["Address"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["Address"].ToString();
                        objRegInfo.City = dsCars.Tables[0].Rows[0]["City"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["City"].ToString();
                        objRegInfo.StateCode = dsCars.Tables[0].Rows[0]["State_Code"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["State_Code"].ToString();
                        objRegInfo.StateID = dsCars.Tables[0].Rows[0]["StateID"].ToString() == "" ? 0 : Convert.ToInt32(dsCars.Tables[0].Rows[0]["StateID"].ToString());
                        objRegInfo.Zip = dsCars.Tables[0].Rows[0]["Zip"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["Zip"].ToString();
                        string StrCarIDs = string.Empty;
                        if (dsCars.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsCars.Tables[1].Rows.Count; i++)
                            {
                                if (StrCarIDs == "")
                                {
                                    StrCarIDs = dsCars.Tables[1].Rows[i]["carid"].ToString();
                                }
                                else
                                {
                                    StrCarIDs = StrCarIDs + "," + dsCars.Tables[1].Rows[i]["carid"].ToString();
                                }
                            }
                        }
                        objRegInfo.CarIDs = StrCarIDs == "" ? "Emp" : StrCarIDs;
                        obj.Add(objRegInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public IList<MobileUserRegData> GetUserData(int UID)
        {
            IList<MobileUserRegData> obj = new List<MobileUserRegData>();
            try
            {
                DataSet dsCars = new DataSet();
                string spNameString = string.Empty;
                Database dbDatabase = DatabaseFactory.CreateDatabase(Global.CarsConnTest);
                spNameString = "USP_GetUSerDetailsByUserID";
                DbCommand dbCommand = null;
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);
                dbDatabase.AddInParameter(dbCommand, "@UID", System.Data.DbType.Int32, UID);
                dsCars = dbDatabase.ExecuteDataSet(dbCommand);
                MobileUserRegData objRegInfo = new MobileUserRegData();
                if (dsCars.Tables.Count > 0)
                {
                    if (dsCars.Tables[0].Rows.Count > 0)
                    {
                        objRegInfo.Name = dsCars.Tables[0].Rows[0]["Name"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["Name"].ToString();
                        objRegInfo.BusinessName = dsCars.Tables[0].Rows[0]["BusinessName"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["BusinessName"].ToString();
                        objRegInfo.UserName = dsCars.Tables[0].Rows[0]["UserName"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["UserName"].ToString();
                        objRegInfo.AltEmail = dsCars.Tables[0].Rows[0]["AltEmail"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["AltEmail"].ToString();
                        objRegInfo.PhoneNumber = dsCars.Tables[0].Rows[0]["PhoneNumber"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["PhoneNumber"].ToString();
                        objRegInfo.AltPhone = dsCars.Tables[0].Rows[0]["AltPhone"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["AltPhone"].ToString();
                        objRegInfo.Address = dsCars.Tables[0].Rows[0]["Address"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["Address"].ToString();
                        objRegInfo.City = dsCars.Tables[0].Rows[0]["City"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["City"].ToString();
                        objRegInfo.StateCode = dsCars.Tables[0].Rows[0]["State_Code"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["State_Code"].ToString();
                        objRegInfo.StateID = dsCars.Tables[0].Rows[0]["StateID"].ToString() == "" ? 0 : Convert.ToInt32(dsCars.Tables[0].Rows[0]["StateID"].ToString());
                        objRegInfo.Zip = dsCars.Tables[0].Rows[0]["Zip"].ToString() == "" ? "Emp" : dsCars.Tables[0].Rows[0]["Zip"].ToString();
                        string StrCarIDs = string.Empty;
                        if (dsCars.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsCars.Tables[1].Rows.Count; i++)
                            {
                                if (StrCarIDs == "")
                                {
                                    StrCarIDs = dsCars.Tables[1].Rows[i]["carid"].ToString();
                                }
                                else
                                {
                                    StrCarIDs = StrCarIDs + "," + dsCars.Tables[1].Rows[i]["carid"].ToString();
                                }
                            }
                        }
                        objRegInfo.CarIDs = StrCarIDs == "" ? "Emp" : StrCarIDs;
                        obj.Add(objRegInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public DataSet GetCenterData(string AgentCenterCode)
        {
            try
            {
                DataSet dsUsers = new DataSet();
                string spNameString = string.Empty;
                Database dbDatabase = DatabaseFactory.CreateDatabase(Global.CARSALES_INSTANCE_NAME);
                spNameString = "USP_GetCenterData";
                DbCommand dbCommand = null;
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);


                dbDatabase.AddInParameter(dbCommand, "@AgentCenterCode", System.Data.DbType.String, AgentCenterCode);

                dsUsers = dbDatabase.ExecuteDataSet(dbCommand);
                return dsUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet HotLeadsPerformLogin(string UserName, string sPassword, string AgentCenterCode)
        {
            try
            {
                DataSet dsUsers = new DataSet();
                string spNameString = string.Empty;
                Database dbDatabase = DatabaseFactory.CreateDatabase(Global.CARSALES_INSTANCE_NAME);
                spNameString = "USP_HotLeadsPerformLoginForMobile";
                DbCommand dbCommand = null;
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);

                dbDatabase.AddInParameter(dbCommand, "@AgentLogUname", System.Data.DbType.String, UserName);
                dbDatabase.AddInParameter(dbCommand, "@AgentLogPassword", System.Data.DbType.String, sPassword);
                dbDatabase.AddInParameter(dbCommand, "@AgentCenterCode", System.Data.DbType.String, AgentCenterCode);


                dsUsers = dbDatabase.ExecuteDataSet(dbCommand);
                return dsUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDatetime()
        {
            try
            {
                DataSet dsCarsData = new DataSet();
                string spNameString = string.Empty;
                Database dbDataBase = DatabaseFactory.CreateDatabase(Global.CARSALES_INSTANCE_NAME);
                spNameString = "USP_GetDatetime";
                DbCommand dbCommand = null;
                dbCommand = dbDataBase.GetStoredProcCommand(spNameString);

                dsCarsData = dbDataBase.ExecuteDataSet(dbCommand);
                return dsCarsData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAllSalesByCenterForTicker(int AgentCenterID)
        {
            try
            {
                DataSet dsCarsData = new DataSet();
                string spNameString = string.Empty;
                Database dbDataBase = DatabaseFactory.CreateDatabase(Global.CARSALES_INSTANCE_NAME);
                spNameString = "USP_GetAllSalesByCenterForTicker";
                DbCommand dbCommand = null;
                dbCommand = dbDataBase.GetStoredProcCommand(spNameString);
                dbDataBase.AddInParameter(dbCommand, "@AgentCenterID", System.Data.DbType.Int32, AgentCenterID);
                dsCarsData = dbDataBase.ExecuteDataSet(dbCommand);
                return dsCarsData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAllCenterSalesByCenterForTicker(int AgentCenterID)
        {
            try
            {
                DataSet dsCarsData = new DataSet();
                string spNameString = string.Empty;
                Database dbDataBase = DatabaseFactory.CreateDatabase(Global.CARSALES_INSTANCE_NAME);
                spNameString = "USP_GetAllCenterSalesByCenterForTicker";
                DbCommand dbCommand = null;
                dbCommand = dbDataBase.GetStoredProcCommand(spNameString);
                dbDataBase.AddInParameter(dbCommand, "@AgentCenterID", System.Data.DbType.Int32, AgentCenterID);
                dsCarsData = dbDataBase.ExecuteDataSet(dbCommand);
                return dsCarsData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet SaveMobileCustomerInfo(string MethodName, string CustomerID, string AuthenticationID, string parameters)
        {
            try
            {
                DataSet dsUsers = new DataSet();
                string spNameString = string.Empty;
                Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
                spNameString = "USP_SaveMobileCustomerInfo";
                DbCommand dbCommand = null;
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);
                dbDatabase.AddInParameter(dbCommand, "@Parameters", System.Data.DbType.String, parameters);
                dbDatabase.AddInParameter(dbCommand, "@MethodName", System.Data.DbType.String, MethodName);
                dbDatabase.AddInParameter(dbCommand, "@CustomerID", System.Data.DbType.String, CustomerID);
                dbDatabase.AddInParameter(dbCommand, "@AuthenticationID", System.Data.DbType.String, AuthenticationID);
                dsUsers = dbDatabase.ExecuteDataSet(dbCommand);
                return dsUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<PackagesInfo> GetPackageDetailsBYUID(string UID)
        {

            IList<PackagesInfo> PackagesList = new List<PackagesInfo>();
            try
            {
                string spNameString = string.Empty;

                IDataReader PackagesInfoReader = null;

                Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
                spNameString = "[USP_MobileGetPackageDetailsByUserID]";
                DbCommand dbCommand = null;
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);

               // dbDatabase.AddInParameter(dbCommand, "@UName", System.Data.DbType.String, UsersID);
                dbDatabase.AddInParameter(dbCommand, "@UId", System.Data.DbType.Int32, UID);

                PackagesInfoReader = dbDatabase.ExecuteReader(dbCommand);

                while (PackagesInfoReader.Read())
                {
                    //Assign values to the MakesInfo object list
                    PackagesInfo ObjPackageInfo = new PackagesInfo();
                    AssignPackagesInfo(PackagesInfoReader, ObjPackageInfo);
                    PackagesList.Add(ObjPackageInfo);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return PackagesList;
        }

        private void AssignPackagesInfo(IDataReader PackagesInfoReader, PackagesInfo ObjPackageInfo)
        {
            try
            {
                ObjPackageInfo.AASuccess = "Success";
                ObjPackageInfo.CarsCount = PackagesInfoReader["CarsCount"].ToString() == "" ? "Emp" : PackagesInfoReader["CarsCount"].ToString();
                ObjPackageInfo.CreatedDate = PackagesInfoReader["CreateDate"].ToString() == "" ? "Emp" : PackagesInfoReader["CreateDate"].ToString();
                ObjPackageInfo.Description = PackagesInfoReader["Description"].ToString() == "" ? "Emp" : PackagesInfoReader["Description"].ToString();
                ObjPackageInfo.MaxCars = PackagesInfoReader["Maxcars"].ToString() == "" ? "Emp" : PackagesInfoReader["Maxcars"].ToString();
                ObjPackageInfo.MaxPhotos = PackagesInfoReader["Maxphotos"].ToString() == "" ? "Emp" : PackagesInfoReader["Maxphotos"].ToString();
                ObjPackageInfo.PackageID = PackagesInfoReader["PackageID"].ToString() == "" ? "Emp" : PackagesInfoReader["PackageID"].ToString();
                ObjPackageInfo.PayDate = PackagesInfoReader["PayDate"].ToString() == "" ? "Emp" : PackagesInfoReader["PayDate"].ToString();
                ObjPackageInfo.PDDate = PackagesInfoReader["PDDate"].ToString() == "" ? "Emp" : PackagesInfoReader["PDDate"].ToString();
                ObjPackageInfo.Price = PackagesInfoReader["Price"].ToString() == "" ? "Emp" : PackagesInfoReader["Price"].ToString();
                ObjPackageInfo.ProcessingFee = PackagesInfoReader["ProcessingFee"].ToString() == "" ? "Emp" : PackagesInfoReader["ProcessingFee"].ToString();
                ObjPackageInfo.UID = PackagesInfoReader["UID"].ToString() == "" ? "Emp" : PackagesInfoReader["UID"].ToString();
                ObjPackageInfo.UserPackID = PackagesInfoReader["UserPackID"].ToString() == "" ? "Emp" : PackagesInfoReader["UserPackID"].ToString();
                ObjPackageInfo.ValidityPeriod = PackagesInfoReader["ValidityPeriod"].ToString() == "" ? "Emp" : PackagesInfoReader["ValidityPeriod"].ToString();
            }
            catch (Exception ex)
            {
            }
        }

        public DataSet USP_UpdateRegUserDetails(UserRegistrationInfo objUserregisInfo)
        {

            string spNameString = string.Empty;
            DataSet dsUserInfo = new DataSet();
            Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
            spNameString = "[USP_MobileUpdateRegistrationDetails]";
            DbCommand dbCommand = null;

            try
            {
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);
                dbDatabase.AddInParameter(dbCommand, "@UId", System.Data.DbType.Int32, objUserregisInfo.UId);
                dbDatabase.AddInParameter(dbCommand, "@Name", System.Data.DbType.String, objUserregisInfo.Name);
                dbDatabase.AddInParameter(dbCommand, "@PhoneNumber", System.Data.DbType.String, objUserregisInfo.PhoneNumber);
                dbDatabase.AddInParameter(dbCommand, "@StateID", System.Data.DbType.Int32, objUserregisInfo.StateID);
                dbDatabase.AddInParameter(dbCommand, "@City", System.Data.DbType.String, objUserregisInfo.City);
                dbDatabase.AddInParameter(dbCommand, "@Address", System.Data.DbType.String, objUserregisInfo.Address);
                dbDatabase.AddInParameter(dbCommand, "@Zip", System.Data.DbType.String, objUserregisInfo.Zip);
                dbDatabase.AddInParameter(dbCommand, "@BusinessName", System.Data.DbType.String, objUserregisInfo.BusinessName);
                dbDatabase.AddInParameter(dbCommand, "@AltEmail", System.Data.DbType.String, objUserregisInfo.AltEmail);
                dbDatabase.AddInParameter(dbCommand, "@AltPhone", System.Data.DbType.String, objUserregisInfo.AltPhone);


                dsUserInfo = dbDatabase.ExecuteDataSet(dbCommand);

                //blnSuccess = objUserLog.SaveUserLog(UserLogInfo, ref lngReturn, "");
                return dsUserInfo;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataSet GetMobileCarPicIDs(int CarID)
        {

            Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
            DbCommand dbCommand = null;
            DataSet ds = new DataSet();
            string spString = string.Empty;
            try
            {
                spString = "USP_GetMobileCarPicIDs";
                dbCommand = dbDatabase.GetStoredProcCommand(spString);
                dbDatabase.AddInParameter(dbCommand,"@CarID", DbType.Int32, CarID);
                ds = dbDatabase.ExecuteDataSet(dbCommand);
           
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public DataSet SaveMobileNewCarDetails(string make,string model,string year,string mileage,string price,int userpackid,int packageid,int uid)
        {

            Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
            DbCommand dbCommand = null;
            DataSet ds = new DataSet();
            string spString = string.Empty;
            try
            {
                spString = "USP_MobileSaveNewCarDetails";
                dbCommand = dbDatabase.GetStoredProcCommand(spString);
                dbDatabase.AddInParameter(dbCommand, "@Make", DbType.String, make);
                dbDatabase.AddInParameter(dbCommand, "@Model", DbType.String, model);
                dbDatabase.AddInParameter(dbCommand, "@Mileage", DbType.String, mileage);
                dbDatabase.AddInParameter(dbCommand, "@price", DbType.String, price);
                dbDatabase.AddInParameter(dbCommand, "@year", DbType.String, year);
                dbDatabase.AddInParameter(dbCommand, "@userpackiD", DbType.Int32, userpackid);
                dbDatabase.AddInParameter(dbCommand, "@packageID", DbType.Int32, packageid);
                dbDatabase.AddInParameter(dbCommand, "@Uid", DbType.Int32, uid);
                ds = dbDatabase.ExecuteDataSet(dbCommand);

            }
            catch (Exception ex)
            {
            }
            return ds;

        }


        public string SaveMobileCarPicture(string PicLocation, string PicType, string PicName, int UID)
        {
            Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
            DbCommand dbCommand = null;
            string picID = string.Empty;
            IDataReader dr = null;
            string spString = string.Empty;
            try
            {
                spString = "[USP_SaveMobileCarPictureByCarID]";
                dbCommand = dbDatabase.GetStoredProcCommand(spString);
                dbDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UID);
                dbDatabase.AddInParameter(dbCommand, "@PicLocation", DbType.String, PicLocation);
                dbDatabase.AddInParameter(dbCommand, "@PicName", DbType.String, PicName);
                dbDatabase.AddInParameter(dbCommand, "@Pictype", DbType.String, PicType);
                dr = dbDatabase.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    picID = dr["picID"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return picID;
        }

        public bool UpdateMobilePicturesByCarId(CarsInfo.UsedCarsInfo objCarsInfo)
        {
            try
            {
                bool bnew = false;
                string spNameString = string.Empty;
                Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
                spNameString = "[USP_UpdateMobilePicturesByCarId]";
                DbCommand dbCommand = null;
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);

                dbDatabase.AddInParameter(dbCommand, "@pic1", System.Data.DbType.String, objCarsInfo.PIC1);
                dbDatabase.AddInParameter(dbCommand, "@pic2", System.Data.DbType.String, objCarsInfo.PIC2);
                dbDatabase.AddInParameter(dbCommand, "@pic3", System.Data.DbType.String, objCarsInfo.PIC3);
                dbDatabase.AddInParameter(dbCommand, "@pic4", System.Data.DbType.String, objCarsInfo.PIC4);
                dbDatabase.AddInParameter(dbCommand, "@pic5", System.Data.DbType.String, objCarsInfo.PIC5);
                dbDatabase.AddInParameter(dbCommand, "@pic6", System.Data.DbType.String, objCarsInfo.PIC6);
                dbDatabase.AddInParameter(dbCommand, "@pic7", System.Data.DbType.String, objCarsInfo.PIC7);
                dbDatabase.AddInParameter(dbCommand, "@pic8", System.Data.DbType.String, objCarsInfo.PIC8);
                dbDatabase.AddInParameter(dbCommand, "@pic9", System.Data.DbType.String, objCarsInfo.PIC9);
                dbDatabase.AddInParameter(dbCommand, "@pic10", System.Data.DbType.String, objCarsInfo.PIC10);
                dbDatabase.AddInParameter(dbCommand, "@Pic11", System.Data.DbType.String, objCarsInfo.PIC11);
                dbDatabase.AddInParameter(dbCommand, "@pic12", System.Data.DbType.String, objCarsInfo.PIC12);
                dbDatabase.AddInParameter(dbCommand, "@pic13", System.Data.DbType.String, objCarsInfo.PIC13);
                dbDatabase.AddInParameter(dbCommand, "@pic14", System.Data.DbType.String, objCarsInfo.PIC14);
                dbDatabase.AddInParameter(dbCommand, "@pic15", System.Data.DbType.String, objCarsInfo.PIC15);
                dbDatabase.AddInParameter(dbCommand, "@pic16", System.Data.DbType.String, objCarsInfo.PIC16);
                dbDatabase.AddInParameter(dbCommand, "@pic17", System.Data.DbType.String, objCarsInfo.PIC17);
                dbDatabase.AddInParameter(dbCommand, "@pic18", System.Data.DbType.String, objCarsInfo.PIC18);
                dbDatabase.AddInParameter(dbCommand, "@pic19", System.Data.DbType.String, objCarsInfo.PIC19);
                dbDatabase.AddInParameter(dbCommand, "@pic20", System.Data.DbType.String, objCarsInfo.PIC20);
                dbDatabase.AddInParameter(dbCommand, "@Pic0", System.Data.DbType.String, objCarsInfo.PIC0);
                dbDatabase.AddInParameter(dbCommand, "@CarID", System.Data.DbType.Int32, objCarsInfo.Carid);
               
                dbDatabase.ExecuteDataSet(dbCommand);
                bnew = true;
                return bnew;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateMobileDescriptionByCarID(int Carid,string Description)
        {
            try
            {
                bool bnew = false;
                string spNameString = string.Empty;
                Database dbDatabase = DatabaseFactory.CreateDatabase(Global.CarsConnTest);
                spNameString = "USP_UpdateMobileDescriptionByCarID";
                DbCommand dbCommand = null;
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);

                dbDatabase.AddInParameter(dbCommand, "@CarID", System.Data.DbType.String, Carid);
                dbDatabase.AddInParameter(dbCommand, "@Description", System.Data.DbType.String, Description);
              
                dbDatabase.ExecuteDataSet(dbCommand);
                bnew = true;
                return bnew;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<MakeCountInfo> GetMakeCounts()
        {
            //Decalaring MakesInfo division object collection
            IList<MakeCountInfo> MakesInfoIList = new List<MakeCountInfo>();

            string spNameString = string.Empty;


            //Setting Connection
            //Global.INSTANCE_NAME = strCurrentConn;

            IDataReader MakesInfoDataReader = null;


            //Connect to the database
            Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);

            //Assign stored procedure name

            spNameString = "[USP_GetAllMakeCountsMobile]";
            DbCommand dbCommand = null;

            try
            {
                //Set stored procedure to the command object
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);

                dbDatabase.AddInParameter(dbCommand, "@makeID", DbType.Int64, 0);

                //Executing stored procedure
                MakesInfoDataReader = dbDatabase.ExecuteReader(dbCommand);

                while (MakesInfoDataReader.Read())
                {
                    //Assign values to the MakesInfo object list
                    MakeCountInfo ObjMakesInfo_Info = new MakeCountInfo();
                    AssignMakesInfoList(MakesInfoDataReader, ObjMakesInfo_Info);
                    MakesInfoIList.Add(ObjMakesInfo_Info);
                }
            }
            catch (Exception ex)
            {
              
            }
            finally
            {
                MakesInfoDataReader.Close();
            }

            return MakesInfoIList;
        }


        public DataSet mobileUpdateCarfeatures(int CarID, int FeatureID, int Isactive, int TranBy)
        {
            try
            {
                DataSet dsCars = new DataSet();
                string spNameString = string.Empty;
                Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
                spNameString = "[USP_UpdateMobileCarFeatures]";
                DbCommand dbCommand = null;
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);

                dbDatabase.AddInParameter(dbCommand, "@CarID", System.Data.DbType.Int32, CarID);
                dbDatabase.AddInParameter(dbCommand, "@FeatureID", System.Data.DbType.Int32, FeatureID);
                dbDatabase.AddInParameter(dbCommand, "@Isactive", System.Data.DbType.Int32, Isactive);
                dbDatabase.AddInParameter(dbCommand, "@TranBy", System.Data.DbType.Int32, TranBy);
                dsCars = dbDatabase.ExecuteDataSet(dbCommand);
                return dsCars;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet UpdateMobileSellerInfo(UsedCarsInfo objUsedCarsInfo, int CarID, int UID)
        {
            try
            {
                bool bnew = false;
                DataSet dsCars = new DataSet();
                string spNameString = string.Empty;
                Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
                spNameString = "USP_UpdateMobileSellerInfo";
                DbCommand dbCommand = null;
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);

                dbDatabase.AddInParameter(dbCommand, "@sellerID", System.Data.DbType.Int32, objUsedCarsInfo.SellerID);
                dbDatabase.AddInParameter(dbCommand, "@sellerName", System.Data.DbType.String, objUsedCarsInfo.SellerName);
                //dbDatabase.AddInParameter(dbCommand, "@address1", System.Data.DbType.String, objUsedCarsInfo.Address1);
                dbDatabase.AddInParameter(dbCommand, "@city", System.Data.DbType.String, objUsedCarsInfo.City);
                dbDatabase.AddInParameter(dbCommand, "@state", System.Data.DbType.String, objUsedCarsInfo.State);
                dbDatabase.AddInParameter(dbCommand, "@Zip", System.Data.DbType.String, objUsedCarsInfo.Zip);
                dbDatabase.AddInParameter(dbCommand, "@Phone", System.Data.DbType.String, objUsedCarsInfo.Phone);
                dbDatabase.AddInParameter(dbCommand, "@email", System.Data.DbType.String, objUsedCarsInfo.Email);
                dbDatabase.AddInParameter(dbCommand, "@CarID", System.Data.DbType.Int32, CarID);
                dbDatabase.AddInParameter(dbCommand, "@UID", System.Data.DbType.Int32, UID);


                dsCars = dbDatabase.ExecuteDataSet(dbCommand);
                return dsCars;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    

        public DataSet CheckMobileCarDetailsByCarID(int carid,int makeModelID,int year)
        {
            Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
            DbCommand dbCommand = null;
            DataSet ds = new DataSet();
            string spString = string.Empty;
            try
            {
                spString = "USP_CheckMobileCarDetails";
                dbCommand = dbDatabase.GetStoredProcCommand(spString);
                dbDatabase.AddInParameter(dbCommand, "@CarID", DbType.Int32, carid);
                dbDatabase.AddInParameter(dbCommand, "@MakeModelID", DbType.Int32, makeModelID);
                dbDatabase.AddInParameter(dbCommand, "@Year", DbType.Int32, year);
                ds = dbDatabase.ExecuteDataSet(dbCommand);
           
            }
            catch (Exception ex)
            {
            }
            return ds;
        }

       

        public bool CheckMobileAuthorizeUSer(string sessionID, int UID)
        {
            Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
            bool bnew = false;
            DbCommand dbCommand = null;
            DataSet ds = new DataSet();
            string spString = string.Empty;
            try
            {
                spString = "USP_CheckMobileAuthorizedUSer";
                dbCommand = dbDatabase.GetStoredProcCommand(spString);
                dbDatabase.AddInParameter(dbCommand, "@SessionID", DbType.String, sessionID);
                dbDatabase.AddInParameter(dbCommand, "@UID", DbType.Int32, UID);
                ds = dbDatabase.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        bnew = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return bnew;
        }


        public bool PerformLogoutMobile(string sessionID, int UID)
        {
            Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
            bool bnew = false;
            DbCommand dbCommand = null;
            DataSet ds = new DataSet();
            string spString = string.Empty;
            try
            {
                spString = "USP_PerformMobileLogout";
                dbCommand = dbDatabase.GetStoredProcCommand(spString);
                dbDatabase.AddInParameter(dbCommand, "@SessionID", DbType.String, sessionID);
                dbDatabase.AddInParameter(dbCommand, "@UID", DbType.Int32, UID);
                dbDatabase.ExecuteReader(dbCommand);    
                bnew=true; 
                
            }
            catch (Exception ex)
            {
            }
            return bnew;
        }

        public bool UpdateMobileCarStatusByCarID(int CarID, int UID, string AdStatusName)
        {
            Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
            bool bnew = false;
            DbCommand dbCommand = null;
            DataSet ds = new DataSet();
            string spString = string.Empty;
            try
            {
                spString = "USP_UpdateMobileCarStatusByCarID";
                dbCommand = dbDatabase.GetStoredProcCommand(spString);
                dbDatabase.AddInParameter(dbCommand, "@CarID", DbType.Int32, CarID);
                dbDatabase.AddInParameter(dbCommand, "@UID", DbType.Int32, UID);
                dbDatabase.AddInParameter(dbCommand, "@AdStatusName", DbType.String, AdStatusName);
                ds=dbDatabase.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        bnew = true;
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return bnew;
        }

        public DataSet MobileSaveUserLog(int UserID)
        {
            Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
            DbCommand dbCommand = null;
            DataSet ds = new DataSet();
            string spString = string.Empty;
            try
            {
                spString = "[USP_SaveMobileUserLog]";
                dbCommand = dbDatabase.GetStoredProcCommand(spString);
                dbDatabase.AddInParameter(dbCommand, "@User_Id", DbType.Int32, UserID);
                // dbDatabase.AddInParameter(dbCommand, "@Login_Ip", DbType.Int32, LoginIP);
                ds = dbDatabase.ExecuteDataSet(dbCommand);

            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        public IList<UsedCarsInfo> MopbileFindCarsByUID(string UID)
        {
            ///objUsedCars.Decalaring CarsInfo division object collection
            IList<UsedCarsInfo> UsedCarsIList = new List<UsedCarsInfo>();

            string spNameString = string.Empty;


            //objUsedCars.Setting Connection
            //objUsedCars.Global.INSTANCE_NAME = strCurrentConn;

            IDataReader UsedCarsDataReader = null;


            //objUsedCars.Connect to the database
            Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);

            //objUsedCars.Assign stored procedure name


            spNameString = "[USP_MobileFindCarsByUID]";

            DbCommand dbCommand = null;

            try
            {
                //objUsedCars.Set stored procedure to the command object
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);
                dbCommand.CommandTimeout = 10000;

                dbDatabase.AddInParameter(dbCommand, "@UID", DbType.String, UID);

                //objUsedCars.Executing stored procedure
                UsedCarsDataReader = dbDatabase.ExecuteReader(dbCommand);
                // DataSet  ds =dbDatabase.ExecuteDataSet(dbCommand);

                while (UsedCarsDataReader.Read())
                {
                    //  objUsedCars.Assign values to the CarsInfo object list
                    UsedCarsInfo ObjCarsInfo_Info = new UsedCarsInfo();
                    AssignCarsInfoList(UsedCarsDataReader, ObjCarsInfo_Info);
                    UsedCarsIList.Add(ObjCarsInfo_Info);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                UsedCarsDataReader.Close();
            }

            return UsedCarsIList;
        }

        private void AssignCarsInfoList(IDataReader UsedCarsDataReader, UsedCarsInfo objUsedCars)
        {
            try
            {

                objUsedCars.PostingID = Convert.ToInt32(UsedCarsDataReader["PostingID"].ToString());
                objUsedCars.Carid = Convert.ToInt32(UsedCarsDataReader["Carid"].ToString());
                objUsedCars.SellerType = UsedCarsDataReader["SellerType"].ToString();
                objUsedCars.SellerID = Convert.ToInt32(UsedCarsDataReader["SellerID"].ToString());
                objUsedCars.DateOfPosting = Convert.ToDateTime(UsedCarsDataReader["DateOfPosting"].ToString());
                objUsedCars.ExpirtyDate = UsedCarsDataReader["ExpirtyDate"].ToString() == "" ? System.DateTime.Now : Convert.ToDateTime(UsedCarsDataReader["ExpirtyDate"].ToString());
                objUsedCars.PackageID = UsedCarsDataReader["PackageID"].ToString() == "" ? 0 : Convert.ToInt32(UsedCarsDataReader["PackageID"].ToString());
                objUsedCars.PaymentID = UsedCarsDataReader["PaymentID"].ToString() == "" ? 0 : Convert.ToInt32(UsedCarsDataReader["PaymentID"].ToString());
                objUsedCars.IsActive = UsedCarsDataReader["IsActive"].ToString() == "" ? true : Convert.ToBoolean(UsedCarsDataReader["IsActive"].ToString());
                objUsedCars.InternalreviewID = UsedCarsDataReader["InternalreviewID"].ToString() == "" ? 0 : Convert.ToInt32(UsedCarsDataReader["InternalreviewID"].ToString());
                objUsedCars.CancelledBy = UsedCarsDataReader["CancelledBy"].ToString() == "" ? 0 : Convert.ToInt32(UsedCarsDataReader["CancelledBy"].ToString());
                objUsedCars.CancelledReason = UsedCarsDataReader["CancelledReason"].ToString();
                objUsedCars.CancelledDate = UsedCarsDataReader["CancelledDate"].ToString() == "" ? System.DateTime.Now : Convert.ToDateTime(UsedCarsDataReader["CancelledDate"].ToString());
                objUsedCars.Zipcode = UsedCarsDataReader["Zipcode"].ToString() == "" ? "Emp" : UsedCarsDataReader["Zipcode"].ToString();



                objUsedCars.Uid = UsedCarsDataReader["Uid"].ToString() == "" ? 0 : Convert.ToInt32(UsedCarsDataReader["Uid"].ToString());


                objUsedCars.SellerName = UsedCarsDataReader["SellerName"].ToString() == "" ? "Emp" : UsedCarsDataReader["SellerName"].ToString();

                objUsedCars.Address1 = UsedCarsDataReader["Address1"].ToString() == "" ? "Emp" : UsedCarsDataReader["Address1"].ToString();
                objUsedCars.Address2 = UsedCarsDataReader["Address2"].ToString() == "" ? "Emp" : UsedCarsDataReader["Address2"].ToString();
                objUsedCars.City = UsedCarsDataReader["City"].ToString() == "" ? "Emp" : UsedCarsDataReader["City"].ToString();
                objUsedCars.State = UsedCarsDataReader["State"].ToString();
                objUsedCars.Zip = UsedCarsDataReader["Zip"].ToString() == "" ? "Emp" : UsedCarsDataReader["Zip"].ToString();
                objUsedCars.Country = UsedCarsDataReader["Country"].ToString() == "1" ? "USA" : UsedCarsDataReader["Country"].ToString();
                objUsedCars.Phone = UsedCarsDataReader["Phone"].ToString();
                objUsedCars.AltPhone = UsedCarsDataReader["AltPhone"].ToString();
                objUsedCars.Fax = UsedCarsDataReader["Fax"].ToString();
                objUsedCars.Email = UsedCarsDataReader["Email"].ToString() == "" ? "Emp" : UsedCarsDataReader["Email"].ToString();
                objUsedCars.AltEmail = UsedCarsDataReader["AltEmail"].ToString() == "" ? "Emp" : UsedCarsDataReader["Email"].ToString();
                objUsedCars.NotesForBuyers = UsedCarsDataReader["NotesForBuyers"].ToString();
                objUsedCars.Model = UsedCarsDataReader["model"].ToString();
                objUsedCars.Make = UsedCarsDataReader["make"].ToString();
                objUsedCars.YearOfMake = UsedCarsDataReader["yearOfMake"].ToString() == "" ? 0 : Convert.ToInt32(UsedCarsDataReader["yearOfMake"].ToString());
                objUsedCars.Mileage = UsedCarsDataReader["mileage"].ToString() == "" ? "0" : UsedCarsDataReader["mileage"].ToString();
                objUsedCars.MakeID = UsedCarsDataReader["makeID"].ToString() == "" ? 0 : Convert.ToInt32(UsedCarsDataReader["makeID"].ToString());
                objUsedCars.MakeModelID = UsedCarsDataReader["makeModelID"].ToString() == "" ? 0 : Convert.ToInt32(UsedCarsDataReader["makeModelID"].ToString());
                objUsedCars.Price = UsedCarsDataReader["price"].ToString() == "" ? 0.00 : Convert.ToDouble(UsedCarsDataReader["price"].ToString());
                objUsedCars.Description = UsedCarsDataReader["description"].ToString() == "" ? "Emp" : UsedCarsDataReader["description"].ToString();

                objUsedCars.Bodytype = UsedCarsDataReader["bodytype"].ToString();

                objUsedCars.BodytypeID = Convert.ToInt32(UsedCarsDataReader["BodytypeID"].ToString() == "" ? "0" : UsedCarsDataReader["BodytypeID"].ToString());
                objUsedCars.FueltypeId = Convert.ToInt32(UsedCarsDataReader["FueltypeID"].ToString() == "" ? "0" : UsedCarsDataReader["FueltypeID"].ToString());
                objUsedCars.Fueltype = UsedCarsDataReader["Fueltype"].ToString();


                objUsedCars.ExteriorColor = UsedCarsDataReader["exteriorColor"].ToString() == "" ? "Emp" : UsedCarsDataReader["exteriorColor"].ToString();
                objUsedCars.NumberOfSeats = UsedCarsDataReader["numberOfSeats"].ToString() == "" ? "Emp" : UsedCarsDataReader["numberOfSeats"].ToString();
                objUsedCars.NumberOfDoors = UsedCarsDataReader["numberOfDoors"].ToString() == "" ? "Emp" : UsedCarsDataReader["numberOfDoors"].ToString();
                objUsedCars.NumberOfCylinder = UsedCarsDataReader["numberOfCylinder"].ToString() == "" ? "Emp" : UsedCarsDataReader["numberOfCylinder"].ToString();
                objUsedCars.Transmission = UsedCarsDataReader["Transmission"].ToString() == "" ? "Emp" : UsedCarsDataReader["Transmission"].ToString();
                objUsedCars.InteriorColor = UsedCarsDataReader["interiorColor"].ToString() == "" ? "Emp" : UsedCarsDataReader["interiorColor"].ToString();

                objUsedCars.VIN = UsedCarsDataReader["VIN"].ToString() == "" ? "Emp" : UsedCarsDataReader["VIN"].ToString();

                objUsedCars.ConditionDescription = UsedCarsDataReader["ConditionDescription"].ToString() == "" ? "Emp" : UsedCarsDataReader["ConditionDescription"].ToString();

                objUsedCars.DriveTrain = UsedCarsDataReader["DriveTrain"].ToString() == "" ? "Emp" : UsedCarsDataReader["DriveTrain"].ToString();

                objUsedCars.Title = UsedCarsDataReader["Title"].ToString() == "" ? "Emp" : UsedCarsDataReader["Title"].ToString();

                objUsedCars.CarUniqueID = UsedCarsDataReader["CarUniqueID"].ToString() == "" ? "Emp" : UsedCarsDataReader["CarUniqueID"].ToString();

                objUsedCars.AdStatus = UsedCarsDataReader["AdStatusName"].ToString() == "" ? "Emp" : UsedCarsDataReader["AdStatusName"].ToString();


                objUsedCars.PIC0 = UsedCarsDataReader["PIC0"].ToString() == "" ? GetStockURL(objUsedCars.Make, objUsedCars.Model) : UsedCarsDataReader["PIC0"].ToString();

                objUsedCars.PIC1 = UsedCarsDataReader["PIC1"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC1"].ToString();
                objUsedCars.PIC2 = UsedCarsDataReader["PIC2"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC2"].ToString();
                objUsedCars.PIC3 = UsedCarsDataReader["PIC3"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC3"].ToString();
                objUsedCars.PIC4 = UsedCarsDataReader["PIC4"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC4"].ToString();
                objUsedCars.PIC5 = UsedCarsDataReader["PIC5"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC5"].ToString();
                objUsedCars.PIC6 = UsedCarsDataReader["PIC6"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC6"].ToString();
                objUsedCars.PIC7 = UsedCarsDataReader["PIC7"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC7"].ToString();
                objUsedCars.PIC8 = UsedCarsDataReader["PIC8"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC8"].ToString();
                objUsedCars.PIC9 = UsedCarsDataReader["PIC9"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC9"].ToString();
                objUsedCars.PIC10 = UsedCarsDataReader["PIC10"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC10"].ToString();
                objUsedCars.PICLOC0 = UsedCarsDataReader["PICLOC0"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC0"].ToString();
                objUsedCars.PICLOC1 = UsedCarsDataReader["PICLOC1"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC1"].ToString();
                objUsedCars.PICLOC2 = UsedCarsDataReader["PICLOC2"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC2"].ToString();
                objUsedCars.PICLOC3 = UsedCarsDataReader["PICLOC3"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC3"].ToString();
                objUsedCars.PICLOC4 = UsedCarsDataReader["PICLOC4"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC4"].ToString();
                objUsedCars.PICLOC5 = UsedCarsDataReader["PICLOC5"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC5"].ToString();
                objUsedCars.PICLOC6 = UsedCarsDataReader["PICLOC6"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC6"].ToString();
                objUsedCars.PICLOC7 = UsedCarsDataReader["PICLOC7"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC7"].ToString();
                objUsedCars.PICLOC8 = UsedCarsDataReader["PICLOC8"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC8"].ToString();
                objUsedCars.PICLOC9 = UsedCarsDataReader["PICLOC9"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC9"].ToString();
                objUsedCars.PICLOC10 = UsedCarsDataReader["PICLOC10"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC10"].ToString();


                objUsedCars.PIC11 = UsedCarsDataReader["PIC11"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC11"].ToString();
                objUsedCars.PIC12 = UsedCarsDataReader["PIC12"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC12"].ToString();
                objUsedCars.PIC13 = UsedCarsDataReader["PIC13"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC13"].ToString();
                objUsedCars.PIC14 = UsedCarsDataReader["PIC14"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC14"].ToString();
                objUsedCars.PIC15 = UsedCarsDataReader["PIC15"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC15"].ToString();
                objUsedCars.PIC16 = UsedCarsDataReader["PIC16"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC16"].ToString();
                objUsedCars.PIC17 = UsedCarsDataReader["PIC17"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC17"].ToString();
                objUsedCars.PIC18 = UsedCarsDataReader["PIC18"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC18"].ToString();
                objUsedCars.PIC19 = UsedCarsDataReader["PIC19"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC19"].ToString();
                objUsedCars.PIC20 = UsedCarsDataReader["PIC20"].ToString() == "" ? "Emp" : UsedCarsDataReader["PIC20"].ToString();


                objUsedCars.PICLOC11 = UsedCarsDataReader["PICLOC11"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC11"].ToString();
                objUsedCars.PICLOC12 = UsedCarsDataReader["PICLOC12"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC12"].ToString();
                objUsedCars.PICLOC13 = UsedCarsDataReader["PICLOC13"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC13"].ToString();
                objUsedCars.PICLOC14 = UsedCarsDataReader["PICLOC14"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC14"].ToString();
                objUsedCars.PICLOC15 = UsedCarsDataReader["PICLOC15"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC15"].ToString();
                objUsedCars.PICLOC16 = UsedCarsDataReader["PICLOC16"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC16"].ToString();
                objUsedCars.PICLOC17 = UsedCarsDataReader["PICLOC17"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC17"].ToString();
                objUsedCars.PICLOC18 = UsedCarsDataReader["PICLOC18"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC18"].ToString();
                objUsedCars.PICLOC19 = UsedCarsDataReader["PICLOC19"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC19"].ToString();
                objUsedCars.PICLOC20 = UsedCarsDataReader["PICLOC20"].ToString() == "" ? "Emp" : UsedCarsDataReader["PICLOC20"].ToString();


                objUsedCars.RowNumber = UsedCarsDataReader["RowNumber"].ToString() == "" ? "Emp" : UsedCarsDataReader["RowNumber"].ToString();
                objUsedCars.TotalRecords = UsedCarsDataReader["TotalRecords"].ToString() == "" ? "Emp" : UsedCarsDataReader["TotalRecords"].ToString();
                objUsedCars.PageCount = UsedCarsDataReader["PageCount"].ToString() == "" ? "Emp" : UsedCarsDataReader["PageCount"].ToString();

                objUsedCars.UserFeedback = UsedCarsDataReader["UserFeedback"].ToString();






                //objUsedCars.pic0 = UsedCarsDataReader["pic0"].ToString();


            }
            catch (Exception ex)
            {
               

            }
        }

        private string GetStockURL(string Make, string Model)
        {
            string StockUrl = string.Empty;

            {
                string StockMake = Make;
                StockMake = StockMake.Replace(" ", "-");
                StockMake = StockMake.Replace("/", "@");
                StockMake = StockMake.Replace("&", "@");
                string StockType = Model.ToString().Replace('&', '@');
                StockType = StockType.Replace("/", "@");
                StockType = StockType.Replace(" ", "-");
                StockUrl = "images/MakeModelThumbs/" + StockMake + "_" + StockType + ".jpg";
            }

            return StockUrl;
        }



        private void AssignMakesInfoList(IDataReader MakesInfoDataReader, MakeCountInfo objMakesInfo)
        {
            try
            {
                objMakesInfo.AASuccess = "Success";
                objMakesInfo.MakeID = int.Parse(MakesInfoDataReader["makeID"].ToString());
                objMakesInfo.Make = Convert.ToString(MakesInfoDataReader["make"]);
                objMakesInfo.MakeCount = int.Parse(MakesInfoDataReader["makeCount"].ToString());
            }
            catch (Exception ex)
            {
               
            }
        }

       
       public IList<MultisiteInfo> GetMultiSitePostingsByCariD(int carID)
        {
            //Decalaring MakesInfo division object collection
            IList<MultisiteInfo> MakesInfoIList = new List<MultisiteInfo>();

            string spNameString = string.Empty;


            //Setting Connection
            //Global.INSTANCE_NAME = strCurrentConn;

            IDataReader MakesInfoDataReader = null;


            //Connect to the database
            Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);

            //Assign stored procedure name

            spNameString = "USP_GetMultiSitePostingURLsByCarID";
            DbCommand dbCommand = null;

            try
            {
                //Set stored procedure to the command object
                dbCommand = dbDatabase.GetStoredProcCommand(spNameString);

                dbDatabase.AddInParameter(dbCommand, "@carid", DbType.Int64, carID);

                //Executing stored procedure
                MakesInfoDataReader = dbDatabase.ExecuteReader(dbCommand);

                while (MakesInfoDataReader.Read())
                {
                    //Assign values to the MakesInfo object list
                    MultisiteInfo ObjMakesInfo_Info = new MultisiteInfo();
                    AssignMultiSiteInfoList(MakesInfoDataReader, ObjMakesInfo_Info);
                    MakesInfoIList.Add(ObjMakesInfo_Info);
                }
            }
            catch (Exception ex)
            {
              
            }
            finally
            {
                MakesInfoDataReader.Close();
            }

            return MakesInfoIList;
        }

       private void AssignMultiSiteInfoList(IDataReader MakesInfoDataReader, MultisiteInfo ObjMakesInfo_Info)
       {
           try
           {
               ObjMakesInfo_Info.AASuccess = "Success";
               ObjMakesInfo_Info.MultiSiteName = MakesInfoDataReader["SiteName"].ToString();
               ObjMakesInfo_Info.MultisitesURL = MakesInfoDataReader["URL"].ToString();
               ObjMakesInfo_Info.PostedDate = MakesInfoDataReader["UrlPostDate"].ToString();
               ObjMakesInfo_Info.ValidDays = MakesInfoDataReader["ValidDays"].ToString();

           }
           catch (Exception ex)
           {
           }
       }

       public DataSet MobileChkPackageForAddCar(int UID)
       {
           try
           {
               DataSet dsCars = new DataSet();
               string spNameString = string.Empty;
               Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
               spNameString = "USP_MobileChkPackageForAddCar";
               DbCommand dbCommand = null;
               dbCommand = dbDatabase.GetStoredProcCommand(spNameString);
               dbDatabase.AddInParameter(dbCommand, "@UID", System.Data.DbType.Int32, UID);
               dsCars = dbDatabase.ExecuteDataSet(dbCommand);
               return dsCars;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }



       public string SaveAddPackageRequest(string UID, string CustomerID, string AuthenticationID)
       {
           Database dbDatabase = DatabaseFactory.CreateDatabase(Global.INSTANCE_NAME);
           string bsucess = "Failure";
           DbCommand dbCommand = null;
           DataSet ds = new DataSet();
           string spString = string.Empty;
           try
           {
               spString = "USP_AddPackageMobileRequest";
               dbCommand = dbDatabase.GetStoredProcCommand(spString);
               dbDatabase.AddInParameter(dbCommand, "@UID", DbType.Int32, UID);
               dbDatabase.AddInParameter(dbCommand, "@CustomerID", DbType.String, CustomerID);
               dbDatabase.AddInParameter(dbCommand, "@AuthenticationID", DbType.String, AuthenticationID);
               ds=dbDatabase.ExecuteDataSet(dbCommand);

               if (ds.Tables[0].Rows.Count > 0)
               {
                   string CustomerName = ds.Tables[0].Rows[0]["Name"].ToString();
                   string Email = ds.Tables[0].Rows[0]["UserName"].ToString();
                   string phone = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();



                   string strMailFormat = string.Empty;

                   StringBuilder sbQuery;
                   string line;

                   string SalesMailFile = System.Web.Hosting.HostingEnvironment.MapPath("~/MailTemplate/AddPackageRequest.txt");

                   StreamReader objStreamReader;

                   objStreamReader = File.OpenText(SalesMailFile);

                   sbQuery = new StringBuilder();

                   while ((line = objStreamReader.ReadLine()) != null)
                   {
                       string strMail = string.Empty;

                       strMail = line + "<br />";

                       if (line.Contains("###Cusname###"))
                       {
                           strMail = line.Replace("###Cusname###", CustomerName) + "<br />";
                       }
                       else if (line.Contains("###Phone###"))
                       {
                           strMail = line.Replace("###Phone###", phone) + "<br />";
                       }
                       else if (line.Contains("###Email###"))
                       {
                           strMail = line.Replace("###Email###", Email) + "<br />";
                       }
                       strMailFormat = strMailFormat + strMail;
                   }
                   UtilityBL objUtility = new UtilityBL();
                   if (Email == "")
                   {
                       Email = "info@unitedcarexchange.com";
                   }
                   string ToEmail = "shobha@datumglobal.net";
                   objUtility.SendMail("127.0.0.1", Email, ToEmail, "Regarding add package request", strMailFormat);

               }
               bsucess = "Success";
              
           }
           catch (Exception ex)
           {
           }
           return bsucess;

       }
    }
}
