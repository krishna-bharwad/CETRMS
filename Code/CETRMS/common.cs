using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CETRMS
{
    public static class common
    {
        public static int ConverBinaryDataToTempFile(byte[] ImageData, string FileName, ref string TempImageURL)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.WEBPAGE, "", ">>>ConverBinaryDataToTempFile():DataLenght-" + ImageData.Length.ToString() + ":FileName-" + FileName);
            int iRetValue = 0;
            try
            {
                ClearTempFiles();

                string filePath = HttpContext.Current.Server.MapPath(@".\TempFiles\" + FileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                var writer = new BinaryWriter(File.OpenWrite(filePath));
                writer.Write(ImageData);
                writer.Close();

                if (File.Exists(filePath))
                    TempImageURL = ConfigurationManager.AppSettings["AppURL"].ToString() + "/TempFiles/" + FileName;
                else
                    logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", "Could not create file: " + filePath);


                iRetValue = 1;
            }
            catch(Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", ex.Message);
                iRetValue = -1;
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.WEBPAGE, "", "<<<ConverBinaryDataToTempFile():RetValue - " + iRetValue.ToString());
            return iRetValue;
        }

        public static int ClearTempFiles()
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.WEBPAGE, "", ">>>ClearTempFiles()");

            int iRetValue = 0;
            try
            {

                string DirectoryPath = HttpContext.Current.Server.MapPath(@".\TempFiles\");

                DirectoryInfo d = new DirectoryInfo(DirectoryPath); //Assuming Test is your Folder

                FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files

                foreach (FileInfo ifile in Files)
                {
                    FileInfo fileInfo = new FileInfo(DirectoryPath + ifile.Name);
                    if (System.DateTime.Now.Subtract(fileInfo.CreationTime).TotalMinutes > Convert.ToInt32(ConfigurationManager.AppSettings["TempFileClearTime"]))
                    {
                        File.Delete(DirectoryPath + ifile.Name);
                    }
                }
            }
            catch(Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", ex.Message);
                iRetValue = -1;
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.WEBPAGE, "", "<<<ClearTempFiles() : RetValue - "+iRetValue.ToString());
            return iRetValue;
        }
        public static string GetValue(string Key, string Source)
        {
            string sValue = string.Empty;
            int iKeyStartPos = Source.IndexOf(Key);
            sValue = Source.Substring(iKeyStartPos);
            int iKeyEndPos = sValue.IndexOf(",");
            sValue = sValue.Substring(0, iKeyEndPos);
            sValue = sValue.Substring(sValue.IndexOf(":") + 2); 
            return sValue;
        }
        public static string GetSoftwareVersion()
        {
            string version;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            version = fvi.FileVersion;
            return version;
        }
        public static string GetPublishDate()
        {
            string sRetValue = string.Empty;
            string filePath = HttpContext.Current.Server.MapPath(@".\bin\CETRMS.dll");
            if (File.Exists(filePath))
            {
                FileVersionInfo.GetVersionInfo(filePath);
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);
                sRetValue = File.GetLastWriteTime(filePath).ToString("G");
            }
            return sRetValue;
        }
    }
}