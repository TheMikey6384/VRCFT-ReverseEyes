using SharpOSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VRCFT_ReverseEyes
{
    internal static class Program
    {
        static UDPSender oscSender;
        static UDPListener oscReciever;
        public static bool extensiveLog;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static void StartTasks(string portIn, string portOut, string ipOut)
        {
            if (oscSender != null)
            {
                oscSender.Close();
            }
            try
            {
                oscSender = new UDPSender(ipOut, int.Parse(portOut));
                Task.Run(() => ListenOSCFT(int.Parse(portIn)));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        static void ListenOSCFT(int listenPort)
        {
            if(oscReciever != null)
            {
                oscReciever.Close();
            }

            try
            {
                oscReciever = new UDPListener(listenPort, HandleFTOscMessage);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Listening for FT OSC messages on port {listenPort}...");
                Console.ReadLine(); // Keep the program running
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }
        static void HandleFTOscMessage(OscPacket packet)
        {

            var messageReceived = (OscBundle)packet;
            if (packet is OscBundle bundle)
            {
                CreateModifiedBundle(messageReceived);

            }
        }
        static void CreateModifiedBundle(OscBundle originalBundle)
        {

            bool pimaxFixed = false;
            Form1 forms = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            //if (forms.pimaxFix == true)
            {

                var modifiedMessages = new List<OscMessage>();
                //pimaxFixed = false;
                foreach (var packet in originalBundle.Messages)
                {
                    if (packet is OscMessage msg)
                    {
                        //if (msg.Arguments.Count > 0 && msg.Arguments[0] is string originalText)
                        //{
                        //Console.WriteLine(msg.Address);
                        string originalText = msg.Address;
                        string modifiedText = originalText;

                        pimaxFixed = false;

                        
                        if (originalText.Contains("Right") && originalText.Contains("Eye"))
                        {
                            
                            modifiedText = originalText.Replace("Right", "Left");
                            pimaxFixed = true;

                            if(extensiveLog)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Switched From R To L: " + msg.Address + " Value: " + msg.Arguments[0]);
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                        }
                        if (originalText.Contains("Right") && originalText.Contains("Brow"))
                        {
                            
                            modifiedText = originalText.Replace("Right", "Left");
                            pimaxFixed = true;

                            if (extensiveLog)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Switched From R To L: " + msg.Address + " Value: " + msg.Arguments[0]);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        
                        if (originalText.Contains("Left") && originalText.Contains("Eye"))
                        {
                            
                            modifiedText = originalText.Replace("Left", "Right");
                            pimaxFixed = true;

                            if (extensiveLog)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Switched From R To L: " + msg.Address + " Value: " + msg.Arguments[0]);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        if (originalText.Contains("Left") && originalText.Contains("Brow"))
                        {
                            
                            modifiedText = originalText.Replace("Left", "Right");
                            pimaxFixed = true;

                            if (extensiveLog)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Switched From R To L: " + msg.Address + " Value: " + msg.Arguments[0]);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }

                        
                        var newMsg = new OscMessage(modifiedText, msg.Arguments[0]);
                        
                        modifiedMessages.Add(newMsg);
                        //}
                        //else
                        //{
                        //    modifiedMessages.Add(msg);
                        //}

                        if (pimaxFixed)
                        {
                            // Use the correct constructor: create a new bundle from the message list
                            ulong timestamp = GetCurrentOscTimeTag();
                            var bund = new OscBundle(timestamp, modifiedMessages.ToArray());
                            //if(forms.smoothEye)
                            {
                                //SendOSCPacketFT(SmoothedPacket(bund));
                            }
                            // else
                            {
                                SendOSCPacketFT(bund);
                            }

                        }
                        else
                        {
                            if(extensiveLog)
                            {
                                Console.WriteLine("Unchanged: " + msg.Address + " Value: " + msg.Arguments[0]);
                            }
                            //SendOSCPacketFT(originalBundle);
                            //Console.ForegroundColor = ConsoleColor.Green;
                            //Console.WriteLine(msg.Address);
                            //Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }


            }
            //else
            {
                //SendOSCPacketFT(originalBundle);
            }

        }
        public static ulong GetCurrentOscTimeTag()
        {
            // NTP epoch starts on 1900-01-01
            DateTime ntpEpoch = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan ts = DateTime.UtcNow - ntpEpoch;

            // Whole seconds go in the upper 32 bits
            uint seconds = (uint)ts.TotalSeconds;

            // Fractional seconds (scaled to fit into 32 bits)
            uint fraction = (uint)((ts.TotalSeconds - seconds) * UInt32.MaxValue);

            // Combine into 64-bit OSC timetag
            return ((ulong)seconds << 32) | fraction;
        }
        static void SendOSCPacketFT(OscPacket packet)
        {
            //var oscMessage = new OscMessage(address, message, true);
            oscSender.Send(packet);
            //Task.Delay(10).Wait();
            //Console.WriteLine($"[OSC Sent] ");
        }
    }
}
