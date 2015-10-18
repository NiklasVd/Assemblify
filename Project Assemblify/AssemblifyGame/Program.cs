using Assemblify.Core;
using System;

namespace AssemblifyGame
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        public static bool IsDebugMode
        {
            get;
            private set;
        }
        public static bool IsTelemetryActivated
        {
            get;
            private set;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Debug.Log("Assemblify Client (Version: " + Properties.Resources.Version +
                "-" + Properties.Resources.Phase + ")");

            CheckVersion();
            EvaluateCommandLineArgs(Environment.GetCommandLineArgs());

            using (var game = new Game())
            {
                game.Exiting += OnGameExiting;
                game.Run();
            }
        }

        private static void CheckVersion()
        {
            // TODO: Get the new client version and update if needed

            var newClientVersion = Properties.Resources.Version; // Replace with newest version from server
            if (newClientVersion != Properties.Resources.Version)
            {
                Debug.Log("A new version is available: " + newClientVersion);
            }
        }

        private static void EvaluateCommandLineArgs(string[] args)
        {
            var evaluatedArgs = new string[args.Length - 1];
            Array.Copy(args, 1, evaluatedArgs, 0, evaluatedArgs.Length);

            foreach (var arg in evaluatedArgs)
            {
                switch (arg)
                {
#if DEBUG
                    case "-DebugMode":
                        IsDebugMode = true;
                        Debug.Log("Enabled the debug mode.");
                        break;
#endif

                    case "-Telemetry":
                        IsTelemetryActivated = true;
                        Debug.Log("Activated telemetry.");
                        break;

                    default:
                        Debug.Log("The command line argument \"" + arg + "\" is unknown.");
                        break;
                }
            }
        }

        private static void OnGameExiting(object sender, EventArgs e)
        {
            // Send telemetry here
            Debug.StoreLog("debug.log", IsDebugMode);
        }
    }
#endif
}
