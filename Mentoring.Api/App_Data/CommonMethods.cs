using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Mentoring.Api
{
    public static class EncryptDecrypt
    {
        const string DEFAULTKEY = "1020305060708090";

        private static byte[] GetMD5Hash(string strKey)
        {
            MD5CryptoServiceProvider objHashMD5 = null;
            byte[] objPwdhash = null;
            try
            {
                objHashMD5 = new MD5CryptoServiceProvider();
                objPwdhash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strKey));
            }
            catch
            {
                //   throw new Exception("MD5_ERROR Crypto.cs::GetMD5Hash() unable to generate MD5 hash.");
            }
            finally
            {
                if (objHashMD5 != null)
                {
                    objHashMD5.Clear();
                }
            }
            return objPwdhash;
        }

        public static string EncryptWithKey(string strDecryptedString, string strKey)
        {
            string strEncrypted = strDecryptedString;
            TripleDESCryptoServiceProvider objDES = null;
            byte[] objBuff;

            if (strDecryptedString.Length == 0)
                return string.Empty;

            try
            {
            GerenateEncryptesString:

                objDES = new TripleDESCryptoServiceProvider();
                if (strKey.Length > 0)
                {
                    objDES.Key = GetMD5Hash(strKey);
                }
                objDES.Mode = CipherMode.ECB;
                objBuff = ASCIIEncoding.ASCII.GetBytes(strDecryptedString);
                strEncrypted = Convert.ToBase64String(objDES.CreateEncryptor().TransformFinalBlock(objBuff, 0, objBuff.Length));


                //if (strEncrypted.Contains("+"))
                //    goto GerenateEncryptesString; 
            }

            catch
            {
                return "CRYPTO_ERROR : Error encrypting string";
            }
            finally
            {
                if (objDES == null)
                {
                    objDES.Clear();
                }
            }
            return strEncrypted;
        }

        public static string DecryptWithKey(string strEncryptedString, string strKey)
        {
            string strDecrypted = strEncryptedString;
            TripleDESCryptoServiceProvider objDES = null;
            byte[] objBuff;
            if (strEncryptedString.Length == 0)
                return string.Empty;

            try
            {
                objDES = new TripleDESCryptoServiceProvider();
                if (strKey.Length > 0)
                    objDES.Key = GetMD5Hash(strKey);

                objDES.Mode = CipherMode.ECB;

                objBuff = Convert.FromBase64String(strEncryptedString);

                strDecrypted = ASCIIEncoding.ASCII.GetString(objDES.CreateDecryptor().TransformFinalBlock(objBuff, 0, objBuff.Length));

            }
            catch
            {
                return "CRYPTO_ERROR : Error encrypting string";
            }
            finally
            {
                if (objDES == null)
                    objDES.Clear();
            }
            return strDecrypted;
        }

        public static string Encrypt(string strDecryptedString)
        {
            return EncryptWithKey(strDecryptedString, DEFAULTKEY);
        }

        public static string Decrypt(string strEncryptedString)
        {
            return DecryptWithKey(strEncryptedString, DEFAULTKEY);
        }
    }

    public static class DTTLcontact
    {
        public static List<TSource> ToList<TSource>(this DataTable dataTable) where TSource : new()
        {
            var dataList = new List<TSource>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var objFieldNames = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                                 select new
                                 {

                                     Name = aProp.Name,
                                     // Type = Nullable.GetUnderlyingType(aProp.PropertyType) ??
                                     // aProp.PropertyType


                                 }).ToList();
            var dataTblFieldNames = (from DataColumn aHeader in dataTable.Columns
                                     select new
                                     {
                                         Name = aHeader.ColumnName,

                                         //Type = aHeader.DataType

                                     }).ToList();


            var commonFields = objFieldNames.Intersect(dataTblFieldNames).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var aTSource = new TSource();
                foreach (var aField in commonFields)
                {
                    PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
                    var value = (dataRow[aField.Name] == DBNull.Value) ?
                    null : dataRow[aField.Name]; //if database field is nullable
                    //propertyInfos.SetValue(aTSource, value, null);
                    // pavan code
                    propertyInfos.SetValue(aTSource, value, null);
                }
                dataList.Add(aTSource);
            }
            return dataList;
        }
    }

    //changes  mades for datatabletolist
    public static class DTTL
    {
        public static List<TSource> ToList<TSource>(this DataTable dataTable) where TSource : new()
        {
            var dataList = new List<TSource>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var objFieldNames = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                                 select new
                                 {

                                     Name = aProp.Name,
                                     // Type = Nullable.GetUnderlyingType(aProp.PropertyType) ??
                                     // aProp.PropertyType


                                 }).ToList();
            var dataTblFieldNames = (from DataColumn aHeader in dataTable.Columns
                                     select new
                                     {
                                         Name = aHeader.ColumnName,

                                         //Type = aHeader.DataType

                                     }).ToList();

            //var dataTblFieldNameswithType =     (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
            //                     select new
            //                     {

            //                         Name = aProp.Name,
            //                        Type = Nullable.GetUnderlyingType(aProp.PropertyType) ??
            //                 aProp.PropertyType


            //                     }).ToList();


            var commonFields = objFieldNames.Intersect(dataTblFieldNames).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var aTSource = new TSource();
                foreach (var aField in commonFields)
                {
                    PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
                    var value = (dataRow[aField.Name] == DBNull.Value) ?
                    null : dataRow[aField.Name]; //if database field is nullable
                    //var datatype = dataTblFieldNameswithType.Where(l => l.Name == aField.Name).Select(x => x.Type).FirstOrDefault();
                    //if (value != null && datatype.Name == "String")
                    //{
                    //    propertyInfos.SetValue(aTSource, value.ToString(), null);
                    //}
                    //else
                    propertyInfos.SetValue(aTSource, value == null ? null : value.ToString(), null);
                }
                dataList.Add(aTSource);
            }
            return dataList;
        }
    }

    public static class LTDT
    {
        public static DataTable ToDataTable<TSource>(this IList<TSource> data)
        {
            DataTable dataTable = new DataTable(typeof(TSource).Name);
            PropertyInfo[] props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                    prop.PropertyType);
            }

            foreach (TSource item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }



}