using SharpOSC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace VRCFT_ReverseEyes
{
    class DualWriter : TextWriter
    {
        private TextWriter consoleWriter;
        private StreamWriter fileWriter;

        public DualWriter(TextWriter console, string logFile)
        {
            consoleWriter = console;
            fileWriter = new StreamWriter(logFile, append: true);
            fileWriter.AutoFlush = true;
        }

        public override Encoding Encoding => consoleWriter.Encoding;

        public override void Write(char value)
        {
            consoleWriter.Write(value);
            fileWriter.Write(value);
        }

        public override void WriteLine(string value)
        {
            consoleWriter.WriteLine(value);
            fileWriter.WriteLine(value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                fileWriter.Dispose();
            base.Dispose(disposing);
        }
    }
    internal static class Program
    {
        static UDPSender oscSender;
        static UDPListener oscReciever;
        public static bool extensiveLog;
        public static string currLogName;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string logFile = $"log_{timestamp}.txt";
            currLogName = logFile;

            Console.SetOut(new DualWriter(Console.Out, logFile));

            //Console.WriteLine("This will appear in the console AND be logged.");
            //Console.WriteLine("Logging made easy!");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static async Task StartTasks(string portIn, string portOut, string ipOut)
        {
            //if (!File.Exists(Application.StartupPath + "\\SharpOSC.dll"))
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("SharpOSC.dll Not Detected! Download at https://github.com/ValdemarOrn/SharpOSC");
            //    Console.ForegroundColor = ConsoleColor.White;
            //}
            //else
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
        //static void HandleFTOscMessage1(OscPacket packet)
        //{
        //    Form1 forms = Application.OpenForms.OfType<Form1>().FirstOrDefault();
        //    if (packet is OscBundle bundle)
        //    {
        //        if (forms.pimaxFixed)
        //        {
        //            HandleFTOscMessage(bundle);
        //        }
        //        else
        //        {
        //            SendOSCPacketFT(packet);
        //        }

        //    }
        //    else if (packet is OscMessage message)
        //    {
        //        //SendOSCPacketFT(packet);
        //        Console.WriteLine($"Packet is a message: {message.Address}");

        //        if (forms.pimaxFixed)
        //        {
        //            FlipEyesFromMessage(message, false);
        //        }
        //        else
        //        {
        //            SendOSCPacketFT(packet);
        //        }

        //    }
        //}
        public static DateTime lastTrack;
        static void HandleFTOscMessage(OscPacket packet)
        {
            Form1 forms = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (packet is OscBundle bundle)
            {
                if(forms.pimaxFixed)
                {
                    if(DateTime.Now >  lastTrack.AddMilliseconds(forms.delay))
                    {
                        CreateModifiedBundle(bundle);
                        lastTrack = DateTime.Now;
                    }
                    
                }
                else
                {
                    SendOSCPacketFT(packet);
                }
                    
            }
            else if (packet is OscMessage message)
            {
                //SendOSCPacketFT(packet);
                Console.WriteLine($"Packet is a message: {message.Address}");

                if(forms.pimaxFixed)
                {
                    if (DateTime.Now > lastTrack.AddMilliseconds(forms.delay))
                    {
                        FlipEyesFromMessage(message, false);
                        lastTrack = DateTime.Now;
                    }
                        
                }
                else
                {
                    SendOSCPacketFT(packet);
                }
                    
            }
        }
        static void CreateModifiedBundle(OscBundle originalBundle)
        {
            
            //Console.WriteLine("Log: " + extensiveLog);
            //Console.WriteLine("Flipped: " + forms.pimaxFixed);

            try
            {
                


                Form1 forms = Application.OpenForms.OfType<Form1>().FirstOrDefault();

                if (forms.pimaxFixed == true)
                {

                    
                    //pimaxFixed = false;
                    foreach (var packet in originalBundle.Messages)
                    {
                        if (packet is OscMessage msg)
                        {
                            //if (msg.Arguments.Count > 0 && msg.Arguments[0] is string originalText)
                            //{
                            //Console.WriteLine(msg.Address);

                            FlipEyesFromMessage(msg, true);
                            
                        }
                    }


                }
                else
                {
                    SendOSCPacketFT(originalBundle);
                }
            }
            catch(Exception e)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);

                if (originalBundle != null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Emergency Sending Bundle Successsfull!");
                    SendOSCPacketFT(originalBundle);
                }
                else
                {
                    Console.WriteLine("Emergency Sending Bundle Failed!");
                }

                    Console.ForegroundColor = ConsoleColor.White;

            }

            

        }
        public static void FlipEyesFromMessage(OscMessage msg, bool fromBundle)
        {
            var modifiedMessages = new List<OscMessage>();
            bool pimaxFixed = false;
            string originalText = msg.Address;
            string modifiedText = originalText;

            pimaxFixed = false;


            if (originalText.Contains("Right") && originalText.Contains("Eye"))
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


            if (fromBundle)
            {
                modifiedMessages.Add(newMsg);
            }
            
            //}
            //else
            //{
            //    modifiedMessages.Add(msg);
            //}

            if (pimaxFixed)
            {
                // Use the correct constructor: create a new bundle from the message list
                if(fromBundle)
                {
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
                    SendOSCPacketFT(newMsg);
                }
                

            }
            else
            {
                
                if (!fromBundle)
                {
                    SendOSCPacketFT(msg);
                    if (extensiveLog)
                    {
                        Console.WriteLine("Unchanged Message: " + msg.Address + " Value: " + msg.Arguments[0]);
                    }
                }
                else
                {
                    if (extensiveLog)
                    {
                        Console.WriteLine("Unchanged Bundle: " + msg.Address + " Value: " + msg.Arguments[0]);
                    }
                }
                //SendOSCPacketFT(originalBundle);
                //Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine(msg.Address);
                //Console.ForegroundColor = ConsoleColor.White;
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
