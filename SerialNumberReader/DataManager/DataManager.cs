using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataManager
{
    /// <summary>
    /// Handles reading and writing to data files. Will be expanded for database/MOM Integration as well
    /// </summary>
    public class DataManager
    {
        #region Properties


        FileStream DataFile;


        #endregion





        //TODO: Implement OpenDataFile
        /// <summary>
        /// Open the Data File for Writing
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool OpenDataFile(String filePath)
        {
            return false;
        }

        //TODO: Create alternates of OpenDataFile


        
        /// <summary>
        /// Closes the data file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool CloseDataFile(string filePath)
        {
            return false;
        }


    }
}
