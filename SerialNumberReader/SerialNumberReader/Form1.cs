using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.InSight;
using DataManager;

namespace SerialNumberReader
{
    public partial class SerialNumberReader : Form
    {


        #region Access and Connection Properties

        /// <summary>
        /// Check Connection status of the Camera
        /// </summary>
        public bool Connected
        {
            get
            {
                if (Insight.State != CvsInSightState.Online)
                    return false;
                else
                    return true;
            }
        }

        //[TODO] Create COM equivalent
        public CvsInSightSecurityAccess Access
        {
            get
            {
                return Insight.Access;
            }
        }


        #region Camera Online Flags
        //Three Online Flags - only SoftOnline is modifiable. All three must be true for camera connection
        /// <summary>
        /// COM Getter Method for DiscreteOnline property
        /// </summary>
        /// <returns></returns>
        public bool GetDiscreteOnline() { return Insight.DiscreteOnline; }

        /// <summary>
        /// COM Getter Method for NativeOnline property
        /// </summary>
        /// <returns></returns>
        public bool GetNativeOnline() { return Insight.NativeOnline; }

        /// <summary>
        /// COM Getter Method for SoftOnline Property
        /// </summary>
        /// <returns></returns>
        public bool GetSoftOnline() { return Insight.SoftOnline; }

        /// <summary>
        /// COM Setter Method for SoftOnline Property
        ///     must be true to connect to use camera
        /// </summary>
        /// <param name="softOnline"></param>
        public void SetSoftOnline(bool softOnline) { Insight.SoftOnline = softOnline; }
        #endregion

        /// <summary>
        /// COM Getter for ExclusiveConnection - when true, multiple network connections to camera are not
        ///     allowed
        /// </summary>
        /// <returns></returns>
        public bool GetExclusiveConnection()
        {
            return Insight.ExclusiveConnection;
        }

        /// <summary>
        /// COM Setter for ExlusiveConnection Property
        /// </summary>
        /// <param name="exConn"></param>
        public void SetExclusiveConnection(bool exConn)
        {
            Insight.ExclusiveConnection = exConn;
        }


        //Determines if Job is able to be modified
        public bool IsJobProtected
        {
            get
            {
                return Insight.IsJobProtected;
            }
        }

        /// <summary>
        /// Checks the protection status of the currently loaded job file.
        /// If unprotected, changes may be made to the job.
        /// </summary>
        /// <returns></returns>
        public bool GetIsJobProtected()
        {
            return Insight.IsJobProtected;
        }

        /// <summary>
        /// Returns the IP Address of the camera
        /// </summary>
        private System.Net.IPAddress LocalIPAddress
        {
            get
            {
                return Insight.LocalIPAddress;
            }
        }


        /// <summary>
        /// Returns the last password used to connect with the camera
        /// </summary>
        public string GetPassword()
        {
            return Insight.Password;
        }

        /// <summary>
        /// Get the last Remote IP Address used to connect with the camera,
        /// in System.Net.IPAddress format
        /// </summary>
        public System.Net.IPAddress RemoteIPAddress { get { return Insight.RemoteIPAddress; } }


        /// <summary>
        /// Get the last Remote IP Address used to connect with the camera,
        /// in string format
        /// </summary>
        public string GetRemoteIPAddressString() { return Insight.RemoteIPAddressString; }

        //public string RemoteIPAddressString { get { return Insight.RemoteIPAddressString; } }
        #endregion


        #region Job Specific Properties

        DataManager.DataManager DataManager;

        /// <summary>
        /// [Insight SDK] Communication wrapper for an Insight sensor.
        /// Used to control and retrieve data from the sensor.
        /// </summary>
        CvsInSight Insight;


        public SerialNumberReader()
        {
            //Initialize the DataManager
            DataManager = new DataManager.DataManager();

            //Get instance of the camera
            Insight = new CvsInSight();

            InitializeComponent();


            //Initialize all cell locations based on the settings file
            SetCellLocations();
        }



       public bool LoadJob(String jobName)
        {
            return false;
        }


        //TODO: Look into using the CVS Events from the Cognex Camera rather than checking
        private bool CheckTrigger()
        {
            return false;
        }




