using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Repository;
using CRMHub.EntityFramework;

namespace CRMHub.Interface
{
    public interface ICustomFieldsRepository
    {
        List<Custom_Manage_Master> GetAllCustomFields(long userId, string moduleId, int flag, string Connectionstring, string dbName);
        bool AddCustomFields(Custom_Manage_Master customfieldsobj, string Connectionstring, string dbName);
        bool UpdateCustomFields(Custom_Manage_Master customfieldsobj, string Connectionstring, string dbName);
        bool InsertCustomFields(Custom_Manage_Master customfieldsobj, string Connectionstring, string dbName, out int FieldId);
        bool ChangeCustomFields(Custom_Manage_Master customfieldsobj, string Connectionstring, string dbName);
        Custom_Manage_Master GetCustomFieldsbyFieldId(long UserId, long FieldId, string Connectionstring, string dbName);

        List<Custom_Manage_Master> GetCustomFieldsByModule(string Module, string Connectionstring, string dbName);        

        bool InsertCustomField(Custom_Value_Master CVMObj, string Connectionstring, string dbName);

        Custom_Value_Master getCustomDetailsByLeadId(long LeadId, string Connectionstring, string dbName);
        Custom_Value_Master getCustomDetailsByContactId(int contactId, string Connectionstring, string dbName);

      List<Custom_Value_Master> GetCustomValueBasedonModule(int RecordIdtemp,string ModuleName, string Connectionstring, string dbName);

      bool UpdateCustomField(Custom_Value_Master CVMObj, string Connectionstring, string dbName);
      List<Custom_DrpChkValues> GetDrpOption(int Fieldid, string Connectionstring, string dbName);


      int AddCustomOptions(Custom_DrpChkValues customDrpChkValuesobj, string Connectionstring, string dbName);
      //bool AddCustomOptions(Custom_DrpChkValues customDrpChkValuesobj, string Connectionstring, string dbName);
      //Custom_DrpChkValues GetDrpOption(int Fieldid, string Connectionstring, string dbName);

      List<Custom_DrpChkValues> GetAllCustomOptions(string Connectionstring, string dbName);

      bool UpdateCustomManagemaster(int FiledId, int DefaultValue, string Connectionstring, string dbName);

      bool DeleteCustomOptions(int OptionId, string Connectionstring, string dbName);
      int DeleteCustomfieldsDetailsByFieldId(int Fieldid, string Connectionstring, string dbName);

      string getManageMasterAccnt_Id(string Column_Label, string Connectionstring, string dbName);
      bool Insertcustomvalue(Custom_Value_Master CVMObj, string Connectionstring, string dbName);
      bool CheckIfmasterlabelExists(string mastercolumnlabel, string Connectionstring, string dbName);
     // bool CheckColumnLabelExistsOrNot(string columnlabel, string Connectionstring, string dbName);
      bool CheckColumnLabelExistsOrNot(string columnlabel, string modulename, string Connectionstring, string dbName);

      Custom_Manage_Master getManageMasterdetails(string Column_Label, string Connectionstring, string dbName);
      string getOptionId(string OptValue, int FieldId, string Connectionstring, string dbName);
    }
}
