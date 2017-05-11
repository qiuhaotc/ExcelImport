using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebMain.Common
{
    public class ConfigOperater
    {
        #region 获取模型 GetConfigModel()
        public static ConfigModel GetConfigModel()
        {
            string fileStr = ReadAllText("Config.json");

            ConfigModel configModel;

            try
            {
                configModel = JsonHelper.ToObj<ConfigModel>(fileStr);
                if (configModel == null)
                {
                    configModel = new ConfigModel();
                    configModel.ListField = new List<FieldModel>();
                }
            }
            catch (Exception)
            {
                configModel = new ConfigModel();
                configModel.ListField = new List<FieldModel>();
            }

            return configModel;
        } 
        #endregion

        #region 获取模型 GetFieldModel()
        public static FieldModel GetFieldModel(string sqlField)
        {
            ConfigModel config = GetConfigModel();
            return config.ListField.Find(u => u.SQLField == sqlField);
        }
        #endregion

        #region 获取模型 AddOrUpdateField()
        public static ConfigModel AddOrUpdateField(ConfigModel configModel, FieldModel fieldModel)
        {
            FieldModel model = configModel.ListField.Find(u => u.SQLField == fieldModel.SQLField);
            if (model != null)
            {
                model.ExcelColomn = fieldModel.ExcelColomn;
                model.ExcelFieldType = fieldModel.ExcelFieldType;
                model.FieldName = fieldModel.FieldName;
                model.ForeignKey = fieldModel.ForeignKey;
                model.ForeignTable = fieldModel.ForeignTable;
                model.IsForeignKey = fieldModel.IsForeignKey;
                model.LinkField = fieldModel.LinkField;
                model.SQLField = fieldModel.SQLField;
                model.SQLFieldType = fieldModel.SQLFieldType;
            }
            else
            {
                configModel.ListField.Add(fieldModel);
            }

            configModel.ListField = configModel.ListField.OrderBy(u => u.ExcelColomn).ToList();

            UpdateConfigModel(configModel);

            return configModel;
        }

        public static bool AddOrUpdateField(FieldModel fieldModel)
        {
            ConfigModel configModel = GetConfigModel();

            FieldModel model = configModel.ListField.Find(u => u.SQLField == fieldModel.SQLField);
            if (model != null)
            {
                model.ExcelColomn = fieldModel.ExcelColomn;
                model.ExcelFieldType = fieldModel.ExcelFieldType;
                model.FieldName = fieldModel.FieldName;
                model.ForeignKey = fieldModel.ForeignKey;
                model.ForeignTable = fieldModel.ForeignTable;
                model.IsForeignKey = fieldModel.IsForeignKey;
                model.LinkField = fieldModel.LinkField;
                model.SQLField = fieldModel.SQLField;
                model.SQLFieldType = fieldModel.SQLFieldType;
            }
            else
            {
                configModel.ListField.Add(fieldModel);
            }

            configModel.ListField = configModel.ListField.OrderBy(u => u.ExcelColomn).ToList();

            UpdateConfigModel(configModel);

            return true;
        }

        #endregion

        #region 更新模型 UpdateConfigModel(ConfigModel model)
        public static bool UpdateConfigModel(ConfigModel model)
        {
            try
            {
                string configModel = JsonHelper.ToJsonStr<ConfigModel>(model);

                if (WriteAllText("Config.json", configModel))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 读写文本 ReadAllText(string fileName)  WriteAllText(string fileName, string text)
        public static string ReadAllText(string fileName)
        {
            string filePath = System.Web.HttpContext.Current.Server.MapPath("/"+fileName);

            string strFile;
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }

                FileStream aFile = new FileStream(filePath, FileMode.Open);
                StreamReader sr = new StreamReader(aFile);

                strFile = sr.ReadToEnd();

                sr.Close();
                aFile.Close();

                return strFile;
            }
            catch (IOException)
            {
                return null;
            }
        }

        public static bool WriteAllText(string fileName, string text)
        {
            try
            {
                string filePath = System.Web.HttpContext.Current.Server.MapPath("/" + fileName);

                FileStream aFile = new FileStream(filePath, FileMode.OpenOrCreate);

                aFile.Seek(0, SeekOrigin.Begin);
                aFile.SetLength(0);

                StreamWriter sw = new StreamWriter(aFile);

                sw.Write(text);

                sw.Close();
                aFile.Close();

                return true;
            }
            catch (IOException)
            {
                return false;
            }
        } 
        #endregion
    }
}