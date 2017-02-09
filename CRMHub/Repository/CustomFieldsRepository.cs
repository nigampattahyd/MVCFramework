using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Interface;
using CRMHub.EntityFramework;

namespace CRMHub.Repository
{
    public class CustomFieldsRepository : ICustomFieldsRepository
    {
        bool Result = false;
        public List<Custom_Manage_Master> GetAllCustomFields(long userId, string moduleId, int flag, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    List<CRM_GetCustomFields_Result> lstCustomFields = ClientEntity.CRM_GetCustomFields(userId, moduleId, flag).ToList();
                    if (lstCustomFields.Count > 0)
                    {
                        return lstCustomFields.Select(customfields => new Custom_Manage_Master
                        {
                            FieldId = customfields.FieldId,
                            Module = customfields.Module,
                            Column_Id = customfields.Column_Id,
                            ActualColumnName = customfields.ActualColumnName,
                            Column_Label = customfields.Column_Label,
                            Column_Type = customfields.Column_Type,
                            Field_Type = customfields.Field_Type,
                            Column_Description = customfields.Column_Description,
                            HoverText = customfields.HoverText,
                            InputDataType = customfields.InputDataType,
                            DefaultValue = customfields.DefaultValue,
                            DisplayPosition = customfields.DisplayPosition,
                            MaxLength = customfields.MaxLength,
                            RequiredField = customfields.RequiredField,
                            MultiValued = customfields.MultiValued,
                            ColumnDrpChkValues = customfields.ColumnDrpChkValues,
                            ModifiedDate = customfields.ModifiedDate,
                            CreatedDate = customfields.CreatedDate,
                            IsActive = customfields.IsActive,
                        }).ToList();
                    }
                    else
                    {
                        List<Custom_Manage_Master> objcustom = new List<Custom_Manage_Master>();
                        return objcustom;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddCustomFields(Custom_Manage_Master customfieldsobj, string Connectionstring, string dbName)
        {
            bool flag = false;
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var result = ClientEntity.CRM_AddEditCustomFields(customfieldsobj.FieldId, customfieldsobj.UserID, customfieldsobj.Module, customfieldsobj.Column_Id,
                        customfieldsobj.ActualColumnName, customfieldsobj.Column_Label, customfieldsobj.Column_Description, customfieldsobj.HoverText,
                        customfieldsobj.Column_Type, customfieldsobj.InputDataType, customfieldsobj.DefaultValue, customfieldsobj.MaxLength,
                        Convert.ToBoolean(customfieldsobj.RequiredField), Convert.ToBoolean(customfieldsobj.IsActive),1);
                    if (result > 0)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception)
            {
                flag = false;
                throw;
            }
            return flag;
        }
        public bool InsertCustomFields(Custom_Manage_Master customfieldsobj, string Connectionstring, string dbName, out int FieldId)
        {
            bool flag = false;
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var output = new System.Data.Entity.Core.Objects.ObjectParameter("NewFieldId", typeof(int));
                    var result = ClientEntity.CRM_AddEditCustomFieldsNew(customfieldsobj.FieldId, customfieldsobj.UserID, customfieldsobj.Module, customfieldsobj.Column_Id,
                        customfieldsobj.ActualColumnName, customfieldsobj.Column_Label, customfieldsobj.Column_Description, customfieldsobj.HoverText,
                        customfieldsobj.Column_Type, customfieldsobj.InputDataType, customfieldsobj.DefaultValue, customfieldsobj.MaxLength,
                        Convert.ToBoolean(customfieldsobj.RequiredField), Convert.ToBoolean(customfieldsobj.IsActive), 1, output);
                    FieldId = Convert.ToInt32(output.Value);
                    if (result > 0)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception)
            {
                flag = false;
                throw;
            }
            return flag;
        }
        
