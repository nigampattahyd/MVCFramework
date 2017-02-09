using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using CRMHub.Interface;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace CRMHub.Repository
{
    public class ItemRepository : IItemRepository
    {
        
      public int createitem(tbl_Items objitems, string Connectionstring, string dbName)
      {
          try
          {
              int result = 0;
              DevEntities obj = new DevEntities(Connectionstring);
              obj.Database.Connection.Open();
              obj.Database.Connection.ChangeDatabase(dbName);
              result = obj.CRM_AddItem(objitems.ItemTypeID, objitems.MeasureID, objitems.ItemLocID, objitems.ItemCode, objitems.ItemName, objitems.Description,
                  objitems.PrimaryVendorID, objitems.AlternateVendorID, objitems.AccountNO, objitems.RateperUnit, objitems.Saleprice, objitems.MinimumQty, objitems.ReorderQty,
                  objitems.QtyonHand, objitems.ItemLocation, objitems.ItemUnits, objitems.Warranty, objitems.Active, objitems.CreatedDate, objitems.modifiedDate, objitems.updatedby, objitems.PartNumber,
                  objitems.CompanyID, objitems.IsDeleted, objitems.NonStock, objitems.QtyCommitted, objitems.QtyOnOrder, objitems.Checked, objitems.ManufacturerID);
             // result = obj.CRM_AddItem(objitems.ItemCode, objitems.ItemTypeID, objitems.ItemName, objitems.RateperUnit, objitems.Saleprice, objitems.Description,objitems.Active);
              return result;
          }
          catch (Exception)
          {
              throw;
          }
      }

      public List<tbl_Items> GetAllItemsList(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
      {
          try
          {
              DevEntities obj = new DevEntities(Connectionstring);
              obj.Database.Connection.Open();
              obj.Database.Connection.ChangeDatabase(dbName);
              var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
              List<tbl_Items> ListItems = obj.tbl_Items.ToList(); 
              TotalCount = ListItems.Count;
              return ListItems;
          }
          catch (Exception)
          {
              throw;
          }
      }

      public tbl_Items edititemdetailsbyitemid(int ItemID, string Connectionstring, string dbName)
      {
          try
          {
              DevEntities obj = new DevEntities(Connectionstring);
              obj.Database.Connection.Open();
              obj.Database.Connection.ChangeDatabase(dbName);

              //tbl_Items ObjItemsDate = new tbl_Items();
              //ObjItemsDate = obj.tbl_Items.Where(Ti => Ti.ItemID == ItemID).FirstOrDefault();

              var result = obj.CRM_GETItemByItemID(ItemID).Select(itemdetails => new tbl_Items
              {
                  ItemTypeID = itemdetails.ItemTypeID,
                  ItemID = ItemID,
                  ItemCode=itemdetails.ItemCode,
                  ItemName=itemdetails.ItemName,
                  RateperUnit=itemdetails.RateperUnit,
                  Saleprice=itemdetails.Saleprice,
                  Description=itemdetails.Description,
                  Active=itemdetails.Active
              }).SingleOrDefault();           
              return result;
          }
          catch (Exception)
          {
              throw;
          }
      }

      public int UpdateitemDetailsByitemId( tbl_Items objitems, string Connectionstring, string dbName)
      {
          try
          {
              DevEntities obj = new DevEntities(Connectionstring);
              obj.Database.Connection.Open();
              obj.Database.Connection.ChangeDatabase(dbName);
              //objproj.DateCreated = DateTime.Now;
              var result = obj.CRM_UpdateItemDetailsByItemId(objitems.ItemID, objitems.ItemTypeID, objitems.MeasureID, objitems.ItemLocID, objitems.ItemCode, objitems.ItemName, objitems.Description,
                  objitems.PrimaryVendorID, objitems.AlternateVendorID, objitems.AccountNO, objitems.RateperUnit, objitems.Saleprice, objitems.MinimumQty, objitems.ReorderQty,
                  objitems.QtyonHand, objitems.ItemLocation, objitems.ItemUnits, objitems.Warranty, objitems.Active, objitems.CreatedDate, objitems.modifiedDate, objitems.updatedby, objitems.PartNumber,
                  objitems.CompanyID, objitems.IsDeleted, objitems.NonStock, objitems.QtyCommitted, objitems.QtyOnOrder, objitems.Checked, objitems.ManufacturerID);
              return result;
          }
          catch (Exception)
          {
              throw;
          }
      }

      public int DeleteItemByItemId(int ItemID, string Connectionstring, string dbName)
      {
          try
          {
              DevEntities obj = new DevEntities(Connectionstring);
              obj.Database.Connection.Open();
              obj.Database.Connection.ChangeDatabase(dbName);
              return obj.CRM_deleteItemByItemId(ItemID);
          }
          catch (Exception)
          {
              throw;
          }

      }
    }


    }