        /// <summary>
        /// Updates all of the important cell locations based on
        /// what is stored in the project settings file
        /// </summary>
        private void SetCellLocations()
        {
            Read_WaferIDCell = new CvsCellLocation(InsightCOM.Properties.Settings.Default.WaferIDCell);
            Read_PartNumCell = new CvsCellLocation(Properties.Settings.Default.PartNumCell);
            Read_OrientationCell = new CvsCellLocation(Properties.Settings.Default.OrientCell);
            Read_WorkOrderCell = new CvsCellLocation(Properties.Settings.Default.WorkOrderCell);
            Read_MaskIDCell = new CvsCellLocation(Properties.Settings.Default.MaskIDCell);

            //Read_BarCountCell= new CvsCellLocation(Properties.Settings.Default.CountCell);

            //BarIndexIncrementButton= new CvsCellLocation(Properties.Settings.Default.IncrementIndexButton);
            Bars_NumFoundCell = new CvsCellLocation(Properties.Settings.Default.NumBarsFoundCell);
            Bars_FirstBarAngleCell = new CvsCellLocation(Properties.Settings.Default.FirstBarAngleCell);
            Bars_CurrentIndexCell = new CvsCellLocation(Properties.Settings.Default.CurrentBarIndexCell);
            //Bars_CentroidXCell = new CvsCellLocation(Properties.Settings.Default.CentroidXCell);
            Bars_CentroidXCell = new CvsCellLocation(Properties.Settings.Default.FirstBarXCell);
            //Bars_CentroidYCell = new CvsCellLocation(Properties.Settings.Default.CentroidYCell);
            Bars_CentroidYCell = new CvsCellLocation(Properties.Settings.Default.FirstBarYCell);
            Bars_BarLengthCell = new CvsCellLocation(Properties.Settings.Default.BarLengthCell);
        }


        /// <summary>
        /// Connect to the In-Sight Sensor at the specified IP Address
        /// </summary>
        /// <param name="ipAddress"> IP Address of the sensor</param>
        /// <param name="username"> Specified username of the sensor</param>
        /// <param name="password"> Specified password of the sensor</param>
        /// <param name="forceConnect">If true, closes all other connections to sensor</param>
        /// <param name="asynchronous"></param>
        public void Connect(string ipAddress, string username, string password,
                                bool forceConnect, bool asynchronous)
        {
            try
            {
                //Using default values so nothing has to be passed
                //Log("Insight32.Connect called with values: ");
                //Log("ipAddress: " + ipAddress);

                //Insight.Connect(ipAddress, username, password,
                //                forceConnect, asynchronous);

                Console.WriteLine("Attempting connection with settings: ");
                Console.WriteLine("Local IP: " + Properties.Settings.Default.Cognex_IP);
                Console.WriteLine("Host: " + Properties.Settings.Default.Cognex_Username);
                Console.WriteLine("Password: " + Properties.Settings.Default.Cognex_Password);


                Insight.Connect(Properties.Settings.Default.Cognex_IP,
                    Properties.Settings.Default.Cognex_Username,
                    Properties.Settings.Default.Cognex_Password, true, false);

                Console.WriteLine("Insight32 Connected!");
                Console.WriteLine("Local IP: " + Insight.LocalIPAddress);
                Console.WriteLine("Remote IP: " + Insight.RemoteIPAddress);
                Console.WriteLine("Host: " + Insight.Sensor.HostName);
                Console.WriteLine("Sensor: " + Insight.Sensor.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while attempting to connect to camera");
                //InsightExceptionHandler(e);
            }
            //catch (InvalidOperationException e)
            //{
            //    insight32Error = "InvOp_E";
            //}
            //catch(CvsInSightLockedException e)
            //{
            //    insight32Error = "Lock_E";
            //}
            //catch(CvsNetworkException e)
            //{
            //    insight32Error = "Net_E";
            //}
            //catch(CvsInvalidLogonException e)
            //{
            //    insight32Error = "InvLog";
            //}
            //catch (CvsSensorAlreadyConnectedException e)
            //{
            //    insight32Error = "AlCon_E";
            //}
            //catch(CvsCdsLoginException e)
            //{
            //    insight32Error = "CdsLog";
            //}
            //catch (CvsException e)
            //{
            //    insight32Error = "Unknown";
            //}

        }



    }
}