        public bool UpdateCustomFields(Custom_Manage_Master customfieldsobj, string Connectionstring, string dbName)
        {
            bool flag = false;
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var result = ClientEntity.CRM_AddEditCustomFields(customfieldsobj.FieldId, customfieldsobj.UserID, customfieldsobj.Module, customfieldsobj.Column_Id,
                        customfieldsobj.ActualColumnName,customfieldsobj.Column_Label, customfieldsobj.Column_Description, customfieldsobj.HoverText,
                        customfieldsobj.Column_Type, customfieldsobj.InputDataType, customfieldsobj.DefaultValue, customfieldsobj.MaxLength,
                        Convert.ToBoolean(customfieldsobj.RequiredField), Convert.ToBoolean(customfieldsobj.IsActive), 2);
                    if (result > 0)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception)
            {
                flag = false;
                throw;
            }
            return flag;
        }
        public bool ChangeCustomFields(Custom_Manage_Master customfieldsobj, string Connectionstring, string dbName)
        {
            bool flag = false;
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var output = new System.Data.Entity.Core.Objects.ObjectParameter("NewFieldId", typeof(int));
                    var result = ClientEntity.CRM_AddEditCustomFieldsNew(customfieldsobj.FieldId, customfieldsobj.UserID, customfieldsobj.Module, customfieldsobj.Column_Id,
                        customfieldsobj.ActualColumnName, customfieldsobj.Column_Label, customfieldsobj.Column_Description, customfieldsobj.HoverText,
                        customfieldsobj.Column_Type, customfieldsobj.InputDataType, customfieldsobj.DefaultValue, customfieldsobj.MaxLength,
                        Convert.ToBoolean(customfieldsobj.RequiredField), Convert.ToBoolean(customfieldsobj.IsActive), 2, output);
                    if (result > 0)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception)
            {
                flag = false;
                throw;
            }
            return flag;
        }

        public Custom_Manage_Master GetCustomFieldsbyFieldId(long UserId, long FieldId, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var lstCustomFieldsbyId = ClientEntity.CRM_GetCustomFieldsbyFieldId(FieldId).ToList();
                    var listofcustomfieldsbyId = lstCustomFieldsbyId.Select(customfieldsbyId => new Custom_Manage_Master
                    {
                        FieldId = customfieldsbyId.FieldId,
                        Module = customfieldsbyId.Module,
                        Column_Id = customfieldsbyId.Column_Id,
                        ActualColumnName = customfieldsbyId.ActualColumnName,
                        Column_Label = customfieldsbyId.Column_Label,
                        Column_Type = customfieldsbyId.Column_Type,
                        Field_Type = customfieldsbyId.Field_Type,
                        Column_Description = customfieldsbyId.Column_Description,
                        HoverText = customfieldsbyId.HoverText,
                        InputDataType = customfieldsbyId.InputDataType,
                        DefaultValue = customfieldsbyId.DefaultValue,
                        DisplayPosition = customfieldsbyId.DisplayPosition,
                        MaxLength = customfieldsbyId.MaxLength,
                        RequiredField = customfieldsbyId.RequiredField,
                        MultiValued = customfieldsbyId.MultiValued,
                        ColumnDrpChkValues = customfieldsbyId.ColumnDrpChkValues,
                        ModifiedDate = customfieldsbyId.ModifiedDate,
                        CreatedDate = customfieldsbyId.CreatedDate,
                        IsActive = customfieldsbyId.IsActive,
                    }).ToList();
                    return listofcustomfieldsbyId.SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Custom_Manage_Master> GetCustomFieldsByModule(string Module, string Connectionstring, string dbName)
        {
            try
            {
                var obj = new DevEntities();
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                bool Active = true;
                List<Custom_Manage_Master> CustomFieldsList = new List<Custom_Manage_Master>();
                //CustomFieldsList = obj.Custom_Manage_Master.Where(CMM => (CMM.IsActive == Active) && (CMM.Module == Module)).ToList();
                //return CustomFieldsList;
                //var values = obj.Custom_DrpChkValues.Where(CMM => (CMM.IsActive == Active) && (CMM.Module == Module)).ToList();

                var model = (from p in obj.Custom_Manage_Master.AsEnumerable()
                             where p.IsActive == Active && p.Module == Module
                             select new Custom_Manage_Master
                             {
                                 MaxLength=p.MaxLength,
                                 HoverText = p.HoverText,
                                 FieldId = p.FieldId,
                                 Column_Type = p.Column_Type,
                                 Column_Label = p.Column_Label,
                                 RequiredField = p.RequiredField,
                                 DefaultValue = p.DefaultValue,
                                 lstCustomOptions = obj.Custom_DrpChkValues.Where(x => x.FieldId == p.FieldId).ToList()
                             }).ToList();

                return model;
            }
            catch (Exception)
            {
                throw;
            }

        }
        //public List<Custom_Manage_Master> GetCustomFieldsByModule(string Module, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        var obj = new DevEntities();
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        bool Active = true;
        //        List<Custom_Manage_Master> CustomFieldsList = new List<Custom_Manage_Master>();
        //    CustomFieldsList = obj.Custom_Manage_Master.Where(CMM => (CMM.IsActive == Active) && (CMM.Module== Module)).ToList();
        //        return CustomFieldsList;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}


        public bool InsertCustomField(Custom_Value_Master CVMObj, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    ClientEntity.Custom_Value_Master.Add(CVMObj);
                    ClientEntity.SaveChanges();
                    return true;
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        
        public bool UpdateCustomField(Custom_Value_Master CVMObj, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    Custom_Value_Master obj = ClientEntity.Custom_Value_Master.Where(CVM => CVM.CValueId == CVMObj.CValueId).FirstOrDefault();
                    obj.CustomFieldvalue = CVMObj.CustomFieldvalue;
                    obj.UserID = CVMObj.UserID;
                    obj.ModifiedDate = CVMObj.ModifiedDate;
                   ClientEntity.SaveChanges(); 
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public Custom_Value_Master getCustomDetailsByContactId(int contactId, string Connectionstring, string dbName)
        //{
        //    DevEntities obj = new DevEntities(Connectionstring);
        //    obj.Database.Connection.Open();
        //    obj.Database.Connection.ChangeDatabase(dbName);
        //    //Lunch objLunch = obj.Lunch.Where(l => l.LunchId == LunchId).FirstOrDefault();
        //    //    return objLunch;
        //    Custom_Value_Master CVMObj = obj.Custom_Value_Master.Where(CVM => CVM.ModuleRecordId == contactId).FirstOrDefault();
        //    return CVMObj;
        //}

        public Custom_Value_Master getCustomDetailsByLeadId(long LeadId, string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);
            //Lunch objLunch = obj.Lunch.Where(l => l.LunchId == LunchId).FirstOrDefault();
            //    return objLunch;
            Custom_Value_Master CVMObj = obj.Custom_Value_Master.Where(CVM => CVM.ModuleRecordId == LeadId).FirstOrDefault();
            return CVMObj;
        }

        public Custom_Value_Master getCustomDetailsByContactId(int contactId, string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);           
            Custom_Value_Master CVMObj = obj.Custom_Value_Master.Where(CVM => CVM.ModuleRecordId == contactId).FirstOrDefault();
            return CVMObj;
        }


       public List<Custom_Value_Master> GetCustomValueBasedonModule(int RecordIdtemp,string ModuleName, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                var CVMList = (from CMM in obj.Custom_Manage_Master.Where(y => y.IsActive == true && y.Module == ModuleName).AsEnumerable()
                               join CVM in obj.Custom_Value_Master.Where(x => x.ModuleRecordIdTmp == RecordIdtemp && x.Module == ModuleName)                               
                               on CMM.FieldId equals CVM.MasterFieldId
                                into temp
                               from rt in temp.DefaultIfEmpty()
                               select new Custom_Value_Master
                               {
                                   MaxLength = CMM.MaxLength,
                                   DefaultValue = CMM.DefaultValue,
                                   RequiredField = CMM.RequiredField,
                                   HoverText = CMM.HoverText,
                                   FieldId = CMM.FieldId,
                                   CValueId = (rt == null ? 0 : rt.CValueId),
                                   MasterFieldId = (rt == null ? 0 : rt.MasterFieldId),
                                   Column_Label = CMM.Column_Label,
                                   CustomFieldvalue = (rt == null ? "" : rt.CustomFieldvalue),
                                   ModuleRecordIdTmp = (rt == null ? 0 : rt.ModuleRecordIdTmp),
                                   Module = (rt == null ? "" : rt.Module),
                                   Column_Type = CMM.Column_Type,
                                   lstCustomOptionsVal = obj.Custom_DrpChkValues.Where(x => x.FieldId == CMM.FieldId).ToList()

                               }).ToList();

                //var CVMList = (from CMM in obj.Custom_Manage_Master.Where(y => y.IsActive == true && y.Module == ModuleName).AsEnumerable()
                //               join CVM in obj.Custom_Value_Master.Where(x => x.ModuleRecordIdTmp == RecordIdtemp && x.Module == ModuleName)
                //               on CMM.FieldId equals CVM.MasterFieldId
                //                into temp
                //               from rt in temp.DefaultIfEmpty()
                //               select new Custom_Value_Master
                //               {
                //                   MaxLength=CMM.MaxLength,
                //                   RequiredField=CMM.RequiredField,
                //                   HoverText=CMM.HoverText,
                //                   FieldId = CMM.FieldId,
                //                   CValueId = (rt == null ? 0 : rt.CValueId),
                //                   MasterFieldId = (rt == null ? 0 : rt.MasterFieldId),
                //                   Column_Label = CMM.Column_Label,
                //                   CustomFieldvalue = (rt == null ? "" : rt.CustomFieldvalue),
                //                   ModuleRecordIdTmp = (rt == null ? 0 : rt.ModuleRecordIdTmp),
                //                   Module = (rt == null ? "" : rt.Module),
                //                   Column_Type = CMM.Column_Type

                //               }).ToList();


               
                return CVMList;
            }
           catch(Exception ex)
            {
                throw ex;
            }

        }


        public List<Custom_DrpChkValues> GetDrpOption(int Fieldid,string Connectionstring, string dbName )
       {
           DevEntities obj = new DevEntities(Connectionstring);
           obj.Database.Connection.Open();
           obj.Database.Connection.ChangeDatabase(dbName);
           List<Custom_DrpChkValues> LstDrpChkValues = obj.Custom_DrpChkValues.Where(DCV => DCV.FieldId == Fieldid).ToList();
           return LstDrpChkValues;

       }

        public int AddCustomOptions(Custom_DrpChkValues customDrpChkValuesobj, string Connectionstring, string dbName)
        {

            try
            {



                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                if (customDrpChkValuesobj.IsDefault == true)
                {
                    obj.Custom_DrpChkValues.Where(d => d.FieldId == customDrpChkValuesobj.FieldId).ToList().ForEach(l => l.IsDefault = false);
                    obj.SaveChanges();
                }
                obj.Custom_DrpChkValues.Add(customDrpChkValuesobj);
                obj.SaveChanges();
                return Convert.ToInt32(customDrpChkValuesobj.DrpValueId);
            }
            catch (Exception)
            {

                throw;
            }

        }
       

        //public bool AddCustomOptions(Custom_DrpChkValues customDrpChkValuesobj, string Connectionstring, string dbName)
        //{
           
        //    try
        //    {

               
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        obj.Custom_DrpChkValues.Add(customDrpChkValuesobj);
        //        obj.SaveChanges();
        //        return true;
        //        //
        //    }
        //    catch (Exception)
        //    {
               
        //        throw;
        //    }
           
        //}

        public List<Custom_DrpChkValues> GetAllCustomOptions(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                //List<Custom_DrpChkValues> LstOptionvals = obj.Custom_DrpChkValues.ToList();
                List<Custom_DrpChkValues> LstOptionvals = obj.Custom_DrpChkValues.ToList();
                return LstOptionvals;

            }
            catch (Exception)
            {
                throw;
            }
        }






        // To Update the Custom Manage master To store Radiobutton Value into Default Value with Field Id
        public bool UpdateCustomManagemaster(int FieldId, int DefaultValue, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    Custom_Manage_Master ObjManagemaster = ClientEntity.Custom_Manage_Master.Where(CMM => CMM.FieldId == FieldId).SingleOrDefault();

                    ObjManagemaster.DefaultValue = DefaultValue.ToString();
                    ClientEntity.SaveChanges();
                    if (ObjManagemaster.Column_Type == "radiobutton")
                    {
                        ClientEntity.Custom_DrpChkValues.Where(f => f.FieldId == FieldId && f.DrpValueId != DefaultValue).ToList().ForEach(t => t.IsDefault = false);
                        ClientEntity.SaveChanges();
                    }


                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public bool DeleteCustomOptions(int OptionId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                Custom_DrpChkValues ObjCusDrpVal = obj.Custom_DrpChkValues.Where(Drp => Drp.DrpValueId == OptionId).SingleOrDefault();
                if (ObjCusDrpVal != null)
                {
                    obj.Custom_DrpChkValues.Remove(ObjCusDrpVal);
                    obj.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return Result;
            }
        }


        public int DeleteCustomfieldsDetailsByFieldId(int Fieldid, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                return obj.CRM_DeleteCustomfields(Fieldid);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Custom_Manage_Master getManageMasterdetails(string Column_Label, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                // var cmp = Convert.ToInt32(Column_Label);
                Custom_Manage_Master Custom_Manage_Master = new Custom_Manage_Master();
                Custom_Manage_Master = obj.Custom_Manage_Master.Where(c => c.Column_Label == Column_Label).FirstOrDefault();
                return Custom_Manage_Master;

                //if (Custom_Manage_Master != null)
                //    return Custom_Manage_Master.FieldId.ToString();
                //else
                //    return "0";
            }
            catch (Exception)
            {
                throw;
            }
        }



        public string getManageMasterAccnt_Id(string Column_Label, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                // var cmp = Convert.ToInt32(Column_Label);
                Custom_Manage_Master Custom_Manage_Master = new Custom_Manage_Master();
                Custom_Manage_Master = obj.Custom_Manage_Master.Where(c => c.Column_Label == Column_Label).FirstOrDefault();
                if (Custom_Manage_Master!= null)
                return Custom_Manage_Master.FieldId.ToString();
                else
                return "0";
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool Insertcustomvalue(Custom_Value_Master CVMObj, string Connectionstring, string dbName)
        {
            using (DevEntities ClientEntity = new DevEntities(Connectionstring))
            {
                ClientEntity.Database.Connection.Open();
                ClientEntity.Database.Connection.ChangeDatabase(dbName);
                ClientEntity.Custom_Value_Master.Add(CVMObj);
                ClientEntity.SaveChanges();
                return true;
            }
        }

        
        //public bool CheckIfmasterlabelExists(string mastercolumnlabel, string Connectionstring, string dbName)
        //{
        //    var Result = false;
        //    try
        //    {
        //   DevEntities obj = new DevEntities();           
             
        //       obj.Database.Connection.Open();
        //       obj.Database.Connection.ChangeDatabase(dbName);
        //       var res = obj.CRM_IscolumnlabelExists(mastercolumnlabel);              
        //         if (res.Count() !=0)
        //         {
        //             Result = true;
        //         }
        //         return Result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}


        //public bool CheckIfmasterlabelExists(string mastercolumnlabel, string Connectionstring, string dbName)
        //{
        //    var Result = false;
        //    try
        //    {
        //        DevEntities obj = new DevEntities();

        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var res = obj.CRM_IscolumnlabelExists(mastercolumnlabel).FirstOrDefault();
        //        if (res.Count() != 0)
        //        {
        //            Result = true;
        //        }
        //        return Result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        public bool CheckIfmasterlabelExists(string mastercolumnlabel, string Connectionstring, string dbName)
        {
            var Result = false;
            try
            {
                DevEntities obj = new DevEntities();

                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var res = obj.CRM_IscolumnlabelExists(mastercolumnlabel);
                if (res.Count() != 0)
                {
                    Result = true;
                }
                return Result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool CheckColumnLabelExistsOrNot(string columnlabel,string modulename, string Connectionstring, string dbName )
        {
            var flag = false;
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var result = obj.CRM_IscolumnlabelExistsForModule(columnlabel, modulename);
                if (result.Count() != 0)
                {
                    flag = true;
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string getOptionId(string OptValue, int FieldId,string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                // var cmp = Convert.ToInt32(Column_Label);

                Custom_DrpChkValues customOptionsValues = new Custom_DrpChkValues();
                customOptionsValues = obj.Custom_DrpChkValues.Where(o => o.DrpValue == OptValue && o.FieldId == FieldId).FirstOrDefault();
                if (customOptionsValues != null)
                    return customOptionsValues.DrpValueId.ToString();
                else
                    return "0";               
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
